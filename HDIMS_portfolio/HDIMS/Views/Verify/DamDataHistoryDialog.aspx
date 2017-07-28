<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub4.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="HDIMS.Utils.Login"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    댐운영자료 보정
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PreHeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
    .claro .dijitDialogUnderlay { background:transparent; } 
</style>
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

    var damStore, damGrid, historyStore, historyGrid, exItemGrid, damTpOptions, damCdOptions,
        damTpCombo, damCdCombo, startDtCal, startHrCombo, endDtCal, endHrCombo, dataTpCombo,
        damGridItems, pStartDtCal, pEndDtCal, sStartDtCal, sEndDtCal, startDtVal, endDtVal;
    var originStoreItems = [], preFrom, preTo, searchFlag = 0;
    var glUpdateCellVal = "";
    var historyUrl = '/DataSearch/GetDamDataHistoryList/';
    var datePattern = { datePattern: "yyyy-MM-dd", selector: "date" };
    var datePattern2 = { datePattern: "yyyyMMdd", selector: "date" };
    var datePattern3 = { datePattern: "yyyyMMddHHmm", selector: "date" };

    var historyStruct = [{
        cells: [
        //{ name: '댐명', field: 'DAMNM', width: "80px", styles: 'text-align:center;vertical-align:middle;' },
                {name: '측정일시', field: 'OBSDT', width: "100px", styles: 'text-align:center;vertical-align:middle;', formatter: formatDate }
            ],
        noscroll: true,
        width: 'auto'
    }, {
        cells: [
                { name: '보정일시', field: 'CGDT', width: "100px", styles: 'text-align:center;background-color:#efefef;vertical-align:middle;', formatter: formatDate },
                { name: '보정자', field: 'CGEMPNM', width: "50px", styles: 'text-align:center;background-color:#efefef;vertical-align:middle;' },
                { name: '저수위', field: 'RWL', width: "50px", styles: 'text-align:center;vertical-align:middle;padding:0 0 0 0;margin:0 0 0 0;' },
                { name: '저수량', field: 'RSQTY', width: "50px", styles: 'text-align:center;background-color:#efefef;vertical-align:middle;' },
                { name: '저수율', field: 'RSRT', width: "50px", styles: 'text-align:center;background-color:#efefef;vertical-align:middle;' },
                { name: '방수로</br>수위', field: 'OSPILWL', width: "50px", styles: 'text-align:center;vertical-align:middle;' },
                { name: '유입량', field: 'IQTY', width: "50px", styles: 'text-align:center;background-color:#efefef;vertical-align:middle;' },
                { name: '기타</br>유입량1', field: 'ETCIQTY1', width: "50px", styles: 'text-align:center;background-color:#efefef;vertical-align:middle;' },
                { name: '기타</br>유입량2', field: 'ETCIQTY2', width: "50px", styles: 'text-align:center;vertical-align:middle;' },
                { name: '공용량', field: 'ETQTY', width: "50px", styles: 'text-align:center;background-color:#efefef;vertical-align:middle;' },
                { name: '총방류량', field: 'TDQTY', width: "50px", styles: 'text-align:center;background-color:#efefef;vertical-align:middle;' },
                { name: '발전</br>방류량', field: 'EDQTY', width: "50px", styles: 'text-align:center;vertical-align:middle;' },
                { name: '기타발전</br>방류량', field: 'ETCEDQTY', width: "50px", styles: 'text-align:center;vertical-align:middle;' },
                { name: '수문</br>방류량', field: 'SPDQTY', width: "50px", styles: 'text-align:center;vertical-align:middle;' },
                { name: '기타</br>방류량1', field: 'ETCDQTY1', width: "50px", styles: 'text-align:center;vertical-align:middle;' },
                { name: '기타</br>방류량2', field: 'ETCDQTY2', width: "50px", styles: 'text-align:center;vertical-align:middle;' },
                { name: '기타</br>방류량3', field: 'ETCDQTY3', width: "50px", styles: 'text-align:center;vertical-align:middle;' },
                { name: '아울렛</br>방류량', field: 'OTLTDQTY', width: "50px", styles: 'text-align:center;vertical-align:middle;' },
                { name: '취수1', field: 'ITQTY1', width: "50px", styles: 'text-align:center;vertical-align:middle;' },
                { name: '취수2', field: 'ITQTY2', width: "50px", styles: 'text-align:center;vertical-align:middle;' },
                { name: '취수3', field: 'ITQTY3', width: "50px", styles: 'text-align:center;vertical-align:middle;' },
                { name: '평균우량', field: 'DAMBSARF', width: "50px", styles: 'text-align:center;background-color:#efefef;vertical-align:middle;' },
                { name: '사유', field: 'CNRSN', width: "100px", styles: 'text-align:center;background-color:#efefef;vertical-align:middle;' },
                { name: '내역', field: 'CNDS', width: "100px", styles: 'text-align:center;background-color:#efefef;vertical-align:middle;' }
            ],
        width: 'auto'
    }
    ];

    var pWin;

    window.onbeforeunload = function () {
        pWin.unmaskBody();
    }

    dojo.ready(function () {
        pWin = window.dialogArguments;

        if (!historyStore) {
            historyStore = new dojox.data.JsonRestStore({
                target: historyUrl
            });
        }
        if (!historyGrid) {
            historyGrid = new dojox.grid.EnhancedGrid({
                noDataMessage: "데이터가 존재하지 않습니다.",
                structure: historyStruct,
                canSort: function (colIndex) {
                    return false;
                }
            });
            historyGrid.startup();
            dojo.byId("historyGridPanel").appendChild(historyGrid.domNode);
        }

        historyGrid.setStore(historyStore, pWin.historyParams);
        historyGrid.update();
        //dijit.byId('historyDialog').show();
    });
</script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<div id="historyGridPanel" data-dojo-type="dijit.layout.ContentPane" data-dojo-props='id:"historyGridPanel", region:"center", style:"margin:0 0 0 0;padding:0 0 0 0;"'  style="width:700px;height:200px;overflow:hidden;" ></div>
</asp:Content>
