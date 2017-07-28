<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub3.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="HDIMS.Utils.Login"%>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	수위자료 보정
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
    html,body{
	    overflow:hidden;
	    margin:0; padding:0; 
	    width:100%; height:100%;
    }
    .dojoxGrid table { 
       margin: 0; 
    } 
    .dojoxGrid
    {
       text-align:center;
       font-size: 12px; 
    } 
    .edited 
    {
        background-color:Red;
    }
    .claro .dijitDialogUnderlay { background:transparent; } 
</style>
<script type="text/javascript">
    dojo.require("dijit.dijit"); // optimize: load dijit layer
    dojo.require("dijit.layout.BorderContainer"); // optimize: load dijit layer
    dojo.require("dijit.layout.TabContainer");
    dojo.require("dojox.layout.ExpandoPane");
    dojo.require("dijit.Dialog");

    dojo.require("dojox.grid.EnhancedGrid");
    dojo.require("dojox.data.AndOrWriteStore");
    dojo.require("dojo.data.ItemFileReadStore");
    dojo.require("dojox.data.JsonRestStore");
    dojo.require("dijit.form.TextBox");
    dojo.require("dijit.form.NumberTextBox");

    dojo.require("dojo.parser");
    dojo.require("dojox.dtl.filter.strings");
    dojo.require("dojox.grid.enhanced.plugins.Filter");
    dojo.require("dojox.grid.enhanced.plugins.exporter.CSVWriter");
    dojo.require("dojox.grid.enhanced.plugins.Printer");
    dojo.require("dojox.grid.enhanced.plugins.IndirectSelection");
    dojo.require("dojox.grid.enhanced.plugins.Selector");
    //dojo.require("dojox.grid.enhanced.plugins.Menu");
    dojo.require("dijit.Tooltip");
    dojo.require('doh.runner');
    dojo.require("dijit.form.Slider");
</script>

<%
    EmpData empdata = new EmpData();
    
    string DAMCD = (string)ViewData["DAMCD"];
    string DAMTYPE = (string)ViewData["DAMTYPE"];
    string OBSCD = (string)ViewData["OBSCD"];
    string OBSDT = (string)ViewData["OBSDT"];
    string EXCD = (string)ViewData["EXCD"];
    string DATATP = (string)ViewData["DATATP"];
%>
<script type="text/javascript" src="/Scripts/common/renderers.js"></script>
<script type="text/javascript">
    var plugins = {
        indirectSelection: {
            headerSelector: true,
            name: "Selection",
            width: "20px",
            styles: "text-align: center;"
        },
        //"exporter": true,
        //"cookie": {},
        printer: true,
        "selector": { row: true, col: false, cell: false }
    };
    var currDate = new Date();
    var prevDate = new Date();
    prevDate.setDate(currDate.getDate()-1); 
    var loginEmpNo = "<%=empdata.GetEmpData(0) %>";
    var loginEmpNm = "<%=empdata.GetEmpData(1) %>";
    var R_DAMCD = '<%=DAMCD %>';
    var R_DAMTYPE = '<%=DAMTYPE %>';
    var R_OBSCD = '<%=OBSCD %>';
    var R_OBSDT = '<%=OBSDT %>';
    var R_EXCD = '<%=EXCD %>';
    var R_DATATP = '<%=DATATP %>';
    var searchFlag = 0;
    var firstload = true;
    var chartMovie;
    var chartParams = { bgcolor: "#FFFFFF", wmode: 'transparent' };
    var chartVars = {
        path: "/Scripts/amcharts/flash/",
        settings_file: "/Config/Chart/VerifyWaterLevelSettings.xml",
        data_file: "/Config/Chart/VerifyRainFallData.xml",
        chart_id: "ChartViewPanel",
        width: '100%',
        height: '100%'
    };

    //전역 변수
    var dhxLayout, dhxGrid, dhxForm, dhxFormData;
    var Date, grid, mainStore, linearGrid, linearStore, historyGrid, historyStore;
    var originStoreItems = [], preFrom, preTo, searchFlag = 0;
    var legendNum = 4;
    var DataUrl = "/Verify/GetWaterLevelVerifyList/";
    var ExcelUrl = "/Verify/GetWaterLevelSearchExcel/?";
    var wlParams = "";
    var datePattern = { datePattern: "yyyy-MM-dd", selector: "date" };
    var datePattern2 = { datePattern: "yyyyMMdd", selector: "date" };
    var datePattern3 = { datePattern: "yyyyMMddHHmm", selector: "date" };
    //전역함수

    //챠트 초기화 완료
    function amChartInited(chart_id){
        chartMovie = document.getElementById(chart_id);
        if (R_OBSCD != '') {
            hidePreloader();
            searchGrid();
        }
    }  
    function amProcessCompleted(chart_id, process_name) {
//        if(process_name=="setData") {
//            searchFlag=3;
//        }
    }

    var saveOriginItems = function () {
        for (var i = 0; i < grid.rowCount; i++) {
            originStoreItems.push(grid.getItem(i));
        }
    };

    var resetFilter = function () {
        var from = originStoreItems[originStoreItems.length - 1]["OBSDT"];
        var to = originStoreItems[0]["OBSDT"];

        filterItems(from, to);
    };

    var filterItems = function (from, to) {
        grid.edit.cancel();
        grid.plugin("selector").clear("cell");
        var items = [];
        for (var i = 0, j = 0; i < originStoreItems.length; i++) {
            var item = originStoreItems[i];
            var obsdt = item["OBSDT"];

            if (obsdt >= from && obsdt <= to) {
                items.push(item);
            }
        }
        grid._clearData();
        grid.scroller.init(items.length, grid.keepRows, grid.rowsPerPage);
        grid.rowCount = items.length;
        for (var i = 0; i < items.length; i++) {
            grid._addItem(items[i], i);
        }
        grid.setScrollTop(0);
    };
    //챠트 x축 날짜범위 변경시 이벤트
    function amGetZoom(chart_id, fromDT, toDT, from_xid, to_xid) {
        var from = fromDT.replace(/-/g, "").replace(/:/g, "").replace(" ", "");
        var to = toDT.replace(/-/g, "").replace(/:/g, "").replace(" ", "");
        if (originStoreItems==null || originStoreItems.length < 1) {
            saveOriginItems();
        } else {
            if (from != preFrom || to != preTo) // 새로운 조회가 아니고, amGetZoom이벤트가 변경된 경우에는 filterItems를 실행
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

    var getChartData = function () {
        var data = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
        var sdata = "<series>";
        var gdata1 = "<graph gid=\"1\">";
        var gdata2 = "<graph gid=\"2\">";
        var gdata3 = "<graph gid=\"3\">";
        data += "<chart>";

        //alert("grid.rowCount = " + grid.rowCount);
        //시발 그리드가 행 갯수를 제대로 리턴못해서 이런거 만듬.
        var cnt = 0;
        if (grid.rowCount == 0) {
            try {
                for (cnt = 0; grid._by_idx[cnt] != undefined && grid._by_idx[cnt] != null; cnt++) {
                    //nothing
                }
            } catch (e) {
            }
        } else {
            cnt = grid.rowCount;
        }

        //var cnt = grid.rowCount;
        if (cnt > 0) {
            j = 0;
            for (var i = cnt - 1; i >= 0; i--) {
                var cm = grid.getItem(i);
                sdata += "<value xid=\"" + j + "\">" + formatDate(grid.store.getValue(cm, "OBSDT")) + "</value>";
                gdata1 += "<value xid=\"" + j + "\" description=\"(EL.m)\">" + grid.store.getValue(cm, "WL") + "</value>";
                gdata2 += "<value xid=\"" + j + "\" description=\"(EL.m)\">" + grid.store.getValue(cm, "EXVL") + "</value>";
                gdata3 += "<value xid=\"" + j + "\" description=\"(㎥/s)\">" + grid.store.getValue(cm, "FLW") + "</value>";
                j++;
            }
            data += sdata + "</series>";
            data += "<graphs>";
            data += gdata1 + "</graph>";
            data += gdata2 + "</graph>";
            data += gdata3 + "</graph>";
            data += "</graphs>";
        } else {
            //series,value//현재시작날짜와 끝날짜로 처리한다.
            var CDHM = dojo.date.locale.format(currDate, datePattern);
            data += "<series>";
            data += "<value xid=\"0\">" + CDHM + "</value>";
            data += "</series>";
            //graphs,graph, value
            data += "<graphs>";
            data += "<graph gid=\"1\">";
            data += "<value xid=\"0\" description=\"(EL.m)\"></value>";
            data += "</graph>";
            data += "<graph gid=\"2\">";
            data += "<value xid=\"0\" description=\"(EL.m)\"></value>";
            data += "</graph>";
            data += "<graph gid=\"3\">";
            data += "<value xid=\"0\" description=\"(㎥/s)\"></value>";
            data += "</graph>";
            data += "</graphs>";
        }
        data += "</chart>";
        searchFlag = 2;
        return data;
    };

    var getColor = function (value, index) {
        var item = grid.getItem(index);
        var retVal = '<div style="background-color:#' + item.EXCOLOR + '">' + value + '</div>';
        return retVal;
    };
    var updateFormatter = function (value, rowIndex) {
        var item = grid.getItem(rowIndex);
        var field = this.constraint.field;
        value = glNumberFormat2(value, 2);
        if (field && field == "EXVL" && item && item.__isDirty) {
            dijit.byId('saveButton').set('disabled', false);
            return '<div style="background-color:#FF8A75">' + value + '</div>';
        }

        return value;
    };

    var numberFormatter = function (value, rowIndex) {
        return glNumberFormat2(value, 2);
    }
    var layout = [{
        cells: [
            { field: "OBSDT", name: "측정일시", width: "120px", styles: 'text-align:center;vertical-align:middle;', formatter: formatTextDate },
            { field: "OBSNM", name: "관측국명", width: "125px", styles: 'text-align:center;vertical-align:middle;' }
        ],
        noscroll: true, 
        width: 'auto'
    }, {
        cells: [[
            { name: "원시", styles: 'text-align:center;background-color:#efefef;', width: "30%", colSpan: 3, headerClasses: "staticHeader" },
            { name: "추정", styles: 'text-align:center;background-color:#efefef;', width: "30%", colSpan: 2, headerClasses: "staticHeader" },
        ], [
            { field: "WL", name: "수위", width: "50px", headerStyles: 'text-align:center;', styles: 'text-align:right;cursor:hand;', formatter: updateFormatter, constraint: { field: 'EXVL' }, editable: true },
            { field: "FLW", name: "유량", width: "50px", styles: 'text-align:center;background-color:#efefef;' },
            { field: "EDEXWAYCONT", name: "보정방법", width: "75px", styles: 'text-align:center;background-color:#efefef;' },
            { field: "EXVL", name: "수위", width: "50px", headerStyles: 'text-align:center;', styles: 'text-align:right;background-color:#efefef;', formatter: numberFormatter },
            { field: "EXCD", name: "검정방법", width: "50px", styles: 'text-align:center;background-color:#efefef;', formatter: getColor }
        ]],
//        onBeforeRow: function (inDataIndex, inSubRows) {
//            inSubRows[0].disable = true;
//        },
        noscroll: true,
        width: 'auto'
    }, {
        cells: [
            { field: "CGEMPNM", name: "보정자", width: "90px", styles: 'text-align:center;vertical-align:middle;background-color:#efefef;cursor:hand;' },
            { field: "CGDT", name: "보정일시", width: "120px", styles: 'text-align:center;vertical-align:middle;background-color:#efefef;cursor:hand;', formatter: formatTextDate },
            { field: "CHKEMPNM", name: "확인자", width: "90px", styles: 'text-align:center;vertical-align:middle;background-color:#efefef;cursor:hand;' },
            { field: "CHKDT", name: "확인일시", width: "120px", styles: 'text-align:center;vertical-align:middle;background-color:#efefef;cursor:hand;', formatter: formatTextDate }
        ],
        width: 'auto'
    }];

    var layoutNoEdit = [{
        cells: [
            { field: "OBSDT", name: "측정일시", width: "120px", styles: 'text-align:center;vertical-align:middle;', formatter: formatTextDate },
            { field: "OBSNM", name: "관측국명", width: "125px", styles: 'text-align:center;vertical-align:middle;' }
        ],
        noscroll: true,
        width: 'auto'
    }, {
        cells: [[
            { name: "원시", styles: 'text-align:center;', width: "30%", colSpan: 3, headerClasses: "staticHeader" },
            { name: "추정", styles: 'text-align:center;', width: "30%", colSpan: 2, headerClasses: "staticHeader" },
        ], [
            { field: "WL", name: "수위", width: "50px", headerStyles: 'text-align:center;', styles: 'text-align:right;', formatter: numberFormatter },
            { field: "FLW", name: "유량", width: "50px", styles: 'text-align:center;' },
            { field: "EDEXWAYCONT", name: "보정방법", width: "75px", styles: 'text-align:center;' },
            { field: "EXVL", name: "수위", width: "50px", headerStyles: 'text-align:center;', styles: 'text-align:right;', formatter: numberFormatter },
            { field: "EXCD", name: "검정방법", width: "50px", styles: 'text-align:center;', formatter: getColor }
        ]],
        //        onBeforeRow: function (inDataIndex, inSubRows) {
        //            inSubRows[0].disable = true;
        //        },
        noscroll: true,
        width: 'auto'
    }, {
        cells: [
            { field: "CGEMPNM", name: "보정자", width: "90px", styles: 'text-align:center;vertical-align:middle;cursor:hand;' },
            { field: "CGDT", name: "보정일시", width: "120px", styles: 'text-align:center;vertical-align:middle;cursor:hand;', formatter: formatTextDate },
            { field: "CHKEMPNM", name: "확인자", width: "90px", styles: 'text-align:center;vertical-align:middle;cursor:hand;' },
            { field: "CHKDT", name: "확인일시", width: "120px", styles: 'text-align:center;vertical-align:middle;cursor:hand;', formatter: formatTextDate }
        ],
        width: 'auto'
    }];



    var dataTpOptions = [
        { text: '10분', value: '10', selected: true },
        { text: '30분', value: '30' },
        { text: '60분', value: '60' }
    ];

    var getParameter = function () {
        var damTp = getSelectValue("damTp");
        var damCd = getSelectValue("damCd");
        var obsCd = getSelectValue("obsCd");
        //var exCd = getSelectValue("exCd");
        var startDt = dojo.date.locale.format(startDtCal.get('value'), datePattern2);
        var startHr = getSelectValue("startHr");
        var endDt = dojo.date.locale.format(endDtCal.get('Value'), datePattern2);
        var endHr = getSelectValue("endHr");
        var dataTp = getSelectValue("dataTp");
        var searchTp = getSelectValue("dateTp");
        var exEmpNo = getSelectValue("exEmpNo");
        if (dataTp == "1") {
            startDt = startDt + startHr + "0000";
            endDt = endDt + endHr + "5959";
        } else if (dataTp == "10" || dataTp == "30") {
            startDt = startDt + startHr + "00";
            endDt = endDt + endHr + "59";
        } else {
            startDt += startHr;
            endDt += endHr;
        };
        //var param = "damType="+damTp+"&damCd="+damCd+"&startDt="+startDt+"&endDt="+endDt+"&dataTp="+dataTp;
        var params = {
            damTp: damTp,
            damCd: damCd,
            obsCd: obsCd,
            //exCd: exCd,
            startDt: startDt,
            endDt: endDt,
            dataTp: dataTp,
            searchTp: searchTp,
            exEmpNo: exEmpNo
        };
        return params;
    };

    var onStyleRow1M = function (row) {

        ////dojo.connect(damGrid, "onStyleRow", onStyleRow1M);
        var item = grid.getItem(row.index);
        if (item == null) return;
        var dataTp = item.TRMDV;

        //alert(dataTp);

        if (dataTp == "60") {
            if (item) {
                var obsdt = item["OBSDT"];
                if (obsdt.substr(8, 2) == '24') {
                    row.customClasses += " isMidnight";
                }
            }
        } else {
            if (item) {
                var obsdt = item["OBSDT"];
                if (obsdt.substr(8, 4) == '2400') {
                    row.customClasses += " isMidnight";
                } else if (obsdt.substr(10, 2) == '00') {
                    row.customClasses += " isHR";
                }
            }
        }
    };

    var getSelectValue = function (id) {
        var sel = dojo.byId(id);
        return sel.options[sel.options.selectedIndex].value;
    };

    var getSelectText = function (id) {
        var sel = dojo.byId(id);
        return sel.options[sel.options.selectedIndex].text;
    };

    //↓↓↓↓↓↓↓↓↓ 버튼클릭 명령들 시작 ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
    var toggleEditable = function (damcd) {
        var dataTp = getSelectValue("dataTp");
//        var cells = layout[1].cells[1];
        if (dataTp == "10" && gl_IsThisUserAbleToChangeData(damcd)) {
            dijit.byId('editButton').set('disabled', false);
            dijit.byId('cancelButton').set('disabled', false);
//            for (var i = 0; i < cells.length; i++) {
//                var cell = cells[i];
//                if (cell.editable != undefined) {
//                    cell.editable = true;
//                }
//            }
            grid.setStructure(layout);
        } else {
            dijit.byId('editButton').set('disabled', true);
            dijit.byId('cancelButton').set('disabled', true);
//            for (var i = 0; i < cells.length; i++) {
//                var cell = cells[i];
//                if (cell.editable != undefined) {
//                    cell.editable = false;
//                }
//            }
            grid.setStructure(layoutNoEdit);
        }
    };
    var searchGrid = function () {
        dijit.byId('saveButton').set('disabled', true);
        originStoreItems = [];
        grid.edit.cancel();
        grid.selection.clear();
        wlParams = getParameter();
        grid.setStore(mainStore, wlParams);
        grid.update();

        toggleEditable(wlParams.damCd);

        $(document.body).mask("조회중입니다...");
    };
    var downloadExcel = function () {
//        if (grid.rowCount > 0) {
//            var param = getParameter();
//            document.location = ExcelUrl + dojo.objectToQuery(param);
//        } else {
//            alert("저장할 데이터가 존재하지 않습니다.");
//        }
        grid.exportGrid("table", function (html) {
            var form = document.forms["downloadExcelForm"];
            form.content.value = html;
            document.forms["downloadExcelForm"].submit();
        });
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
        if (grid.rowCount > 0) {
            chartMovie.exportImage("/Verify/ExportDamdataChartImage");
        } else {
            alert("그래프데이터가 존재하지 않습니다.");
            return;
        }
    };
    var showAllChart = function () {
        resetFilter();
        loadChartPanel();
        //chartMovie.showAll();
    };
    var saveConfirm = function () {
        showNewWindow("/Verify/WaterLevelSaveDialog", window, "dialogHeight:260px;dialogWidth:490px;");
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
    var resetSaveDialog = function () {
        dojo.byId('submitEdExLvl').options[0].selected = true;
        dojo.byId('submitEdExWay').options[0].selected = true;
        dojo.byId('exRsn').options[0].selected = true;
        //dojo.byId('cnrsnText').value = '';
        dojo.byId('cndsText').value = '';
    };
    var saveDialogWin;

    var saveData = function (edexlvl, edexway, exrsn, exrsncont, cnds, saveWin) {
        saveDialogWin = saveWin;
        var cgempnm = loginEmpNm;
        var cgdt = dojo.date.locale.format(new Date(), datePattern3);

        // todo: 변경아이템에 보정등급,레벨, 사유, ? 를 넣어야함.
        var dirty = dojox.rpc.JsonRest.getDirtyObjects();
        var oItem;

        if (dirty && dirty.length > 0) {
            for (i = 0; i < dirty.length; i++) {
                var targetRec = dirty[i].object;
                var otargetRec = dirty[i].old;
                grid.store.changing(targetRec);
                targetRec["EDEXWAY"] = edexway;
                targetRec["EDEXLVL"] = edexlvl;
                targetRec["EXRSN"] = exrsn;
                targetRec["CNRSN"] = exrsncont;
                targetRec["CNDS"] = cnds;
                targetRec["CGEMPNM"] = cgempnm;
                targetRec["CGDT"] = cgdt;
            }
        }
        var kwArgs = {
            global: false,
            revertOnError: false,
            incrementalUpdates: false,
            alwaysPostNewItems: false,
            onComplete: saveCompleted
        };
        mainStore.save(kwArgs);
    };
    var saveCompleted = function (scope, actions) {
        cancelUpdateAll();
        dijit.byId('saveButton').set('disabled', true);
        if (saveDialogWin) saveDialogWin.close();
    };

    var showEditDialog = function () {
        $(document.body).mask();
        showNewWindow("/Verify/WaterLevelUpdateDialog", window, "dialogHeight:450px;dialogWidth:600px;");
    };

    var cancelUpdateAll = function () {
        dijit.byId('saveButton').set('disabled', true);
        originStoreItems = [];
        grid.edit.cancel();
        mainStore.revert();
        //searchGrid();
    };

    function swap(a, b) {
        var temp = a;
        a=b;
        b=temp;
    }
    //↑↑↑↑↑↑↑↑↑ 버튼클릭 명령들 끝   ↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑
    
    //↓↓↓↓↓↓↓↓↓ 콤보박스 변경 이벤트 시작 ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
    var changeDamType = function () {
        var val = getSelectValue("damTp");
        damCdOptions = glGetDamCdList(val);
        setOptions("damCd", damCdOptions);
    };
    var changeDamCd = function () {
        var val = getSelectValue("damCd");
        obsCdOptions = glGetObsCdList('WL', val);
        setOptions("obsCd", obsCdOptions);
    };
    
    //↑↑↑↑↑↑↑↑↑ 콤보박스 변경 이벤트 끝   ↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

    //↓↓↓↓↓↓↓↓↓ 그리드 이벤트 시작 ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
    var completeFetch = function (items, request) {

        $(document.body).unmask();
        //alert("complete");

        loadChartPanel();
    };

    var historyParams = {};

    var showHistoryDialog = function (ev) {
        if (ev.cellIndex > 8) {
            var item = grid.getItem(ev.rowIndex);

            historyParams = {
                obscd: item.OBSCD,
                obsdt: item.OBSDT,
                trmdv: item.TRMDV
            };

            showNewWindow("/Verify/WaterLevelHistoryDialog", window, "dialogHeight:205px;dialogWidth:705px;", true);
        }
    };

    var highlightModifiedRow = function (row) {
                    if (row.over) {
                        //change font color
                        //row.customStyles = 'color:red';
                        //change background color
                        row.customStyles += 'background-color:#FFB93F;';
                    }

//        var edexway = grid.store.getValue(grid.getItem(rowIndex), 'EDEXWAY');

//        if (edexway != '') {
//            row.customStyles += 'background-color:#FFB93F;';
//        }
    }
    //↑↑↑↑↑↑↑↑↑ 그리드 이벤트 끝   ↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

    var malert = function (msg) {
        Message.innerHTML = msg;
        dijit.byId('MessageDialog').show()
    };

    function expand(thistag) {
        styleObj = document.getElementById(thistag).style;
        if (styleObj.display == 'none') {
            styleObj.display = '';
        }
        else
        { styleObj.display = 'none'; }
    }
    var unmaskBody = function () {
        $(document.body).unmask();
    }
    var gridselectstart = function (e) {
        var e = e || window.event,
            s = e.srcElement || e.target;
        if (s && s.id.substr(0, "dijit_form_TextBox".length) == "dijit_form_TextBox") {
            e.stopPropagation();
            return true;
        }
    }

    //↓↓↓↓↓↓↓↓↓ 초기화 시작 ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
    dojo.ready(function () {
        initChartPanel();
        mainStore = new dojox.data.JsonRestStore({
            target: DataUrl
            , timeout: 3600000
            , idAttribute: "ID"
        });
        grid = new dojox.grid.EnhancedGrid({
            id: "grid",
            //store:  mainStore,
            structure: layout,
            plugins: plugins,
            //rowsPerPage: 10,
            keepRows: 144,
            //columnReordering: true,
            //autoLoad: false,
            noDataMessage: "데이터가 존재하지 않습니다.",
            //queryOptions: { cache: true },
            selectable: true,
            canSort: function (colIndex) {
                return false;
            },
            onHeaderCellClick: function (e) {
                if (!dojo.hasClass(e.cell.id, "staticHeader")) {
                    e.grid.setSortIndex(e.cell.index);
                    e.grid.onHeaderClick(e);
                }
            },
            onHeaderCellMouseOver: function (e) {
                if (!dojo.hasClass(e.cell.id, "staticHeader")) {
                    dojo.addClass(e.cellNode, this.cellOverClass);
                }
            },
            onStyleRow: function (e) {
                //1.상단의 컬럼(원시, 추정)의 셀은 보이지 않게 함.
                if (e.node.children[0].children[0].rows.length == 2) {
                    dojo.style(e.node.children[0].children[0].rows[0], 'display', 'none');
                }
            }
        }, document.createElement("div"));
        dojo.connect(grid, "_onFetchComplete", completeFetch);
        dojo.connect(grid, "onRowDblClick", showHistoryDialog);
        dojo.connect(grid, "onStyleRow", onStyleRow1M);
        grid.startup();
        dojo.byId("MainGridPanel").appendChild(grid.domNode);

        dojo.connect(dojo.byId("damTp"), "onchange", changeDamType);
        dojo.connect(dojo.byId("damCd"), "onchange", changeDamCd);

        //댐타입
        damTpOptions = GetAllDamTpList();
        setOptions("damTp", damTpOptions);
        setSelectOption("damTp", R_DAMTYPE);
        changeDamType();

        //댐명
        setSelectOption("damCd", R_DAMCD);
        changeDamCd();

        //관측국명
        setSelectOption("obsCd", R_OBSCD);

        //기타
        edExLvlOptions = gl_edexlvl;
        gl_excd_W.unshift({ text: '선택하세요', value: '' });
        exCdOptions = gl_excd_W;
        exEmpNoOptions = gl_empno;
        setOptions("edExLvl", edExLvlOptions);
        //setOptions("exCd", exCdOptions);
        setOptions("exEmpNo", exEmpNoOptions);
        setOptions("dataTp", dataTpOptions);

        //setSelectOption("exCd", R_EXCD);
        setSelectOption("dataTp", R_DATATP);

        //날짜
        var startday = null, endday = null;
        if (R_OBSDT == '') {
            startday = dojo.date.locale.format(prevDate, datePattern);
            startday = dojo.date.locale.format(currDate, datePattern);
        } else {
            var pDate = dojo.date.add(dojo.date.locale.parse(R_OBSDT, datePattern2), "day", -1);
            startday = dojo.date.locale.format(pDate, datePattern); //2일씩 검색한다.
            endday = formatDate(R_OBSDT);
        }
        startDtCal = new dijit.form.DateTextBox({
            id: "startDt",
            name: "startDt",
            value: startday,
            style: "width:100px;"
        }, "startDt");
        endDtCal = new dijit.form.DateTextBox({
            id: "endDt",
            name: "endDt",
            value: endday,
            style: "width:100px;"
        }, "endDt");

        setOptions("startHr", gl_24times);
        setOptions("endHr", gl_24times);
        setSelectOption("endHr", "24");

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
            connectId: ["editButton"],
            position: "above",
            showDelay: 0,
            label: "직접수정, 선형보간, 지수함수곡선 등을 적용합니다"
        });
        new dijit.Tooltip({
            connectId: ["cancelButton"],
            position: "above",
            showDelay: 0,
            label: "확정하기 전의 데이터를 복원합니다"
        });
        new dijit.Tooltip({
            connectId: ["saveButton"],
            position: "above",
            showDelay: 0,
            label: "보정한 데이터를 확정합니다"
        });

        dijit.byId('saveButton').set('disabled', true);


        if (R_OBSCD == '') hidePreloader();

        dojo.connect(dojo.byId("MainGridPanel"), "onselectstart", gridselectstart);
        //asdf.sadf(); //mainStore
    });

</script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<div id="menu-title" class="title_box" style="position:absolute;text-align:left;height:40px;top:10px;left:10px;" unselectable="on" onselectstart="return false">
		<span style="font-weight:bold;font-size:12px;margin-left:10px" unselectable="on" onselectstart="return false">
	        <img src="<%=Page.ResolveUrl("/Images") %>/icons/highlighter.png" align="absmiddle" />&nbsp;&nbsp;
        수위자료 보정
        </span>
    </div>
<br />
<p align="right" unselectable="on" onselectstart="return false">※ 보정이력란을 더블 클릭하면 보정이력을 볼 수 있습니다.</p>

    <div id="border1" data-dojo-type="dijit.layout.BorderContainer" data-dojo-props='style:"position:absolute;top:50px; width: 100%; height: 99%;"'>
        <div id="topPanel" data-dojo-type="dijit.layout.ContentPane" data-dojo-props='id:"topPanel", region:"top", style:"height: 50px;background-color:#D3E3F8;"' unselectable="on" onselectstart="return false">
         <!-- 댐구분, 댐명, 시작날,시작시,끝날,끝시, 구분, 조회, 엑셀 -->
         <table>
         <tr>
            <td >댐&nbsp;&nbsp;구&nbsp;&nbsp;분 :</td>
            <td><select id="damTp" class="wsComboBox" style="width:110px"></select></td>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;댐 명 :</td>
            <td><select id="damCd" class="wsComboBox" widgetId="damCdCb" style="width:110px"></select></td>
            <td>관측국 :</td>
            <td><select id="obsCd" class="wsComboBox" style="width:110px"></select></td>
            <td>&nbsp;&nbsp;일 시 :</td>
            <td>
                <select id="dateTp" class="wsComboBox" style="width:80px">
                    <option value="obdt">측정일시</option>
                    <option value="chdt">확인일시</option>
                    <option value="vrdt">보정일시</option>
                </select>
            </td>
            <td><input id="startDt" /><select id="startHr" class="wsComboBox"></select>~<input id="endDt" /><select id="endHr" class="wsComboBox"></select></td>
         </tr>
     
         <tr>
             <td>보정등급 :</td>
             <td><select id="edExLvl" class="wsComboBox" style="width:110px"></select></td>
             <td></td><td></td>
<!--             <td>품질등급 :</td>
             <td><select id="exCd" class="wsComboBox" style="width:110px"></select></td>-->
             <td>&nbsp;&nbsp;&nbsp;사 원 :</td>
             <td><select id="exEmpNo" class="wsComboBox" style="width:110px"></select></td>
             <td>&nbsp;&nbsp;구 분 : </td>
             <td><select id="dataTp" class="wsComboBox" style="width:80px"></select></td>

             <td>
                 <button id="searchButton" dojoType="dijit.form.Button" data-dojo-props='id:"searchButton"' onClick="searchGrid()"><img src='<%=Page.ResolveUrl("~/Images") %>/icons/magnifier.png'> 조 회</button>
                 <button id="excelButton" dojoType="dijit.form.Button" data-dojo-props='id:"excelButton"' onClick="downloadExcel()" ><img src='<%=Page.ResolveUrl("~/Images") %>/icons/document-excel-table.png'> 엑 셀</button>
                 <button id="editButton" dojoType="dijit.form.Button" data-dojo-props='id:"editButton"' onClick="showEditDialog()"><img src='<%=Page.ResolveUrl("~/Images") %>/icons/border-color.png'> 보 정</button>
                 <button id="cancelButton" dojoType="dijit.form.Button" data-dojo-props='id:"cancelButton"' onClick="cancelUpdateAll()"><img src='<%=Page.ResolveUrl("~/Images") %>/icons/cross-circle.png'> 취 소</button>
                 <button id="saveButton" dojoType="dijit.form.Button" data-dojo-props='id:"saveButton"' onClick="saveConfirm()"><img src='<%=Page.ResolveUrl("~/Images") %>/icons/disks.png'> 저 장</button>
                 <button id="chartPanelButton" data-dojo-type="dijit.form.Button" data-dojo-props='id:"chartPanelButton",onClick:toggleChartPanel'><img src='/Images/icons/chart-up-color.png'> 그래프</button>
             </td>
         </tr>
         </table>

     
     
	    </div>
        <div id="MainGridPanel" data-dojo-type="dijit.layout.ContentPane" data-dojo-props='id:"MainGridPanel", region:"center", style:"margin:0 0 0 0;padding:0 0 0 0;overflow:hidden;"'  unselectable="on">
	    </div>
	    <div id="ChartPanel" data-dojo-type="dojox.layout.ExpandoPane" data-dojo-props='id:"ChartPanel", region:"bottom", style:" height: 40%;margin:0px 0px 40px 0px;", splitter:true' unselectable="on" onselectstart="return false">
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
    <div id="MessageDialog" data-dojo-type="dijit.Dialog" data-dojo-props='title:"메세지", "aria-describedby":"intro", style:"width:150px;height:150px;"'>
        <div id="Message" style="width:100%;height:150px;"></div>
    </div>

<form name="downloadExcelForm" action="/Common/DownloadExcel" method="post">
<input type="hidden" name="content" value="" />
<input type="hidden" name="fileName" value="수위보정자료.xls" />
</form>
</asp:Content>
