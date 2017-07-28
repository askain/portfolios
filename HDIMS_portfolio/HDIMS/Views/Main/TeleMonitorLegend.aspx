<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub3.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	설비상태 모니터링 범례
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    //선택안되게 하는 것과.. 한번에 화면 보이게 하는 것.
    setSelectable(document.body);
    hidePreloader();
</script>
    
    <div id="menu-title" align="center" style="text-align:left;">
        <br />
	    <span style="font-weight:bold; margin-left:10px;">
        <img src="<%=Page.ResolveUrl("/Images") %>/icons/gear.png" align="absmiddle"/>&nbsp;&nbsp;
        결측 아이콘 정보 
        </span>
    </div>
    <table align='center' style='margin:10px 10px 10px 10px;padding:0px 0px 0px 0px;' border='1px'>
    <tr>
    <td style='width:25px;' align='center'><img src='<%=Page.ResolveUrl("~/Images") %>/monitor/flag_icon_01.png'></td>
    <td style='width:200px'>&nbsp;수위계 이상</td>
    </tr>
    <tr>
    <td align='center'><img src='<%=Page.ResolveUrl("~/Images") %>/monitor/flag_icon_02.png'></td>
    <td>&nbsp;우량계 이상</td>
    </tr>
    <tr>
    <td align='center'><img src='<%=Page.ResolveUrl("~/Images") %>/monitor/flag_icon_03.png'></td>
    <td>&nbsp;전원 이상</td>
    </tr>
    <tr>
    <td align='center'><img src='<%=Page.ResolveUrl("~/Images") %>/monitor/flag_icon_04.png'></td>
    <td>&nbsp;수질센서 이상</td>
    </tr>
    <!--<tr>
    <td align='center'><img src='<%=Page.ResolveUrl("~/Images") %>/monitor/flag_icon_05.png'></td>
    <td>&nbsp;수문개도 이상</td>
    </tr>-->
    <tr>
    <td align='center'><img src='<%=Page.ResolveUrl("~/Images") %>/monitor/flag_icon_06.png'></td>
    <td>&nbsp;RTU 전원 리셋</td>
    </tr>
    <tr>
    <td align='center'><img src='<%=Page.ResolveUrl("~/Images") %>/monitor/flag_icon_07.png'></td>
    <td>&nbsp;와치독 리셋</td>
    </tr>
    <tr>
    <td align='center'><img src='<%=Page.ResolveUrl("~/Images") %>/monitor/flag_icon_08.png'></td>
    <td>&nbsp;LAN Port 이상</td>
    </tr>
    <tr>
    <td align='center'><img src='<%=Page.ResolveUrl("~/Images") %>/monitor/flag_icon_10.png'></td>
    <td>&nbsp;VSAT(T200) Open Error</td>
    </tr>
    <tr>
    <td align='center'><img src='<%=Page.ResolveUrl("~/Images") %>/monitor/flag_icon_11.png'></td>
    <td>&nbsp;VSAT 이벤트 통신 결측</td>
    </tr>
    <tr>
    <td align='center'><img src='<%=Page.ResolveUrl("~/Images") %>/monitor/flag_icon_12.png'></td>
    <td>&nbsp;CDMA Port Open Error</td>
    </tr>
    <tr>
    <td align='center'><img src='<%=Page.ResolveUrl("~/Images") %>/monitor/flag_icon_13.png'></td>
    <td>&nbsp;CDMA 이벤트 통신 결측</td>
    </tr>
    <tr>
    <td align='center'><img src='<%=Page.ResolveUrl("~/Images") %>/monitor/flag_icon_14.png'></td>
    <td>&nbsp;유선 Port Open Error</td>
    </tr>
    <tr>
    <td align='center'><img src='<%=Page.ResolveUrl("~/Images") %>/monitor/flag_icon_15.png'></td>
    <td>&nbsp;유선 이벤트 통신 결측</td>
    </tr>
    <tr>
    <td align='center'><img src='<%=Page.ResolveUrl("~/Images") %>/monitor/flag_icon_16.png'></td>
    <td>&nbsp;Multicast 소켓 에러</td>
    </tr>
    <tr>
    <td align='center'><img src='<%=Page.ResolveUrl("~/Images") %>/monitor/flag_icon_17.png'></td>
    <td>&nbsp;UDP 이벤트 통신 결측</td>
    </tr>
    <tr>
    <td align='center'><img src='<%=Page.ResolveUrl("~/Images") %>/monitor/flag_icon_18.png'></td>
    <td>&nbsp;IDU Ping Check Error</td>
    </tr>
    <tr>
    <td align='center'><img src='<%=Page.ResolveUrl("~/Images") %>/monitor/flag_icon_19.png'></td>
    <td>&nbsp;RDU 메모리 이상</td>
    </tr>
    </table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PreHeadContent" runat="server">
</asp:Content>
