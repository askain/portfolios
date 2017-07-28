<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Collections" %>
<%@ Import Namespace="WEBSOLTOOL.Util" %>
<% 
    string id = (string)ViewData["id"];
    string type = (string)ViewData["type"];
    IList<Hashtable> data = (List<Hashtable>)ViewData["data"];
    decimal AcuRF = 0;
    decimal AcuRF10 = 0;
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
<script type="text/javascript" src="<%=Page.ResolveUrl("~/Scripts")%>/jquery-1.6.1.min.js"></script>
<link rel="stylesheet" type="text/css" href="/Content/sub.css" />
<title>구글어스용 벌룬</title>
</head>
<script type="text/javascript">
</script>
<body style="padding:0px;margin:0px;">

    <%if (data.Count > 0) { %>
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center" class="bl_title"><%=data[0]["NAME"]%></td>
        </tr>
        <tr>
            <td width="100%" style="padding:0px;">
            <%if (type.Equals("RF")) { %>
                <table width="100%" border="1" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="95px" align="center" class="bl_t1"><b>측정시간</b></td>
                        <td align="right" class="bl_t1"><b>TM우량</b></td>
                        <td align="right" class="bl_t1"><b>강우강도</b></td>
                    </tr>
                <% for (int i = 0; i < 6; i++){%>
                    <tr>
                        <td align="center" class="balloon"><%=data[i]["TIME"]%></td>
                        <td align="right" class="balloon"><%=data[i]["ACURF"]%></td>
                        <td align="right" class="balloon"><%=data[i]["ACURF10"]%></td>
                    </tr>
                <%} %>
                </table>
            <%} %>
            </td>
        </tr>
    </table>
    <%} else { %>
    <!--DUBMMRF테이블에 RFOBSCD정보가 없거나 VM_OBSCODE에 RFOBCD정보가 없을경우-->
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td height=130 valign="middle" align="left" class="bl_title">강우정보 또는 댐정보가 존재 하지 않습니다.</td>
        </tr>
    </table>
    <%} %>
</body>
</html>