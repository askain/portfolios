<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub3.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="HDIMS.Utils.Login"%>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	알람현황 통계분석
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
<script type="text/javascript" src="/Common/AlarmCodeJs"></script>
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
    var damTpCombo, damCdCombo, obsCdCombo, alarmCdCombo, dispTpCombo, statTpCombo, dataTpCombo;
    var originStoreItems = [], preFrom, preTo, searchFlag = 0;
    var legendNum = 3;
    var DataUrl = "/Statistics/GetAlarmStatList/";
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

    var alarmStruct = [{
        cells: [
            { field: "DAMNM", name: "댐명", width: "100px", styles: 'text-align:center;vertical-align:middle;' },
            { field: "MGTCOMMENT", name: "관리단명", width: "100px", hidden: true, styles: 'text-align:center;vertical-align:middle;' },
            { field: "OBSDT", name: "기간", width: "70px", styles: 'text-align:center;vertical-align:middle;', formatter: convStrToDateMin }
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
    var statTpOptions = [
        { text: '일별 통계', value: 'D' },
        { text: '월별 통계', value: 'M', selected: true },
        { text: '항목별통계', value: 'I' }
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
        var startDt = dojo.date.locale.format(startDtCal.getValue(), datePattern2);
        var endDt = dojo.date.locale.format(endDtCal.getValue(), datePattern2);
        var statTp = getSelectValue("statTp");
        var alarmCd = getMultiValues("alarmCd");
        var dispTp = getSelectValue("dispTp");

        var params = {
            damtp: damTp,
            damcd: damCd,
            sdate: startDt,
            edate: endDt,
            stattp: statTp,
            alarmcd: alarmCd,
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
                form.fileName.value = "알람현황통계.xls";
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
        var struct = makeGridColumns();

        statGrid = new dojox.grid.EnhancedGrid({
            id: "statGrid",
            structure: struct,
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

    var makeGridColumns = function () {
        var len = 0, ad, cd, id, idnm, addtime, timelen, columns = [];
        var alarmCdList = getMultiValues("alarmCd");
        for (var i = 0, j = 0; i < gl_alarmcodes.length; i++) {
            cd = gl_alarmcodes[i].ALARMNM;
            id = gl_alarmcodes[i].ALARMCD;
            if (parseInt(id) < 0) idnm = "M" + Math.abs(parseInt(id)); //oracle에서 -허용하지 않음.
            else idnm = id;
            if (alarmCdList != null && alarmCdList != "") {
                if (alarmCdList.lastIndexOf(id) != -1) {
                    columns[j] = { name: cd, field: "V_" + idnm, width: '60px', styles: 'text-align:center;vertical-align:middle;' };
                    j++;
                }
            } else {
                columns[j] = { name: cd, field: "V_" + idnm, width: '60px', styles: 'text-align:center;vertical-align:middle;' };
                j++;
            }
        }
        var tmpSt = alarmStruct;
        tmpSt[1].cells = columns;
        return tmpSt;
    };

    var showReport = function () {
        var dataTp = $("#dataTp").val();

        var url = '/Report/AlarmReport/?';
        var param = dojo.objectToQuery(getParameter());
        jQuery.popupWindow2({
            windowName: "AlarmReport",
            width: 1024,
            height: 700,
            windowURL: url + param,
            centerScreen: 1,
            resizable: 1,
            scrollbars: 2
        });
    };
    var changeStructByalarmCd = function (u) {
        var targetField = "";
        if (typeof (u) == "string") {
            var setHidden = u == "all" ? false : true;
            for (var i = 0; i < alarmStruct[1].cells.length; i++) {
                if (i < 13) {
                    alarmStruct[1].cells[i].hidden = setHidden;
                } else {
                    break;
                }
            }
        } else {
            targetField += u.value
            for (var i = 0; i < alarmStruct[1].cells.length; i++) {
                console.log(i + ":" + alarmStruct[1].cells[i].field + "," + targetField + "," + u.checked);
                if (alarmStruct[1].cells[i].field == targetField) {
                    alarmStruct[1].cells[i].hidden = !u.checked;
                }
            }
        }

        createGrid();
    };
    var changeStructByDisp = function (dispTp) {
        if (dispTp == "") {  //댐별
            alarmStruct[0].cells[0].hidden = false;
            alarmStruct[0].cells[1].hidden = true;
        } else if (dispTp == "MGTCD") {
            alarmStruct[0].cells[0].hidden = true;
            alarmStruct[0].cells[1].hidden = false;
        }
        createGrid();
    }
    var changeStructByStat = function (statTp) {
        if (statTp == "I") {
            alarmStruct[0].cells[2].hidden = true;
        } else {
            alarmStruct[0].cells[2].hidden = false;
        }
        createGrid();
    }

    var getAlarmCodes = function () {
        var list = [], len = gl_alarmcodes.length;
        for (var i = 0; i < len; i++) {
            list[i] = { text: gl_alarmcodes[i].ALARMNM, value: gl_alarmcodes[i].ALARMCD };
        }
        return list;
    }

    //↓↓↓↓↓↓↓↓↓ 초기화 시작 ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
    dojo.addOnLoad(function () {
        //기타
        setOptions("alarmCd", getAlarmCodes());
        setOptions("statTp", statTpOptions);
        setOptions("dispTp", dispTpOptions);

        //댐타입
        damTpOptions = glGetDamTpList(false, "전 체");
        damCdOptions = glGetDamCdList("");
        setOptions("damTp", damTpOptions);
        setOptions("damCd", damCdOptions);

        var startDtVal = dojo.date.locale.format(new Date(currDate.getFullYear(), currDate.getMonth()-12, "1"), datePattern);
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

        createStore();
        createGrid();

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
            }
        });
        var damCdCombo = $("#damCd").multiselect({
            minWidth: 150,
            noneSelectedText: "전 체",
            selectedText: '#개 선택',
            checkAllText: '선택',
            uncheckAllText: '해제'
        });
        damCdCombo.multiselect("uncheckAll");
        var alarmCdCombo = $("#alarmCd").multiselect({
            multiple: true,
            minWidth: 200,
            checkAllText: '선택',
            uncheckAllText: '해제',
            noneSelectedText: "선택사항없음",
            selectedText: '#개 선택',
            click: function (e, u) {
                changeStructByalarmCd(u);
            },
            checkAll: function () {
                changeStructByalarmCd("all");
            },
            uncheckAll: function () {
                changeStructByalarmCd("none");
            }
        });
        alarmCdCombo.multiselect("checkAll");
        var statTpCombo = $("#statTp").multiselect({
            header: false,
            multiple: false,
            minWidth: 100,
            height: 100,
            noneSelectedText: "전 체",
            selectedList: 1,
            click: function (e, u) {
                changeStructByStat(u.value);
            }
        });
        dispTpCombo = $("#dispTp").multiselect({
            header: false,
            multiple: false,
            minWidth: 110,
            height: 65,
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
        adjustMainPanel();

        hidePreloader();
    });

</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="menu-title" class="title_box" style="position:absolute;text-align:left;height:40px;top:10px;left:10px;">
	<span style="font-weight:bold;font-size:12px;margin-left:10px">
    <img src="<%=Page.ResolveUrl("/Images") %>/icons/highlighter.png" align="absmiddle"/>&nbsp;&nbsp;
    알람현황 분석
    </span>
</div>
<div id="search-panel" style="position:absolute;top:50px;left:5px;background-color:#D3E3F8;border:1px solid #B5BCC7;width:99%;">
          <table>
        <tr>
            <td >댐&nbsp;&nbsp;구&nbsp;&nbsp;분 : </td>
            <td><select id="damTp" name="damTp" style="width:110px;text-align:left;"></select></td>
            <td>댐&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;명 : </td>
            <td><select id="damCd" name="damCd" style="width:110px;text-align:left;"></select></td>
            <td>검색기간 : </td>
            <td style="text-align:left"><input id="startDt" />~<input id="endDt" /></td>
            <td>항목선택 : </td>
            <td><select id="alarmCd" name="alarmCd" style="width:110px"></select></td>
            <td>통계구분 : </td>
            <td><select id="statTp" name="statTp" style="width:110px"></select></td>
        </tr>
        <tr>
            <td>관리단/댐 : </td>
            <td><select id="dispTp" name="dispTp" style="width:110px"></select></td>
            <td colspan="8" style="text-align:right">
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


