<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	설비상태 모니터링
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<% 
    string DAMCD = (string)ViewData["DAMCD"];
%>
<script type="text/javascript" src="/Common/Code2Js"></script>
<script type="text/javascript" src="<%=Page.ResolveUrl("~/Scripts") %>/extjs/ux/TabCloseMenu.js"></script>
<style type="text/css">
.new-tab {
    background-image:url(<%=Page.ResolveUrl("~/Images")%>/new_tab.gif) !important;
}
.tabs {
    background-image:url(<%=Page.ResolveUrl("~/Images")%>/images/tabs.gif ) !important;
}
</style>
<script type="text/javascript">
    var currDate = new Date();
    var R_DAMCD = '<%=DAMCD %>';
    var R_DAMTP = glGetDamTp(R_DAMCD);
    if(R_DAMCD == '') {
        R_DAMTP = glGetDefaultDamTp();    
        R_DAMCD = glGetDefaultDamCd();
     
    }
    if(R_DAMTP=="") R_DAMTP="D";

    Ext.onReady(function () {
        Ext.BLANK_IMAGE_URL = '/Scripts/extjs/s.gif';

        var bd = Ext.getBody();
        var currentItem;
        var viewType = "D";

        function formatDate(value) {
            return value ? Ext.Date.dateFormat(value, 'Y-m-d H:i') : ''; // value.dateFormat('d.m.Y H: i:s'); 
        }

        /* 코드 모델 정의 */
        Ext.define('WkCdMode', {
            extend: 'Ext.data.Model',
            idProperty: 'WKCD',
            fields: [
                { name: 'WKCD', type: 'string' },
                { name: 'WKNM', type: 'string' }
            ]
        });

        /* 수계 저장소 정의 */
        var wkCodeStore = Ext.create('Ext.data.Store', {
            autoDestroy: true, model: 'WkCdMode',
            proxy: {
                type: 'ajax',
                url: '<%=Page.ResolveUrl("~/DamBoObsMng")%>/GetWK?allyn=Y',
                reader: { type: 'json', root: 'Data' }
            },
            //sorters: [{ property: 'Ordernum', direction: 'ASC'}],
            autoLoad: true,
            listeners: {
                load : function(store, records, successful) {
                    Ext.getCmp('wkCd').setValue(records[0].data.WKCD);
                }
            }
        });

        /* 코드 모델 정의 */
        Ext.define('CodeMode', {
            extend: 'Ext.data.Model',
            idProperty: 'KEY',
            fields: [
                { name: 'KEY', type: 'string' },
                { name: 'VALUE', type: 'string' },
                { name: 'ORDERNUM', type: 'int' }
            ]
        });

        /* 댐코드 저장소 정의 */
        var damCodeStore = Ext.create('Ext.data.Store', {
            autoDestroy: true, model: 'CodeMode',
            proxy: {
                type: 'ajax',
                url: '<%=Page.ResolveUrl("~/Common")%>/DamCodeList?DamType='+R_DAMTP,
                reader: { type: 'json', root: 'Data' }
            },
            sorters: [{ property: 'Ordernum', direction: 'ASC'}],
            autoLoad: true,
            listeners: {
                load : function(store, records, successful) {
                    if(records && records.length > 0 && records[0].data) {
                        if (R_DAMCD.length > 0 && store.getById(R_DAMCD) != null) {
                            Ext.getCmp('damCd').setValue(R_DAMCD);
                        } else {
                            Ext.getCmp('damCd').setValue(records[0].data.KEY);
                        }
                    }
                }
            }
        });
        
        /* 댐명 선택후 관측국 Combo 로드 셋팅 */
        var searchPanel = Ext.create('Ext.form.Panel', {
            bodyPadding: 0,
            frame: true,
            height: 100,
            bodyPadding: 5,
            layout:'hbox',
            items: [{
                xtype: 'combo'
                , id: 'wkCd'
                , name: 'wkCd'
				, fieldLabel: '수계'
                , labelWidth: 60
                , labelAlign: 'right'
                , width: 190
				, store: wkCodeStore
                , value: R_DAMTP
                , displayField: 'WKNM'
                , valueField: 'WKCD'
                , queryMode: 'local'
                , forceSelection: true
                , listeners: {
                    change: function (field, newValue, oldValue, options) {
                        var damTp = Ext.getCmp('damTp').getValue();
                        damCodeStore.proxy.url = '<%=Page.ResolveUrl("~/Common")%>/DamCodeList/?wkcd=' + newValue + '&DamType=' + damTp;
                        damCodeStore.load();
                    }
                }
            }, {
                xtype: 'combo'
                , id: 'damTp'
                , name: 'damTp'
				, fieldLabel: '구분'
                , labelWidth: 60
                , labelAlign: 'right'
                , width: 190
				, store: [['D', '다목적댐'], ['W', '용수전용댐'], ['B', '다기능보']]
                , value: R_DAMTP
                , displayField: 'VALUE'
                , valueField: 'KEY'
                , queryMode: 'local'
                , forceSelection: true
                , listeners: {
                    change: function (field, newValue, oldValue, options) {
                        var wkcd = Ext.getCmp('wkCd').getValue();
                        damCodeStore.proxy.url = '<%=Page.ResolveUrl("~/Common")%>/DamCodeList/?wkcd=' + wkcd + '&DamType=' + newValue;
                        damCodeStore.load();
                    }
                }
            }, {
                xtype: 'combo'
                , id: 'damCd'
                , name: 'damCd'
				, fieldLabel: '댐명'
                , labelWidth: 60
                , labelAlign: 'right'
                , width: 200
                , displayField: 'VALUE'
                , valueField: 'KEY'
				, store: damCodeStore
                , queryMode: 'local'
                , listeners: {
                    change: function (field, newValue, oldValue, options) {
                        var damNm = damCodeStore.findRecord('KEY', newValue).get('VALUE');
                        addTab(damNm, newValue, true);
                    }
                }
            },{ 
                xtype: 'displayfield',
                width: 10
            },{
                 xtype: 'radiogroup',
                 //fieldLabel: '보기구분', 
                 width: 150,                         
                 items: [             
                     {
                         boxLabel: '상세보기', 
                         name: 'viewtp', 
                         inputValue: 'D', 
                         checked: true, 
                         listeners : {
                            change: function(field,newValue,oldValue) {
                                if(newValue==true) {
                                    viewType = "D";
                                    renderObsData(currentDamCd);
                                }
                            }
                         }
                     },             
                     {
                        boxLabel: '간략보기', 
                        name: 'viewtp', 
                        inputValue: 'S',
                        listeners: {
                            change: function(field,newValue,oldValue) {
                                if(newValue==true) {
                                    viewType="S";
                                    renderObsData(currentDamCd);
                                }
                            }
                        }
                     }
                 ]
            },{
                xtype: 'button',
                name: 'btnShowLegend',
                text: '<span style="font-weight: bold;vertical-align:middle">범 례</span>',
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/application-table.png',
                width: 70,
                valign: 'top',
                height: 24,
                handler: function () {
                    jQuery.popupWindow2({
                        windowName: "TeleMonitorLegend",
                        width: 250,
                        height: 450,
                        windowURL: '<%=Page.ResolveUrl("~/Main")%>/TeleMonitorLegend',
                        centerScreen: 1,
                        resizable: 0
                    });
                }
            },{ 
                xtype: 'displayfield',
                width: 10
            },{
                xtype: 'button',
                name: 'btnReload',
                text: '<span style="font-weight: bold;vertical-align:middle">조 회</span>',
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/magnifier.png',
                width: 70,
                valign: 'top',
                height: 24,
                handler: function () {
                    var damcd = Ext.getCmp('damCd').getValue();
                    var damNm = damCodeStore.findRecord('KEY', damcd).get('VALUE');
                    addTab(damNm, damcd, true);

                }
            }]
        });
        
        var tabPanel = Ext.createWidget('tabpanel', {
            id: 'tab-panel',
            region: 'center',
            resizeTabs: true,
            enableTabScroll: true,
            defaults: {
                autoScroll: false,
                bodyPadding: 10
            },
            items: [],
            plugins: Ext.create('Ext.ux.TabCloseMenu', {
                extraItemsTail: [
                    '-',
                    {
                        text: 'Closable',
                        checked: true,
                        hideOnClick: true,
                        handler: function (item) {
                            currentItem.tab.setClosable(item.checked);
                        }
                    }
                ],
                listeners: {
                    aftermenu: function () {
                        currentItem = null;
                    },
                    beforemenu: function (menu, item) {
                        var menuitem = menu.child('*[text="Closable"]');
                        currentItem = item;
                        menuitem.setChecked(item.closable);
                    }
                }
            })
        });

        var currentDamCd = "";
        var reloadMap = new Ext.util.HashMap();
        var firstLoading = false;
        var startTabReload = function(damCd) {
//            if(!reloadMap.containsKey(damCd)) {
//                var task = {run: function() {
//                    if(firstLoading!=true) {
//                        renderObsData(damCd);
//                    } else {
//                        firstLoading=false;
//                    }
//                },interval:60000};
//                Ext.TaskManager.start(task);
//                reloadMap.add(damCd,task);
//            }
            renderObsData(damCd);
        }
        var stopTabReload = function(damCd) {
            if(reloadMap.containsKey(damCd)) {
                Ext.TaskManager.stop(reloadMap.get(damCd));
                reloadMap.removeAtKey(damCd);
            }
        }

        function renderObsData(damcd) {
            var damTp = Ext.getCmp("damTp").getValue();
            currentDamCd = damcd;
            var url = "<%=Page.ResolveUrl("~/Main") %>/DamObsMonitor/?DamCd="+damcd+"&viewTp="+viewType+"&DamTp="+damTp;
            tabPanel.getComponent("tab_"+damcd).update("<iframe width=100% height=100% frameborder=0 src='"+url+"'></iframe>");
        }

        var addTab = function (tabTitle, damCd, closable) {
            var tabItemId = "tab_" + damCd;
            var tab = tabPanel.getComponent(tabItemId);
            if (tab) {
                tabPanel.setActiveTab(tab);
                renderObsData(tab.id)
            } else {
                tabPanel.add({
                    id: damCd,
                    itemId: tabItemId,
                    title: tabTitle,
                    iconCls: 'tabs',
                    tabTip: damCd,
                    listeners: {
                        render: function (tab) {
                            renderObsData(tab.id)
                            firstLoading = true;
                        },
                        deactivate: function (tab) {
                            //alert('deactivate'+damCd);
                            stopTabReload(damCd);
                        },
                        show: function (tab) {
                            //alert("show");
                            // 오재우 차장 요청으로 Reload 제거
                            startTabReload(damCd);
                            stopTabReload(damCd);
                        }
                    },
                    closable: closable
                }).show();
            }
        };

        var mainForm = Ext.create('Ext.Viewport', {
            layout: {
                type: 'border'
                , padding: 0
            },
            border: 0,
            items: [{
                region: 'north',
                height: 90,
                width: '100%',
                border: 3,
                layout: {
                    type: 'vbox',     
                    align : 'stretch',
                    pack  : 'start'
                },
                items: [{
                    height: 28,
                    border: 0,
                    padding: '0 20 0 5',
                    contentEl: 'menu-title'
                },  {border: 0,contentEl:'explaination' }, searchPanel]
            }, tabPanel],
            renderTo: Ext.getBody() // 랜더링이 되는곳 정의
        });

        var gp = Ext.getCmp('search-form'); ///// 판넬이나 폼판넬등 컴포넌트를 불러올때 사용
    });
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="menu-title" class="title_box x-hide-display" align="center" style="background:none; width:100%">
    <table width="100%">
        <tr>
            <td align="left">   
                <img src="<%=Page.ResolveUrl("/Images") %>/icons/wrench--exclamation.png" align="absmiddle" style="margin-left:15px; padding-bottom:10px"/>&nbsp;
                <span style="font-weight:bold;color:#3F7BC4; font-size:15px" >설비상태 모니터링</span></td>
            <td align="right"><span style="font-weight:bold">※ 더블클릭시 해당 리스트로 이동합니다.</span></td>
        </tr>
    </table>
</div>
<div id="explaination" class="x-hide-display" align="right" >
	
</div>
</asp:Content>
