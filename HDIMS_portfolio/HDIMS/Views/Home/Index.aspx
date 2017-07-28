<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
HDIMS::실시간 수문자료 품질관리 시스템
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
<%
    string empno = (string)ViewData["empno"];
    string empnm = (string)ViewData["empnm"];
    string authcode = (string)ViewData["authcode"];
    string longitude = (string)ViewData["longitude"];
    string latitude = (string)ViewData["latitude"];
    string range = (string)ViewData["range"];
%>
<link rel="stylesheet" type="text/css" href="<%=Page.ResolveUrl("~/Scripts") %>/extjs/ux/statusbar/css/statusbar.css" />
<script type="text/javascript" src="<%=Page.ResolveUrl("~/Scripts") %>/extjs/ux/statusbar/StatusBar.js"></script>
<script type="text/javascript" src="<%=Page.ResolveUrl("~/Scripts") %>/jquery.popupWindow.js"></script>
<script type="text/javascript">
    var menuStatStore;    


    Ext.onReady(function () {
        Ext.BLANK_IMAGE_URL = '/Scripts/extjs/s.gif';

        Ext.QuickTips.init();



        /*메뉴 접속 모델 정의 */
        var menuStatModel = Ext.define('menuStatModel', {
            extend: 'Ext.data.Model',
            idProperty: 'ACC_DATE',
            fields: [
                { name: 'MENU_ID', type: 'int' },
                { name: 'ACC_DATE', type: 'int' },
                { name: 'IP', type: 'int' }
            ]
        });

        /*메뉴 접속 저장소 정의 */
        menuStatStore = Ext.create('Ext.data.Store', {
            autoDestroy: true,
            model: 'menuStatModel',
            proxy: {
                type: 'ajax',
                url: '/SysStats/InsertMenuStat',
                reader: { type: 'json', root: 'Data' }
            },
            autoLoad: false
        });

        Ext.state.Manager.setProvider(Ext.create('Ext.state.CookieProvider'));
        var loginMesg = "<%=empnm %>님 접속중";
        var clock = Ext.create('Ext.toolbar.TextItem', {text: Ext.Date.format(new Date(), 'Y-m-d H:i')})
        var currentUsersBtn = "<%= authcode%>" == "01" ? {
            type: 'button',
            id: 'popupCurrentUsers',
            text: '현 접속자 현황',
            handler: onMenuClick
        } : '';
        var statusBar = Ext.create('Ext.ux.StatusBar', {
            region: 'south',
            id: 'main-statusbar',
            items: [{
                xtype:'splitbutton',
                text:'시작 메뉴',
                width: 100,
                menuAlign: 'br-tr?',
                menu: Ext.create('Ext.menu.Menu', {
                    items: [{
                            id: 'popupRadar',
                            text: '레이더영상보기',
                            handler: onMenuClick
                        },{
                            id: 'logout',
                            text: '로그아웃',
                            handler: onMenuClick
                        }
                    ]
                })
            },'-',loginMesg,'-'
            <% if(authcode.Equals("01")) { %>, currentUsersBtn, '-' <% } %>
            , {xtype: 'button', id: 'logout2', text: '로그아웃', handler: onMenuClick }, '-',clock]
        });
        var FullScreenFlag = false;
        function onMenuClick(item) {
            if(item.id=="logout" || item.id=="logout2" ) {
                document.location.href="<%=Page.ResolveUrl("~/") %>Login/Logout";
            } else if(item.id=="FullScreen") {
                enableFullScreen();
            } else if(item.id=="popupRadar") {
                popupRadar();
            } else if(item.id=="popupCurrentUsers") {
                popupCurrentUsers();
            }
        }

        function popupRadar() {
            jQuery.popupWindow2({
                windowName: "GoogleEarth",
                width: 900,
                height: 700,
                windowURL: '/Main/GoogleEarth',
                centerScreen: 1,
                resizable: 1,
                scrollbars: 2
            });
        }

        function popupCurrentUsers() {
            jQuery.popupWindow2({
                windowName: "CurrentUsers",
                width: 450,
                height: 350,
                windowURL: '/Main/CurrentUsers',
                centerScreen: 1,
                resizable: 1,
                scrollbars: 2
            });
        }
        function enableFullScreen() {
            if(!FullScreenFlag) {
                //상단메뉴 제거
                //Ext.getCmp("menuPanel").hide();
                //Ext.get("topMenuFlash").hide();
                Ext.getCmp("headPanel").hide();
                FullScreenFlag = true;
                Ext.getCmp("FullScreen").setText("전체화면취소");
            } else {
                //상단메뉴 원위치
                //Ext.getCmp("menuPanel").show();
                Ext.get("headPanel").show();
                //Ext.getCmp("topMenuFlash").show();
                Ext.getCmp("FullScreen").setText("전체화면보기");
                FullScreenFlag = false;
            }
        }
        /* 주기성 작업 정의 */
        //상태바 날짜 변경 작업
        var updateClock = function(){ 
            Ext.fly(clock.getEl()).update(Ext.Date.format(new Date(), 'Y-m-d H:i'));
        } 
        Ext.TaskManager.start({run: updateClock,interval: 30000});

        //세션 업데이트
        <% if(!empno.Trim().Equals("")) { %>
        var updateSession = function() {
                Ext.Ajax.request({
                    url: '/Login/UpdateSession',
                    method: 'POST',
                    scope: this,
                    params: {
                        userId: "<%=empno %>"
                    },
                    success: function (result, request) {
                        var jsonData = result.responseText;
                    },
                    failure: function (response, opts) {
                        //alert("fail");
                    }
                });
        }
        Ext.TaskManager.start({run: updateSession,interval: 60000});
        <% } %>
        /* 주기성 작업 정의  끝 */
        
        var store = Ext.create('Ext.data.TreeStore', {
            fields: [
                { name: 'text', type: 'string' },
                { name: 'link', type: 'string' },
                { name: 'id', type: 'string' },
                { name: 'leaf', type: 'boolean' },
                { name: 'depth', type: 'string' },
                { name: 'ord', type: 'string' },
                { name: 'flag', type: 'string' },
                { name: 'parentId', type: 'string' }
            ],
            proxy: {
                type: 'ajax',
                url: '/Common/MenuList/?useFlag=Y&authCode=<%=ViewData["authcode"] %>',
                listeners: {
                    exception: function (proxy, response, operation) {
                        var json = Ext.decode(response.responseText);
                        Ext.MessageBox.show({
                            title: 'ERROR',
                            msg: json.msg,
                            icon: Ext.MessageBox.ERROR,
                            buttons: Ext.Msg.OK
                        });
                    }
                }
            },
            folderSort: true,
            sorters: [{
                property: 'ord',
                direction: 'ASC'
            }]
        });
        
        
        var treePanel = Ext.create('Ext.tree.Panel', {
            id: 'menuPanel',
            region: 'west',
            useArrows: true,
            frame: true,
            title: 'HDIMS 메뉴',
            width: 200,
            minWidth:150,
            store: store,
            collapsible: true,
            collapsed: true,
            split: true,
            rootVisible: false,
            autoScroll: true,
            margins: '0 0 5 5',
            root: {
                expanded: false
            },
            listeners: {
                itemclick: function (view,rec,item,index,eventObj) {
                    if(rec.get('leaf')==true) {
                        document.getElementById("main-iframe").src=rec.get('link');
                        //alert(rec.get('id'));
                        logMenuAccess(rec.get('id'));
                    }
                }
            }
        });
        treePanel.setLoading(false);
        Ext.create('Ext.Viewport', {
            id:'pagePanel',
            layout: 'border',
            renderTo: Ext.getBody(),
            items: [{
                id: 'headPanel',
                region: 'north',
                height: 45,
                border: false,
                collapsible: false,
                split: false,
                contentEl: 'head-panel',
                margins: '0 0 0 0'
            } , statusBar, treePanel, {
                id:'mainPanel',
                region: 'center',
                frame: true,
                height: 900,
                autoHeight: true,
                autoScroll: true
            }],
            enableKeyEvents: true, 
            listeners:{     
                'keyup': function(f, e){         
                    var charCode = e.getCharCode();         
                    var key      = e.getKey();   
                    alert(charCode + ":" + key);  
                 } 
            }
        });
        


        treePanel.collapse("left",true);
        var flashvars = {route: '/'};
        var params = {wmode: 'transparent'};
        var attributes = {};
        Ext.get("mainPanel").update("<iframe id='main-iframe' style='z-index:1000000' width=100% height=100% frameborder=0 src='/Main/RealMonitor'></iframe>");
        //swfobject.embedSWF("<%=Page.ResolveUrl("~/flash") %>/hdims.swf", "logoFlash", "800", "90", "9.0.0", "<%=Page.ResolveUrl("~/flash") %>/expressInstall.swf",flashvars,params,attributes); 
    });

    function logMenuAccess(id) {
        menuStatStore.load({
            params: {
                menuId : id
            }
        });
    }

    function gfnLoadUrl(id, url) {
        if(url!=undefined && url!="") {
            top.document.getElementById("main-iframe").src=url;
        }
        if(id!=undefined && id!="") {
            logMenuAccess(id);
        }
    }

</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<%
    HDIMS.Utils.Login.EmpData empData = new HDIMS.Utils.Login.EmpData();
    string host = Request.Url.Host + ":" + Request.Url.Port;  
    string xmlUrl = "http://" + host + "/Common/GetTopMenuList/?UseFlag=Y&authCode=" + empData.GetEmpData(3);
    string encXmlUrl = Server.UrlEncode(xmlUrl);
 %>
    <div id="head-panel">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
	<tr>
		<td align="right" background="/Images/img_t_bg.gif" height="45" scope="row">
            <img src="/Images/login/logout.gif" alt="" style="cursor:hand;" onclick="document.location.href='/Login/Logout';">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    </table>
    </div>
    <object  id="topMenuFlash" style="z-index:10;position:absolute;left:0px;top:0px;width:1800px;height:67px;" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0" width="1800" height="67">
      <param name="movie" value="<%=Page.ResolveUrl("~/flash") %>/menu5.swf">
      <param name="quality" value="high">
      <param name="wmode" value="transparent">
      <param NAME="FlashVars" VALUE="xmlUrl=<%=encXmlUrl %>">
      <embed src="<%=Page.ResolveUrl("~/flash") %>/menu5.swf" FlashVars="xmlUrl=<%=encXmlUrl %>" quality="high" pluginspage="http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash" type="application/x-shockwave-flash" width="1800" height="67" wmode="transparent" style="z-index:10;" ></embed>
    </object>
</asp:Content>
