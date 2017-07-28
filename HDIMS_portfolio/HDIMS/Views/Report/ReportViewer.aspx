<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ReportViewer
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="PreHeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<object id="Rdviewer" name="Rdviewer" style="z-index:10" classid="clsid:8068959B-E424-45AD-B62B-A3FA45B1FBAF" codebase="<%=Page.ResolveUrl("~/Cab") %>/rdviewer40.cab#version=4,0,0,156">
</object>

</asp:Content>
