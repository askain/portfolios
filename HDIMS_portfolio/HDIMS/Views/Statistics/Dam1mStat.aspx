<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub3.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="HDIMS.Utils.Login"%>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	결측률 분석
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
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
</script>

<%
    EmpData empdata = new EmpData();
%>
<script type="text/javascript" src="/Scripts/common/renderers.js"></script>
<script type="text/javascript">

    var currDate = new Date();
    var prevDate = new Date();
    prevDate.setDate(currDate.getDate() - 2);
    var loginEmpNo = "<%=empdata.GetEmpData(0) %>";
    var loginEmpNm = "<%=empdata.GetEmpData(1) %>";
    var searchFlag = 0;
    var firstload = true;

    //전역 변수
    var Date, statGrid, mainStore, rdsGrid, rdsStore, historyGrid, historyStore;
    var damTpCombo, damCdCombo, obsCdCombo, exCdCombo, dispTpCombo, statTpCombo, dataTpCombo;
    var originStoreItems = [], preFrom, preTo, searchFlag = 0;
    var legendNum = 3;
    var DataUrl = "/Statistics/GetDam1mStatList/";
    var ExcelUrl = "/Statistics/GetDamDataStatExcel/?";
    var rfParams = "";
    var datePattern = { datePattern: "yyyy-MM-dd", selector: "date" };
    var datePattern2 = { datePattern: "yyyyMMdd", selector: "date" };
    var datePattern3 = { datePattern: "yyyyMMddHHmm", selector: "date" };
    //전역함수

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

    var getColor = function (value, index) {
        var item = statGrid.getItem(index);
        var retVal = '<div style="background-color:#' + item.EXCOLOR + '">' + value + '</div>';
        return retVal;
    };


    var convPerc = function (value, rowIndex) {
        return value + "%";
    };

    var damStruct = [{
        cells: [
            { field: "MGTCOMMENT", name: "관리단명", width: "100px", hidden: true, styles: 'text-align:center;vertical-align:middle;' },
            { field: "DAMNM", name: "댐명", width: "100px", styles: 'text-align:center;vertical-align:middle;' },
            { field: "OBSNM", name: "관측소명", width: "100px", hidden: true, styles: 'text-align:center;vertical-align:middle;' }
        ],
        noscroll: true,
        width: 'auto'
    }, {
        cells: [],
        width: 'auto'
    }];

    var dispTpOptions = [
        { text: '관리단별', value: 'MGTCD' },
        { text: '댐별', value: '', selected: true }
    ];

    var dispTpOptions2 = [
        { text: '관리단별', value: 'MGTCD' },
        { text: '댐별', value: '', selected: true },
        { text: '관측소별', value: 'OBSCD'}
    ];

    var dataTpOptions = [
        { text: '수신률(1분)', value: '1' },
        { text: '수위결측률(30분)', value: 'WL30', selected: true },
        { text: '우량결측률(30분)', value: 'RF30', selected: true }
    ];

    var statTpOptions = [
        { text: '일별 통계', value: 'D' },
        { text: '월별 통계', value: 'M', selected: true }
    ];

    var getMultiValues = function (id) {
        var val = $("#" + id).multiselect("getChecked").map(function () {
            return this.value;
        }).get();
        return val.toString();
    }
    var getParameter = function () {
        var damTp = getSelectValue("damTp");
        var damCd = getMultiValues("damCd");
        var obsCd = getMultiValues("obsCd");
        var dataTp = getMultiValues("dataTp");
        var startDt = dojo.date.locale.format(startDtCal.getValue(), datePattern2);
        var endDt = dojo.date.locale.format(endDtCal.getValue(), datePattern2);
        var statTp = getSelectValue("statTp");
        var dispTp = getSelectValue("dispTp");

        var params = {
            damtp: damTp,
            damcd: damCd,
            obscd: obsCd,
            sdate: startDt,
            edate: endDt,
            datatp: dataTp,
            stattp: statTp,
            disptp: dispTp
        };

        return params;
    };

    var getSelectValue = function (id) {
        var sel = dojo.byId(id);
        return sel.options[sel.options.selectedIndex].value;
    };

    //↓↓↓↓↓↓↓↓↓ 버튼클릭 명령들 시작 ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
    var searchGrid = function () {
        var params = getParameter();
        if (params) {
            statGrid.setStore(mainStore, params);
            $(document.body).mask("조회중입니다...");
        }
    };
    var downloadExcel = function () {

        if (statGrid.rowCount > 0) {
            statGrid.exportGrid("table", function (html) {
                var form = document.forms["downloadExcelForm"];
                form.content.value = html;
                form.fileName.value = "결측률통계.xls";
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
    var plugins = {
        selector: { row: true, col: false, cell: false },
        printer: true
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
            structure: damStruct,
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

    var malert = function (msg) {
        Message.innerHTML = msg;
        dijit.byId('MessageDialog').show()
    };



    var showReport = function () {
        var url = '/Report/Dam1mReport/?';
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

    var createStruct = function (statTp) {
        var startDt = startDtCal.getValue();
        var endDt = endDtCal.getValue();
        var startFmt = dojo.date.locale.format(startDt, datePattern2);
        var endFmt = dojo.date.locale.format(endDt, datePattern2);
        var sameTp = "D";
        var len = 0, i = 0, cdate, cdfield, cdname;
        damStruct[1].cells = [];
        if (statTp == "D") {
            if (startFmt.substr(0, 4) != endFmt.substr(0, 4)) sameTp = "Y";
            else if (startFmt.substr(0, 6) != endFmt.substr(0, 6)) sameTp = "M";
            else sameTp = "D";

            len = dojo.date.difference(startDt, endDt, "day");
            for (i = 0; i <= len; i++) {
                cdate = dojo.date.add(startDt, "day", i);
                if (sameTp == "D") {
                    cdname = dojo.date.locale.format(cdate, { datePattern: "dd일", selector: "date" });
                    cdfield = dojo.date.locale.format(cdate, { datePattern: "dd", selector: "date" });
                } else if (sameTp == "M") {
                    cdname = dojo.date.locale.format(cdate, { datePattern: "MM월 dd일", selector: "date" });
                    cdfield = dojo.date.locale.format(cdate, { datePattern: "MMdd", selector: "date" });
                } else {
                    cdname = dojo.date.locale.format(cdate, { datePattern: "yyyy년 MM-dd", selector: "date" });
                    cdfield = dojo.date.locale.format(cdate, { datePattern: "yyyyMMdd", selector: "date" });
                }
                damStruct[1].cells[i] = { field: "C_" + cdfield, name: cdname, width: "50px", styles: 'text-align:center;vertical-align:middle;', formatter: glNumberFormat };
            }
        } else {
            if (startFmt.substr(0, 4) != endFmt.substr(0, 4)) sameTp = "Y";
            else sameTp = "M"
            len = dojo.date.difference(startDt, endDt, "month");
            for (i = 0; i <= len; i++) {
                cdate = dojo.date.add(startDt, "month", i);
                if (sameTp == "M") {
                    cdname = dojo.date.locale.format(cdate, { datePattern: "MM월", selector: "date" });
                    cdfield = dojo.date.locale.format(cdate, { datePattern: "MM", selector: "date" });
                } else {
                    cdname = dojo.date.locale.format(cdate, { datePattern: "yyyy년 MM월", selector: "date" });
                    cdfield = dojo.date.locale.format(cdate, { datePattern: "yyyyMM", selector: "date" });
                }
                damStruct[1].cells[i] = { field: "C_" + cdfield, name: cdname, width: "50px", styles: 'text-align:center;vertical-align:middle;', formatter: glNumberFormat };
            }
        }

    };
    var changeStructByDisp = function (dispTp) {
        if (dispTp == "") {  //댐별
            damStruct[0].cells[0].hidden = true;
            damStruct[0].cells[1].hidden = false;
            damStruct[0].cells[2].hidden = true;
        } else if (dispTp == "MGTCD") {
            damStruct[0].cells[0].hidden = false;
            damStruct[0].cells[1].hidden = true;
            damStruct[0].cells[2].hidden = true;
        } else if (dispTp == "OBSCD") {
            damStruct[0].cells[0].hidden = true;
            damStruct[0].cells[1].hidden = false;
            damStruct[0].cells[2].hidden = false;
        }
        createGrid();
    }
    var changeStructByStat = function (tp) {
        var statTp = (tp == null) ? $("#statTp").val() : tp;
        createStruct(statTp);
        createGrid();
    }

    var changeDispTp = function (tp) {
        if (tp == "WL30" || tp == "RF30") {
            setOptions("dispTp", dispTpOptions2);
            $("#obsCd").multiselect("enable");
            
        } else {
            setOptions("dispTp", dispTpOptions);
            $("#obsCd").multiselect("disable");
        }
        $("#dispTp").multiselect("refresh");
       
    }
    //↓↓↓↓↓↓↓↓↓ 초기화 시작 ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
    dojo.addOnLoad(function () {

        setOptions("statTp", statTpOptions);
        setOptions("dataTp", dataTpOptions);
        setOptions("dispTp", dispTpOptions2);

        //댐타입
        damTpOptions = glGetDamTpList(false, "전 체");

        damCdOptions = glGetDamCdList("");
        obsCdOptions = glGetObsCdList("", "");

        setOptions("damTp", damTpOptions);
        setOptions("damCd", damCdOptions);

        setOptions("obsCd", obsCdOptions);

        var startDtVal = dojo.date.locale.format(new Date(currDate.getFullYear(), currDate.getMonth() - 12, "1"), datePattern);
        var endDtVal = dojo.date.locale.format(currDate, datePattern);

        //날짜
        startDtCal = new dijit.form.DateTextBox({
            id: "startDt",
            name: "startDt",
            value: startDtVal,
            style: "width:100px;",
            onChange: function () {
                var dt = this.getValue();
                endDtCal.constraints.min = new Date(dt.getFullYear(), dt.getMonth(), dt.getDate());
                changeStructByStat();
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
                changeStructByStat();
            }
        }, "endDt");

        var damTpCombo = $("#damTp").multiselect({
            header: false,
            multiple: false,
            minWidth: 100,
            height: 120,
            noneSelectedText: "전 체",
            selectedList: 1,
            click: function (e, u) {
                var damTp = u.value;
                var checkTp = (damTp == "") ? "uncheckAll" : "checkAll";
                damCdOptions = glGetDamCdList(u.value);
                setOptions("damCd", damCdOptions);
                $("#damCd").multiselect("refresh");
                $("#damCd").multiselect(checkTp);
                var datatp = $("#dataTp").val();
                if (datatp == "WL30" || datatp == "RF30") {
                    var arr = $.map(damCdOptions, function (item, idx) {
                        return item.value;
                    });
                    var damcd = arr.join(",");
                    datatp = datatp.substring(0, 2);
                    obsCdOptions = glGetObsCdList(datatp, damcd);
                    setOptions("obsCd", obsCdOptions);
                    $("#obsCd").multiselect("refresh");
                    $("#obsCd").multiselect("uncheckAll");
                }
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
                if (datatp == "WL30" || datatp == "RF30") {
                    datatp = datatp.substring(0, 2);
                    obsCdOptions = glGetObsCdList(datatp, damcd);
                    setOptions("obsCd", obsCdOptions);
                    $("#obsCd").multiselect("refresh");
                    $("#obsCd").multiselect("uncheckAll");
                }
            }
        });
        damCdCombo.multiselect("uncheckAll");

        var obsCdCombo = $("#obsCd").multiselect({
            minWidth: 150,
            noneSelectedText: "전 체",
            selectedText: '#개 선택',
            checkAllText: '선택',
            uncheckAllText: '해제'
        });
        obsCdCombo.multiselect("uncheckAll");

        var statTpCombo = $("#statTp").multiselect({
            header: false,
            multiple: false,
            minWidth: 150,
            height: 60,
            noneSelectedText: "전 체",
            selectedList: 1,
            click: function (e, u) {
                changeStructByStat(u.value);
            }
        });
        var dataTpCombo = $("#dataTp").multiselect({
            header: false,
            multiple: false,
            minWidth: 150,
            height: 90,
            selectedList: 1,
            click: function (e, u) {
                var val = u.value;
                changeDispTp(val);
                var dispTp = $("#dispTp").val();
                changeStructByDisp(dispTp);
            }
        });
        dispTpCombo = $("#dispTp").multiselect({
            header: false,
            multiple: false,
            minWidth: 110,
            height: 90,
            //noneSelectedText: "전 체",
            selectedList: 1,
            click: function (e, u) {
                changeStructByDisp(u.value);
            }
        });

        dojo.connect(dojo.byId("searchGridBtn"), "onclick", searchGrid);
        dojo.connect(dojo.byId("downloadExcelBtn"), "onclick", downloadExcel);
        dojo.connect(dojo.byId("showReportBtn"), "onclick", showReport);
        //console.log(document.body.innerHTML);

        createStore();
        changeStructByStat("M");

        adjustMainPanel();

        hidePreloader();
    });

</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="menu-title" class="title_box" style="position:absolute;text-align:left;height:40px;top:10px;left:10px;">
	<span style="font-weight:bold;font-size:12px;margin-left:10px">
    <img src="<%=Page.ResolveUrl("/Images") %>/icons/highlighter.png" align="absmiddle"/>&nbsp;&nbsp;
    결측률 분석
    </span>
</div>
<br />
<p align="right">※ 오늘 데이터는 통계산출시 제외됩니다.&nbsp;&nbsp;</p>
<div id="search-panel" style="position:absolute;top:50px;left:5px;background-color:#D3E3F8;border:1px solid #B5BCC7;width:99%;">
          <table>
        <tr>
            <td> 댐&nbsp;&nbsp;구&nbsp;&nbsp;분 : </td>
            <td><select id="damTp" name="damTp" style="width:110px;text-align:left;"></select></td>
            <td> 댐&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;명 : </td>
            <td><select id="damCd" name="damCd" style="width:110px;text-align:left;"></select></td>
            <td> 관측소명 : </td>
            <td><select id="obsCd" name="obsCd" style="width:110px"></select></td>
            <td> 검색기간 : </td>
            <td style="text-align:left"><input id="startDt" /> ~ <input id="endDt" /></td>
        </tr>
        <tr>
            <td>댐 / 관측소 : </td>
            <td><select id="dispTp" name="dispTp" style="width:110px"></select></td>
            <td> 통계 구분 : </td>
            <td><select id="statTp" name="statTp" style="width:110px"></select></td>
            <td> 자료 구분 : </td>
            <td><select id="dataTp" name="dataTp" style="width:110px"></select></td>
            <td colspan="2" style="text-align:right">
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


