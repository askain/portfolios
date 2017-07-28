<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub4.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	기간별 수문자료 보고서
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
<%
    //?damtp=&damcd=&obscd=&sdate=20111101&edate=20111117&datatp=WL&disptp=&stattp=D&excd=&etccd=
    string damTp = Request["damtp"];
    string mgtCd = Request["mgtcd"];
    string obsDt = Request["obsdt"];
    string statTp = Request["stattp"];
    
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
    var damTp = "<%=damTp %>";
    var mgtCd = "<%=mgtCd %>";
    var obsDt = "<%=obsDt %>";
    var startDt = "";
    var endDt = "";
    var dLen = 6;

    function getServerUrl() {
        var protocol = document.location.protocol;
        var domain = document.domain;
        var port = document.location.port;
        var url = "http://" + domain;
        if (port != "") url += ":" + port;
        return url;
    }


    function getParamsForReport() {
        //댐,관측소 구분, 일별,월별, 항목별 구분, 
        //obsdt, 날짜길이[8], mgtCd, statTp, 댐월별, 댐분기별
        ///rp [20110901] [] [댐월별] [댐분기별]
        ///rp [시작날짜] [끝날짜] [관리단코드] [월별] [분기별]
        var params = "/rp ";
        var ret;

        if (statTp == "M") {
            ret = getStatDateRange(obsDt);

        } else if (statTp == "Q") {
            ret = getStatDateRange(obsDt.substr(0, 4) + "년" + obsDt.substr(4, 1) + "분기");

        }
        params += " [" + ret.sDate + "]";
        params += " [" + ret.eDate + "]";

        if (damTp != "") {
            damTp = "'" + damTp.replace(/,/gi, "','") + "'"

            params += " [" + damTp + "]";
        } else {
            params += " []";
        }
        
        if (mgtCd != "") {
            mgtCd = "'" + mgtCd.replace(/,/gi, "','") + "'"

            if (mgtCd == "'alldams'") { mgtCd = ""; }

            params += " [" + mgtCd + "]";
        } else {
            params += " []";
        }

        if (statTp == "M") {
            params += " [M]";
        } else if (statTp == "Q") {
            params += " [Q]";
        } else {
            params += " [M]";
        }

        if (mgtCd == "") {
            params += " []";    // [] 관리자용
        } else {
            params += " [1]";   // [1] 관리단 담당자용
        }

        return params;
    }
    var mrdUrl = getServerUrl();

    function getMrdPath() {
        if (statTp == "M") {
            mrdPath = "/mrd/autoreport.mrd";
        } else {
            mrdPath = "/mrd/autoreport_Q.mrd";
        }
        return mrdUrl + mrdPath;
    }

    function rdOpen() {
        var url = getMrdPath();
        var params = "<%=param %>" + " " + getParamsForReport();
        //alert(url + ":" + params);
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
<%
    string docTitle = "수문자료 보고서";
 %>
<br />
<div id="menu-title" class="title_box" align="center" style="left:20px;text-align:left;">
	<span style="font-weight:bold; margin-left:10px;">
    <img src="<%=Page.ResolveUrl("/Images") %>/icons/application-blue.png" align="absmiddle"/>&nbsp;&nbsp;
    <%=docTitle %>
    </span>
</div>
<object id=Rdviewer name=Rdviewer style="width:100%;height:645px" classid="clsid:8068959B-E424-45AD-B62B-A3FA45B1FBAF" codebase="<%=Page.ResolveUrl("~/Cab") %>/rdviewer40.cab#version=4,0,0,156"></object>

</asp:Content>
