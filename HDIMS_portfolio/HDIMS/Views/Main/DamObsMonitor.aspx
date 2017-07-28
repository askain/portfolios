<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import namespace="System.Collections.Generic" %>
<%@ Import namespace="System.Collections" %>
<%@ Import namespace="HDIMS.Models.Domain.Main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	댐별 관측국 설비상태 모니터링
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PreHeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="HeadContent" runat="server">
<%
    string viewTp = (string)ViewData["ViewTp"];
    string damTp = Request["DamTp"];
 %>
<style type="text/css">
.monitor_icons {background: url(<%=Page.ResolveUrl("~/Images")%>/monitor/flag_icon.png) no-repeat top left; }
.monitor_icon_01 {background-position: 0 0; width: 16px; height: 16px;}/* 수위계 이상 */
.monitor_icon_02 {background-position: 0 -32px; width: 16px; height: 16px;} /* 우량계 이상 */
.monitor_icon_03 {background-position: 0 -64px; width: 16px; height: 16px;} /* 전원 이상 */
.monitor_icon_04 {background-position: 0 -48px; width: 16px; height: 16px;} /* 수질센서 이상 */
.monitor_icon_05 {background-position: 0 -64px; width: 16px; height: 16px;} /* 수문개도 이상 */
.monitor_icon_06 {background-position: 0 -80px; width: 16px; height: 16px;} /* RTU 전원 RESET */
.monitor_icon_07 {background-position: 0 -272px; width: 16px; height: 16px;} /* 와치독 RESET */
.monitor_icon_08 {background-position: 0 -96px; width: 16px; height: 16px;} /* LAN Port 이상 */
.monitor_icon_09 {background-position: 0 -112px; width: 16px; height: 16px;} /* CDMA Modem 이상 */
.monitor_icon_10 {background-position: 0 -128px; width: 16px; height: 16px;} /* VSAT(T200) Open Error */
.monitor_icon_11 {background-position: 0 -144px; width: 16px; height: 16px;} /* VSAT 이벤트 통신 결측 */
.monitor_icon_12 {background-position: 0 -160px; width: 16px; height: 16px;} /* CDMA Port Open Error */
.monitor_icon_13 {background-position: 0 -176px; width: 16px; height: 16px;} /* CDMA 이벤트 통신 결측 */
.monitor_icon_14 {background-position: 0 -192px; width: 16px; height: 16px;} /* 유선 Port Open Error */
.monitor_icon_15 {background-position: 0 -208px; width: 16px; height: 16px;} /* 유선 이벤트 통신 결측 */
.monitor_icon_16 {background-position: 0 -224px; width: 16px; height: 16px;} /* Multicast 소켓 에러 */
.monitor_icon_17 {background-position: 0 -240px; width: 16px; height: 16px;} /* UDP 이벤트 통신 결측 */
.monitor_icon_18 {background-position: 0 -256px; width: 16px; height: 16px;} /* IDU Ping Check Error */
.monitor_icon_19 {background-position: 0 -272px; width: 16px; height: 16px;} /* RTU 메모리 이상 */
</style>
<script type="text/javascript">
    var currDate = new Date();
    var damTp = "<%=damTp %>";
    var popupSearch = function(damcd, obscd, obsdt, obstp) {
        var url = "/DataSearch/EquipSearch/?";
        var dd = obsdt.replace(/-/g,"").replace(" ","").replace(":","");
        var param = "damtp="+damTp+"&damcd="+damcd+"&obscd="+obscd+"&obsdt="+dd+"&obstp="+obstp;
        jQuery.popupWindow2({
            windowName: "TeleMonitor",
            width: screen.width-50,
            height: screen.height-50,
            windowURL: url+param,
            resizable: 1,
            top: 1,
            left: 1,
            scrollbars: 2
        });
    }

    Ext.onReady(function () {
        Ext.BLANK_IMAGE_URL = '/Scripts/extjs/s.gif';
        /* 코드 모델 정의 */
        Ext.define('CodeMode', {
            extend: 'Ext.data.Model',
            fields: [
                { name: 'Key', type: 'string' },
                { name: 'Value', type: 'string' },
                { name: 'Ordernum', type: 'int' }
            ]
        });

        /* 댐코드 저장소 정의 */
        var damCodeStore = Ext.create('Ext.data.Store', {
            autoDestroy: true, model: 'CodeMode',
            proxy: {
                type: 'ajax',
                url: '<%=Page.ResolveUrl("~/Common")%>/DamCodeList',
                reader: { type: 'json', root: 'Data' }
            },
            sorters: [{ property: 'Ordernum', direction: 'ASC'}],
            autoLoad: true
        });



        var addObsContent = function (damcd, obsdt, gathertime, obscd, obstp, obsnm, battbolt, wlvl, rfvl, pri_icon, sec_icon,img_text, img_text2, title_color, primary_data, secondary_call) {
            
            var html = "";
            var primsg = (primary_data==0)?"위성망상태 정상":"위성망상태 이상";
            var secmsg = "";
            if(secondary_call==1 || secondary_call==3) {
                secmsg = "위성망 호출";
            } else if(secondary_call==2) {
                secmsg = "유선망 호출";
            } else if(secondary_call==4) {
                secmsg = "CDMA망 호출";
            }

            <% if(viewTp.Equals("D")) { %>

            html += "<table border=1 bordercolor='" + title_color + "' ondblclick='javascript:popupSearch(\""+damcd+"\",\""+obscd+"\",\""+obsdt+"\",\""+obstp+"\");' style='cursor:hand;'><tr><td>";
            html += "   <table border=1 bordercolor='#C4D6EB' style='width:158px;height:120px; padding: 0;' bgcolor='#FFFFFF'>";
            html += "       <tr bgcolor='" + title_color + "'><td colspan='2' style='text-align:left;font-weight:bold;' width='100%'>";
            html += "           <table border=0 width=100%>";
            html += "               <tr>";
            if (title_color!="#BF0000" && sec_icon!=""){
                html += "                   <td width=22px align='right'>&nbsp;</td>";
            }else{
                html += "                   <td width=22px align='right'><img src=/images/monitor/" + pri_icon + " align='absmiddle' alt='"+primsg+"'></td>";
            }
            html += "                   <td align='center'><font color='#FFFFFF' style='font-size:11px;'>&nbsp;&nbsp;"  + obsnm + "</font></td>";
            if(sec_icon=="") {
                html += "                   <td width=22px align='left'>&nbsp;</td>";
            } else {
                html += "                   <td width=22px align='left'><img src=/images/monitor/" + sec_icon + " align='absmiddle' alt='"+secmsg+"'></td>";
            }
            html += "               </tr>";
            html += "           </table>";
            html += "       </td></tr>";
            html += "       <tr><td style='width:70px;text-align:center;'>" + rfvl + "</td><td style='width:70px;text-align:center'>" + wlvl + "</td></tr>";
            html += "       <tr><td align='left' valign='top' rowspan='2' style='padding: 2px 2px 2px 2px'>" + img_text + "</td><td align='center'>" + img_text2 + "</td></tr>";
            html += "       <tr><td style='text-align:center'>"+battbolt+"</td></tr>";
            html += "       <tr><td colspan='2' style='text-align:center;font-weight:bold;height:20px;'>"+gathertime+"</td></tr>";
            html += "   </table>";
            html += "</td></tr></table>";
            
            <% } else { %>
            
            html += "<table border=1 bordercolor='" + title_color + "'ondblclick='javascript:popupSearch(\""+damcd+"\",\""+obscd+"\",\""+obsdt+"\",\""+obstp+"\");' style='cursor:hand;'><tr><td>";
            html += "   <table border=1 bordercolor='#C4D6EB' style='width:158px;height:70px; padding: 0;' bgcolor='#FFFFFF'>";
            html += "       <tr bgcolor='" + title_color + "'><td colspan='2' style='text-align:left;font-weight:bold;' width='100%'>";
            html += "           <table border=0 width=100%>";
            html += "               <tr>";
            if (title_color!="#BF0000" && sec_icon!=""){
                html += "                   <td width=22px align='right'>&nbsp;</td>";
            }else{
                html += "                   <td width=22px align='right'><img src=/images/monitor/" + pri_icon + " align='absmiddle' alt='"+primsg+"'></td>";
            }
            html += "                   <td align='center'><font color='#FFFFFF' style='font-size:11px;'>&nbsp;&nbsp;"  + obsnm + "</font></td>";
            if(sec_icon=="") {
            html += "                   <td width=22px align='left'>&nbsp;</td>";
            } else {
            html += "                   <td width=22px align='left'><img src=/images/monitor/" + sec_icon + " align='absmiddle' alt='"+secmsg+"'></td>";
            }
            html += "               </tr>";
            html += "           </table>";
            html += "       </td></tr>";
            html += "       <tr><td align='left' valign='top' style='width:70px;padding: 2px 2px 2px 2px'>" + img_text + "</td><td align='center'>" + img_text2 + "</td></tr>";
            html += "       <tr><td colspan='2' style='text-align:center;font-weight:bold;height:20px;'>"+gathertime+"</td></tr>";
            html += "   </table>";
            html += "</td></tr></table>";
            
            <% } %>

            return html;
        }

        //columns 수 계산 default 6개 지정
        //만약 6*180 screen.width
        var columnsNum = 6;
        
        try {
            var screenWidth = 1200;
            if($.browser.msie)
                screenWidth = document.body.offsetWidth;
            else
                screenWidth = window.outerWidth;

            columnsNum = Math.ceil(screenWidth/175);
        } catch(e) {}
        var obsPanel = Ext.create('Ext.form.Panel', {
            frame: false,
            region: 'center',
            autoWidth: true,
            autoHeight: true,
            autoScroll: true,
            flex: 1,
            layout: {
                type: 'table',
                columns: columnsNum
            },
            defaultStyle: {
                padding: '0 0 0 0'
            },
            bodyPadding: 0,
            items: [
<% 
var ObsList = ViewData["ObsList"] as IList<ObsCode>;
var ObsCnt = ObsList.Count;
var ObsIdx = 0; 
var title_color="";
var img_text = "";
var img_text2 = "";
var pri_icon = "";
var sec_icon = "";
string Obstp = "";

foreach(ObsCode code in ObsList) { 
    ObsIdx++;
    Obstp = "";
    // 타이틀 배경색
    if (code.DATA_STATUS == 1) {title_color="#BF0000";}
    else if(code.RTU_RESET == 1||code.VSAT_EVENT == 1||code.CDMA_EVENT == 1||code.WIRE_EVENT == 1||code.UDP_EVENT == 1){title_color="#0C9641";}
    else{title_color="#04549C";};

    // 이미지 생성
    img_text = "&nbsp;";
    if (code.WL_SEN == 1){img_text += "<img src=/images/monitor/flag_icon_01.png align=absmiddle alt=\"수위계 점검 결과 이상\">&nbsp;";};
    if (code.RF_SEN == 1){img_text += "<img src=/images/monitor/flag_icon_02.png align=absmiddle alt=\"우량계 점검 결과 이상\">&nbsp;";};
    //if (code.PWRSTS == 1){img_text += "<img src=/images/monitor/flag_icon_03.png align=absmiddle>&nbsp;";};
    if (code.WQ_SEN == 1){img_text += "<img src=/images/monitor/flag_icon_04.png align=absmiddle alt=\"수질측정센서 결과 이상\">&nbsp;";};
    //if (code.DOORSTS == 1){img_text += "<img src=/images/monitor/flag_icon_05.png align=absmiddle>&nbsp;";};
    if (code.RTU_RESET == 1){img_text += "<img src=/images/monitor/flag_icon_06.png align=absmiddle alt=\"RTU 외부전원 Off에 의해 Reset\">&nbsp;";};
    if (code.WDT_RESET == 1){img_text += "<img src=/images/monitor/flag_icon_07.png align=absmiddle alt=\"RTU 자체점검(와치독)에 의해 Reset\">&nbsp;";};
    if (code.LAN_PORT == 1){img_text += "<img src=/images/monitor/flag_icon_08.png align=absmiddle alt=\"LAN Port 점검 결과 이상\">&nbsp;";};
    if (code.CDMA_MODEM == 1){img_text += "<img src=/images/monitor/flag_icon_09.png align=absmiddle alt=\"CDMA 모뎀 점검 결과 이상\">&nbsp;";};
    if (code.VSAT_PORT == 1){img_text += "<img src=/images/monitor/flag_icon_10.png align=absmiddle alt=\"T200 위성(Serial) 통신포트 이상\">&nbsp;";};
    if (code.VSAT_EVENT == 1){img_text += "<img src=/images/monitor/flag_icon_11.png align=absmiddle alt=\"위성 이벤트 통신 결측\">&nbsp;";};
    if (code.CDMA_PORT == 1){img_text += "<img src=/images/monitor/flag_icon_12.png align=absmiddle alt=\"CDMA 통신 포트 Open 이상\">&nbsp;";};
    if (code.CDMA_EVENT == 1){img_text += "<img src=/images/monitor/flag_icon_13.png align=absmiddle alt=\"CDMA 이벤트 통신 결측\">&nbsp;";};
    if (code.WIRE_PORT == 1){img_text += "<img src=/images/monitor/flag_icon_14.png align=absmiddle alt=\"유선망 통신 포트 Open 이상\">&nbsp;";};
    if (code.WIRE_EVENT == 1){img_text += "<img src=/images/monitor/flag_icon_15.png align=absmiddle alt=\"유선망 이벤트 통신 결측\">&nbsp;";};
    if (code.MULTICAST_SOCKET == 1){img_text += "<img src=/images/monitor/flag_icon_16.png align=absmiddle alt=\"Multicast 소켓 에러\">&nbsp;";};
    if (code.UDP_EVENT == 1){img_text += "<img src=/images/monitor/flag_icon_17.png align=absmiddle alt=\"UDP 이벤트 전송 통신 결측\">&nbsp;";};
    if (code.IDU_PING == 1){img_text += "<img src=/images/monitor/flag_icon_18.png align=absmiddle alt=\"위성 단말기(IDU) Ping 체크 에러\">&nbsp;";};
    if (code.RTU_MEMORY == 1){img_text += "<img src=/images/monitor/flag_icon_19.png align=absmiddle alt=\"RTU 메모리 이상\">&nbsp;";};

    img_text2 = "";
    if (code.DOORSTS == 1){img_text2 += "&nbsp;&nbsp;<img src=/images/monitor/mon_icon01_02.png align=absmiddle alt=\"도어상태 이상\">";}else{img_text2 += "&nbsp;&nbsp;<img src=/images/monitor/mon_icon01_01.png align=absmiddle alt=\"도어상태 정상\">";};
    if (code.PWRSTS == 1){img_text2 += "&nbsp;&nbsp;<img src=/images/monitor/mon_icon02_02.png align=absmiddle alt=\"전원상태 이상\">";}else{img_text2 += "&nbsp;&nbsp;<img src=/images/monitor/mon_icon02_01.png align=absmiddle alt=\"전원상태 정상\">";};
    if (code.BATTSTS == 1){img_text2 += "&nbsp;&nbsp;<img src=/images/monitor/mon_icon03_02.png align=absmiddle alt=\"밧데리상태 이상\">&nbsp;&nbsp;";}else{img_text2 += "&nbsp;&nbsp;<img src=/images/monitor/mon_icon03_01.png align=absmiddle alt=\"밧데리상태 정상\">&nbsp;&nbsp;";};

    if(code.PRIMARY_DATA == 1) {
        pri_icon = "commtype_icon_0.png";
    } else {
        pri_icon = "commtype_icon_1.png";
    }
    if (code.SECONDARY_CALL == 4){
        sec_icon = "commtype_icon_3.png";
    } else if (code.SECONDARY_CALL == 2){
        sec_icon = "commtype_icon_2.png";
    } else if(code.SECONDARY_CALL==1 || code.SECONDARY_CALL==3) {
        sec_icon = "commtype_icon_1.png";
    } else {
        sec_icon = ""; //"commtype_icon_4.png";
    } 

    if(code.OBSTP == "RF"){
        Obstp = "(우)";
    }else if(code.OBSTP == "WL"){
        Obstp = "(수)";
    }

%>
    {
        id: 'obspanel_<%=code.OBSCD %>',
        frame: true,
        style: 'width:172px; height: 120px;',
        html: addObsContent('<%=code.DAMCD%>','<%=code.OBSDT %>','<%=code.GATHERING_TIME%>','<%=code.OBSCD %>','<%=code.OBSTP%>','<%=code.OBSNM %> <%=Obstp %>','<%=code.BATTVOLT %>','<%=code.WLVL %>','<%=code.RFVL %>','<%=pri_icon %>','<%=sec_icon %>','<%=img_text %>','<%=img_text2 %>','<%=title_color %>','<%=code.PRIMARY_DATA %>','<%=code.SECONDARY_CALL %>')
<% if(ObsIdx < ObsList.Count) { %>
    },
<% } else { %>
    }
<% } %>
<% 
}
%>
            ]
        });
        var mainForm = Ext.create('Ext.Viewport', {
            frame: true,
            layout: 'border',
            items: [obsPanel],
            renderTo: Ext.getBody()
        });
    });

</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!-- <%=ViewData["DamCd"] %>-->
<!-- <%=ViewData["ViewTp"] %>-->
</asp:Content>




