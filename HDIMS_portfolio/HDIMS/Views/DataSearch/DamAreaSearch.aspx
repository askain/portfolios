<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub3.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="HDIMS.Utils.Login"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    댐유역 강우
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PreHeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<%
    EmpData empdata = new EmpData();
%>
<script type="text/javascript" src="/Scripts/common/renderers.js"></script>
<script src="/Common/AllDamCodeJs/?disptp=RF" type="text/javascript"></script>
<script src="/Common/AllDamTypeJs" type="text/javascript"></script>
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
    var prevDate = new Date();
    prevDate.setDate(currDate.getDate() - 1);
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
        damTpCombo, damCdCombo, startDtCal, dataTpCombo,
        damGridItems, obscdListFor1M;
    var damSearchUrl = "/DataSearch/GetDamAreaSearchList";
    var damExcelUrl = "/Common/DownloadExcel/?";
    var historyUrl = '/DataSearch/GetWaterLevelHistoryList/';
    var datePattern = { datePattern: "yyyy-MM-dd", selector: "date" };
    var datePattern2 = { datePattern: "yyyyMMdd", selector: "date" };
    var timePattern = { timePattern: "HH:mm", selector: "time" };

    var eventCnt = 0;
    function adjustMainPanel() {
        var h = 0;
        if ($.browser.msie) {//hacked together for IE browsers
            h = document.body.offsetHeight;
        } else {
            h = window.outerHeight;
        }
        h = h - 85;
        $("#mainPanel").css({ height: h });

    }

    var DataTpOptions = [
        { text: '실시간', value: '1' },
        { text: '10분강우', value: '10', selected: true },
        { text: '30분강우', value: '30' },
        { text: '시간강우', value: '60' },
        { text: '일별강우', value: 'DD' },
        { text: '월별강우', value: 'MM' }
    ];
    //    var SelectColorOptions = [
    //        { text: '없음', value: 'N' },
    //        { text: '표현', value: 'Y' }
    //    ];

        var obsdtFormatter = function (val, rowIndex) {
            var ret_val = val;
            if (val == "TSACU")
                ret_val = "금회 누계";
            else if (val == "TYACU")
                ret_val = "금일 누계";
            else if (val == "YYACU")
                ret_val = "전일 누계";
            else
                ret_val = convStrToDateMin(val);

            return ret_val;
        };

    var damStruct = [{
        cells: [
            { name: '일 자', field: 'OBSDT', width: "100px", styles: 'text-align:center;vertical-align:middle;', formatter: obsdtFormatter }
        ],
        noscroll: true,
        width: 'auto'
    }, {
        cells: [],
        width: 'auto'
    }];

    var curFirstDate = new Date(currDate.getYear(), currDate.getMonth(), currDate.getDay(), 0, 0, 0);

    var valueFormatter = function (inValue, rowId, cellId) {
        if (rowId < 3) {

            if (inValue==null || parseFloat(inValue) == 0) {
                var backColor = (rowId==0?"#FFECEC":(rowId==1?"#ECFFEC":"#E8E8FF")); //0:#FFECEC,1:#ECFFEC, 2:#E8E8FF
                return '<div style="background-color:'+backColor+';font-weight:bold;">0.0</div>';
            } else if (parseFloat(inValue) > 0 && parseFloat(inValue) < 10) {
                return '<div style="background-color:#FFFF00;font-weight:bold;">' + inValue + '</div>';
            } else if (parseFloat(inValue) >= 10) {
                return '<div style="background-color:#FFFF00;font-weight:bold;color:#FF0000">' + inValue + '</div>';
            }
        } else {
            if (inValue == null) {
                return '&nbsp;';
            } else if (parseFloat(inValue) > 0 && parseFloat(inValue) < 10) {
                return '<div style="background-color:#FFFF00">' + inValue + '</div>';
            } else if (parseFloat(inValue) >= 10) {
                return '<div style="background-color:#FFFF00;color:#FF0000">' + inValue + '</div>';
            }
        }
        return inValue;
    }
    var makeGridColumns = function () {
        var len = 0, ad, cd, idnm, addtime, timelen, columns = [];
        var damCdList = getMultiValues("damCd");
        for (var i = 0, j = 0; i < damCdOptions.length; i++) {
            cd = damCdOptions[i].text;
            idnm = damCdOptions[i].value;
            //console.log(damCdList + "==>cd : " + cd + ", idnm : " + idnm + ": " + damCdList.lastIndexOf(idnm))
            if (damCdList != null && damCdList != "") {
                if (damCdList.lastIndexOf(idnm) != -1) {
                    columns[j] = { name: cd, field: "V_" + idnm, width: '50px', styles: 'text-align:center;vertical-align:middle;', formatter: valueFormatter };
                    j++;
                }
            } else {
                columns[j] = { name: cd, field: "V_" + idnm, width: '50px', styles: 'text-align:center;vertical-align:middle;', formatter: valueFormatter };
                j++;
            }
        }
        var tmpSt = damStruct;
        tmpSt[1].cells = columns;
        return tmpSt;
    };

    var getGraph = function (idx, val) {
        var cSet = "";
        cSet += "<graph gid='" + (idx + 1) + "'>";
        cSet += "<title><![CDATA[" + val + "]]></title>";
        cSet += "<axis>left</axis>";
        cSet += "<color>" + CHColor[idx] + "</color>"
        cSet += "<line_width>2</line_width>";
        cSet += "<balloon_text><![CDATA[<b>" + val + " : {value}</b>]]></balloon_text>";
        cSet += "<hidden>1</hidden>";
        cSet += "</graph>";
        return cSet;
    }

    var rebuildChartSettings = function () {
        var damCdList = getMultiValues("damCd");
        var cnt = damCdOptions.length;
        var searchNm = getSelectText("dataTp");

        var cSet, cd, idnm;
        if (cnt > 0) {
            cSet = "<settings><graphs>";
            for (var i = 0, j = 0; i < cnt; i++) {
                cd = damCdOptions[i].text;
                idnm = damCdOptions[i].value;
                if (damCdList != null && damCdList != "") {
                    if (damCdList.lastIndexOf(idnm) != -1) {
                        cSet += getGraph(j, cd);
                        j++;
                    }
                } else {
                    cSet += getGraph(j, cd);
                    j++;
                }

            }
            cSet += "</graphs>";
            cSet += "<labels>";
            cSet += "    <label lid=\"0\">";
            cSet += "     <text><![CDATA[<b>" + searchNm + "</b>]]></text>";
            cSet += "      <x>0</x>";
            cSet += "      <y>50%</y>";
            cSet += "      <width>10</width>";
            cSet += "      <rotate>true</rotate>";
            cSet += "    </label>";
            cSet += "  </labels>";
            cSet += "</settings>";
            chartMovie.setSettings(cSet);
        }
    };


    var getChartData = function () {
        var tdata = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
        var sdata = ""; //series
        var gdata = ""; //graph
        tdata += "<chart>";

        var cnt = damGridItems.length;
        var damCdList = getMultiValues("damCd");
        var cd, idnm, cdd, gval;
        if (cnt > 0) {
            for (var i = cnt-1,kk=0; i >= 3; i--,kk++) {
                cm = damGridItems[i];
                cdd = convStrToDateMin(cm.OBSDT);
                sdata += "<value xid=\"" + kk + "\">" + cdd + "</value>";
            }

            for (var j = 0, k = 0; j < damCdOptions.length; j++) {
                cd = damCdOptions[j].text;
                idnm = damCdOptions[j].value;
                if (damCdList != null && damCdList != "") {
                    if (damCdList.lastIndexOf(idnm) != -1) {
                        gdata += "<graph gid=\"" + (k + 1) + "\">";
                        for (var i = cnt - 1, kk = 0; i >= 3; i--, kk++) {
                            cm = damGridItems[i];
                            gval = eval("cm.V_" + idnm);
                            gdata += "<value xid=\"" + kk + "\">" + gval + "</value>";
                        }
                        gdata += "</graph>";
                        k++;
                    }
                } else {
                    gdata += "<graph gid=\"" + (k + 1) + "\">";
                    for (var i = cnt - 1, kk = 0; i >= 3; i--, kk++) {
                        cm = damGridItems[i];
                        gval = eval("cm.V_" + idnm);
                        gdata += "<value xid=\"" + kk + "\">" + gval + "</value>";
                    }
                    gdata += "</graph>";
                    k++;
                }
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

    var getParameter = function (url) {
        var damTp = getSelectValue("damTp");
        var damCd = getMultiValues("damCd");
        var startDt = dojo.date.locale.format(startDtCal.getValue(), datePattern2);
        var endDt = dojo.date.locale.format(endDtCal.getValue(), datePattern2);
        var startHr = getSelectValue("startHr");
        var endHr = getSelectValue("endHr");
        var dataTp = getSelectValue("dataTp");

        //댐목록이 전체 일 경우는 전체를 보낸다.
        if (damCd == null || damCd == "") {
            var arr = $.map(damCdOptions, function (item, idx) {
                return item.value;
            });
            damCd = arr.join(",");
        }
        if (dataTp == "1") {
            startDt = startDt + startHr + "0000";
            endDt = endDt + endHr + "5959";
        } else if (dataTp == "10" || dataTp == "30") {
            startDt = startDt + startHr + "00";
            endDt = endDt + endHr + "59";
        } else {
            startDt += startHr;
            endDt += endHr;
        }
        //alert(damTp + "==" + damCd + "==" + startDt + "==" + endDt + "==" + dataTp);
        var param = {
            damTp: damTp,
            damCd: damCd,
            startDt: startDt,
            endDt: endDt,
            dataTp: dataTp
        };
        return param;
    };

    var getSelectValue = function (id) {
        var sel = dojo.byId(id);
        return sel.options[sel.options.selectedIndex].value;
    };

    var getSelectText = function (id) {
        var sel = dojo.byId(id);
        return sel.options[sel.options.selectedIndex].text;
    };

    var downloadExcel = function () {
        if (damGrid.rowCount < 1) {
            alert("저장할 데이터가 존재하지 않습니다.\n먼저 조회를 하셔야 합니다.");
            return;
        }
        damGrid.exportGrid("table", function (html) {
            var form = document.forms["downloadExcelForm"];
            form.content.value = html;
            form.fileName.value = "본댐수위이력조회.xls";
            document.forms["downloadExcelForm"].submit();
        });
    };

    var changeDamType = function () {
        var val = getSelectValue("damTp");
        damCdOptions = GetAllDamCodeList(val, "전체");
        setOptions("damCd", damCdOptions);
    };

    var changeDataType = function () {
       clearChartPanel();
    };


   var createGrid = function () {
       clearChartPanel();

        var struct = makeGridColumns()
        if (damGrid) damGrid.destroyRecursive();

        damGrid = new dojox.grid.EnhancedGrid({
            id: "damGrid",
            structure: struct,
            rowsPerPage: 40,
            plugins: plugins,
            selectionMode: "single",
            canSort: function (colIndex) {
                return false;
            }
        }, document.createElement("div"));
        damGrid.placeAt(dojo.byId("MainGridPanel"));
        damGrid.startup();
        dojo.connect(damGrid, "_onFetchComplete", completeFetch);
        dojo.connect(damGrid, "onStyleRow", function (row) {
            var item = damGrid.getItem(row.index);
            var dataTp = getSelectValue("dataTp");
            var isMark = false;
            if (item) {
                var obsdt = damGrid.store.getValue(item, "OBSDT");
                if (dataTp == "60") {
                    if (obsdt.substr(8, 2) == '24') {
                        row.customClasses += " isMidnight";
                    }
                } else {
                    if (obsdt.substr(8, 4) == '2400') {
                        row.customClasses += " isMidnight";
                    } else if (obsdt.substr(10, 2) == '00') {
                        row.customClasses += " isHR";
                    }
                }

            }
        });
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

    var getMultiValues = function (id) {
        var val = $("#" + id).multiselect("getChecked").map(function () {
            return this.value;
        }).get();
        return val.toString();
    }

    var searchGrid = function () {
        var param = getParameter();

        if (param.dataTp == "1") {
            dijit.byId('chartPanelButton').set('disabled', true);
        } else {
            dijit.byId('chartPanelButton').set('disabled', false);
        }
        //closeChartPanel();
        dijit.byId('showAllChartBtn').set('disabled', true);
        clearChartPanel();
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
            loadChartPanel();
            ChartPanel.toggle();    //open
            ChartPanel.opened = true;
        } else {
            ChartPanel.toggle();    //close
            ChartPanel.opened = false;
        }
    };

    dojo.ready(function () {

        damTpOptions = GetAllDamTpList(false, "전 체");
        damCdOptions = GetAllDamCodeList("");

        var startDtVal = dojo.date.locale.format(prevDate, datePattern);
        var endDtVal = dojo.date.locale.format(currDate, datePattern);

        setOptions("damTp", damTpOptions);
        setOptions("damCd", damCdOptions);
        setOptions("dataTp", DataTpOptions);


        startDtCal = new dijit.form.DateTextBox({
            id: "startDt",
            name: "startDt",
            value: startDtVal,
            style: "width:95px;"
        }, "startDt");

        endDtCal = new dijit.form.DateTextBox({
            id: "endDt",
            name: "endDt",
            value: endDtVal,
            style: "width:95px;"
        }, "endDt");

        setOptions("startHr", gl_24times);
        setOptions("endHr", gl_24times);
        setSelectOption("endHr", "24");

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
            label: "본댐수위자료를 조회합니다"
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

        var damTpCombo = $("#damTp").multiselect({
            header: false,
            multiple: false,
            minWidth: 110,
            height: 150,
            noneSelectedText: "전 체",
            selectedList: 1,
            click: function (e, u) {
                var damTp = u.value;
                var checkTp = (damTp == "") ? "uncheckAll" : "checkAll";
                damCdOptions = GetAllDamCodeList(u.value);
                setOptions("damCd", damCdOptions);
                $("#damCd").multiselect("refresh");
                $("#damCd").multiselect(checkTp);
                //createGrid();
            }
        });
        var damCdCombo = $("#damCd").multiselect({
            minWidth: 140,
            noneSelectedText: "전 체",
            selectedText: '#개 선택',
            checkAllText: '선택',
            uncheckAllText: '해제',
            click: function (e, u) {
                createGrid();
            },
            checkAll: function () {
                createGrid();
            },
            uncheckAll: function () {
                createGrid();
            }
        });

        var dataTpCombo = $("#dataTp").multiselect({
            header: false,
            multiple: false,
            minWidth: 120,
            height: 180,
            selectedList: 1,
            click: function (e, u) {
                createGrid();
            }
        });

        damCdCombo.multiselect("uncheckAll");

        dijit.byId('chartPanelButton').set('disabled', true);
        dijit.byId('showAllChartBtn').set('disabled', true);
        initChartPanel();

        adjustMainPanel();

        hidePreloader();

    });

    $(window).bind('resize', function () {
        if (eventCnt > 0) {
            adjustMainPanel();
            eventCnt = 0;
        } else {
            eventCnt++;
        }
    });
</script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<div id="menu-title" class="title_box" style="position:absolute;text-align:left;height:40px;top:10px;left:10px;">
	<span style="font-weight:bold;font-size:12px;margin-left:10px">
        <img src="<%=Page.ResolveUrl("/Images") %>/icons/highlighter.png" align="absmiddle" />&nbsp;&nbsp;
        댐유역 강우
    </span>
</div>
<br />
<div id="search-panel" style="position:absolute;top:50px;left:5px;background-color:#D3E3F8;border:1px solid #B5BCC7;width:99%;height:30px;padding-top:5px;padding-bottom:5px;">
     <!-- 댐구분, 댐명, 시작날,시작시,끝날,끝시, 구분, 조회, 엑셀 -->
     &nbsp;<label class="wslabel">댐구분 :</label> 
     <select id="damTp" class="wsComboBox"></select>&nbsp;&nbsp;
     <label class="wslabel">댐 명 :</label>  
     <select id="damCd" class="wsComboBox" style="width:100px;"></select>
     <label class="wslabel">기 간 :</label>  
     <input id="startDt"/><select id="startHr" class="wsComboBox"></select> ~ <input id="endDt" /><select id="endHr" class="wsComboBox"></select>
     <label class="wslabel">자 료 :</label>  
     <select id="dataTp" class="wsComboBox" onchange="changeDataType();"></select>&nbsp;&nbsp;
     <button id="searchButton" data-dojo-type="dijit.form.Button" data-dojo-props='id:"searchButton",onClick:searchGrid'><img src='<%=Page.ResolveUrl("~/Images") %>/icons/magnifier.png'>조 회</button>
     <button id="excelButton" data-dojo-type="dijit.form.Button" data-dojo-props='id:"excelButton",onClick:downloadExcel'><img src='<%=Page.ResolveUrl("~/Images") %>/icons/document-excel-table.png'>엑 셀</button>
     <button id="chartPanelButton" data-dojo-type="dijit.form.Button" data-dojo-props='id:"chartPanelButton",onClick:toggleChartPanel'><img src='/Images/icons/chart-up-color.png'>그래프</button>
</div>
<div id="mainPanel" data-dojo-type="dijit.layout.BorderContainer"
	data-dojo-props='style:"position:absolute;top:90px; width: 100%; height: 85%;"'>
	<div id="MainGridPanel" data-dojo-type="dijit.layout.ContentPane" data-dojo-props='id:"MainGridPanel", region:"center", style:"margin:0px 0px 0px 0px;padding:0 0 0 0;;overflow:hidden;"'>
	</div>
	<div id="ChartPanel" data-dojo-type="dojox.layout.ExpandoPane" data-dojo-props='id:"ChartPanel", region:"bottom", style:" height: 360px;margin:0px 0px 10px 0px;", splitter:true, startExpanded:false, showTitle:false, opened:false'>
        <div id="ChartMenuPanel" style="width:100%;height:20px;text-align:right;">
        <button id="exportImgBtn" data-dojo-type="dijit.form.Button" data-dojo-props='id:"exportImgBtn",onClick:exportImage'><img src='<%=Page.ResolveUrl("~/Images") %>/icons/disk--arrow.png'> 이미지저장</button>
        <button id="showAllChartBtn" data-dojo-type="dijit.form.Button" data-dojo-props='id:"showAllChartBtn",onClick:showAllChart'><img src='<%=Page.ResolveUrl("~/Images") %>/icons/arrow-resize.png'> 전체보기</button>
        <button id="closechartButton" data-dojo-type="dijit.form.Button" data-dojo-props='id:"closechartButton",onClick:toggleChartPanel'><img src='/Images/icons/cross-circle.png'> 닫기</button>
        &nbsp;&nbsp;</div>
        <div id="ChartViewPanel" style="height:95%"></div>
	</div>
</div>
<form name="downloadExcelForm" action="/Common/DownloadExcel" method="post">
<input type="hidden" name="content" value="" />
<input type="hidden" name="fileName" value="" />
</form>
</asp:Content>
