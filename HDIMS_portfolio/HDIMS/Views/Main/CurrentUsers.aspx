<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub3.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="HDIMS.Utils.Login"%>
<%@ Import Namespace="System.Collections"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	현 접속자 현황
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%
    EmpData empdata = new EmpData();

    IList<Hashtable> currentUsers = empdata.GetCurrentUsersWithDamNm();
    
%>
<script type="text/javascript">
    dojo.addOnLoad(function () {
        //선택안되게 하는 것과.. 한번에 화면 보이게 하는 것.
        //setSelectable(document.body);
        hidePreloader();
    });
</script>
    
    <div id="menu-title" align="center" style="text-align:left;">
        <br />
	    <span style="font-weight:bold; margin-left:10px;">
        <img src="<%=Page.ResolveUrl("/Images") %>/icons/user.png" align="absmiddle"/>&nbsp;&nbsp;
        현 접속자 현황
        </span>
    </div>
    <table border='1' cellspacing="0">
    <tr style='background-color:#EFEFEF' height="20">
    <td style='width:80px;font-weight:bold;font-size:12px;' align='center' valign="middle">사번</td>
    <td style='width:100px;font-weight:bold;font-size:12px;' align='center' valign="middle">이름</td>
    <td style='width:150px;font-weight:bold;font-size:12px;' align='center' valign="middle">접속시간</td>
    <td style='width:150px;font-weight:bold;font-size:12px;' align='center' valign="middle">소속</td>
    </tr>
<%  if (currentUsers.Count == 0)
    {
%>
    <tr>
    <td colspan='4' align='center'>접속자 정보가 없습니다.</td>
    </tr>
<% 
    }
    foreach (Hashtable user in currentUsers)
    {
%>
    <tr height="18">
    <td style='width:80px;' align='center'  ><%= user["SESSID"] %></td>
    <td style='width:100px'>&nbsp;<%= user["EMPNM"]%></td>
    <td style='width:150px'>&nbsp;<%= user["LOGINDT2"]%></td>
    <td style='width:150px'>&nbsp;<%= user["DEPT"]%></td>
    </tr>
<% } %>
    </table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PreHeadContent" runat="server">
</asp:Content>
