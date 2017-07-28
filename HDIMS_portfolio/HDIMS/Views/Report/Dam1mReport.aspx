<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub4.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
결측률분석 보고서
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
<%
    string damType = Request["damtp"];
    string damCd = Request["damcd"];
    string obsCd = Request["obsCd"];
    string sdate = Request["sdate"];
    string edate = Request["edate"];
    string dispTp = Request["disptp"];
    string statTp = Request["stattp"];
    string dataTp = Request["datatp"];
    
    string mrdpath = "";
    string param = "/rfn [http://rdserver.kowaco.or.kr:8080/RDServer/rdagent.jsp] /rdn [HDIMS]";
 %>
<style type="text/css">
    .x-panel-framed {
        padding: 0;
    }
</style>
<script type="text/javascript">
    var currDate = new Date();
    var statTp = "<%=statTp %>";
    var dLen = statTp == "Y" ? 4 : (statTp == "M" ? 6 : 8);
    var startDt = "<%=sdate %>";
    var endDt = "<%=edate %>";
    var damCd = "<%=damCd %>";
    var obsCd = "<%=obsCd %>";
    var dispTp = "<%=dispTp %>";
    var dataTp = "<%=dataTp %>";
    var startAt, datelen;

    function getServerUrl() {
        var protocol = document.location.protocol;
        var domain = document.domain;
        var port = document.location.port;
        var url = "http://" + domain;
        if (port != "") url += ":" + port;
        return url;
    }


    function getParamsForReport() {
        var params = "/rp ";
        params += " [" + startDt + "]";
        params += " [" + endDt + "]";
        params += " [" + dLen + "]";
        if (damCd != "") {
            damCd = "'" + damCd.replace(/,/gi, "','") + "'"
            params += " [" + damCd + "]";
        } else {
            params += " []";
        }
        if (obsCd != "") {
            obsCd = "'" + obsCd.replace(/,/gi, "','") + "'"
            params += " [" + obsCd + "]";
        } else {
            params += " []";
        }
        params += " []"; //exCd

        //동일년, 동일월, 일자별로 처리하는 부분
        if (dispTp == "") { //댐일경우
            if (statTp == "D") {
                params += " [DD] [1] [] [] [] [] []";
            } else if (statTp == "M") {
                params += " [DM] [] [1] [] [] [] []";
            } else {
                params += " [DI] [] [] [1] [] [] []";
            }
        } else if (dispTp == "OBSCD") { //관측소 기준일 경우
            if (statTp == "D") {
                params += " [OD] [] [] [] [1] [] []";
            } else if (statTp == "M") {
                params += " [OM] [] [] [] [] [1] []";
            } else {
                params += " [OI] [] [] [] [] [] [1]";
            }
        } else if (dispTp == "MGTCD") { //관리단 기준일 경우
            if (statTp == "D") {
                params += " [MD] [] [] [] [] [] []";
            } else if (statTp == "M") {
                params += " [MM] [] [] [] [] [] []";
            } else {
                params += " [MI] [] [] [] [] [] []";
            }
        }

        return params;
    }
    var mrdUrl = getServerUrl();

    function getMrdPath() {
        if (dataTp == "1") mrdPath = "/mrd/dam1mstat.mrd";
        else mrdPath = "/mrd/dam30mstat.mrd";
        return mrdUrl + mrdPath;
    }

    function rdOpen() {
        var url = getMrdPath();
        var params = "<%=param %>" + " " + getParamsForReport();
        Rdviewer.AutoAdjust = true;
        Rdviewer.SetCacheOption(0);
        Rdviewer.ZoomDefault();
        Rdviewer.FileOpen(url, params);
    }

    var resizeReport = function () {
        document.getElementById("Rdviewer").style.height = "100%";
    }

    $(document).ready(function () {
        resizeReport();
        rdOpen();
    });
    window.onresize = resizeReport;
</script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<br />
<div id="menu-title" class="title_box" align="center" style="left:20px;text-align:left;">
	<span style="font-weight:bold; margin-left:10px;">
    <img src="<%=Page.ResolveUrl("/Images") %>/icons/application-blue.png" align="absmiddle"/>&nbsp;&nbsp;
    결측률 분석결과
    </span>
</div>
<object id=Rdviewer name=Rdviewer style="width:100%;height:645px" classid="clsid:8068959B-E424-45AD-B62B-A3FA45B1FBAF" codebase="<%=Page.ResolveUrl("~/Cab") %>/rdviewer40.cab#version=4,0,0,156"></object>
</asp:Content>
