<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub4.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="HDIMS.Utils.Login"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    댐운영자료 보정자료 저장
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
    var damSearchUrl = "/Verify/GetDamDataVerifyList";

    var datePattern = { datePattern: "yyyy-MM-dd", selector: "date" };
    var datePattern2 = { datePattern: "yyyyMMdd", selector: "date" };
    var datePattern3 = { datePattern: "yyyyMMddHHmm", selector: "date" };
    var pWin; //parent window object

    var windowClose = function () {
        window.close();
    }

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

    
    var resetSaveDialog = function () {
        dojo.byId('edExLvl').options[0].selected = true;
        dojo.byId('edExWay').options[0].selected = true;
        dojo.byId('cnrsnText').value = '';
        dojo.byId('cndsText').value = '';
    };

    var saveData = function () {
        if (getSelectValue("edExLvl") == "") {
            alert("보정등급을 선택하세요.");
            return false;
        }
        if (getSelectValue("edExWay") == "") {
            alert("보정방법을 선택하세요.");
            return false;
        }
        //선택된 셀에 두가지의 정보를 입력한다.
        //보정등급, 보정방법, 사유, 내역 입력
        var edExLvl = getSelectValue("edExLvl");
        var edExWay = getSelectValue("edExWay");
        var cnrsnText = dojo.byId("cnrsnText").value;
        var cndsText = dojo.byId("cndsText").value;
        $(document.body).mask("변경된 데이터를 저장중입니다...");
        pWin.saveData(edExWay, edExLvl, cnrsnText, cndsText, window);
    };

    window.onbeforeunload = function () {
        pWin.unmaskBody();
    }

    dojo.ready(function () {
        pWin = window.dialogArguments;
        setOptions("edExLvl", gl_edexlvl2);
        setOptions("edExWay", gl_edexway_DD2);

    });
</script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<table cellpadding="10">
    <tr>
    <td style="background-color:#EFEFEF;padding:5px;font-weight:bold;">보정등급 : </td>
    <td><select id="edExLvl" class="wsComboBox" style="width:380px;overflow:hidden;"></select></td>
    </tr>
    <tr>
    <td style="background-color:#EFEFEF;padding:5px;font-weight:bold;">보정방법 : </td>
    <td><select id="edExWay" class="wsComboBox" style="width:380px;overflow:hidden;"></select></td>
    </tr>
    <tr>
    <td style="background-color:#EFEFEF;padding:5px;font-weight:bold;">사&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;유 : </td>
    <td><input id="cnrsnText" style="width:375px;overflow:hidden;"/></td>
    </tr>
    <tr>
    <td style="background-color:#EFEFEF;padding:5px;font-weight:bold;">내&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;역 : </td>
    <td><textarea id="cndsText" style="width:375px;height:120px;overflow:hidden;"></textarea></td>
    </tr>
    <tr>
    <td colspan="2" align="right">
    <button dojoType="dijit.form.Button" onClick="windowClose();"><img src='<%=Page.ResolveUrl("~/Images") %>/icons/cross-circle.png'>취 소</button>
    <button dojoType="dijit.form.Button" onClick="saveData()"><img src='<%=Page.ResolveUrl("~/Images") %>/icons/disks.png'>저 장</button>
    </td>
    </tr>
    </table>
</asp:Content>
