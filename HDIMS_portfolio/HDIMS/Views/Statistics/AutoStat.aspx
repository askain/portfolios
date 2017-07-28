<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub3.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	기간별 신뢰도분석
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript" src="/Common/DamMgtCodeJs"></script>
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
    var damTpCombo, mgtCdCombo, obsCdCombo, exCdCombo, dispTpCombo, statTpCombo;
    var originStoreItems = [], preFrom, preTo, searchFlag = 0;
    var legendNum = 3;
    var DataUrl = "/Statistics/GetAutoStatList/";
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

    var convStatTp = function (value, rowIndex) {
        return value == "M" ? "월별" : "분기별";
    }

    var convObsdt = function (value, rowIndex) {
        var statTp = getSelectValue("statTp");
        if (statTp == "M") {
            return value.substr(0, 4) + "년 " + value.substr(4, 2) + "월";
        } else {
            return value.substr(0, 4) + "년 " + value.substr(4, 1) + "분기";
        }
        return "";
    }

    var convSubject = function (value, rowIndex) {
        var ret = ""
        var statTp = getSelectValue("statTp");
        var item = statGrid.getItem(rowIndex);
        var mgtnm = statGrid.store.getValue(item, "MGTNM");
        var obsdt = statGrid.store.getValue(item, "OBSDT");
        if (statTp == "M") {
            obsdt = obsdt.substr(0, 4) + "년 " + obsdt.substr(4, 2) + "월";
        } else {
            obsdt = obsdt.substr(0, 4) + "년 " + obsdt.substr(4, 1) + "분기";
        }
        ret = mgtnm + " " + obsdt + " " + "수문자료 신뢰도 분석";
        return ret;
    }

    var autoReportStruct = [{
        cells: [
            { field: "MGTNM", name: "관리단명", width: "200px", styles: 'text-align:center;vertical-align:middle;cursor:hand;' },
            { field: "STATTP", name: "구분", width: "100px", styles: 'text-align:center;vertical-align:middle;cursor:hand;', formatter: convStatTp },
            { field: "OBSDT", name: "기간", width: "150px", styles: 'text-align:center;vertical-align:middle;cursor:hand;', formatter: convObsdt },
            { field: "SUBJECT", name: "제 목", width: "600px", styles: 'padding-left:10px;text-align:left;vertical-align:middle;cursor:hand;', formatter: convSubject }
        ]
    }];

    var statTpOptions = [
        { text: '월  별', value: 'M', selected: true },
        { text: '분기별', value: 'Q' }
    ];

    var getMultiValues = function (id) {
        var val = $("#" + id).multiselect("getChecked").map(function () {
            return this.value;
        }).get();
        return val.toString();
    };
    var getParameter = function () {
        var damTp = getMultiValues("damTp");
        var mgtCd = getMultiValues("mgtCd");
        var startDt = dojo.date.locale.format(startDtCal.getValue(), datePattern2);
        var endDt = dojo.date.locale.format(endDtCal.getValue(), datePattern2);
        var statTp = getSelectValue("statTp");

        var params = {
            damtp: damTp,
            mgtcd: mgtCd,
            sdate: startDt,
            edate: endDt,
            stattp: statTp
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
    var gridDblClick = function () {
        var item = statGrid.getItem(statGrid.selection.selectedIndex);
        var damTp = statGrid.store.getValue(item, "DAMTYPE");
        if (damTp == null) { damTp = ""; }
        var mgtCd = statGrid.store.getValue(item, "MGTCD");
        var obsDt = statGrid.store.getValue(item, "OBSDT");
        var statTp = getSelectValue("statTp");

        var url = '/Report/AutoReport/?';
        var param = "damtp=" + damTp + "&mgtcd=" + mgtCd + "&obsdt=" + obsDt + "&stattp=" + statTp;
        jQuery.popupWindow2({
            windowName: "AutoReport",
            width: 1024,
            height: 700,
            windowURL: url + param,
            centerScreen: 1,
            resizable: 1,
            scrollbars: 2
        });
    };

    var createStore = function () {
        mainStore = new dojox.data.JsonRestStore({
            target: DataUrl
            , timeout: 3600000
            , idAttribute: "ID"
        });
    };

    var createGrid = function () {
        if (statGrid) {
            statGrid.destroyRecursive();
        }

        statGrid = new dojox.grid.EnhancedGrid({
            id: "statGrid",
            structure: autoReportStruct,
            plugins: plugins,
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
        dojo.connect(statGrid, "onRowDblClick", gridDblClick);
        dojo.connect(statGrid, "_onFetchComplete", completeFetch);
    };

    var showReport = function () {

        var url = '/Report/AutoReport/?';
        var param = dojo.objectToQuery(getParameter());
        jQuery.popupWindow2({
            windowName: "AutoReport",
            width: 1024,
            height: 700,
            windowURL: url + param,
            centerScreen: 1,
            resizable: 1,
            scrollbars: 2
        });
    };

    var changeStructByStat = function (statTp) {
        if (statTp == "I") {
            autoReportStruct[0].cells[2].hidden = true;
        } else {
            autoReportStruct[0].cells[2].hidden = false;
        }
        createGrid();
    };

    //↓↓↓↓↓↓↓↓↓ 초기화 시작 ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
    dojo.addOnLoad(function () {
        exEmpNoOptions = gl_empno;
        setOptions("statTp", statTpOptions);

        //댐타입
        damTpOptions = glGetDamTpList(false, undefined);
        mgtCdOptions = glGetMgtCdList("");
        setOptions("damTp", damTpOptions);
        setOptions("mgtCd", mgtCdOptions);

        var startDtVal = dojo.date.locale.format(new Date(currDate.getFullYear(), currDate.getMonth() - 3, "1"), datePattern);
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
        createGrid();

        var damTpCombo = $("#damTp").multiselect({
            //header: false,
            //multiple: false,
            //selectedList: 1,
            minWidth: 150,
            height: 120,
            noneSelectedText: "전 체",
            selectedText: '#개 선택',
            checkAllText: '선택',
            uncheckAllText: '해제',
            click: function (e, u) {
                mgtCdOptions = glGetMgtCdList(u.value);
                setOptions("mgtCd", mgtCdOptions);
                $("#mgtCd").multiselect("refresh");
                $("#mgtCd").multiselect("checkAll");
                createGrid();
            }
        });
        damTpCombo.multiselect("uncheckAll");

        var mgtCdCombo = $("#mgtCd").multiselect({
            minWidth: 150,
            noneSelectedText: "전 체",
            selectedText: '#개 선택',
            checkAllText: '선택',
            uncheckAllText: '해제',
            click: function (e, u) {
                var mgtCd = getMultiValues("mgtCd");
                createGrid();
            }
        });
        mgtCdCombo.multiselect("uncheckAll");

        var statTpCombo = $("#statTp").multiselect({
            header: false,
            multiple: false,
            minWidth: 120,
            height: 60,
            noneSelectedText: "전 체",
            selectedList: 1,
            click: function (e, u) {
                createGrid();
            }
        });

        hidePreloader();
        dojo.connect(dojo.byId("searchGridBtn"), "onclick", searchGrid);
        dojo.connect(dojo.byId("downloadExcelBtn"), "onclick", downloadExcel);
        dojo.connect(dojo.byId("showReportBtn"), "onclick", showReport);

        adjustMainPanel();
    });

</script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div id="menu-title" class="title_box" style="position:absolute;text-align:left;height:40px;top:10px;left:10px;">
	<span style="font-weight:bold; margin-left:10px;">
    <img src="<%=Page.ResolveUrl("/Images") %>/icons/wrench--pencil.png" align="absmiddle"/>&nbsp;&nbsp;
    기간별 통계분석
    </span>
</div>
<br />
<p align="right">※ 오늘 데이터는 통계산출시 제외됩니다.&nbsp;&nbsp;</p>
<div id="search-panel" style="position:absolute;top:50px;left:5px;background-color:#D3E3F8;border:1px solid #B5BCC7;width:99%;">
          <table>
        <tr>
            <td >댐구분 : </td>
            <td><select id="damTp" name="damTp" style="width:110px;text-align:left;"></select></td>
            <td>댐관리단 : </td>
            <td><select id="mgtCd" name="mgtCd" style="width:110px;text-align:left;"></select></td>
            <td>검색기간 : </td>
            <td style="text-align:left"><input id="startDt" />~<input id="endDt" /></td>
            <td>통계구분 : </td>
            <td><select id="statTp" name="statTp" style="width:110px"></select></td>
            <td>
                <button id="searchGridBtn" data-dojo-type="dijit.form.Button"><img src='<%=Page.ResolveUrl("~/Images") %>/icons/magnifier.png'> 조 회</button>
                <%--<button id="downloadExcelBtn" data-dojo-type="dijit.form.Button"><img src='<%=Page.ResolveUrl("~/Images") %>/icons/document-excel-table.png'> 엑 셀</button>
                <button id="showReportBtn" data-dojo-type="dijit.form.Button"><img src='<%=Page.ResolveUrl("~/Images") %>/icons/border-color.png'> 보고서</button>--%>
            </td>
        </tr>
     </table>
</div>
<div id="MainPanel" data-dojo-type="dijit.layout.BorderContainer" data-dojo-props='style:"position:absolute;top:85px; width: 100%;height:85%;"'>
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
