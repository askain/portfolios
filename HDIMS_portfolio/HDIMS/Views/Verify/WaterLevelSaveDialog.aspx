<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub4.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="HDIMS.Utils.Login"%>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	수위자료 보정 저장
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
    prevDate.setDate(currDate.getDate() - 2);
    var loginEmpNo = "<%=empdata.GetEmpData(0) %>";
    var loginEmpNm = "<%=empdata.GetEmpData(1) %>";

    //전역 변수
    var dhxLayout, dhxGrid, dhxForm, dhxFormData;
    var Date, grid, mainStore, linearGrid, linearStore, historyGrid, historyStore;
    var originStoreItems = [], preFrom, preTo, searchFlag = 0;
    var legendNum = 4;
    var wlParams = "";
    var datePattern = { datePattern: "yyyy-MM-dd", selector: "date" };
    var datePattern2 = { datePattern: "yyyyMMdd", selector: "date" };
    var datePattern3 = { datePattern: "yyyyMMddHHmm", selector: "date" };
    //전역함수

    var windowClose = function () {
        window.close();
    }

    var getSelectValue = function (id) {
        var sel = dojo.byId(id);
        return sel.options[sel.options.selectedIndex].value;
    };

    var getSelectText = function (id) {
        var sel = dojo.byId(id);
        return sel.options[sel.options.selectedIndex].text;
    };

    
    var resetSaveDialog = function () {
        dojo.byId('submitEdExLvl').options[0].selected = true;
        dojo.byId('submitEdExWay').options[0].selected = true;
        dojo.byId('exRsn').options[0].selected = true;
        //dojo.byId('cnrsnText').value = '';
        dojo.byId('cndsText').value = '';
    };
    var saveData = function () {
        var edexlvl = getSelectValue("submitEdExLvl");
        var edexway = getSelectValue("submitEdExWay");
        var exrsn = getSelectValue("exRsn");
        var exrsncont = getSelectText("exRsn");

        if (edexlvl == '') {
            alert("보정등급을 선택하세요.");
            return false;
        }
        if (edexway == "") {
            alert("보정방법을 선택하세요.");
            return false;
        }
        if (exrsn == "") {
            alert("증상을 선택하세요.");
            return false;
        }

        //var cnrsm = dojo.byId("cnrsnText").getAttribute("value");
        var cnds = dojo.byId("cndsText").innerText;

        //alert(edexlvl + " " + edexway + " " + exrsn + " " + exrsncont + " " + cnds + " " + window);

        $(document.body).mask("변경된 데이터를 저장중입니다...");
        pWin.saveData(edexlvl, edexway, exrsn, exrsncont, cnds, window);
    };


    window.onbeforeunload = function () {
        pWin.unmaskBody();
    }

    //↓↓↓↓↓↓↓↓↓ 초기화 시작 ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
    dojo.ready(function () {
        pWin = window.dialogArguments;
        exCdOptions = gl_excd_W;
        setOptions("submitEdExLvl", gl_edexlvl2);
        setOptions("submitEdExWay", gl_edexway_WL2);
        setOptions("exRsn", exCdOptions);

        
    });

</script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<table >
    <tr>
        <td>보정등급&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;</td>
        <td><select id="submitEdExLvl" class="wsComboBox" style="width:400px;overflow:hidden;"></select>&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
        <td>보정방법&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;</td>
        <td><select id="submitEdExWay" class="wsComboBox" style="width:400px;overflow:hidden;"></select>&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
    <td>증&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;상&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;</td>
    <td><select id="exRsn" class="wsComboBox" style="width:400px;overflow:hidden;"></select>&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <!--
    <tr>
        <td>사&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;유&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;</td>
        <td><input id="cnrsnText" style="width:395px;overflow:hidden;"/>&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    -->
    <tr>
        <td>내&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;역&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;</td>
        <td><textarea id="cndsText" style="width:395px;height:100px;overflow:hidden;"></textarea>&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
        <td colspan="2" align="right">
        <button dojoType="dijit.form.Button" onClick="windowClose()"><img src='<%=Page.ResolveUrl("~/Images") %>/icons/cross-circle.png'> 취 소</button>
        <button dojoType="dijit.form.Button" onClick="saveData()"><img src='<%=Page.ResolveUrl("~/Images") %>/icons/disks.png'> 저 장</button>
        </td>
    </tr>
</table>
</asp:Content>
