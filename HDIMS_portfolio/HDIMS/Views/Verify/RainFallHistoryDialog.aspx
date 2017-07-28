<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub4.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="HDIMS.Utils.Login"%>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	우량자료 보정이력
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
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

    dojo.require('doh.runner');
</script>

<%
    EmpData empdata = new EmpData();
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
        printer: true,
        //"cookie": {},
        selector: { row: true, col: false, cell: false }
    };
    var currDate = new Date();
    var prevDate = new Date();
    prevDate.setDate(currDate.getDate() - 2);
    var loginEmpNo = "<%=empdata.GetEmpData(0) %>";
    var loginEmpNm = "<%=empdata.GetEmpData(1) %>";
    var searchFlag = 0;
    //전역 변수
    var dhxLayout, dhxGrid, dhxForm, dhxFormData;
    var Date, grid, mainStore, rdsGrid, rdsStore, historyGrid, historyStore;
    var originStoreItems = [], preFrom, preTo, searchFlag = 0;
    var legendNum = 3;
    var historyUrl = '/DataSearch/GetRainFallHistoryList/';
    var rfParams = "";
    var datePattern = { datePattern: "yyyy-MM-dd", selector: "date" };
    var datePattern2 = { datePattern: "yyyyMMdd", selector: "date" };
    var datePattern3 = { datePattern: "yyyyMMddHHmm", selector: "date" };
    //전역함수


    var historyLayout = [{
        cells: [
            { field: "OBSDT", name: "측정일시", width: "100px", styles: 'text-align:center;', formatter: formatDate },
            { field: "CGDT", name: "보정일시", width: "100px", styles: 'text-align:center;', formatter: formatDate },
            { field: "CGEMPNM", name: "보정자", width: "80px", styles: 'text-align:center;' },
            { field: "PYACURF", name: "계측우량", width: "50px", styles: 'text-align:center;' },
            { field: "RF", name: "시간우량", width: "50px", styles: 'text-align:center;' },
            { field: "EDEXLVLCONT", name: "보정등급", width: "100px", styles: 'text-align:center;' },
            { field: "EDEXWAYCONT", name: "보정방법", width: "100px", styles: 'text-align:center;' },
            { field: "CNRSN", name: "증상", width: "200px", styles: 'text-align:center;' },
            { field: "CNDS", name: "내역", width: "200px", styles: 'text-align:center;' }
        ],
        //noscroll: true,
        width: 'auto'
    }];



    //↓↓↓↓↓↓↓↓↓ 초기화 시작 ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
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
                //store: historyStore,
                structure: historyLayout,
                canSort: function (colIndex) {
                    return false;
                }
            });
            historyGrid.startup();
            dojo.byId("historyGridPanel").appendChild(historyGrid.domNode);
        }

        historyGrid.setStore(historyStore, pWin.historyParams);
        historyGrid.update();
    });

</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div id="historyGridPanel" data-dojo-type="dijit.layout.ContentPane" data-dojo-props='id:"historyGridPanel", region:"center", style:"margin:0 0 0 0;padding:0 0 0 0;"'  style="width:700px;height:200px;overflow:hidden;" ></div>
</asp:Content>


