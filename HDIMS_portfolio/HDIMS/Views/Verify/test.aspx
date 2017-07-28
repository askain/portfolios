<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub4.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="HDIMS.Utils.Login"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    우량자료 검정
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PreHeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<%
    EmpData empdata = new EmpData();

    string table = ViewData["table"].ToString();
%>
<script type="text/javascript" src="/Scripts/common/code.js"></script>
<script type="text/javascript" src="/Scripts/common/code2.js"></script>
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

    var currDate = new Date();
    var loginEmpNo = "<%=empdata.GetEmpData(0) %>";
    var loginEmpNm = "<%=empdata.GetEmpData(1) %>";
    var searchFlag = 0;
    var chartMovie;
    var chartSettingsUrl = "/Config/Chart/VerifyRainFallSearchSettings.xml";
    var chartDataUrl = "/DataSearch/GetRainfallChartData/?";
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
        damGridItems;
    var damSearchUrl = "/DataSearch/GetRainfallVerifySearchList";
    var damExcelUrl = "/DataSearch/GetRainfallVerifySearchExcel/?";
    var historyUrl = '/DataSearch/GetRainFallHistoryList/';
    var datePattern = { datePattern: "yyyy-MM-dd", selector: "date" };
    var datePattern2 = { datePattern: "yyyyMMdd", selector: "date" };
    var timePattern = { timePattern: "HH:mm", selector: "time" };
    var datePattern4 = { datePattern: "yyyy-MM-dd HH:mm:ss", selector: "date" };

    var dataTpOptions = [
        { text: '10분', value: '10', selected: true },
        { text: '30분', value: '30' },
        { text: '60분', value: '60' }
    ];

    var searchTpOptions = [
        { text: '계측우량', value: 'ACURF' },
        { text: '시간우량', value: 'RF' }
    ];
    var hasHistoryFormatter = function (inValue, rowId, cellId, cellField, cellObj, rowObj) {
        try {
            var item = damGrid.getItem(rowId);
            var cellNumber = cellId.field.substr(cellId.field.lastIndexOf('_') + 1);
            var EDEXLVL = item["EDEXLVL_" + cellNumber];
            var EXCOLOR = item["EXCOLOR_" + cellNumber];

            if (EDEXLVL != null) {
                return '<div style="font-weight: bold;font-size:14px;">' + inValue + '</div>';
            } else if (EXCOLOR != null) {
                return '<div style="background-color:#' + EXCOLOR + '">' + inValue + '</div>';
            } else {
                return inValue;
            }
        }
        catch (e) {
            alert(e);
        }
    };
    var damStruct = [{
        cells: [
                { name: '관측국', field: 'OBSNM', width: "100px", styles: 'text-align:center;' },
                { name: '관측국코드', field: 'OBSCD', width: "100px", styles: 'text-align:center;' },
                { name: '총누계', field: 'PTACURF', width: "50px", styles: 'text-align:center;' },
                { name: '전일누계', field: 'PPDACURF', width: "50px", styles: 'text-align:center;' },
                { name: '금일누계', field: 'PDACURF', width: "50px", styles: 'text-align:center;' }
            ],
        noscroll: true,
        width: 'auto'
    }, {
        cells: [],
        width: 'auto'
    }
    ];
    var historyLayout = [{
        cells: [
            { field: "OBSDT", name: "측정일시", width: "100px", styles: 'text-align:center;', formatter: formatDate },
            { field: "CGDT", name: "보정일시", width: "100px", styles: 'text-align:center;', formatter: formatDate },
            { field: "CGEMPNM", name: "보정자", width: "80px", styles: 'text-align:center;' },
            { field: "PYACURF", name: "계측우량", width: "50px", styles: 'text-align:center;' },
            { field: "RF", name: "시간우량", width: "50px", styles: 'text-align:center;' },
            { field: "EDEXLVLCONT", name: "보정등급", width: "100px", styles: 'text-align:center;' },
            { field: "EDEXWAYCONT", name: "보정방법", width: "100px", styles: 'text-align:center;' },
            { field: "CNRSN", name: "사유", width: "200px", styles: 'text-align:center;' },
            { field: "CNDS", name: "내역", width: "200px", styles: 'text-align:center;' }
        ],
        //noscroll: true,
        width: 'auto'
    }];

    var curFirstDate = new Date(currDate.getYear(), currDate.getMonth(), currDate.getDay(), 0, 0, 0);
    var makeGridColumns = function (dateTp, searchTp) {
        var len = 0, ad, cd, idnm, addtime, timelen, columns = [];
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
                columns[i] = { name: cd, field: idnm, width: '50px', styles: 'text-align:center;background-color:#efefef;' };
            } else {
                columns[i] = { name: cd, field: idnm, width: '50px', styles: 'text-align:center;', formatter: hasHistoryFormatter };
            }
        }
        columns[timelen] = { name: '티센계수', field: "DBSNTSN", styles: 'text-align:center;' };
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
                cSet += "</graph>";
            }
            cSet += "</graphs></settings>";
            chartMovie.setSettings(cSet);
        }

    };

    var getChartData = function () {
        var tdata = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
        var sdata = ""; //series
        var gdata = ""; //graph
        tdata += "<chart>";

        var cnt = damGridItems.length;
        var searchTp = getSelectValue("searchTp");
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
                    if (keyVal) {
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

    var loadChartPanel = function () {

        var data = getChartData();
        chartMovie.setData(data);
        rebuildChartSettings();
    };

    var getParameter = function (url, isObj) {
        var damCd = getSelectValue("damCd");
        var param = {
            damCd: damCd,
            run: '1'
        };
        return param;
    };

    var getSelectValue = function (id) {
        var sel = dojo.byId(id);
        return sel.options[sel.options.selectedIndex].value;
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
        chartMovie.reloadSettings(chartSettingsUrl);
        chartMovie.reloadData(chartDataUrl);
    };

    var createGrid = function (dataTp, searchTp) {
        var struct = makeGridColumns(dataTp, searchTp)
        if (damGrid) damGrid.destroyRecursive();

        damGrid = new dojox.grid.EnhancedGrid({
            id: "damGrid",
            structure: struct,
            //queryOptions: { cache: true },
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
    }
    var beginFetch = function () {
        alert("start");
    }
    var completeFetch = function (items, request) {
        damGridItems = items;
        $(document.body).unmask();
        loadChartPanel();
    };
    var errorFetch = function () {
        alert("error");
    }
    var searchGrid = function () {
        var param = getParameter();
        document.location = "/Verify/test/?" + dojo.objectToQuery(param);
    }
    var legendNum = 10;

    var selectLegend = function () {
        for (var i = 0; i < legendNum; i++) {
            chartMovie.showGraph(i);
            chartMovie.selectGraph(i);
        }
    }
    var deselectLegend = function () {
        for (var i = 0; i < legendNum; i++) {
            chartMovie.hideGraph(i);
            chartMovie.deselectGraph(i);
        }
    }
    var exportImage = function () {
        if (damGridItems != null && damGridItems.length > 0) {
            chartMovie.exportImage("/Verify/ExportRainfallChartImage");
        } else {
            alert("그래프데이터가 존재하지 않습니다.");
            return;
        }
    }

    var showAllChart = function () {
        chartMovie.showAll();
    }


    dojo.ready(function () {
        dojo.connect(dojo.byId("damTp"), "onchange", changeDamType);
        //댐타입
        damTpOptions = glGetDamTpList(true);
        setOptions("damTp", damTpOptions);
        setSelectOption("damTp", 'D');
        changeDamType();


        //initChartPanel();
        //hidePreloader();
        //asdf.asdf();

    });
</script>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<div id="menu-title" class="title_box" style="position:absolute;text-align:left;height:40px;top:10px;left:10px;">
	<span style="font-weight:bold;font-size:12px;margin-left:10px">
    <img src="<%=Page.ResolveUrl("/Images") %>/icons/highlighter.png" align="absmiddle"/>&nbsp;&nbsp;
    우량자료 보정
    </span>
</div>
<br />
<p align="right">※ 보정이력란을 더블 클릭하면 보정이력을 볼 수 있습니다.</p>
<div id="topPanel" data-dojo-type="dijit.layout.ContentPane" data-dojo-props='id:"topPanel", region:"top", style:"height: 50px;background-color:#D3E3F8;"'   unselectable="on">
     <!-- 댐구분, 댐명, 시작날,시작시,끝날,끝시, 구분, 조회, 엑셀 -->
    <td >댐&nbsp;&nbsp;구&nbsp;&nbsp;분 :</td>
    <td><select id="damTp" class="wsComboBox" style="width:110px"></select></td>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;댐 명 :
    <select id="damCd" class="wsComboBox" widgetId="damCdCb" style="width:110px"></select>
    <button dojoType="dijit.form.Button" onClick="searchGrid()"><img src='<%=Page.ResolveUrl("~/Images") %>/icons/magnifier.png'> 조 회</button>
</div>
<div style="width:100px;height:100px">
<%=ViewData["table"] %>
</div>
</asp:Content>
