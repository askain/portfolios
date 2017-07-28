<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub3.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="HDIMS.Utils.Login"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    댐운영자료 보정이력
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PreHeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
    .claro .dijitDialogUnderlay { background:transparent; } 
    .dojoxGridCellSelected { background-color: red; }
</style>
<%
    EmpData empdata = new EmpData();
%>
<script type="text/javascript" src="/Scripts/common/renderers.js"></script>
<script type="text/javascript" src="/Common/DamColTypeJs"></script>
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

    var currDate = new Date();
    var prevDate = new Date();
    prevDate.setDate(currDate.getDate() - 1);
    var loginEmpNo = "<%=empdata.GetEmpData(0) %>";
    var loginEmpNm = "<%=empdata.GetEmpData(1) %>";
    var chartMovie;
    var chartParams = { bgcolor: "#FFFFFF", wmode: 'transparent' };
    var chartSettingsUrl = "/Config/Chart/VerifyDamSettings.xml";
    var chartDataUrl = "/Config/Chart/VerifyDamData.xml";
    var chartVars = {
        path: "/Scripts/amcharts/flash/",
        settings_file: chartSettingsUrl,
        data_file: chartDataUrl,
        chart_id: "ChartViewPanel",
        width: '100%',
        height: '90%'
    };
    var damStore, damGrid, damTpOptions, damCdOptions, 
        damTpCombo, damCdCombo, startDtCal, startHrCombo, endDtCal, endHrCombo, dataTpCombo,
        damGridItems, pStartDtCal, pEndDtCal, sStartDtCal, sEndDtCal, startDtVal, endDtVal;
    var originStoreItems = [], preFrom, preTo, searchFlag = 0;
    var glUpdateCellVal = "";
    var damSearchUrl = "/Verify/GetDamDataVerifyList";
    var damExcelUrl = "/Verify/GetDamDataVerifyExcel/?";
    var datePattern = { datePattern: "yyyy-MM-dd", selector: "date" };
    var datePattern2 = { datePattern: "yyyyMMdd", selector: "date" };
    var datePattern3 = { datePattern: "yyyyMMddHHmm", selector: "date" };

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

    
    var resetFilter = function () {
        var from = originStoreItems[originStoreItems.length - 1]["OBSDT"];
        var to = originStoreItems[0]["OBSDT"];

        filterItems(from, to);
    };

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
        damGrid.setScrollTop(0);
        dijit.byId('showAllChartBtn').set('disabled', false);
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
        var gd5 = "<graph gid=\"5\">";
        var gd6 = "<graph gid=\"6\">";
        var gd7 = "<graph gid=\"7\">";
        var gd8 = "<graph gid=\"8\">";
        var gd9 = "<graph gid=\"9\">";
        var gd10 = "<graph gid=\"10\">";
        var gd11 = "<graph gid=\"11\">";
        var gd12 = "<graph gid=\"12\">";
        var gd13 = "<graph gid=\"13\">";
        var gd14 = "<graph gid=\"14\">";
        var gd15 = "<graph gid=\"15\">";
        var gd16 = "<graph gid=\"16\">";
        var gd17 = "<graph gid=\"17\">";
        var gd18 = "<graph gid=\"18\">";
        var gd19 = "<graph gid=\"19\">";
        var gd20 = "<graph gid=\"20\">";
        data += "<chart>";
        var cnt = damGrid.rowCount;
        var rid, cm;
        if (cnt > 0) {
            var j = 0;
            for (var i = cnt - 1; i >= 0; i--) {
                cm = damGrid.getItem(i);
                sdata += "<value xid=\"" + j + "\">" + convStrToDateMin(damGrid.store.getValue(cm, "OBSDT")) + "</value>";
                gd1 += "<value xid=\"" + j + "\">" + damGrid.store.getValue(cm, "RWL") + "</value>";
                gd2 += "<value xid=\"" + j + "\">" + damGrid.store.getValue(cm, "RSQTY") + "</value>";
                gd3 += "<value xid=\"" + j + "\">" + damGrid.store.getValue(cm, "RSRT") + "</value>";
                gd4 += "<value xid=\"" + j + "\">" + damGrid.store.getValue(cm, "OSPILWL") + "</value>";
                gd5 += "<value xid=\"" + j + "\">" + damGrid.store.getValue(cm, "IQTY") + "</value>";
                gd6 += "<value xid=\"" + j + "\">" + damGrid.store.getValue(cm, "ETCIQTY1") + "</value>";
                gd7 += "<value xid=\"" + j + "\">" + damGrid.store.getValue(cm, "ETCIQTY2") + "</value>";
                gd8 += "<value xid=\"" + j + "\">" + damGrid.store.getValue(cm, "ETQTY") + "</value>";
                gd9 += "<value xid=\"" + j + "\">" + damGrid.store.getValue(cm, "TDQTY") + "</value>";
                gd10 += "<value xid=\"" + j + "\">" + damGrid.store.getValue(cm, "EDQTY") + "</value>";
                gd11 += "<value xid=\"" + j + "\">" + damGrid.store.getValue(cm, "ETCEDQTY") + "</value>";
                gd12 += "<value xid=\"" + j + "\">" + damGrid.store.getValue(cm, "SPDQTY") + "</value>";
                gd13 += "<value xid=\"" + j + "\">" + damGrid.store.getValue(cm, "ETCDQTY1") + "</value>";
                gd14 += "<value xid=\"" + j + "\">" + damGrid.store.getValue(cm, "ETCDQTY2") + "</value>";
                gd15 += "<value xid=\"" + j + "\">" + damGrid.store.getValue(cm, "ETCDQTY3") + "</value>";
                gd16 += "<value xid=\"" + j + "\">" + damGrid.store.getValue(cm, "OTLTDQTY") + "</value>";
                gd17 += "<value xid=\"" + j + "\">" + damGrid.store.getValue(cm, "ITQTY1") + "</value>";
                gd18 += "<value xid=\"" + j + "\">" + damGrid.store.getValue(cm, "ITQTY2") + "</value>";
                gd19 += "<value xid=\"" + j + "\">" + damGrid.store.getValue(cm, "ITQTY3") + "</value>";
                gd20 += "<value xid=\"" + j + "\">" + damGrid.store.getValue(cm, "DAMBSARF") + "</value>";
                j++;
            }
            data += sdata + "</series>";
            data += "<graphs>";
            data += gd1 + "</graph>";
            data += gd2 + "</graph>";
            data += gd3 + "</graph>";
            data += gd4 + "</graph>";
            data += gd5 + "</graph>";
            data += gd6 + "</graph>";
            data += gd7 + "</graph>";
            data += gd8 + "</graph>";
            data += gd9 + "</graph>";
            data += gd10 + "</graph>";
            data += gd11 + "</graph>";
            data += gd12 + "</graph>";
            data += gd13 + "</graph>";
            data += gd14 + "</graph>";
            data += gd15 + "</graph>";
            data += gd16 + "</graph>";
            data += gd17 + "</graph>";
            data += gd18 + "</graph>";
            data += gd19 + "</graph>";
            data += gd20 + "</graph>";
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
            data += "<graph gid=\"3\">";
            data += "<value xid=\"0\"></value>";
            data += "</graph>";
            data += "</graphs>";
        }
        data += "</chart>";
        searchFlag = 2;
        return data;
    };


    var dataTpOptions = [
        { text: '1분', value: '1' },
        { text: '10분', value: '10', selected: true },
        { text: '30분', value: '30' },
        { text: '60분', value: '60' },
        { text: '일자료', value: 'DAY' }
    ];

    var markFormmatter = function (value, rowIndex) {
        var item = damGrid.getItem(rowIndex);
        var field = this.constraint.field;
        var obsdt = damGrid.store.getValue(item, "OBSDT", null);
        var tp = getSelectValue("dataTp");
        var val = value;
        if (field == "OBSDT") {
            val = convStrToDateMin(value);
        }
        var isMark = false;


        if (tp == "1" && obsdt.substring(8, 12) == "0001") {
            isMark = true;
        } else if (tp == "10" && obsdt.substring(8, 12) == "0010") {
            isMark = true;
        } else if (tp == "30" && obsdt.substring(8, 12) == "0030") {
            isMark = true;
        } else if (tp == "60" && obsdt.substring(8, 10) == "01") {
            isMark = true;
        } else if (tp == "DAY" && obsdt.substring(6, 8) == "01") {
            isMark = true;
        }
        if (isMark == true) {
            return '<div style="background-color: #E9D4FF;font-weight:bold;font-color:#000000;">' + val + '</div>';
        }
        return val;
    };

    var updateFormmatter = function (value, rowIndex, cellId, cellField, cellObj, rowObj) {
        var field = this.constraint.field;
        value = _numberFormat(field, value);
        
        value = hasHistoryFormatter(value, rowIndex, cellId, cellField, cellObj, rowObj);
        var item = damGrid.getItem(rowIndex);
        
        if (item && field && damGrid.store.getValue(item, field + "_CK") == "1") {
            dijit.byId('saveButton').set('disabled', false);
            return '<div style="background-color: #FF8A75;margin:0 0 0 0;padding:0 0 0 0;">' + value + '</div>';
        }

        return value;
    };

    var numberFormmatter = function (value, rowIndex, cellId, cellField, cellObj, rowObj) {
        value = hasHistoryFormatter(value, rowIndex, cellId, cellField, cellObj, rowObj);
        value = _numberFormat(this.constraint.field, value);
        return value;
    }
    var _numberFormat = function (field, value) {
        var digitField = eval("damFieldDigit." + field);
        //console.log(digitField);
        if (digitField) value = glNumberFormat2(value, digitField);
        return value;
    };
    var hasHistoryFormatter = function (inValue, rowId, cellId, cellField, cellObj, rowObj) {
        try {
            var item = damGrid.getItem(rowId);
            //            var cellNumber = cellId.field.substr(cellId.field.lastIndexOf('_') + 1);
            var HASHISTORY = item[cellId.field + "_CK"];

            if (HASHISTORY == 'Y') {
                //return inValue;     
                return '<div style="font-weight: bold;font-size:14px;">' + inValue + '</div>';
            }
            return inValue;
        }
        catch (e) {
            alert(e);
        }
    };
    var objectFormatter = function (value, rownIndex) {
        if (typeof arguments[0] == 'object') {
            return "{ ... }";
        } 
        return value;

    };

    var formatTextDate2 = function (value) {
        return "<div style='mso-number-format:\\@;'>" + convStrToDateMin(value) + "</div>";
    }

    var damEditField = {
        RWL: true, RSQTY: false, RSRT: false, OSPILWL: true, IQTY: false, ETCIQTY1: false, ETCIQTY2: true, ETQTY: false, TDQTY: false,
        EDQTY: true, ETCEDQTY: true, SPDQTY: true, ETCDQTY1: true, ETCDQTY2: true, ETCDQTY3: true, OTLTDQTY: true,
        ITQTY1: true, ITQTY2: true, ITQTY3: true, DAMBSARF: false, CGEMPNM: false, CGDT: false, CHKEMPNM: false, CHKDT: false
    };
    var damFieldDigit = {
        RWL: 2, RSQTY: 3, RSRT: 1, OSPILWL: 2, IQTY: 3, ETCIQTY1: 3, ETCIQTY2: 2, ETQTY: 3, TDQTY: 2,
        EDQTY: 2, ETCEDQTY: 2, SPDQTY: 2, ETCDQTY1: 2, ETCDQTY2: 2, ETCDQTY3: 2, OTLTDQTY: 2,
        ITQTY1: 2, ITQTY2: 2, ITQTY3: 2, DAMBSARF: 2
    };
    var damStruct = [{
        defaultCell: {
            type: dojox.grid.cells._Widget
        },
        cells: [
                { name: '댐명', field: 'DAMNM', width: "80px", styles: 'text-align:center;vertical-align:middle;', constraint: { field: 'DAMNM'} },
                { name: '댐코드', field: 'DAMCD', width: "60px", styles: 'text-align:center;vertical-align:middle;', datatype: "string", constraint: { field: 'DAMCD'} },
                { name: '측정일시', field: 'OBSDT', width: "120px", styles: 'text-align:center;vertical-align:middle;', datatype: "string", constraint: { field: 'OBSDT' }, formatter: formatTextDate }
            ],
        noscroll: true,
        width: 'auto'
    }, {
        defaultCell: {
            type: dojox.grid.cells._Widget
        },
        cells: [
                { name: '저수위', field: 'RWL', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;cursor:hand;', editable: true, formatter: updateFormmatter, constraint: { field: 'RWL'} },
                { name: '저수량', field: 'RSQTY', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;background-color:#efefef;', formatter: numberFormmatter, constraint: { field: 'RSQTY'} },
                { name: '저수율', field: 'RSRT', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;background-color:#efefef;', formatter: numberFormmatter, constraint: { field: 'RSRT'} },
                { name: '방수로</br>수위', field: 'OSPILWL', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;cursor:hand;', datatype: "number", editable: true, formatter: updateFormmatter, constraint: { field: 'OSPILWL'} },
                { name: '유입량', field: 'IQTY', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;background-color:#efefef;', formatter: numberFormmatter, constraint: { field: 'IQTY'} },
                { name: '기타</br>유입량1', field: 'ETCIQTY1', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;background-color:#efefef;', formatter: numberFormmatter, constraint: { field: 'ETCIQTY1'} },
                { name: '기타</br>유입량2', field: 'ETCIQTY2', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;cursor:hand;', datatype: "number", editable: true, formatter: updateFormmatter, constraint: { field: 'ETCIQTY2'} },
                { name: '공용량', field: 'ETQTY', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;background-color:#efefef;', formatter: numberFormmatter, constraint: { field: 'ETQTY'} },
                { name: '총방류량', field: 'TDQTY', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;background-color:#efefef;', formatter: numberFormmatter, constraint: { field: 'TDQTY'} },
                { name: '발전</br>방류량', field: 'EDQTY', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;cursor:hand;', datatype: "number", editable: true, formatter: updateFormmatter, constraint: { field: 'EDQTY'} },
                { name: '기타발전</br>방류량', field: 'ETCEDQTY', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;cursor:hand;', datatype: "number", editable: true, formatter: updateFormmatter, constraint: { field: 'ETCEDQTY'} },
                { name: '수문</br>방류량', field: 'SPDQTY', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;cursor:hand;', datatype: "number", editable: true, formatter: updateFormmatter, constraint: { field: 'SPDQTY'} },
                { name: '기타</br>방류량1', field: 'ETCDQTY1', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;cursor:hand;', datatype: "number", editable: true, formatter: updateFormmatter, constraint: { field: 'ETCDQTY1'} },
                { name: '기타</br>방류량2', field: 'ETCDQTY2', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;cursor:hand;', datatype: "number", editable: true, formatter: updateFormmatter, constraint: { field: 'ETCDQTY2'} },
                { name: '기타</br>방류량3', field: 'ETCDQTY3', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;cursor:hand;', datatype: "number", editable: true, formatter: updateFormmatter, constraint: { field: 'ETCDQTY3'} },
                { name: '아울렛</br>방류량', field: 'OTLTDQTY', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;cursor:hand;', datatype: "number", editable: true, formatter: updateFormmatter, constraint: { field: 'OTLTDQTY'} },
                { name: '취수1', field: 'ITQTY1', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;cursor:hand;', datatype: "number", editable: true, formatter: updateFormmatter, constraint: { field: 'ITQTY1'} },
                { name: '취수2', field: 'ITQTY2', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;cursor:hand;', datatype: "number", editable: true, formatter: updateFormmatter, constraint: { field: 'ITQTY2'} },
                { name: '취수3', field: 'ITQTY3', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;cursor:hand;', datatype: "number", editable: true, formatter: updateFormmatter, constraint: { field: 'ITQTY3'} },
                { name: '평균우량', field: 'DAMBSARF', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;background-color:#efefef;', formatter: numberFormmatter, constraint: { field: 'DAMBSARF'} },
                { name: '보정자', field: 'CGEMPNM', width: "50px", styles: 'text-align:center;background-color:#efefef;vertical-align:middle;cursor:hand;' },
                { name: '보정일시', field: 'CGDT', width: "120px", styles: 'text-align:center;background-color:#efefef;vertical-align:middle;cursor:hand;', formatter: formatTextDate2 },
                { name: '확인자', field: 'CHKEMPNM', width: "50px", styles: 'text-align:center;background-color:#efefef;vertical-align:middle;cursor:hand;' },
                { name: '확인일시', field: 'CHKDT', width: "120px", styles: 'text-align:center;background-color:#efefef;vertical-align:middle;cursor:hand;', formatter: formatTextDate2 }
            ],
        width: 'auto'
    }
    ];

    var damStructNoEdit = [{
        defaultCell: {
            type: dojox.grid.cells._Widget
        },
        cells: [
                { name: '댐명', field: 'DAMNM', width: "80px", styles: 'text-align:center;vertical-align:middle;', datatype: "date", constraint: { field: 'DAMNM'} },
                { name: '댐코드', field: 'DAMCD', width: "60px", styles: 'text-align:center;vertical-align:middle;', constraint: { field: 'DAMCD'} },
                { name: '측정일시', field: 'OBSDT', width: "120px", styles: 'text-align:center;vertical-align:middle;', constraint: { field: 'OBSDT' }, formatter: formatTextDate }
            ],
        noscroll: true,
        width: 'auto'
    }, {
        defaultCell: {
            type: dojox.grid.cells._Widget
        },
        cells: [
                { name: '저수위', field: 'RWL', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;', formatter: numberFormmatter, constraint: { field: 'RWL'} },
                { name: '저수량', field: 'RSQTY', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;', formatter: numberFormmatter, constraint: { field: 'RSQTY'} },
                { name: '저수율', field: 'RSRT', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;', formatter: numberFormmatter, constraint: { field: 'RSRT'} },
                { name: '방수로</br>수위', field: 'OSPILWL', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;', formatter: numberFormmatter, constraint: { field: 'OSPILWL'} },
                { name: '유입량', field: 'IQTY', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;', formatter: numberFormmatter, constraint: { field: 'IQTY'} },
                { name: '기타</br>유입량1', field: 'ETCIQTY1', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;', formatter: numberFormmatter, constraint: { field: 'ETCIQTY1'} },
                { name: '기타</br>유입량2', field: 'ETCIQTY2', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;', formatter: numberFormmatter, constraint: { field: 'ETCIQTY2'} },
                { name: '공용량', field: 'ETQTY', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;', formatter: numberFormmatter, constraint: { field: 'ETQTY'} },
                { name: '총방류량', field: 'TDQTY', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;', formatter: numberFormmatter, constraint: { field: 'TDQTY'} },
                { name: '발전</br>방류량', field: 'EDQTY', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;', formatter: numberFormmatter, constraint: { field: 'EDQTY'} },
                { name: '기타발전</br>방류량', field: 'ETCEDQTY', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;', formatter: numberFormmatter, constraint: { field: 'ETCEDQTY'} },
                { name: '수문</br>방류량', field: 'SPDQTY', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;', formatter: numberFormmatter, constraint: { field: 'SPDQTY'} },
                { name: '기타</br>방류량1', field: 'ETCDQTY1', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;', formatter: numberFormmatter, constraint: { field: 'ETCDQTY1'} },
                { name: '기타</br>방류량2', field: 'ETCDQTY2', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;', formatter: numberFormmatter, constraint: { field: 'ETCDQTY2'} },
                { name: '기타</br>방류량3', field: 'ETCDQTY3', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;', formatter: numberFormmatter, constraint: { field: 'ETCDQTY3'} },
                { name: '아울렛</br>방류량', field: 'OTLTDQTY', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;', formatter: numberFormmatter, constraint: { field: 'OTLTDQTY'} },
                { name: '취수1', field: 'ITQTY1', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;', formatter: numberFormmatter, constraint: { field: 'ITQTY1'} },
                { name: '취수2', field: 'ITQTY2', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;', formatter: numberFormmatter, constraint: { field: 'ITQTY2'} },
                { name: '취수3', field: 'ITQTY3', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;', formatter: numberFormmatter, constraint: { field: 'ITQTY3'} },
                { name: '평균우량', field: 'DAMBSARF', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;', formatter: numberFormmatter, constraint: { field: 'DAMBSARF'} },
                { name: '보정자', field: 'CGEMPNM', width: "50px", styles: 'text-align:center;vertical-align:middle;cursor:hand;' },
                { name: '보정일시', field: 'CGDT', width: "120px", styles: 'text-align:center;vertical-align:middle;cursor:hand;', formatter: formatTextDate2 },
                { name: '확인자', field: 'CHKEMPNM', width: "50px", styles: 'text-align:center;vertical-align:middle;cursor:hand;' },
                { name: '확인일시', field: 'CHKDT', width: "120px", styles: 'text-align:center;vertical-align:middle;cursor:hand;', formatter: formatTextDate2 }
            ],
        width: 'auto'
    }
    ];
    var damStructDay = [{
        defaultCell: {
            type: dojox.grid.cells._Widget
        },
        cells: [
                { name: '댐명', field: 'DAMNM', width: "80px", styles: 'text-align:center;vertical-align:middle;', constraint: { field: 'DAMNM'} },
                { name: '댐코드', field: 'DAMCD', width: "60px", styles: 'text-align:center;vertical-align:middle;', constraint: { field: 'DAMCD'} },
                { name: '측정일자', field: 'OBSDT', width: "120px", styles: 'text-align:center;vertical-align:middle;', constraint: { field: 'OBSDT' }, formatter: formatTextDate2 }
            ],
        noscroll: true,
        width: 'auto'
    }, {
        defaultCell: {
            type: dojox.grid.cells._Widget
        },
        cells: [
                { name: '저수위', field: 'RWL', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '전년<br/>저수위', field: 'VYRWL', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '예년<br/>저수위', field: 'OYRWL', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '최고<br/>저수위', field: 'MXRWL', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '최고<br/>저수위일자', field: 'MXRWLDH', width: "70px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '최저<br/>저수위', field: 'MNRWL', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '최저<br/>저수위일자', field: 'MNRWLDH', width: "70px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '저수량', field: 'RSQTY', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '전년<br/>저수량', field: 'VYRSQTY', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '예년<br/>저수량', field: 'OYRSQTY', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '유입량', field: 'IQTY', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '전년<br/>유입량', field: 'VYIQTY', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '예년<br/>유입량', field: 'OYIQTY', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '최대<br/>유입량', field: 'MXIQTY', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '최대<br/>유입량일자', field: 'MXIQTYDH', width: "70px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '기타<br/>유입량1', field: 'ETCIQTY1', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '기타<br/>유입량2', field: 'ETCIQTY2', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '총방류량', field: 'TDQTY', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '전년<br/>총방류량', field: 'VYTDQTY', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '예년<br/>총방류량', field: 'OYTDQTY', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '최대<br/>방류량', field: 'MXDQTY', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '최대<br/>방류량일자', field: 'MXDQTYDH', width: "70px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '발전<br/>방류량', field: 'EDQTY', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '기타<br/>발전방류량', field: 'ETCEDQTY', width: "60px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '여수로<br/>방류량', field: 'SPDQTY', width: "60px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '기타<br/>방류량1', field: 'ETCDQTY1', width: "60px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '기타<br/>방류량2', field: 'ETCDQTY2', width: "60px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '기타<br/>방류량3', field: 'ETCDQTY3', width: "60px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: 'OUTLET<br/>방류량', field: 'OTLTDQTY', width: "60px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '취수량1', field: 'ITQTY1', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '취수량2', field: 'ITQTY2', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '취수량3', field: 'ITQTY3', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '강수량', field: 'RF', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '전년<br/>강수량', field: 'VYRF', width: "60px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '예년<br/>강수량', field: 'OYRF', width: "60px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '금년<br/>년누계', field: 'PYACURF', width: "60px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '전년<br/>년누계', field: 'VYACURF', width: "60px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '예년<br/>년누계', field: 'OYAACURF', width: "60px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '최대<br/>강우량', field: 'MXRF', width: "60px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '최대<br/>강우일시', field: 'MXRFDH', width: "70px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '검보정레벨', field: 'EDEXLVL', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '저수율', field: 'RSRT', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '공용량', field: 'ETQTY', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: 'HDAPS<br/>발전방류량', field: 'HEDQTY', width: "60px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '발전정보<br/>발전방류량', field: 'GEDQTY', width: "60px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '07시<br/>금일강수량', field: 'RF07', width: "60px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '전년<br/>발전방류량', field: 'VYEDQTY', width: "60px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '전년<br/>기타발전방류량', field: 'VYETCEDQTY', width: "90px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '전년<br/>여수로방류량', field: 'VYSPDQTY', width: "90px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '전년<br/>기타방류량1', field: 'VYETCDQTY1', width: "70px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '전년<br/>기타방류량2', field: 'VYETCDQTY2', width: "70px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '전년<br/>기타방류량3', field: 'VYETCDQTY3', width: "70px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '전년<br/>OUTLET방류량', field: 'VYOTLTDQTY1', width: "90px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '전년<br/>취수량1', field: 'VYITQTY1', width: "60px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '전년<br/>취수량2', field: 'VYITQTY2', width: "60px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '전년<br/>취수량3', field: 'VYITQTY3', width: "60px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '예년<br/>발전방류량', field: 'OYEDQTY', width: "70px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '예년<br/>기타발전방류량', field: 'OYETCEDQTY', width: "90px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '예년<br/>여수로방류량', field: 'OYSPDQTY', width: "90px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '예년<br/>기타방류량1', field: 'OYETCDQTY1', width: "70px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '예년<br/>기타방류량2', field: 'OYETCDQTY2', width: "70px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '예년<br/>기타방류량3', field: 'OYETCDQTY3', width: "70px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '예년<br/>OUTLET방류량', field: 'OYOTLTDQTY1', width: "90px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '예년<br/>취수량1', field: 'OYITQTY1', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '예년<br/>취수량2', field: 'OYITQTY2', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '예년<br/>취수량3', field: 'OYITQTY3', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '방수로<br/>수위', field: 'OSPILWL', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '계획<br/>방류량', field: 'ESSSQTY', width: "50px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '예년<br/>기타유입량1', field: 'OYETCIQTY1', width: "70px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' },
                { name: '예년<br/>기타유입량2', field: 'OYETCIQTY2', width: "70px", headerStyles: 'text-align:center;vertical-align:middle;', styles: 'text-align:right;' }
            ],
        width: 'auto'
    }
    ];
    
    var getParameter = function (url, isObj) {
        var damTp = getSelectValue("damTp");
        var damCd = getSelectValue("damCd");
        var startDt = dojo.date.locale.format(startDtCal.getValue(), datePattern2);
        var startHr = getSelectValue("startHr");
        var endDt = dojo.date.locale.format(endDtCal.getValue(), datePattern2);
        var endHr = getSelectValue("endHr");
        var dataTp = getSelectValue("dataTp");
        if (dataTp == "1") {
            startDt = startDt + startHr + "0000";
            endDt = endDt + endHr + "5959";
        } else if (dataTp == "10" || dataTp == "30") {
            startDt = startDt + startHr + "00";
            endDt = endDt + endHr + "59";
        } else if (dataTp == "DAY") {
            startDt = startDt;
            endDt = endDt;
        } else {
            startDt += startHr;
            endDt += endHr;
        };
        var param = {
            damType: damTp,
            damCd: damCd,
            startDt: startDt,
            endDt: endDt,
            dataTp: dataTp
        };
        return param;

    };

    var getSelectValue = function (id) {
        var sel = document.getElementById(id);
        return sel.options[sel.options.selectedIndex].value;
    };

    var cancelUpdateAll = function () {
        searchFlag = 1;
        originStoreItems = [];
        dijit.byId('saveButton').set('disabled', true);
        damGrid.edit.cancel();
        damGrid.plugin("selector").clear("cell");
        damGrid.store.revert();
    };

    var applyCancelAll = function () {
        exItemGrid.edit.cancel();
        dijit.byId("updateAllDlg").hide();

    }

    var downloadExcel = function () {
        //        if (damGrid.rowCount > 0) {
        //            var param = getParameter();
        //            document.location.href = damExcelUrl + dojo.objectToQuery(param);
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
        damCdOptions = glGetDamCdList(val);
        setOptions("damCd", damCdOptions);
    };

    var getStructure = function () {
        var damCd = getSelectValue("damCd");
        var dataTp = getSelectValue("dataTp");
        var struct = damStruct;
        var editStruct = gl_IsThisUserAbleToChangeData(damCd) == true ? damStruct : damStructNoEdit;
        if (dataTp == "DAY") {
            struct = dojo.clone(damStructDay);
        } else if (dataTp == "10") {
            struct = dojo.clone(editStruct);
        } else {
            struct = dojo.clone(editStruct);
        }
        var damColType = eval("gl_damColumnTypes.DC" + damCd);
        if (damColType) {
            for (var i = 0; i < struct[1].cells.length; i++) {
                var fieldName = struct[1].cells[i].field;
                if (damColType[fieldName] && damColType[fieldName] != "") {
                    struct[1].cells[i].name = damColType[fieldName];
                }
            }
        }
        return struct;
    }
    var changeDamCd = function() {
       var damCd = getSelectValue("damCd");
       //toggleEditable(damCd);
    }

    var beginFetch = function () {
        alert("start");
    };
    var completeFetch = function (items, request) {
        //damGridItems = items;
        $(document.body).unmask();
        var dataTp = getSelectValue("dataTp");
        if (dataTp != "DAY") {
            if (items.length > 0)
                dijit.byId('chartPanelButton').set('disabled', false);
            if (searchFlag == 1) {
                loadChartPanel();
            }
        }
    };
    var errorFetch = function () {
        alert("error");
    };

    var toggleEditable = function (damcd) {
        var dataTp = getSelectValue("dataTp");
        var struct = getStructure();
        if (dataTp == "10" && gl_IsThisUserAbleToChangeData(damcd)) {
            dijit.byId('editButton').set('disabled', false);
            dijit.byId('cancelButton').set('disabled', false);
        } else {
            dijit.byId('editButton').set('disabled', true);
            dijit.byId('cancelButton').set('disabled', true);
        }
        damGrid.setStructure(struct);
    };

    var searchGrid = function () {
        dijit.byId('saveButton').set('disabled', true);
        var param = getParameter();
        
        if (param.damCd == "" && param.dataTp == "1") {
            alert("1분자료에서는 댐전체를 조회할 수 없습니다.");
            return undefined;
        }
        //        if (param.dataTp == "1") {
        //            dijit.byId('chartPanelButton').set('disabled', false);
        //            dojo.connect(damGrid, "onStyleRow", onStyleRow1M);
        //        } else //if (param.dataTp == "10") 
        //        {
        //            dijit.byId('chartPanelButton').set('disabled', false);
        //            dojo.connect(damGrid, "onStyleRow", onStyleRow1M);
        //        }
        //        else {
        //            dijit.byId('chartPanelButton').set('disabled', false);
        //            dojo.disconnect(damGrid, "onStyleRow", onStyleRow1M);
        //        }
        dijit.byId('chartPanelButton').set('disabled', true);
        dojo.connect(damGrid, "onStyleRow", onStyleRow1M);
        //closeChartPanel();
        originStoreItems = [];
        searchFlag = 1;
        damGrid.edit.cancel();
        damGrid.plugin("selector").clear("cell");

        damGrid.setStore(damStore, param);
        toggleEditable(param.damCd);

        $(document.body).mask("조회중입니다...");
    };
    var legendNum = 10;

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
        resetFilter();
        //loadChartPanel();
        //chartMovie.showAll();
        dijit.byId('showAllChartBtn').set('disabled', true);
    };
    var openChartPanel = function () {
        var ChartPanel = dijit.byId("ChartPanel");

        if (ChartPanel.opened == false) {
            loadChartPanel();
            ChartPanel.toggle();    //open
            ChartPanel.opened = true;
        }
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

    var showUpdateAllWin = function () {

        openChartPanel();
        //선택한 자료가 있는지 확인
        var selected = damGrid.plugin("selector").getSelected("cell");
        if (selected.length < 1) {
            alert("보정할 자료를 선택하여 합니다.");
            return;
        }
        $(document.body).mask();
        var nWin = showNewWindow("/Verify/DamDataUpdateDialog", window, "dialogHeight:560px;dialogWidth:880px;");
    };

    var unmaskBody = function () {
        $(document.body).unmask();
    }

    var historyParams = {};

    var showHistoryDialog = function (ev) {
        if (ev.cellIndex > 21) {
            var item = damGrid.getItem(ev.rowIndex);
            historyParams = {
                damcd: item.DAMCD,
                obsdt: item.OBSDT,
                trmdv: item.TRMDV
            };
            showNewWindow("/Verify/DamDataHistoryDialog", window, "dialogHeight:205px;dialogWidth:705px;",true);
        }
    };
    var getSelectedItems = function () {
        var cellSelected = {};
        var selected = damGrid.plugin("selector").getSelected("cell");
        var cellItems = damGrid.layout.cells;

        for (var i = 0; i < cellItems.length; i++) {
            var cell = cellItems[i];
            if (cell.editable == true) {
                cellSelected[cell.field] = [];
            }
        }
        //각 cell별로 수정범위의 데이터를 넣는다.
        dojo.forEach(selected, function (item) {
            var cell = damGrid.getCell(item.col);
            if (cellSelected[cell.field]) {

                var DamcdAndObsdt = item.id.split('_');
                item["DAMCD"] = DamcdAndObsdt[0];
                item["OBSDT"] = DamcdAndObsdt[1];
                cellSelected[cell.field].push(item);
            }
        });

        return cellSelected;
    };

    var saveUpdateAll = function () {
        showNewWindow("/Verify/DamDataSaveDialog", window, "dialogHeight:240px;dialogWidth:460px;");
    };

    var saveButtonDisable = function () {
        dijit.byId('saveButton').set('disabled', true);
    }

    var saveDialogWin;

    var saveCompleted = function (scope, actions) {
        damGrid.plugin("selector").clear("cell");
        damStore.revert();
        dijit.byId('saveButton').set('disabled', true);
        if (saveDialogWin) saveDialogWin.close();
    };

    var saveError = function (scope, value) {
        alert("보정 데이터 저장중에 에러가 발생하였습니다.[" + value + "]");
        
        if (saveDialogWin) saveDialogWin.close();
    };

    var saveData = function (edExWay, edExLvl, cnrsnText, cndsText, saveWin) {
        saveDialogWin = saveWin;


        //보정자, 보정일시 CGEMPNO, CGEMPNM, CGDT
        var cgempno = loginEmpNo;
        var cgempnm = loginEmpNm;
        var cgdt = dojo.date.locale.format(new Date(), { datePattern: "yyyyMMddHHmmss", selector: "date" });

        var dirty = dojox.rpc.JsonRest.getDirtyObjects();
        var oItem;
        if (dirty && dirty.length > 0) {
            for (i = 0; i < dirty.length; i++) {
                var targetRec = dirty[i].object;
                targetRec["EDEXWAY"] = edExWay;
                targetRec["EDEXLVL"] = edExLvl;
                targetRec["CGEMPNO"] = cgempno;
                targetRec["CGEMPNM"] = cgempnm;
                targetRec["CGDT"] = cgdt;
                targetRec["CNRSN"] = cnrsnText;
                targetRec["CNDS"] = cndsText;
            }
        }
        //alert(dirtyObjects.length);
        var postActions = [], actionIdx = 0,
            postItems = [], postIdx = 0,
            delItems = [];
        var postCnt = 144;
        var dirtyObj = dojox.rpc.JsonRest.getDirtyObjects();
        for (var i = 0; i < dirtyObj.length; i++) {
            var dirty = dirtyObj[i];
            var object = dirty.object;
            var old = dirty.old;
            var json = "";
            if (object) {
                json = dojox.json.ref.toJson(object);
                postItems[postIdx++] = dojo.fromJson(json);
                if ((postIdx % postCnt) == 0) {
                    postActions[actionIdx++] = { "Data": postItems };
                    postItems = [], postIdx = 0;
                } else if (i >= (dirtyObj.length - 1)) {
                    postActions[actionIdx++] = { "Data": postItems };
                }
            } else if (old) {
                json = dojox.json.ref.toJson(old);
                delItems.push(dojo.fromJson(json));
            }
            //console.log("i[" + i + "] : " + json + ", type : " + typeof (json));
        }
        var delParams = { "Data": delParams };
        var responseCnt = 1;
        //        alert(dirtyObj.length + ":" + postActions.length);
        //        for (var j = 0; j < postActions.length; j++) {
        //            var tt = postActions[j];
        //            alert(j  + ":" + tt.Data.length);
        //        }
        //        return;
        for (var j = 0; j < postActions.length; j++) {
            var postParams = postActions[j];
            //alert(j + " start");
            dojo.xhrPost({
                url: '/Verify/GetDamDataVerifyList',
                postData: dojo.toJson(postParams),
                handleAs: "json",
                contentType: "application/json; charset=utf-8",
                load: function (data) {
                    var jsonData = dojo.fromJson(data);
                    //alert("load : " + responseCnt + ":" + postActions.length);
                    if (responseCnt++ >= postActions.length) {
                        if (saveDialogWin) saveDialogWin.close();
                        damGrid.plugin("selector").clear("cell");
                        damStore.revert();
                        dijit.byId('saveButton').set('disabled', true);
                    }
                },
                error: function (error) {

                    //alert("error : " + responseCnt + ":" + postActions.length);
                    if (responseCnt++ >= postActions.length) {
//                        if (saveDialogWin) saveDialogWin.close();
                        //                        alert("보정데이터 저장중 에러가 발생하였습니다.");
                        if (saveDialogWin) saveDialogWin.close();
                        damGrid.plugin("selector").clear("cell");
                        damStore.revert();
                        dijit.byId('saveButton').set('disabled', true);
                    }
                }
            });
        }
        //if (saveDialogWin) saveDialogWin.close();

        //        damStore.save({
        //            global: false,
        //            syncMode: true, 
        //            onComplete: saveCompleted
        //        });
    }
    var onStyleRow1M = function (row) {
        ////dojo.connect(damGrid, "onStyleRow", onStyleRow1M);
        var item = damGrid.getItem(row.index);
        var param = getParameter();

        if (param.dataTp == "60") {
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
        //damGrid.focus.styleRow(row);
        //damGrid.edit.styleRow(row); 
    };
    var createGrid = function () {
        var selVal = getSelectValue("dataTp");
        var struct = damStruct;

        if (selVal == "DAY") struct = damStructDay;
        else if (selVal == "1" || selVal == "30" || selVal == "60") struct = damStructNoEdit;

        if (damGrid) damGrid.destroyRecursive();

        damGrid = new dojox.grid.EnhancedGrid({
            id: "damGrid",
            structure: struct,
            //headerMenu: dijit.byId("gridMenu"),
            //rowsPerPage: 10,
            keepRows: 100,
            //keepSelection: true,
            //fastScroll: false,
            selectable: true,
            canSort: function (colIndex) {
                return false;
            },
            plugins: {
                //indirectSelection: { headerSelector: true, name: "Selection", width: "20px", styles: "text-align: center;" },
                selector: { row: false, col: false, cell: true },
                printer: true
            }
        }, document.createElement("div"));
        damGrid.placeAt(dojo.byId("MainGridPanel"));
        damGrid.startup();

        dojo.connect(damGrid, "_onFetchComplete", completeFetch);
        dojo.connect(damGrid, "onRowDblClick", showHistoryDialog);
        dojo.connect(damGrid, "onApplyCellEdit", function (inValue, inRowIndex, inAttrName) {
            var item = this.getItem(inRowIndex);
            this.store.setValue(item, inAttrName + "_CK", "1"); //수정표시
        });
        dojo.connect(damGrid, "onSelected", function (inRowIndex) {

        });

        dojo.connect(damGrid, "onKeyPress", function (e) {
            //복사 Ctrl+C, 붙이기 Ctrl+V
            if (e.ctrlKey == true) {
                if (e.charOrCode == "C" || e.charOrCode == "c") {
                    //현재 선택된 값을 복사한다.
                    var item = this.getItem(e.rowIndex);
                    var val = this.store.getValue(item, e.cell.field);
                    window.clipboardData.setData("Text", val);
                } else if (e.charOrCode == "V" || e.charOrCode == "v") {
                    var val = window.clipboardData.getData("Text");
                    var pasteItems = [];
                    var gs = this.plugin("selector").getSelected("cell"), item, cell, attr;
                    if (val && val.length > 0 && isNumber(val) && gs.length > 0) {
                        dojo.forEach(gs, function (it) {
                            cell = damGrid.getCell(it.col);
                            attr = cell.field;
                            if (attr &&
                                (attr == "RWL" || attr == "OSPILWL" || attr == "ETCIQTY2"
                                || attr == "EDQTY" || attr == "ETCEDQTY" || attr == "SPDQTY" || attr == "ETCDQTY1"
                                || attr == "ETCDQTY2" || attr == "ETCDQTY3" || attr == "OTLTDQTY"
                                || attr == "ITQTY1" || attr == "ITQTY2" || attr == "ITQTY3"
                                )
                            ) {
                                item = damGrid.getItem(it.row);
                                pasteItems[pasteItems.length] = { item: item, attr: attr, val1: parseFloat(val), val2: "1" };
                            }
                        });
                        var i = 0;
                        var tmpRec, attr, val1, val2;
                        var ro = new RepeatingOperation(function () {
                            tmpRec = pasteItems[i].item;
                            attr = pasteItems[i].attr;
                            val1 = pasteItems[i].val1;
                            val2 = pasteItems[i].val2;
                            damGrid.store.changing(tmpRec);
                            tmpRec[attr] = val1;
                            tmpRec[attr + "_CK"] = val2;
                            if (++i < pasteItems.length) { ro.step(); }
                            else {
                                damGrid.update();
                                if (dijit.byId("ChartPanel").opend == true)
                                    loadChartPanel();
                            }
                        }, 100);
                        ro.step();
                    }
                }
            }
        });
        //initChartPanel();
    }

    var changeDataTp = function () {
        var dataTp = getSelectValue("dataTp");
        //        if (dataTp == "DAY") {
        //            var endDt = endDtCal.getValue();
        //            startDtCal.setValue(new Date(endDt.getFullYear(), endDt.getMonth(), "1"));
        //        } else {
        //            endDtCal.setValue(endDtVal);
        //            endDtCal.onChange();
        //            startDtCal.setValue(startDtVal);
        //        }
        if (dataTp == "DAY") {
            clearChartPanel();
            closeChartPanel();
            dijit.byId('chartPanelButton').set('disabled', true);
        }
        createGrid();
        clearChartPanel();
    }

    var showGraph = function () {
        if (dojo.hasClass("ChartPanel", 'showinfo')) {
            dojo.fx.wipeOut({ node: "ChartPanel", duration: 1 }).play();
            this.attr("label", "그래프보기");
        }
        else {
            dojo.fx.wipeIn({ node: "ChartPanel", duration: 1 }).play();
            this.attr("label", "그래프숨기기");
        }
        dojo.toggleClass("ChartPanel", 'showinfo');
    };

    var linkToCims = function () {
        var damCd = getSelectValue("damCd");
        var selectDt = dojo.date.locale.format(startDtCal.getValue(), datePattern2);

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


    var getDamGridDirtyObjects = function () {
        return dojox.rpc.JsonRest.getDirtyObjects();
    }

    var gridselectstart = function (e) {
        var e = e || window.event,
            s = e.srcElement || e.target;
        if (s && s.id.substr(0, "dijit_form_TextBox".length) == "dijit_form_TextBox") {
            e.stopPropagation();
            return true;
        }
    }

    dojo.ready(function () {
        initChartPanel();

        damTpOptions = GetAllDamTpList(false);
        //damCdOptions = glGetDamCdList("D");

        startDtVal = dojo.date.locale.format(prevDate, datePattern);
        endDtVal = dojo.date.locale.format(currDate, datePattern);
        yesterDtVal = dojo.date.locale.format(prevDate, datePattern);


        dojo.connect(dojo.byId("damTp"), "onchange", changeDamType);
        dojo.connect(dojo.byId("damCd"), "onchange", changeDamCd);
        setOptions("damTp", damTpOptions);
        setSelectOption("damTp", glGetDefaultDamTp());
        changeDamType();
        setSelectOption("damCd", glGetDefaultDamCd());

        //setOptions("damCd", damCdOptions);
        setOptions("startHr", gl_24times);
        setOptions("endHr", gl_24times);
        setOptions("dataTp", dataTpOptions);
        setSelectOption("endHr", "24");

        startDtCal = new dijit.form.DateTextBox({
            id: "startDt",
            name: "startDt",
            value: startDtVal,
            style: "width:90px;",
            onChange: function () {
                var dt = this.getValue();
                endDtCal.constraints.min = new Date(dt.getFullYear(), dt.getMonth(), dt.getDate());
            }
        }, "startDt");

        endDtCal = new dijit.form.DateTextBox({
            id: "endDt",
            name: "endDt",
            value: endDtVal,
            style: "width:90px;",
            onChange: function () {
                var dt = this.getValue();
                startDtCal.constraints.max = new Date(dt.getFullYear(), dt.getMonth(), dt.getDate());
            }
        }, "endDt");


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
            label: "댐운영 전자료를 조회합니다"
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
            label: "전일평균, 지정시간평균, 선형보간산출 등을 적용합니다"
        });
        new dijit.Tooltip({
            connectId: ["cancelButton"],
            position: "above",
            showDelay: 0,
            label: "확정하기 전의 데이터를 복원합니다"
        });
        new dijit.Tooltip({
            connectId: ["chartPanelButton"],
            position: "above",
            showDelay: 0,
            label: "현재 조회한 자료를 바탕으로 그래프를 출력합니다"
        });
        new dijit.Tooltip({
            connectId: ["saveButton"],
            position: "above",
            showDelay: 0,
            label: "보정한 데이터를 확정합니다"
        });

        createGrid();
        dojo.connect(dojo.byId("MainGridPanel"), "onselectstart", gridselectstart);
        dijit.byId('editButton').set('disabled', true);
        dijit.byId('cancelButton').set('disabled', true);
        dijit.byId('saveButton').set('disabled', true);
        dijit.byId('chartPanelButton').set('disabled', true);
        dijit.byId('showAllChartBtn').set('disabled', true);
        hidePreloader();
    });
</script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

<div id="menu-title" class="title_box" style="position:absolute;text-align:left;height:40px;top:10px;left:10px;">
	<div style="font-weight:bold;font-size:12px;margin-left:10px">
        <img src="<%=Page.ResolveUrl("/Images") %>/icons/television.png" align="absmiddle" />&nbsp;&nbsp;
        댐운영자료 검보정
    </div>
</div>
<br />
<p align="right">※ 보정이력란을 더블 클릭하면 보정이력을 볼 수 있습니다.</p>
<div id="mainPanel" data-dojo-type="dijit.layout.BorderContainer" data-dojo-props='style:"position:absolute;top:50px; width: 100%; height: 98%;"'>
	<div id="topPanel" data-dojo-type="dijit.layout.ContentPane" data-dojo-props='id:"topPanel", region:"top", style:"height: 30px;background-color:#D3E3F8;"'>
     <!-- 댐구분, 댐명, 시작날,시작시,끝날,끝시, 구분, 조회, 엑셀 -->
     <label class="wslabel">댐구분 :</label>
     <select id="damTp" class="wsComboBox"></select>&nbsp;

     <label class="wslabel">댐 명 :</label>
     <select id="damCd" class="wsComboBox" style="width:100px;"></select>&nbsp;

     <label class="wslabel">일 시 :</label> 
     <input id="startDt"/>
     <select id="startHr" class="wsComboBox"></select> ~ 
     <input id="endDt" />
     <select id="endHr" class="wsComboBox"></select>&nbsp;

     <label class="wslabel">구 분 :</label> 
     <select id="dataTp" class="wsComboBox" onchange="javascript: changeDataTp()"></select>&nbsp;

     <button id="searchButton" data-dojo-type="dijit.form.Button" data-dojo-props='id:"searchButton",onClick:searchGrid'><img src='<%=Page.ResolveUrl("~/Images") %>/icons/magnifier.png'> 조회</button>
     <button id="excelButton" data-dojo-type="dijit.form.Button" data-dojo-props='id:"excelButton",onClick:downloadExcel'><img src='<%=Page.ResolveUrl("~/Images") %>/icons/document-excel-table.png'> 엑셀</button>
     <button id="editButton" data-dojo-type="dijit.form.Button" data-dojo-props='id:"editButton",onClick:showUpdateAllWin'><img src='<%=Page.ResolveUrl("~/Images") %>/icons/border-color.png'> 보정</button>
     <button id="cancelButton" data-dojo-type="dijit.form.Button" data-dojo-props='id:"cancelButton",onClick:cancelUpdateAll'><img src='<%=Page.ResolveUrl("~/Images") %>/icons/cross-circle.png'> 취소</button>
     <button id="chartPanelButton" data-dojo-type="dijit.form.Button" data-dojo-props='id:"chartPanelButton",onClick:toggleChartPanel'><img src='/Images/icons/chart-up-color.png'> 그래프</button>
     <button id="saveButton" data-dojo-type="dijit.form.Button" data-dojo-props='id:"saveButton",onClick:saveUpdateAll'><img src='<%=Page.ResolveUrl("~/Images") %>/icons/disks.png'> 저장</button>
     <button id="cimsButton" data-dojo-type="dijit.form.Button" data-dojo-props='id:"cimsButton",onClick:linkToCims'><img src='<%=Page.ResolveUrl("~/Images") %>/icons/application-list.png'> 고장보고</button>
     </div>
	<div id="MainGridPanel" data-dojo-type="dijit.layout.ContentPane" data-dojo-props='id:"MainGridPanel", region:"center", style:"margin:0 0 0 0;padding:0 0 0 0;overflow:hidden;"'>
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
<input type="hidden" name="fileName" value="댐운영자료.xls" />
</form>
</asp:Content>
