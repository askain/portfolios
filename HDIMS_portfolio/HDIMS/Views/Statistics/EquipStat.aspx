<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub3.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	설비상태 통계
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">
    dojo.require("dijit.dijit"); // optimize: load dijit layer
    dojo.require("dijit.layout.BorderContainer"); // optimize: load dijit layer
    dojo.require("dijit.layout.TabContainer");
    dojo.require("dijit.Dialog");

    dojo.require("dojox.grid.EnhancedGrid");
    dojo.require("dojox.data.AndOrWriteStore");
    dojo.require("dojo.data.ItemFileReadStore");
    dojo.require("dojox.data.JsonRestStore");
    dojo.require("dijit.form.TextBox");
    dojo.require("dijit.form.NumberTextBox");
    dojo.require("dojox.dtl.filter.strings");
    dojo.require("dojox.grid.enhanced.plugins.Filter");
    dojo.require("dojox.grid.enhanced.plugins.exporter.CSVWriter");
    dojo.require("dojox.grid.enhanced.plugins.Printer");
    dojo.require("dojox.grid.enhanced.plugins.IndirectSelection");
    dojo.require("dojox.grid.enhanced.plugins.Selector");
    dojo.require('doh.runner');
    var plugins = {
        selector: { row: true, col: false, cell: false },
        printer: true
    };

    var currDate = new Date();

    //전역 변수
    var Date, statGrid, mainStore, rdsGrid, rdsStore, historyGrid, historyStore;
    var damTpCombo, damCdCombo, obsCdCombo, exCdCombo, dispTpCombo, statTpCombo, dataTpCombo;
    var originStoreItems = [], preFrom, preTo, searchFlag = 0;
    var legendNum = 3;
    var DataUrl = "/Statistics/GetEquipStatList/";
    //var ExcelUrl = "/Statistics/GetRainFallStatExcel/?";
    var rfParams = "";
    var datePattern = { datePattern: "yyyy-MM-dd", selector: "date" };
    var datePattern2 = { datePattern: "yyyyMMdd", selector: "date" };
    var datePattern3 = { datePattern: "yyyyMMddHHmm", selector: "date" };

    var eventCnt = 0;
    function adjustMainPanel() {
        var h = 0;
        if ($.browser.msie) {//hacked together for IE browsers
            h = document.body.offsetHeight;
        } else {
            h = window.outerHeight;
        }
        h = h - 105;
        $("#MainPanel").css({ height: h });
    }

    var waterLevelStruct = [{
        cells: [
            { field: "DAMNM", name: "댐명", width: "100px", styles: 'text-align:center;vertical-align:middle;' },
            { field: "OBSNM", name: "관측소명", width: "100px", hidden: true, styles: 'text-align:center;vertical-align:middle;' },
            { field: "MGTCOMMENT", name: "관리단명", width: "100px", hidden: true, styles: 'text-align:center;vertical-align:middle;' },
            { field: "OBSDT", name: "기간", width: "100px", styles: 'text-align:center;vertical-align:middle;', formatter: convStrToDateMin }
        ],
        noscroll: true,
        width: 'auto'
    }, {
        cells: [
            { field: "BATTSTS", name: "베터리<br/>상태", width: "60px", styles: 'text-align:center;vertical-align:middle;', formatter: glNumberFormat },
            { field: "BATTVOLT", name: "베터리<br/>전압", width: "60px", styles: 'text-align:center;vertical-align:middle;', formatter: glNumberFormat },
            { field: "DOORSTS", name: "DOOR<br/>상태", width: "60px", styles: 'text-align:center;vertical-align:middle;', formatter: glNumberFormat },
            { field: "WL_SEN", name: "수위<br/>센서", width: "70px", styles: 'text-align:center;vertical-align:middle;', formatter: glNumberFormat },
            { field: "RF_SEN", name: "우량<br/>센서", width: "70px", styles: 'text-align:center;vertical-align:middle;', formatter: glNumberFormat },
            { field: "WQ_SEN", name: "수질<br/>센서", width: "70px", styles: 'text-align:center;vertical-align:middle;', formatter: glNumberFormat },
            { field: "ETC_SEN", name: "기타<br/>센서", width: "70px", styles: 'text-align:center;vertical-align:middle;', formatter: glNumberFormat },
            { field: "PWRSTS", name: "배터리<br/>저전압", width: "70px", styles: 'text-align:center;vertical-align:middle;', formatter: glNumberFormat },
            { field: "RTU_MEMORY", name: "RTU<br/>메모리", width: "70px", styles: 'text-align:center;vertical-align:middle;', formatter: glNumberFormat },
            { field: "RTU_RESET", name: "RTU<br/>리셋", width: "70px", styles: 'text-align:center;vertical-align:middle;', formatter: glNumberFormat },
        //{ field: "WDT_RESET", name: "자체점검<br/>RTU 리셋", width: "70px", styles: 'text-align:center;vertical-align:middle;', formatter: glNumberFormat },
            {field: "LAN_PORT", name: "LAN<br>포트", width: "60px", styles: 'text-align:center;vertical-align:middle;', formatter: glNumberFormat },
            { field: "CDMA_MODEM", name: "CDMA<br/>모뎀", width: "60px", styles: 'text-align:center;vertical-align:middle;', formatter: glNumberFormat },
            { field: "VSAT_PORT", name: "VSAT<br>포트", width: "60px", styles: 'text-align:center;vertical-align:middle;', formatter: glNumberFormat },
            { field: "CDMA_PORT", name: "CDMA<br/>포트", width: "60px", styles: 'text-align:center;vertical-align:middle;', formatter: glNumberFormat },
            { field: "WIRE_PORT", name: "유선<br/>포트", width: "60px", styles: 'text-align:center;vertical-align:middle;', formatter: glNumberFormat },
            { field: "MULTICAST_SOCKET", name: "멀티<br>케스트", width: "60px", styles: 'text-align:center;vertical-align:middle;', formatter: glNumberFormat },
            { field: "VSAT_EVENT", name: "VSAT<br>이벤트", width: "60px", styles: 'text-align:center;vertical-align:middle;', formatter: glNumberFormat },
            { field: "CDMA_EVENT", name: "CDMA<br/>이벤트", width: "60px", styles: 'text-align:center;vertical-align:middle;', formatter: glNumberFormat },
            { field: "WIRE_EVENT", name: "유선<br/>이벤트", width: "60px", styles: 'text-align:center;vertical-align:middle;', formatter: glNumberFormat },
            { field: "UDP_EVENT", name: "UDP<br/>이벤트", width: "60px", styles: 'text-align:center;vertical-align:middle;', formatter: glNumberFormat },
            { field: "IDU_PING", name: "IDU<br/>Ping", width: "60px", styles: 'text-align:center;vertical-align:middle;', formatter: glNumberFormat },
            { field: "SNR", name: "SNR", width: "60px", styles: 'text-align:center;vertical-align:middle;', formatter: glNumberFormat },
            { field: "PRIMARY_DATA", name: "주망<br>상태", width: "60px", styles: 'text-align:center;vertical-align:middle;', formatter: glNumberFormat },
            { field: "SECONDARY_CALL", name: "보조망<br>호출", width: "70px", styles: 'text-align:center;vertical-align:middle;', formatter: glNumberFormat },
            { field: "DATA_STATUS", name: "데이터<br>결측", width: "60px", styles: 'text-align:center;vertical-align:middle;', formatter: glNumberFormat },
            
        ],
        width: 'auto'
    }];

    var dispTpOptions = [
        { text: '관리단별', value: 'MGTCD' },
        { text: '댐별', value: '', selected: true },
        { text: '관측소별', value: 'OBSCD' }
    ];
    var statTpOptions = [
        { text: '일별 통계', value: 'D' },
        { text: '월별 통계', value: 'M', selected: true },
        { text: '항목별통계', value: 'I' }
    ];
    var dataTpOptions = [
        { text: '수위 자료', value: 'WL', selected: true },
        { text: '우량 자료', value: 'RF' },
        { text: '수위+우량', value: 'WR' }
    ];
    var getMultiValues = function (id) {
        var val = $("#" + id).multiselect("getChecked").map(function () {
            return this.value;
        }).get();
        return val.toString();
    };
    var getParameter = function () {
        var damTp = getSelectValue("damTp");
        var damCd = getMultiValues("damCd");
        var obsCd = getMultiValues("obsCd");
        var startDt = dojo.date.locale.format(startDtCal.getValue(), datePattern2);
        var endDt = dojo.date.locale.format(endDtCal.getValue(), datePattern2);
        var dataTp = getMultiValues("dataTp");
        var dispTp = getSelectValue("dispTp");
        var statTp = getSelectValue("statTp");
        var exCd = getMultiValues("exCd");
        //        var param = "damType=" + damTp + "&damCd=" + damCd + "&startDt=" + startDt + "&endDt=" + endDt + "&dataTp=" + dataTp;
        //        alert(param);
        //        return;
        var params = {
            damtp: damTp,
            damcd: damCd,
            obscd: obsCd,
            sdate: startDt,
            edate: endDt,
            datatp: dataTp,
            disptp: dispTp,
            stattp: statTp,
            excd: exCd,
            etccd: ''
        };
        //dtype, stype, displayType
        return params;
    };
    var getSelectValue = function (id) {
        var sel = dojo.byId(id);
        return sel.options[sel.options.selectedIndex].value;
    };

    //↓↓↓↓↓↓↓↓↓ 버튼클릭 명령들 시작 ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
    var searchGrid = function () {
        rfParams = getParameter();
        if (rfParams) {
            statGrid.setStore(mainStore, rfParams);

            $(document.body).mask("조회중입니다...");
        }
    };
    var downloadExcel = function () {

        if (statGrid.rowCount > 0) {
            statGrid.exportGrid("table", function (html) {
                var form = document.forms["downloadExcelForm"];
                form.content.value = html;
                document.forms["downloadExcelForm"].submit();
            });
        } else {
            alert("저장할 데이터가 존재하지 않습니다.");
        }
    };
    //↑↑↑↑↑↑↑↑↑ 버튼클릭 명령들 끝   ↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

    //↓↓↓↓↓↓↓↓↓ 콤보박스 변경 이벤트 시작 ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓


    //↑↑↑↑↑↑↑↑↑ 콤보박스 변경 이벤트 끝   ↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

    //↓↓↓↓↓↓↓↓↓ 그리드 이벤트 시작 ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
    var completeFetch = function (items, request) {
        $(document.body).unmask();
    };

    //↑↑↑↑↑↑↑↑↑ 그리드 이벤트 끝   ↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

    var createStore = function () {
        mainStore = new dojox.data.JsonRestStore({
            target: DataUrl
            , timeout: 3600000
            , idAttribute: "ID"
        });
    };

    var getStructure = function (dataTp) {

//        if (dataTp == "WL") {
//            return waterLevelStruct;
//        } else if (dataTp == "RF") {
//            return rainfallStruct;
//        } else {
//            return allStruct;
//        }
        return waterLevelStruct;
    };

    var createGrid = function (dataTp) {
        if (statGrid) {
            statGrid.destroyRecursive();
        }

        statGrid = new dojox.grid.EnhancedGrid({
            id: "statGrid",
            structure: getStructure(dataTp),
            plugins: plugins,
            rowsPerPage: 40,
            noDataMessage: "데이터가 존재하지 않습니다.",
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
                //1.보이지 상단의 컬럼(원시, 추정)의 셀은 보이지 않게 함.
                if (e.node.children[0].children[0].rows.length == 2) {
                    dojo.style(e.node.children[0].children[0].rows[0], 'display', 'none');
                }
            }
        }, document.createElement("div"));
        //dojo.byId("MainGridPanel").appendChild(statGrid.domNode);
        statGrid.placeAt(dojo.byId("MainGridPanel"));
        statGrid.startup();
        dojo.connect(statGrid, "_onFetchComplete", completeFetch);
    };

    var showReport = function () {
        var dataTp = $("#dataTp").val();

        var url = '/Report/EquipReport/?';
        var param = dojo.objectToQuery(getParameter());
        jQuery.popupWindow2({
            windowName: "GoogleEarth",
            width: 1024,
            height: 700,
            windowURL: url + param,
            centerScreen: 1,
            resizable: 1,
            scrollbars: 2
        });
    };

    var changeStructByExcd = function (u) {
        try {
            if (typeof (u) == "string") {
                var setHidden = u == "all" ? false : true;
                for (var i = 0; i < waterLevelStruct[1].cells.length; i++) {
                    waterLevelStruct[1].cells[i].hidden = setHidden;
                }
            } else {
                for (var i = 0; i < waterLevelStruct[1].cells.length; i++) {
                    if (waterLevelStruct[1].cells[i].field == u.value) {
                        waterLevelStruct[1].cells[i].hidden = !u.checked;
                        break;
                    }
                }
            }
        } catch (e) { }
        var dataTp = $("#dataTp").val();
        createGrid(dataTp);
    };
    var changeStructByDisp = function (dispTp) {
        if (dispTp == "OBSCD") {
            waterLevelStruct[0].cells[0].hidden = false;
            waterLevelStruct[0].cells[1].hidden = false;
            waterLevelStruct[0].cells[2].hidden = true;
        } else if (dispTp == "MGTCD") {
            waterLevelStruct[0].cells[0].hidden = true;
            waterLevelStruct[0].cells[1].hidden = true;
            waterLevelStruct[0].cells[2].hidden = false;
        } else {
            waterLevelStruct[0].cells[0].hidden = false;
            waterLevelStruct[0].cells[1].hidden = true;
            waterLevelStruct[0].cells[2].hidden = true;
        }
        var dataTp = $("#dataTp").val();
        createGrid(dataTp);
    };
    var changeStructByStat = function (statTp) {
        if (statTp == "I") {
            waterLevelStruct[0].cells[3].hidden = true;
        } else {
            waterLevelStruct[0].cells[3].hidden = false;
        }
        var dataTp = $("#dataTp").val();
        createGrid(dataTp);
    };

    //↓↓↓↓↓↓↓↓↓ 초기화 시작 ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
    dojo.addOnLoad(function () {
        //기타
        //edExLvlOptions = gl_edexlvl;
        exCdOptions = gl_equipExCode;
        exEmpNoOptions = gl_empno;
        //setOptions("edExLvl", edExLvlOptions);
        setOptions("exCd", exCdOptions);
        setOptions("dataTp", dataTpOptions);
        setOptions("statTp", statTpOptions);
        setOptions("dispTp", dispTpOptions);

        //댐타입
        damTpOptions = glGetDamTpList(true, "전 체");
        damCdOptions = glGetDamCdList("");
        obsCdOptions = glGetObsCdList("", "");
        setOptions("damTp", damTpOptions);
        setOptions("damCd", damCdOptions);
        setOptions("obsCd", obsCdOptions);
        //changeDamType();
        //changeDamCd();

        //dojo.connect(dojo.byId("damTp"), "onchange", changeDamType);
        //dojo.connect(dojo.byId("damCd"), "onchange", changeDamCd);
        //dojo.connect(dojo.byId("dataTp"), "onchange", changeDataTp);

        var startDtVal = dojo.date.locale.format(new Date(currDate.getFullYear(), currDate.getMonth()-12, "1"), datePattern);
        var endDtVal = dojo.date.locale.format(currDate, datePattern);

        //날짜
        startDtCal = new dijit.form.DateTextBox({
            id: "startDt",
            name: "startDt",
            value: startDtVal,
            style: "width:120px;",
            onChange: function () {
                var dt = this.getValue();
                endDtCal.constraints.min = new Date(dt.getFullYear(), dt.getMonth(), dt.getDate());
            }
        }, "startDt");
        endDtCal = new dijit.form.DateTextBox({
            id: "endDt",
            name: "endDt",
            value: endDtVal,
            style: "width:120px;",
            onChange: function () {
                var dt = this.getValue();
                startDtCal.constraints.max = new Date(dt.getFullYear(), dt.getMonth(), dt.getDate());
            }
        }, "endDt");

        createStore();
        createGrid("WL");

        var damTpCombo = $("#damTp").multiselect({
            header: false,
            multiple: false,
            minWidth: 150,
            height: 90,
            noneSelectedText: "전 체",
            selectedList: 1,
            click: function (e, u) {
                damCdOptions = glGetDamCdList(u.value);
                setOptions("damCd", damCdOptions);
                $("#damCd").multiselect("refresh");
                $("#damCd").multiselect("uncheckAll");
                var arr = $.map(damCdOptions, function (item, idx) {
                    return item.value;
                });
                var damcd = arr.join(",");
                var datatp = $("#dataTp").val();
                obsCdOptions = glGetObsCdList(datatp, damcd);
                setOptions("obsCd", obsCdOptions);
                $("#obsCd").multiselect("refresh");
                $("#obsCd").multiselect("uncheckAll");
            }
        });
        var damCdCombo = $("#damCd").multiselect({
            minWidth: 150,
            noneSelectedText: "전 체",
            selectedText: '#개 선택',
            checkAllText: '선택',
            uncheckAllText: '해제',
            click: function (e, u) {
                var damcd = getMultiValues("damCd");
                var datatp = $("#dataTp").val();
                obsCdOptions = glGetObsCdList(datatp, damcd);
                setOptions("obsCd", obsCdOptions);
                $("#obsCd").multiselect("refresh");
                $("#obsCd").multiselect("uncheckAll");
            }
        });
        damCdCombo.multiselect("uncheckAll");
        var obsCdCombo = $("#obsCd").multiselect({
            minWidth: 150,
            noneSelectedText: "전 체",
            checkAllText: '선택',
            uncheckAllText: '해제',
            selectedText: '#개 선택'
        });
        obsCdCombo.multiselect("uncheckAll");
        var exCdCombo = $("#exCd").multiselect({
            multiple: true,
            minWidth: 150,
            checkAllText: '선택',
            uncheckAllText: '해제',
            noneSelectedText: "선택사항없음",
            selectedText: '#개 선택',
            click: function (e, u) {
                changeStructByExcd(u);
            },
            checkAll: function () {
                changeStructByExcd("all");
            },
            uncheckAll: function () {
                changeStructByExcd("none");
            }
        });
        exCdCombo.multiselect("checkAll");
        var dispTpCombo = $("#dispTp").multiselect({
            header: false,
            multiple: false,
            minWidth: 150,
            height: 95,
            noneSelectedText: "전 체",
            selectedList: 1,
            click: function (e, u) {
                changeStructByDisp(u.value);
            }
        });
        var statTpCombo = $("#statTp").multiselect({
            header: false,
            multiple: false,
            minWidth: 150,
            height: 100,
            noneSelectedText: "전 체",
            selectedList: 1,
            click: function (e, u) {
                changeStructByStat(u.value);
            }
        });
        var dataTpCombo = $("#dataTp").multiselect({
            header: false,
            multiple: false,
            minWidth: 110,
            height: 90,
            selectedList: 1
        });
        hidePreloader();
        dojo.connect(dojo.byId("searchGridBtn"), "onclick", searchGrid);
        dojo.connect(dojo.byId("downloadExcelBtn"), "onclick", downloadExcel);
        dojo.connect(dojo.byId("showReportBtn"), "onclick", showReport);
        //console.log(document.body.innerHTML);
        adjustMainPanel();
    });

</script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div id="menu-title" class="title_box" style="position:absolute;text-align:left;height:40px;top:10px;left:10px;">
	<span style="font-weight:bold; margin-left:10px;">
    <img src="<%=Page.ResolveUrl("/Images") %>/icons/wrench--pencil.png" align="absmiddle"/>&nbsp;&nbsp;
    설비상태 분석
    </span>
</div>
<br />
<p align="right">※ 오늘 데이터는 통계산출시 제외됩니다.&nbsp;&nbsp;</p>
<div id="search-panel" style="position:absolute;top:50px;left:5px;background-color:#D3E3F8;border:1px solid #B5BCC7;width:99%;">
          <table>
        <tr>
            <td >댐&nbsp;&nbsp;구&nbsp;&nbsp;분 : </td>
            <td><select id="damTp" name="damTp" style="width:110px;text-align:left;"></select></td>
            <td>댐&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;명 : </td>
            <td><select id="damCd" name="damCd" style="width:110px;text-align:left;"></select></td>
            <td>관&nbsp;&nbsp;측&nbsp;&nbsp;국 : </td>
            <td><select id="obsCd" name="obsCd" style="width:110px;text-align:left;"></select></td>
            <td>검색기간 : </td>
            <td style="text-align:left"><input id="startDt" />~<input id="endDt" /></td>
        </tr>
     
        <tr>
            
            <%--<td>보정등급 : </td>
            <td><select id="edExLvl" class="wsComboBox" style="width:110px"></select></td>--%>
            <td>항목선택 : </td>
            <td><select id="exCd" name="exCd" style="width:110px"></select></td>
            <td>댐/관측소 : </td>
            <td><select id="dispTp" name="dispTp" style="width:110px"></select></td>
            <td>통계구분 : </td>
            <td><select id="statTp" name="statTp" style="width:110px"></select></td>
            <td>자료구분 : </td>
            <td style="text-align:left;"><select id="dataTp" name="dataTp" style="width:110px"></select></td>
            <td>
                <button id="searchGridBtn" data-dojo-type="dijit.form.Button"><img src='<%=Page.ResolveUrl("~/Images") %>/icons/magnifier.png'> 조 회</button>
                <button id="downloadExcelBtn" data-dojo-type="dijit.form.Button"><img src='<%=Page.ResolveUrl("~/Images") %>/icons/document-excel-table.png'> 엑 셀</button>
                <button id="showReportBtn" data-dojo-type="dijit.form.Button"><img src='<%=Page.ResolveUrl("~/Images") %>/icons/border-color.png'> 보고서</button>
            </td>
        </tr>
     </table>
</div>
<div id="MainPanel" data-dojo-type="dijit.layout.BorderContainer" data-dojo-props='style:"position:absolute;top:110px; width: 100%;height:85%;"'>
    <div id="MainGridPanel" data-dojo-type="dijit.layout.ContentPane" data-dojo-props='id:"MainGridPanel", region:"center", style:"margin:0 0 0 0;padding:0 0 0 0;overflow:hidden;"'>
	</div>
</div>
<form name="downloadExcelForm" action="/Common/DownloadExcel" method="post">
<input type="hidden" name="content" value="" />
<input type="hidden" name="fileName" value="" />
</form>
<script type="text/javascript">

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
