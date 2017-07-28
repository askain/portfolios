<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub3.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	수위/우량(1분) 조회
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
<%
    string R_DAMTP = Request["damtp"];
    string R_DAMCD = Request["damcd"];
    string R_OBSCD = Request["obscd"];
    string R_OBSTP = Request["obstp"];
    string R_OBSDT = Request["obsdt"];
 %>
<script type="text/javascript" src="/Scripts/common/renderers.js"></script>
<script type="text/javascript">
    var R_DAMTP = "<%=R_DAMTP %>";
    var R_DAMCD = "<%=R_DAMCD %>";
    var R_OBSCD = "<%=R_OBSCD %>";
    var R_OBSTP = "<%=R_OBSTP %>";
    var R_OBSDT = "<%=R_OBSDT %>";
    var R_STARTDT = "";
    var R_STARTHR = "";
    var R_ENDDT = "";
    var R_ENDHR = "";
    if (R_OBSDT != null && R_OBSDT.length >= 10) {
        R_STARTDT = R_OBSDT.substring(0, 8);
        R_STARTHR = R_OBSDT.substring(8, 10);
        R_ENDDT = R_OBSDT.substring(0, 8);
        R_ENDHR = R_OBSDT.substring(8, 10);
    }
</script>
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
    dojo.require("dojox.grid.cells.dijit");
    dojo.require("dojo.date");
    dojo.require("dojo.date.locale");
    dojo.require("dojo.data.ItemFileWriteStore");
    dojo.require("dojo.fx");

    dojo.require("dojo.parser");

    var chartMovie;
    var chartParams = { bgcolor: "#FFFFFF", wmode: 'transparent' };
    var chartVars = {
        path: "/Scripts/amcharts/flash/",
        settings_file: "/Config/Chart/NetworkSearchSettings.xml",
        data_file: "/Config/Chart/NetworkSearchData.xml",
        chart_id: "ChartViewPanel",
        width: '100%',
        height: '90%'
    };
    var damStore, damGrid, historyStore, historyGrid, exItemGrid, damTpOptions, damCdOptions,
        damTpCombo, damCdCombo, startDtCal, startHrCombo, endDtCal, endHrCombo, dataTpCombo,
        damGridItems, pStartDtCal, pEndDtCal, sStartDtCal, sEndDtCal, startDtVal, endDtVal;
    var originStoreItems = [], preFrom, preTo, searchFlag = 0;
    var legendNum = 2;
    var SearchUrl = "/DataSearch/GetNetworkSearchList";
    var datePattern = { datePattern: "yyyy-MM-dd", selector: "date" };
    var datePattern2 = { datePattern: "yyyyMMdd", selector: "date" };
    var datePattern3 = { datePattern: "yyyyMMddHHmm", selector: "date" };

    var currDate = new Date();
    var currHour = currDate.getHours();
    var prevDate = new Date();
    prevDate.setDate(currDate.getDate() - 1);
    var prevHour = prevDate.getHours();

    //챠트 초기화 완료
    function amChartInited(chart_id) {
        chartMovie = document.getElementById(chart_id);
    }
    function amProcessCompleted(chart_id, process_name) {
        //alert(process_name);
    }
    //챠트 x축 날짜범위 변경시 이벤트
    var saveOriginItems = function () {
        for (var i = 0; i < damGrid.rowCount; i++) {
            originStoreItems.push(damGrid.getItem(i));
        }
    }

    var filterItems = function (from, to) {
        damGrid.edit.cancel();
        damGrid.plugin("selector").clear("cell");
        var items = [];
        for (var i = 0, j = 0; i < originStoreItems.length; i++) {
            var item = originStoreItems[i];
            var obsdt = item["OBSDT"];

            if (obsdt >= from && obsdt <= to) {
                items.push(item);
            }
        }
        damGrid._clearData();
        damGrid.scroller.init(items.length, damGrid.keepRows, damGrid.rowsPerPage);
        damGrid.rowCount = items.length;
        for (var i = 0; i < items.length; i++) {
            damGrid._addItem(items[i], i);
        }
        dijit.byId('showAllChartBtn').set('disabled', false);
        damGrid.setScrollTop(0);
    };

    function amGetZoom(chart_id, fromDT, toDT, from_xid, to_xid) {
        if (fromDT == undefined || toDT == undefined) {
            return undefined;
        }

        var from = fromDT.replace(/-/g, "").replace(/:/g, "").replace(" ", "");
        var to = toDT.replace(/-/g, "").replace(/:/g, "").replace(" ", "");

        if (originStoreItems.length < 1) {
            saveOriginItems();
        } else {
            if (from != preFrom || to != preTo)
                filterItems(from, to);
        }
        preFrom = from, preTo = to;
    }


    function initChartPanel() {
        var url = "/Scripts/amcharts/flash/amline.swf",
            ver = "8.0.0",
            pos = "ChartViewPanel";
        instPath = "/Scripts/amcharts/flash/expressInstall.swf";
        swfobject.embedSWF(url, pos, chartVars.width, chartVars.height, ver, instPath, chartVars, chartParams);
    }
    var loadChartPanel = function () {
        var data = getChartData();
        chartMovie.setData(data);
        searchFlag = 3;
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
        data += "<graph gid=\"4\">";
        data += "<value xid=\"0\"></value>";
        data += "</graph>";
        data += "</graphs>";

        data += "</chart>";
        if (chartMovie) {
            chartMovie.setData(data);
        }
        searchFlag = 1;
    };

    var getChartData = function () {
        var data = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
        var sdata = "<series>";
        var gd1 = "<graph gid=\"1\">";
        var gd2 = "<graph gid=\"2\">";
        var gd3 = "<graph gid=\"3\">";
        var gd4 = "<graph gid=\"4\">";
        data += "<chart>";
        var cnt = damGrid.rowCount;
        var rid, cm;
        if (cnt > 0) {
            var j = 0;
            for (var i = cnt - 1; i >= 0; i--) {
                cm = damGrid.getItem(i);
                sdata += "<value xid=\"" + j + "\">" + convStrToDateMin(damGrid.store.getValue(cm, "OBSDT")) + "</value>";
                gd1 += "<value xid=\"" + j + "\">" + damGrid.store.getValue(cm, "CHVAL") + "</value>";
                gd2 += "<value xid=\"" + j + "\">" + damGrid.store.getValue(cm, "SATVAL") + "</value>";
                gd3 += "<value xid=\"" + j + "\">" + damGrid.store.getValue(cm, "CDMAVAL") + "</value>";
                gd4 += "<value xid=\"" + j + "\">" + damGrid.store.getValue(cm, "WIREVAL") + "</value>";
                j++;
            }
            data += sdata + "</series>";
            data += "<graphs>";
            data += gd1 + "</graph>";
            data += gd2 + "</graph>";
            data += gd3 + "</graph>";
            data += gd4 + "</graph>";
            data += "</graphs>";
        } else {
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
            data += "</graphs>";
        }
        data += "</chart>";
        searchFlag = 2;
        return data;
    };

    var selectLegend = function () {
        for (var i = 0; i < legendNum; i++) {
            chartMovie.showGraph(i);
            //chartMovie.selectGraph(i);
        }
    };
    var deselectLegend = function () {
        for (var i = 0; i < legendNum; i++) {
            chartMovie.hideGraph(i);
            //chartMovie.deselectGraph(i);
        }
    };
    var exportImage = function () {
        if (damGrid.rowCount > 0) {
            chartMovie.exportImage("/Verify/ExportDamdataChartImage");
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

    var dataTpOptions = [
        { text: '수위', value: 'WL', selected: true },
        { text: '우량', value: 'RF' }
    //,{ text: '수위+우량', value: 'WR' }
    ];

    var equipFormatter = function (inValue, rowId, cellId, cellField, cellObj, rowObj) {
        try {
            var ret_val = inValue;
            if (inValue == undefined || inValue == null) ret_val = "-";

            if (parseInt(inValue) <= 0) {
                return '<div style="color:#ff0000">' + ret_val + '</div>';
            }
            return ret_val;
        }
        catch (e) {
            //alert(e);
        }
    };
    var damStruct = [{
        cells: [
                { name: '측정일시', field: 'OBSDT', width: "100px", styles: 'text-align:center;vertical-align:middle;', formatter: convStrToDateMin },
                { name: '댐명', field: 'DAMNM', width: "100px", styles: 'text-align:center;vertical-align:middle;' },
                { name: '관측국', field: 'OBSNM', width: "100px", styles: 'text-align:center;vertical-align:middle;' },
                { name: '데이터', field: 'CHVAL', width: "100px", styles: 'text-align:center;vertical-align:middle;', formatter: equipFormatter },
                { name: '위성입력', field: 'SATVAL', width: "100px", styles: 'text-align:center;vertical-align:middle;', formatter: equipFormatter },
                { name: 'CDMA입력', field: 'CDMAVAL', width: "100px", styles: 'text-align:center;vertical-align:middle;', formatter: equipFormatter },
                { name: '유선입력', field: 'WIREVAL', width: "100px", styles: 'text-align:center;vertical-align:middle;', formatter: equipFormatter }
            ],
        noscroll: false,
        width: 'auto'
    }
    ];

    var getParameter = function () {
        var damCd = getSelectValue("damCd");
        var obsCd = getSelectValue("obsCd");
        var dataTp = getSelectValue("dataTp");
        var startDt = dojo.date.locale.format(startDtCal.getValue(), datePattern2);
        var startHr = getSelectValue("startHr");
        var endDt = dojo.date.locale.format(endDtCal.getValue(), datePattern2);
        var endHr = getSelectValue("endHr");

        startDt = startDt + startHr + '0000';
        if (endHr == "23") {
            endDt = endDt + "240000";
        }
        else {
            endDt = endDt + endHr + "5900";
        }

        var params = {
            DamCd: damCd,
            ObsCd: obsCd,
            DataTp: dataTp,
            StartDt: startDt,
            EndDt: endDt
        };
        return params;
    };

    var getSelectValue = function (id) {
        var sel = document.getElementById(id);

        if (sel.options.length == 0) return '';

        return sel.options[sel.options.selectedIndex].value;
    };

    var downloadExcel = function () {
        damGrid.exportGrid("table", function (html) {
            var form = document.forms["downloadExcelForm"];
            form.content.value = html;
            document.forms["downloadExcelForm"].submit();
        });
    };

    var changeDamType = function () {
        var val = getSelectValue("damTp");
        damCdOptions = glGetDamCdList(val);
        setOptions("damCd", damCdOptions);
    };

    var changeDamCd = function () {
        var damCd = getSelectValue("damCd");
        var dataTp = getSelectValue("dataTp");
        obsCdOptions = glGetObsCdList(dataTp, damCd);
        setOptions("obsCd", obsCdOptions);
    };
    var changeDataTp = function () {
        var damCd = getSelectValue("damCd");
        var dataTp = getSelectValue("dataTp");
        obsCdOptions = glGetObsCdList(dataTp, damCd);
        setOptions("obsCd", obsCdOptions);
    };
    var completeFetch = function (items, request) {
        damGridItems = items;
        $(document.body).unmask();
        loadChartPanel();
    };
    var searchGrid = function () {
        var param = getParameter();

        damGrid.plugin("selector").clear("row");
        //closeChartPanel();
        clearChartPanel();
        dijit.byId('chartPanelButton').set('disabled', false);

        damGrid.setStore(damStore, param);
        //toggleEditable();
        dijit.byId('showAllChartBtn').set('disabled', true);

        $(document.body).mask("조회중입니다...");
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

    var createGrid = function () {
        var struct = damStruct;

        if (damGrid) damGrid.destroyRecursive();

        damGrid = new dojox.grid.EnhancedGrid({
            id: "damGrid",
            structure: struct,
            rowsPerPage: 50,
            noDataMessage: "데이터가 존재하지 않습니다.",
            errorMessage: "조회 도중 에러가 발생했습니다.",
            //keepSelection: true,
            //fastScroll: false,
            selectable: false,
            canSort: function (colIndex) {
                return false;
            },
            plugins: {
                //indirectSelection: { headerSelector: true, name: "Selection", width: "20px", styles: "text-align: center;" },
                selector: { row: true, col: false, cell: false },
                printer: true
            }
        }, document.createElement("div"));
        damGrid.placeAt(dojo.byId("MainGridPanel"));
        damGrid.startup();

        dojo.connect(damGrid, "_onFetchComplete", completeFetch);
        dojo.connect(damGrid, "onStyleRow", onStyleRow1M);

        //initChartPanel();
    };


    dojo.addOnLoad(function () {

        if (R_STARTDT != null && R_STARTDT.length > 0)
            currDate = new Date(R_STARTDT.substring(0, 4), parseInt(R_STARTDT.substring(4, 6)) - 1, R_STARTDT.substring(6, 8));

        startDtVal = dojo.date.locale.format(prevDate, datePattern);
        endDtVal = dojo.date.locale.format(currDate, datePattern);

        dojo.connect(dojo.byId("damTp"), "onchange", changeDamType);
        dojo.connect(dojo.byId("damCd"), "onchange", changeDamCd);
        dojo.connect(dojo.byId("dataTp"), "onchange", changeDataTp);

        if (R_DAMCD == null || R_DAMCD.length < 7) R_DAMCD = glGetDefaultDamCd();
        if (R_DAMTP == null || R_DAMTP.length < 1) R_DAMTP = glGetDefaultDamTp();
        if (R_OBSTP == null || R_OBSTP.length < 1) R_OBSTP = "WL";


        damTpOptions = glGetDamTpList(false);
        damCdOptions = glGetDamCdList(R_DAMTP);
        obsCdOptions = glGetObsCdList("", R_DAMCD);

        setOptions("dataTp", dataTpOptions);
        setOptions("damTp", damTpOptions);

        setSelectOption("dataTp", R_OBSTP);
        setSelectOption("damTp", R_DAMTP);
        changeDamType();
        setSelectOption("damCd", R_DAMCD);
        changeDamCd();

        if (R_OBSCD != null && R_OBSCD > 0)
            setSelectOption("obsCd", R_OBSCD);


        setOptions("startHr", gl_24times);
        setOptions("endHr", gl_24times);

        if (R_STARTHR == null || R_STARTHR.length < 1) R_STARTHR = "00";
        else { //지정한 시간의 1시간이전으로 처리(속도??)
            var hr = parseInt(R_STARTHR) - 1;
            hr = (hr < 0) ? 0 : hr;
            R_STARTHR = (hr < 10) ? "0" + hr : hr;
        }
        setSelectOption("startHr", R_STARTHR);

        if (R_ENDHR == null || R_ENDHR.length < 1) R_ENDHR = "24";
        setSelectOption("endHr", R_ENDHR);

        startDtCal = new dijit.form.DateTextBox({
            id: "startDt",
            name: "startDt",
            value: startDtVal,
            style: "width:100px;",
            onChange: function () {
                var dt = this.getValue();
                endDtCal.constraints.min = new Date(dt.getFullYear(), dt.getMonth(), dt.getDate());
            }
        }, "startDt");

        endDtCal = new dijit.form.DateTextBox({
            id: "endDt",
            name: "endDt",
            value: endDtVal,
            style: "width:100px;",
            onChange: function () {
                var dt = this.getValue();
                startDtCal.constraints.max = new Date(dt.getFullYear(), dt.getMonth(), dt.getDate());
            }
        }, "endDt");

        damStore = new dojox.data.JsonRestStore({
            target: SearchUrl,
            timeout: 3600000
            , idAttribute: 'ID'
        });

        //툴팁
        new dijit.Tooltip({
            connectId: ["searchButton"],
            position: "above",
            showDelay: 0,
            label: "네트워크별데이터를 조회합니다"
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

        dijit.byId('chartPanelButton').set('disabled', true);
        dijit.byId('showAllChartBtn').set('disabled', true);
        createGrid();
        initChartPanel();

        setSelectable(document.body);
        if (R_DAMCD.length > 0 && R_OBSCD.length > 0 && R_OBSTP.length > 0 && R_OBSDT.length > 0) {
            searchGrid();
        }
        hidePreloader();

    });

</script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<div id="menu-title" class="title_box" style="position:absolute;text-align:left;height:40px;top:10px;left:10px;">
		<span style="font-weight:bold;font-size:12px;margin-left:10px">
	        <img src="<%=Page.ResolveUrl("/Images") %>/icons/monitor-window.png" align="absmiddle" />&nbsp;&nbsp;
        수위/우량(1분) 조회
        </span>
    </div>

<div id="mainPanel" data-dojo-type="dijit.layout.BorderContainer" data-dojo-props='style:"position:absolute;top:50px; width: 100%; height: 98%;"'>
	<div id="topPanel" data-dojo-type="dijit.layout.ContentPane" data-dojo-props='id:"topPanel", region:"top", style:"height: 30px;background-color:#D3E3F8;"'>
     <!-- 댐구분, 댐명, 시작날,시작시,끝날,끝시, 구분, 조회, 엑셀 -->
     <label class="wslabel">댐구분 :</label>
     <select id="damTp" class="wsComboBox"></select>&nbsp;

     <label class="wslabel">댐명 :</label>
     <select id="damCd" class="wsComboBox" style="width:100px;"></select>&nbsp;

     <label class="wslabel">관측국 :</label>
     <select id="obsCd" class="wsComboBox" style="width:110px;"></select>&nbsp;

     <label class="wslabel">구분 :</label>
     <select id="dataTp" class="wsComboBox" style="width:60px;"></select>&nbsp;

     <label class="wslabel">일 시 :</label> 
     <input id="startDt"/>
     <select id="startHr" class="wsComboBox"></select> ~ 
     <input id="endDt" />
     <select id="endHr" class="wsComboBox"></select>&nbsp;

     <button id="searchButton" data-dojo-type="dijit.form.Button" data-dojo-props='id:"searchButton",onClick:searchGrid'><img src='<%=Page.ResolveUrl("~/Images") %>/icons/magnifier.png'> 조 회</button>
     <button id="excelButton" data-dojo-type="dijit.form.Button" data-dojo-props='id:"excelButton",onClick:downloadExcel'><img src='<%=Page.ResolveUrl("~/Images") %>/icons/document-excel-table.png'> 엑 셀</button>
     <button id="chartPanelButton" data-dojo-type="dijit.form.Button" data-dojo-props='id:"chartPanelButton",onClick:toggleChartPanel'><img src='/Images/icons/chart-up-color.png'> 그래프</button>
     
    </div>
	<div id="MainGridPanel" data-dojo-type="dijit.layout.ContentPane" data-dojo-props='id:"MainGridPanel", region:"center", style:"margin:0px 0px 0px 0px;padding:0 0 0 0;overflow:hidden;"'>
	</div>
    <div id="ChartPanel" data-dojo-type="dojox.layout.ExpandoPane" data-dojo-props='id:"ChartPanel", region:"bottom", style:" height: 360px;margin:0 0 0 0;", splitter:true, startExpanded:false, showTitle:false, opened:false'>
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
</div>

<form name="downloadExcelForm" action="/Common/DownloadExcel" method="post">
<input type="hidden" name="content" value="" />
<input type="hidden" name="fileName" value="수위/우량(1분)조회.xls" />
</form>
</asp:Content>
