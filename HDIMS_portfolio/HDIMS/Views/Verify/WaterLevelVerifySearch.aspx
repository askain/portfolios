<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub3.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="HDIMS.Utils.Login"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    수위자료 검정
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PreHeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<%
    EmpData empdata = new EmpData();
%>
<script type="text/javascript" src="/Scripts/common/renderers.js"></script>
<script type="text/javascript">
    dojo.require("dijit.dijit");
    dojo.require("dijit.Editor");
    dojo.require("dijit.Dialog");
    dojo.require("dijit.Calendar");
    dojo.require("dijit.ColorPalette");
    dojo.require("dijit.Tooltip");
    dojo.require("dijit.layout.AccordionContainer");
    dojo.require("dijit.layout.TabContainer");
    dojo.require("dijit.layout.LinkPane");
    dojo.require("dijit.layout.BorderContainer");
    dojo.require("dijit.layout.SplitContainer");
    dojo.require("dijit.layout.ContentPane");
    dojo.require("dojox.layout.ExpandoPane");
    dojo.require("dijit.form.FilteringSelect");
    dojo.require("dijit.form.Button");
    dojo.require("dijit.form.ToggleButton");
    dojo.require("dijit.form.ComboBox");
    dojo.require("dijit.form.Select");
    dojo.require("dijit.form.DateTextBox");
    dojo.require("dijit.form.Form");
    dojo.require("dojox.data.JsonRestStore");
    dojo.require("dojox.widget.PlaceholderMenuItem");
    dojo.require("dojox.grid.enhanced.plugins.Filter");
    dojo.require("dojox.grid.enhanced.plugins.exporter.CSVWriter");
    dojo.require("dojox.grid.enhanced.plugins.Printer");
    dojo.require("dojox.grid.enhanced.plugins.Cookie");
    dojo.require("dojox.grid.enhanced.plugins.IndirectSelection");
    dojo.require("dojox.grid.enhanced.plugins.NestedSorting");
    dojo.require("dojox.grid.enhanced.plugins.Selector");
    dojo.require("dojox.grid.enhanced.plugins.Menu");
    dojo.require("dojox.grid.enhanced.plugins.DnD");
    dojo.require("dojox.grid.enhanced.plugins.Search");
    dojo.require("dojox.grid.enhanced.plugins.CellMerge");
    dojo.require("dojox.grid.enhanced.plugins.Pagination");
    dojo.require("dojox.grid.enhanced.plugins.GridSource");
    dojo.require("dojox.grid.EnhancedGrid");
    dojo.require("dojo.date");
    dojo.require("dojo.date.locale");
    dojo.require("dojo.parser");
    var plugins = {
        printer: true
    };
    var currDate = new Date();
    var loginEmpNo = "<%=empdata.GetEmpData(0) %>";
    var loginEmpNm = "<%=empdata.GetEmpData(1) %>";
    var searchFlag = 0;
    var chartMovie;
    var chartSettingsUrl = "/Config/Chart/VerifyWaterLevelSearchSettings.xml";
    var chartDataUrl = "/DataSearch/GetWaterLevelChartData/?";
    var chartParams = { bgcolor: "#FFFFFF", wmode: 'transparent' };
    var chartVars = {
        path: "/Scripts/amcharts/flash/",
        settings_file: chartSettingsUrl,
        data_file: chartDataUrl,
        chart_id: "ChartViewPanel",
        width: '100%',
        height: '90%'
    };
    //챠트 초기화 완료
    function amChartInited(chart_id) {
        chartMovie = document.getElementById(chart_id);
    }
    function amProcessCompleted(chart_id, process_name) {

    }
    //챠트 x축 날짜범위 변경시 이벤트
    function amGetZoom(chart_id, fromDT, toDT, from_xid, to_xid) {
        if (searchFlag == 3) {
            var from = fromDT;
            var to = toDT;
            damDataStore.filter(function (rec, val) {
                return rec.OBSDT >= from && rec.OBSDT <= to;
            });
            dijit.byId('showAllChartBtn').set('disabled', false);
        }
    }

    function initChartPanel() {
        var url = "/Scripts/amcharts/flash/amline.swf",
            ver = "8.0.0",
            pos = "ChartViewPanel";
        instPath = "/Scripts/amcharts/flash/expressInstall.swf";
        swfobject.embedSWF(url, pos, chartVars.width, chartVars.height, ver, instPath, chartVars, chartParams);
    }

    var damStore, damGrid, historyStore, historyGrid, damUrl, damTpOptions, damCdOptions,
        damTpCombo, damCdCombo, selectDtCal, dataTpCombo,
        damGridItems, obscdListFor1M;
    var damSearchUrl = "/DataSearch/GetWaterlevelVerifySearchList";
    var damExcelUrl = "/DataSearch/GetWaterLevelVerifySearchExcel/?";
    var historyUrl = '/DataSearch/GetWaterLevelHistoryList/';
    var datePattern = { datePattern: "yyyy-MM-dd", selector: "date" };
    var datePattern2 = { datePattern: "yyyyMMdd", selector: "date" };
    var timePattern = { timePattern: "HH:mm", selector: "time" };

    var dataTpOptions = [
        { text: '1분', value: '1' },
        { text: '10분', value: '10', selected: true },
        { text: '30분', value: '30' },
        { text: '60분', value: '60' }
    ];

    var searchTpOptions = [
        { text: '수위', value: 'WL' },
        { text: '유량', value: 'FLW' }
    ];

    var SelectColorOptions = [
        { text: '없음', value: 'N' },
        { text: '표현', value: 'Y' }
    ];

    var damStruct = [{
        cells: [
            { name: '댐명', field: 'DAMNM', width: "100px", styles: 'text-align:center;' },
            { name: '관측국', field: 'OBSNM', width: "100px", styles: 'text-align:center;' },
            { name: '관측국코드', field: 'OBSCD', width: "100px", styles: 'text-align:center;' }
        ],
        noscroll: true,
        width: 'auto'
    }, {
        cells: [],
        width: 'auto'
    }];

    var historyLayout = [{
        cells: [
            { field: "OBSDT", name: "측정일시", width: "100px", styles: 'text-align:center;', formatter: formatDate },
            { field: "CGDT", name: "보정일시", width: "100px", styles: 'text-align:center;', formatter: formatDate },
            { field: "CGEMPNM", name: "보정자", width: "80px", styles: 'text-align:center;' },
            { field: "WL", name: "수위", width: "50px", styles: 'text-align:center;' },
            { field: "FLW", name: "유량", width: "50px", styles: 'text-align:center;' },
            { field: "EDEXLVLCONT", name: "보정등급", width: "100px", styles: 'text-align:center;' },
            { field: "EDEXWAYCONT", name: "보정방법", width: "100px", styles: 'text-align:center;' },
            { field: "CNRSN", name: "사유", width: "200px", styles: 'text-align:center;' },
            { field: "CNDS", name: "내역", width: "200px", styles: 'text-align:center;' }
        ],
        //noscroll: true,
        width: 'auto'
    }];

    var hasHistoryFormatter = function (inValue, rowId, cellId, cellField, cellObj, rowObj) {
        try {
            var item = damGrid.getItem(rowId);
            var cellNumber = cellId.field.substr(cellId.field.lastIndexOf('_') + 1);
            var EDEXLVL = item["EDEXLVL_" + cellNumber];
            var EXCOLOR = item["EXCOLOR_" + cellNumber];
            if (inValue == undefined || inValue == null) inValue = "&nbsp;";
            else inValue = glNumberFormat2(inValue, 2);
            if (EDEXLVL != null) {
                inValue = '<div style="font-weight: bold;font-size:14px;">' + inValue + '</div>';
            }
            if (EXCOLOR != null && getSelectValue('SelectColor') == 'Y') {
                inValue = '<div style="background-color:#' + EXCOLOR + '">' + inValue + '</div>';
            }

            return inValue;
        }
        catch (e) {
            alert(e);
        }
    };

    var curFirstDate = new Date(currDate.getYear(), currDate.getMonth(), currDate.getDay(), 0, 0, 0);

    var minColumns = [{
        cells: [
            { field: "OBSDT", name: "측정일시", width: "100px", styles: 'text-align:center;vertical-align:middle;', formatter: formatDate }
        ],
        width: 'auto'
    }];

    //1분조회시 헤더생성
    var setGridColumnsFor1Min = function () {

        var damCd = getSelectValue("damCd");
        //관측국정보를 취득
        var xhrArgs = {
            url: "/Common/ObsCodeList",
            //postData: "Some random text", 
            //handleAs: "text", 
            content: {
                DamCode: damCd,
                ObsTp: 'WL',
                Tp: undefined,
                firstvalue: undefined
            },
            load: function (data) {
                jsonData = dojo.fromJson(data);
                if (jsonData.Data == null || jsonData.Data.length == 0) {
                    //$("#RdsGridPanel").mask("인근관측국이 없거나, 인근관측국의 우량데이터가  없습니다.");
                    alert("에러가 발생하였습니다.");
                }
                obscdListFor1M = jsonData.Data;
                tempColumns = jQuery.extend(true, {}, minColumns[0]);  //clone

                for (var i = 0; i < obscdListFor1M.length; i++) {
                    tempColumns.cells.push({ field: "OBSCD_" + obscdListFor1M[i].KEY, name: obscdListFor1M[i].VALUE, width: "100px", styles: 'text-align:center;vertical-align:middle;' });
                }

                damGrid.setStructure(tempColumns);
                     
            },
            error: function (error) {
                alert(error);
            }
        };
        var deferred = dojo.xhrPost(xhrArgs);
    };

    var makeGridColumns = function (dateTp, searchTp) {
        var len = 0, ad, cd, idnm, addtime, timelen, columns = [];
        if(dateTp=="1") {
            return minColumns;
        }
        if (dateTp == "30") {
            addtime = 30, timelen = 48;
        } else if (dateTp == "60") {
            addtime = 60, timelen = 24;
        } else {
            addtime = 10, timelen = 144;
        }
        for (var i = 0; i < timelen; i++) {
            ad = dojo.date.add(curFirstDate, "minute", (i + 1) * addtime)
            cd = dojo.date.locale.format(ad, timePattern);
            if (cd == "00:00") cd = "24:00";
            idnm = searchTp + "_" + i;
            if ((dateTp == "10" || dateTp == "30") && cd.substring(3, 5) == "00") {
                columns[i] = { name: cd, field: idnm, width: '50px', headerStyles:'text-align:center;', styles: 'text-align:right;background-color:#efefef;cursor:hand;', formatter: hasHistoryFormatter };
            } else {
                columns[i] = { name: cd, field: idnm, width: '50px', headerStyles: 'text-align:center;', styles: 'text-align:right;cursor:hand;', formatter: hasHistoryFormatter };
            }

        }
        var tmpSt = damStruct;
        tmpSt[1].cells = columns;
        return tmpSt;
    };

    var rebuildChartSettings = function () {
        var cnt = damGridItems.length;

        var cSet, obsNm;
        if (cnt > 0) {
            cSet = "<settings><graphs>";
            for (var i = 0; i < cnt; i++) {
                var cm = damGridItems[i];
                cSet += "<graph gid='" + (i + 1) + "'>";
                cSet += "<title><![CDATA[" + cm.OBSNM + "]]></title>";
                cSet += "<axis>left</axis>";
                cSet += "<color>" + CHColor[i] + "</color>"
                cSet += "<line_width>2</line_width>";
                cSet += "<balloon_text><![CDATA[<b>" + cm.OBSNM + " : {value}</b>]]></balloon_text>";
                //if (i != 0) {
                    cSet += "<hidden>1</hidden>";
                //}
                cSet += "</graph>";
            }
            cSet += "</graphs></settings>";
            chartMovie.setSettings(cSet);
        }
    };

//    var rebuildChartSettingsFor1M = function () {
//        var cnt = damGridItems.length;
//        var prevObscd = null;
//        var gidCnt = obscdListFor1M.length;
//        //1분일경우 시간은 바로 간다.
//        var cSet, obsNm;
//        if (cnt > 0) {
//            cSet = "<settings><graphs>";
//            //for (var i = 0; i < cnt; i++) {
//                for (var gid = 1; gid <= gidCnt; gid++) {
//                    var obsnm = obscdListFor1M[gid - 1].VALUE;
//                    cSet += "<graph gid='" + gid + "'>";
//                    cSet += "<title><![CDATA[" + obsnm + "]]></title>";
//                    cSet += "<axis>left</axis>";
//                    cSet += "<color>" + CHColor[gid - 1] + "</color>"
//                    cSet += "<line_width>2</line_width>";
//                    cSet += "<balloon_text><![CDATA[<b>" + obsnm + " : {value}</b>]]></balloon_text>";
//                    cSet += "</graph>";
//                }
//            //}
//            cSet += "</graphs></settings>";

//            chartMovie.setSettings(cSet);
//        }
//    };

    var getChartData = function () {
        var tdata = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
        var sdata = ""; //series
        var gdata = ""; //graph
        tdata += "<chart>";
        
        var cnt = damGridItems.length;
        var searchTp = getSelectValue("searchTp");
        var SelectColor = getSelectValue("SelectColor");
        var dataTp = getSelectValue("dataTp");
        var selectDt = selectDtCal.getValue();

        var temDt = dojo.date.locale.format(selectDt, datePattern);
        var selDt = dojo.date.locale.parse(temDt + " 00:00:00", { datePattern: "yyyy-MM-dd HH:mm:ss", selector: "date" });

        var timelen = 144;
        var interval = 10;
        if (dataTp == "30") {
            timelen = 48;
            interval = 30;
        } else if (dataTp == "60") {
            timelen = 24;
            interval = 60;
        }
        var cm, cdt, cdd, keyVal, edexlvl, excolor;
        if (cnt > 0) {
            for (var i = 0; i < cnt; i++) {
                cm = damGridItems[i];
                gdata += "<graph gid=\"" + (i + 1) + "\">";
                for (var j = 0; j < timelen; j++) {
                    keyVal = eval("cm." + searchTp + "_" + j);
                    if (keyVal != null) {
                        if (i == 0) {
                            if (j == (timelen - 1)) { //24:00일 경우
                                cdd = "24:00";
                            } else {
                                cdt = dojo.date.add(selDt, "minute", (j + 1) * interval);
                                cdd = dojo.date.locale.format(cdt, timePattern);
                            }
                            sdata += "<value xid=\"" + j + "\">" + cdd + "</value>";
                        }
                        gdata += "<value xid=\"" + j + "\">" + keyVal + "</value>";
                    } else {
                        break;
                    }
                    //row cell값의 판단을 한다. 보정여부, EXCOLOR 
                    edexlvl = eval("cm.EDEXLVL_" + j);
                    excolor = eval("cm.EXCOLOR_" + j);

                    if (edexlvl == "null" || edexlvl == undefined) edexlvl = "";
                    if (excolor == "null" || excolor == undefined) excolor = "";
                    if (edexlvl != "") {
                        //damGrid.setCellTextStyle(rid, (5 + j), "font-weight:bold;");
                    } else if (excolor != "") {
                        //damGrid.setCellTextStyle(rid, (5 + j), "font-weight:bold;color:" + excolor + ";");
                    }

                }
                gdata += "</graph>";
            }
        } else {
            sdata = "<value xid='1'></value>";
            gdata = "<value xid='1'></value>";
        }
        tdata += "<series>" + sdata + "</series>";
        tdata += "<graphs>" + gdata + "</graphs>";
        tdata += "</chart>";

        return tdata;
    };

//    var getChartDataFor1M = function () {
//        var tdata = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
//        var sdata = ""; //series
//        var gdata = ""; //graph
//        tdata += "<chart>";

//        var cnt = damGridItems.length;
//        var searchTp = getSelectValue("searchTp");
//        var dataTp = getSelectValue("dataTp");
//        var selectDt = selectDtCal.getValue();

//        var temDt = dojo.date.locale.format(selectDt, datePattern);
//        var selDt = dojo.date.locale.parse(temDt + " 00:00:00", { datePattern: "yyyy-MM-dd HH:mm:ss", selector: "date" });

//        var interval = 1;
//        var timelen = 144;

//        var gidCnt = obscdListFor1M.length;
//        var cm, cdt, cdd, keyVal, edexlvl, excolor;
//        try {
//            if (cnt > 0) {
//                for (var j = 0; j < gidCnt; j++) {
//                    gdata += "<graph gid=\"" + (j + 1) + "\">";
//                    var obscd_1 = "OBSCD_" + obscdListFor1M[j].KEY;
//                    //var obsnm = obscdListFor1M[j].VALUE;
//                    for (var i = 0; i < cnt; i++) {
//                        cm = damGridItems[i];
//                        keyVal = eval("cm." + obscd_1);
//                        if (keyVal) {
//                            if (j == 0) {
//                                if (cm.OBSDT.substr(8, 4) == '0000') { //24:00일 경우
//                                    cdd = "24:00";
//                                } else {
//                                    cdt = dojo.date.add(selDt, "minute", (j + 1) * interval);
//                                    cdd = dojo.date.locale.format(cdt, timePattern);
//                                }
//                                sdata += "<value xid=\"" + j + "\">" + cdd + "</value>";
//                            }
//                            gdata += "<value xid=\"" + j + "\">" + keyVal + "</value>";
//                        }
//                    }
//                    gdata += "</graph>";
//                }
//            } else {
//                sdata = "<value xid='1'></value>";
//                gdata = "<value xid='1'></value>";
//            }
//        } catch (e) { alert(e); }
//        tdata += "<series>" + sdata + "</series>";
//        tdata += "<graphs>" + gdata + "</graphs>";
//        tdata += "</chart>";

//        return tdata;
//    };



    var loadChartPanel = function () {
        var data = null;
        var dataTp = getSelectValue("dataTp");
        if (dataTp == "1" || damGridItems == null) {
            //data = getChartDataFor1M();
            return undefined;
        } else {
            data = getChartData();
        }
        rebuildChartSettings();
        chartMovie.setData(data);

//        if (dataTp == "1") {
//            rebuildChartSettingsFor1M();
//        } else {
//        }
    };
    var clearChartPanel = function () {
        //closeChartPanel();
        var data = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
        var sdata = "<series>";
        data += "<chart>";
        //series,value
        var CDHM = dojo.date.locale.format(currDate, datePattern);
        data += "<series>";
        data += "<value xid=\"0\">" + CDHM + "</value>";
        data += "</series>";
        //graphs,graph, value
        data += "<graphs>";
        data += "<graph gid=\"1\">";
        data += "<value xid=\"0\"></value>";
        data += "</graph>";
        data += "<graph gid=\"2\">";
        data += "<value xid=\"0\"></value>";
        data += "</graph>";
        data += "<graph gid=\"3\">";
        data += "<value xid=\"0\"></value>";
        data += "</graph>";
        data += "</graphs>";

        data += "</chart>";
        if (chartMovie) {
            chartMovie.setData(data);
        }
        searchFlag = 1;
    };

    var getParameter = function (url, isObj) {
        var damTp = getSelectValue("damTp");
        var damCd = getSelectValue("damCd");
        var selectDt = dojo.date.locale.format(selectDtCal.getValue(), datePattern2);
        var dataTp = getSelectValue("dataTp");
        var searchTp = getSelectValue("searchTp");
        if (isObj) {
            var param = {
                damType: damTp,
                damCd: damCd,
                selectDay: selectDt,
                dataTp: dataTp,
                SearchType: searchTp
            };
            return param;

        } else {
            var param = "damType=" + damTp + "&damCd=" + damCd + "&selectDay=" + selectDt + "&dataTp=" + dataTp + "&SearchType=" + searchTp;
            return url + param;
        }

    };

    var getSelectValue = function (id) {
        var sel = dojo.byId(id);
        return sel.options[sel.options.selectedIndex].value;
    };

    var downloadExcel = function () {
//        if (damGridItems != null && damGridItems.length > 0) {
//            document.location = getParameter(damExcelUrl);
//        } else {
//            alert("저장할 데이터가 존재하지 않습니다.");
//        }
        damGrid.exportGrid("table", function (html) {
            var form = document.forms["downloadExcelForm"];
            form.content.value = html;
            document.forms["downloadExcelForm"].submit();
        });
    };

    var changeDamType = function () {
        var val = getSelectValue("damTp");
        damCdOptions = glGetDamCdList(val, "전체");
        setOptions("damCd", damCdOptions);
    };

    var changeDataType = function () {
        var dataTp = getSelectValue("dataTp");
        var searchTp = getSelectValue("searchTp");
        createGrid(dataTp, searchTp);
        dijit.byId('chartPanelButton').set('disabled', true);
        clearChartPanel();
        //chartMovie.reloadSettings(chartSettingsUrl);
        //chartMovie.reloadData(chartDataUrl);
    };

    var gridDblClick = function () {
        var item = damGrid.getItem(damGrid.selection.selectedIndex);
        var damType = getSelectValue("damTp");
        var damCd = damGrid.store.getValue(item, "DAMCD");// getSelectValue("damCd");
        var selectDt = dojo.date.locale.format(selectDtCal.getValue(), datePattern2);
        var dataTp = getSelectValue("dataTp");
        var searchTp = getSelectValue("searchTp");
        var obsCd = damGrid.store.getValue(item, "OBSCD");
        var url = "/Verify/WaterLevelVerify/";
        var params = '?DAMCD=' + damCd + '&OBSCD=' + obsCd + '&OBSDT=' + selectDt + '&DAMTYPE=' + damType + "&DATATP=" + dataTp;
        //alert(params);
        jQuery.popupWindow2({
            windowName: "bojung",
            width: 1100,
            height: 700,
            windowURL: url + params,
            centerScreen: 1,
            resizable: 1
        });
    };

    var showLegend = function () {
        jQuery.popupWindow2({
            windowName: "wlLegend",
            width: 1000,
            height: 700,
            windowURL: '<%=Page.ResolveUrl("~/Code")%>/WLRF_Legend/?type=W',
            centerScreen: 1,
            resizable: 1
        });
    };

    var linkToCims = function () {
        var damCd = getSelectValue("damCd");
        var selectDt = dojo.date.locale.format(selectDtCal.getValue(), datePattern2);

        jQuery.popupWindow2({
            windowName: "cims",
            width: 1100,
            height: 700,
            windowURL: 'http://cims.kwater.or.kr/Main/DataSearch2/?damcd=' + damCd + '&obsdt=' + selectDt,
            //windowURL: 'http://localhost:59123/Main/DataSearch2/?damcd=' + damCd + '&obsdt=' + selectDt,   //테스트용
            centerScreen: 1,
            resizable: 1
        });
    };

    var showHistoryDialog = function (ev) {
        //grid cell rowIndex
        var item = damGrid.getItem(ev.rowIndex);
        var time = ev.cell.getHeaderNode().innerText.replace(':', '');
        var OBSDT = item.OBSDT.substring(0, 8) + time;

        var params = {
            obscd: item.OBSCD,
            obsdt: OBSDT,
            trmdv: item.TRMDV
        };
        if (!historyStore) {
            historyStore = new dojox.data.JsonRestStore({
                target: historyUrl
            });
        }
        if (!historyGrid) {
            historyGrid = new dojox.grid.EnhancedGrid({
                noDataMessage: "데이터가 존재하지 않습니다.",
                //store: historyStore,
                structure: historyLayout,
                canSort: function (colIndex) {
                    return false;
                }
            });
            historyGrid.startup();
            dojo.byId("historyGridPanel").appendChild(historyGrid.domNode);
        }

        historyGrid.setStore(historyStore, params);
        historyGrid.update();
        dijit.byId('historyDialog').show();
    };
    var onStyleRow1M = function (row) {
        ////dojo.connect(damGrid, "onStyleRow", onStyleRow1M);
        var item = damGrid.getItem(row.index);

        if (item) {
            var obsdt = item["OBSDT"];
            if (obsdt.substr(8, 4) == '2400') {
                row.customClasses += " isMidnight";
            } else if (obsdt.substr(10, 2) == '00') {
                row.customClasses += " isHR";
            }
        }
        //damGrid.focus.styleRow(row);
        //damGrid.edit.styleRow(row); 
    };
    var createGrid = function (dataTp, searchTp) {
        var struct = makeGridColumns(dataTp, searchTp)
        if (damGrid) damGrid.destroyRecursive();

        damGrid = new dojox.grid.EnhancedGrid({
            id: "damGrid",
            structure: struct,
            //queryOptions: { cache: true },
            //rowsPerPage: 40,
            keepRows:100,
            plugins: plugins,
            selectionMode: "single",
            canSort: function (colIndex) {
                return false;
            }
        }, document.createElement("div"));
        damGrid.placeAt(dojo.byId("MainGridPanel"));
        damGrid.startup();
        dojo.connect(damGrid, "onRowDblClick", gridDblClick);
        dojo.connect(damGrid, "_onFetchComplete", completeFetch);
        dojo.connect(damGrid, "onCellContextMenu", showHistoryDialog);
    };
    var beginFetch = function () {
        alert("start");
    };
    var completeFetch = function (items, request) {
        damGridItems = items;
        $(document.body).unmask();
        loadChartPanel();
    };
    var errorFetch = function () {
        alert("error");
    };
    var searchGrid = function () {
        var param = getParameter(damSearchUrl, true);

        if (param.damCd == "" && param.dataTp == "1") {
            alert("1분자료에서는 댐전체를 조회할 수 없습니다.");
            return undefined;
        }

        if (param.dataTp == "1") {
            setGridColumnsFor1Min();
            dijit.byId('chartPanelButton').set('disabled', true);
            dojo.connect(damGrid, "onStyleRow", onStyleRow1M);
        } else {
            dijit.byId('chartPanelButton').set('disabled', false);
            dojo.disconnect(damGrid, "onStyleRow", onStyleRow1M);
        }
        //closeChartPanel();
        dijit.byId('showAllChartBtn').set('disabled', true);
        damGrid.setStore(damStore, param);
        $(document.body).mask("조회중입니다...");
    };
    var legendNum = 10;

    var selectLegend = function () {
        for (var i = 0; i < legendNum; i++) {
            chartMovie.showGraph(i);
            chartMovie.selectGraph(i);
        }
    };
    var deselectLegend = function () {
        for (var i = 0; i < legendNum; i++) {
            chartMovie.hideGraph(i);
            chartMovie.deselectGraph(i);
        }
    };
    var exportImage = function () {
        if (damGridItems != null && damGridItems.length > 0) {
            chartMovie.exportImage("/Verify/ExportWaterlevelChartImage");
        } else {
            alert("그래프데이터가 존재하지 않습니다.");
            return;
        }
    };

    var showAllChart = function () {
        chartMovie.showAll();
        dijit.byId('showAllChartBtn').set('disabled', true);
    };
    var closeChartPanel = function () {
        var ChartPanel = dijit.byId("ChartPanel");

        if (ChartPanel.opened == true) {
            ChartPanel.toggle();    //open
            ChartPanel.opened = false;
        }
    };
    var toggleChartPanel = function () {
        var ChartPanel = dijit.byId("ChartPanel");

        if (ChartPanel.opened == false) {
            //loadChartPanel();
            ChartPanel.toggle();    //open
            ChartPanel.opened = true;
        } else {
            ChartPanel.toggle();    //close
            ChartPanel.opened = false;
        }
    };

    dojo.ready(function () {

        damTpOptions = GetAllDamTpList(false, "전체");
        //damCdOptions = glGetDamCdList("D", "전체");

        var selectDtVal = dojo.date.locale.format(currDate, datePattern);

        dojo.connect(dojo.byId("damTp"), "onchange", changeDamType);
        dojo.connect(dojo.byId("dataTp"), "onchange", changeDataType);
        dojo.connect(dojo.byId("searchTp"), "onchange", changeDataType);

        setOptions("damTp", damTpOptions);
        setSelectOption("damTp", glGetDefaultDamTp());
        changeDamType();
        setSelectOption("damCd", glGetDefaultDamCd());

        //setOptions("damCd", damCdOptions);
        setOptions("dataTp", dataTpOptions);
        setOptions("searchTp", searchTpOptions);
        setOptions("SelectColor", SelectColorOptions);


        selectDtCal = new dijit.form.DateTextBox({
            id: "selectDt",
            name: "selectDt",
            value: selectDtVal,
            style: "width:100px;"
        }, "selectDt");

        damStore = new dojox.data.JsonRestStore({
            target: damSearchUrl,
            timeout: 3600000,
            idAttribute: 'ID'
        });

        //툴팁
        new dijit.Tooltip({
            connectId: ["searchButton"],
            position: "above",
            showDelay: 0,
            label: "수위자료를 조회합니다"
        });
        new dijit.Tooltip({
            connectId: ["excelButton"],
            position: "above",
            showDelay: 0,
            label: "엑셀파일을 다운로드 합니다"
        });
        new dijit.Tooltip({
            connectId: ["chartPanelButton"],
            position: "above",
            showDelay: 0,
            label: "현재 조회한 자료를 바탕으로 그래프를 출력합니다"
        });

        createGrid("10", "WL");

        dijit.byId('chartPanelButton').set('disabled', true);
        dijit.byId('showAllChartBtn').set('disabled', true);
        initChartPanel();
        hidePreloader();

    });
</script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<div id="menu-title" class="title_box" style="position:absolute;text-align:left;height:40px;top:10px;left:10px;" unselectable="on" onselectstart="return false">
	<span style="font-weight:bold;font-size:12px;margin-left:10px" unselectable="on" onselectstart="return false">
        <img src="<%=Page.ResolveUrl("/Images") %>/icons/highlighter.png" align="absmiddle" />&nbsp;&nbsp;
        수위자료 검정
    </span>
</div>
<br />
<p align="right">※ 데이터를 오른쪽 클릭하면 보정이력을 볼 수 있습니다.</p>
<div id="mainPanel" data-dojo-type="dijit.layout.BorderContainer"
	data-dojo-props='style:"position:absolute;top:50px; width: 100%; height: 99%;"'>
	<div id="topPanel" data-dojo-type="dijit.layout.ContentPane" data-dojo-props='id:"topPanel", region:"top", style:"height: 30px;background-color:#D3E3F8;"' unselectable="on" onselectstart="return false">
     <!-- 댐구분, 댐명, 시작날,시작시,끝날,끝시, 구분, 조회, 엑셀 -->
     <label class="wslabel">댐구분 :</label> 
     <select id="damTp" class="wsComboBox"></select>&nbsp;&nbsp;
     <label class="wslabel">댐 명 :</label>  
     <select id="damCd" class="wsComboBox" style="width:100px;"></select>
     <label class="wslabel">일 시 :</label>  
     <input id="selectDt"/>&nbsp;&nbsp;
     <label class="wslabel">구 분 :</label>  
     <select id="dataTp" class="wsComboBox"></select>&nbsp;&nbsp;
     <label class="wslabel">검색구분 :</label>  
     <select id="searchTp" class="wsComboBox" style="width:50px;"></select>&nbsp;&nbsp;
     <label class="wslabel">보정색표현 :</label>  
     <select id="SelectColor" class="wsComboBox" style="width:50px;"></select>&nbsp;&nbsp;
     <button id="searchButton" data-dojo-type="dijit.form.Button" data-dojo-props='id:"searchButton",onClick:searchGrid'><img src='<%=Page.ResolveUrl("~/Images") %>/icons/magnifier.png'> 조 회</button>
     <button id="legendButton" data-dojo-type="dijit.form.Button" data-dojo-props='id:"legendButton",onClick:showLegend'><img src='<%=Page.ResolveUrl("~/Images") %>/icons/application-table.png'> 범 례</button>
     <button id="excelButton" data-dojo-type="dijit.form.Button" data-dojo-props='id:"excelButton",onClick:downloadExcel'><img src='<%=Page.ResolveUrl("~/Images") %>/icons/document-excel-table.png'> 엑 셀</button>
     <button id="chartPanelButton" data-dojo-type="dijit.form.Button" data-dojo-props='id:"chartPanelButton",onClick:toggleChartPanel'><img src='/Images/icons/chart-up-color.png'> 그래프</button>
     <button id="cimsButton" data-dojo-type="dijit.form.Button" data-dojo-props='id:"cimsButton",onClick:linkToCims'><img src='<%=Page.ResolveUrl("~/Images") %>/icons/application-list.png'> 고장보고서</button>
	</div>
	<div id="MainGridPanel" data-dojo-type="dijit.layout.ContentPane" data-dojo-props='id:"MainGridPanel", region:"center", style:"margin:0px 0px 0px 0px;padding:0 0 0 0;;overflow:hidden;"'>
	</div>
    <!-- <div id="EmptyPanel1" data-dojo-type="dijit.layout.ContentPane" data-dojo-props='id:"EmptyPanel1", region:"bottom", style:"height:1px;"'></div>-->
	<div id="ChartPanel" data-dojo-type="dojox.layout.ExpandoPane" data-dojo-props='id:"ChartPanel", region:"bottom", style:" height: 360px;margin:0px 0px 10px 0px;", splitter:true, startExpanded:false, showTitle:false, opened:false' unselectable="on" onselectstart="return false">
        <div id="ChartMenuPanel" style="width:100%;height:20px;text-align:right;">
        <%--
        <button id="selectAllBtn" data-dojo-type="dijit.form.Button" data-dojo-props='id:"selectAllBtn",onClick:selectLegend'><img src='<%=Page.ResolveUrl("~/Images") %>/icons/funnel--plus.png'> 전체선택</button>
        <button id="deselectAllBtn" data-dojo-type="dijit.form.Button" data-dojo-props='id:"deselectAllBtn",onClick:deselectLegend'><img src='<%=Page.ResolveUrl("~/Images") %>/icons/funnel--minus.png'> 전체해제</button>
        --%>
        <button id="exportImgBtn" data-dojo-type="dijit.form.Button" data-dojo-props='id:"exportImgBtn",onClick:exportImage'><img src='<%=Page.ResolveUrl("~/Images") %>/icons/disk--arrow.png'> 이미지저장</button>
        <button id="showAllChartBtn" data-dojo-type="dijit.form.Button" data-dojo-props='id:"showAllChartBtn",onClick:showAllChart'><img src='<%=Page.ResolveUrl("~/Images") %>/icons/arrow-resize.png'> 전체보기</button>
        <button id="closechartButton" data-dojo-type="dijit.form.Button" data-dojo-props='id:"closechartButton",onClick:toggleChartPanel'><img src='/Images/icons/cross-circle.png'> 닫기</button>
        &nbsp;&nbsp;</div>
        <div id="ChartViewPanel" style="height:95%"></div>
	</div>
    <div id="historyDialog" data-dojo-type="dijit.Dialog" data-dojo-props='title:"변경이력"' style="overflow:hidden;" >
        <div id="historyGridPanel" data-dojo-type="dijit.layout.ContentPane" data-dojo-props='id:"historyGridPanel", region:"center", style:"margin:0 0 0 0;padding:0 0 0 0;"'  style="width:700px;height:200px;overflow:hidden;" ></div>
    </div>
</div>
<form name="downloadExcelForm" action="/Common/DownloadExcel" method="post">
<input type="hidden" name="content" value="" />
<input type="hidden" name="fileName" value="수위검정자료.xls" />
</form>
</asp:Content>
