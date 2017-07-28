<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	메뉴 통계
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PreHeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">

    Ext.onReady(function () {
        Ext.BLANK_IMAGE_URL = '/Scripts/extjs/s.gif';
        Ext.tip.QuickTipManager.init();


        /* 메뉴별 통계 TotalCount 모델 정의 */
        var menuStatsTotCntModel = Ext.define('menuStatsTotCntModel', {
            extend: 'Ext.data.Model',
            idProperty: 'TOT_CNT',
            fields: [
                { name: 'TOT_CNT', type: 'int' }
            ]
        });

        /* 메뉴별 통계 TotalCount 프록시 정의 */
        var menuStatsTotCntProxy =
        {
            type: 'ajax',
            url: '<%=Page.ResolveUrl("~/SysStats")%>/GetMenuStatsTotCnt',
            reader: { type: 'json', root: 'Data' },
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
        };

        /* 메뉴별 통계 TotalCount 저장소 정의 */
        var menuStatsTotCntStore = Ext.create('Ext.data.Store', {
            id: 'menuStatsTotCntStore',
            model: 'menuStatsTotCntModel',
            autoDestroy: true,
            autoLoad: false,
            proxy: menuStatsTotCntProxy,
            listeners: {
                load: function (store, records, successful, operation) {
                    if (records.length != 0) {
                        Ext.getCmp('totalCnt').setValue('Total :  ' + '<span style="color:red">' + records[0].data.TOT_CNT + '</span>');
                    }
                }
            }
        });


        /* 메뉴별 통계 모델 정의 */
        var menuStatsModel = Ext.define('menuStatsModel', {
            extend: 'Ext.data.Model',
            idProperty: 'ACC_DATE',
            fields: [
                { name: 'MENU_ID', type: 'int' },
                { name: 'ACC_DATE', type: 'string' },
                { name: 'IP', type: 'string' },
                { name: 'FULL_PATH', type: 'string' },
                { name: 'MENU_NAME', type: 'string' },
                { name: 'MENU_URI', type: 'string' },
                { name: 'CNT', type: 'int' }
            ]
        });

        /* 메뉴별 통계 프록시 정의 */
        var menuStatsProxy =
        {
            type: 'ajax',
            url: '<%=Page.ResolveUrl("~/SysStats")%>/GetMenuStatsList',
            reader: { type: 'json', root: 'Data' },
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
        };

        /* 메뉴별 통계 저장소 정의 */
        var menuStatsStore = Ext.create('Ext.data.Store', {
            id: 'menuStatsStore',
            model: 'menuStatsModel',
            autoDestroy: true,
            autoLoad: false,
            proxy: menuStatsProxy,
            listeners: {
                load: function (store, records, successful, operation) {
                }
            }
        });


        /* 메뉴별 통계 그리드 컬럼 정의 */
        var menuStatsColumns =
        [
            { header: '메뉴 PATH', flex: 0.6, minWidth: 80, dataIndex: 'FULL_PATH', align: 'center' },
            { header: '메뉴', flex: 0.5, minWidth: 60, dataIndex: 'MENU_NAME', align: 'center' },
            { header: '메뉴 URI', flex: 0.5, minWidth: 60, dataIndex: 'MENU_URI', align: 'center' },
            { header: '접속수', flex: 0.3, minWidth: 40, dataIndex: 'CNT', align: 'center' }
        ];

        /* 메뉴별 통계 그리드 tbar 아이템 정의 */
        var menuStatsTbarItems =
        [
            '',
            {
                xtype: 'datefield',
                name: 'startDt',
                id: 'startDt',
                width: 110,
                value: new Date(),
                maxValue: new Date(),
                format: 'Y-m-d'
            }, {
                xtype: 'displayfield',
                name: 'dispfld01',
                value: '',
                width: 10
            }, {
                xtype: 'displayfield',
                name: 'dispfld02',
                value: '~',
                width: 10
            }, {
                xtype: 'displayfield',
                name: 'dispfld01',
                value: '',
                width: 10
            }, {
                xtype: 'datefield',
                name: 'endDt',
                id: 'endDt',
                width: 110,
                value: new Date(),
                maxValue: new Date(),
                format: 'Y-m-d',
                align: 'right'
            }, '', {
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/navigation-000-frame.png',
                text: '<span style="vertical-align:middle">조회</span>',
                itemId: 'search',
                tooltip: '조회',
                handler: function () {
                    var sDt = Ext.util.Format.date(Ext.getCmp('startDt').getValue(), 'Ymd');
                    var eDt = Ext.util.Format.date(Ext.getCmp('endDt').getValue(), 'Ymd');
                    sDt = sDt + '000000';
                    eDt = eDt + '235959';
                    menuStatsStore.load({
                        params: {
                            startDt: sDt,
                            endDt: eDt
                        }
                    });
                    menuStatsTotCntStore.load({
                        params: {
                            startDt: sDt,
                            endDt: eDt
                        }
                    });
                }
            }, '', {
                xtype: 'displayfield',
                id: 'totalCnt',
                name: 'totalCnt',
                value: '',
                style: { padding: '0 0 4 0' },
                width: 200
            }
        ];


        //////////////////////////////////////////////// 그리드 시작 ////////////////////////////////////////////////


        /* 메뉴별 통계 그리드 정의 */
        var menuStatsGrid = Ext.create('Ext.grid.Panel', {
            id: 'menuStatsGrid',
            name: 'menuStatsGrid',
            frame: true,
            flex: 1,
            region: 'center',
            columns: menuStatsColumns,
            store: menuStatsStore,
            tbar: menuStatsTbarItems,
            listeners: {
                selectionchange: function (view, selections, options) {
                }
            }
        });

        //////////////////////////////////////////////// 그리드 종료 ////////////////////////////////////////////////


        //        var menuStatsChart = Ext.create('Ext.chart.Chart', {
        //            flex: 1,
        //            region: 'east',
        //            animate: true,
        //            store: menuStatsStore,
        //            axes: [{
        //                type: 'Numeric',
        //                position: 'bottom',
        //                fields: ['CNT'],
        //                label: {
        //                    renderer: Ext.util.Format.numberRenderer('0,0')
        //                },
        //                title: '접속수',
        //                grid: true,
        //                minimum: 0
        //            }, {
        //                type: 'Category',
        //                position: 'left',
        //                fields: ['KORNAME'],
        //                title: '사원'
        //            }],
        //            series: [{
        //                type: 'bar',
        //                axis: 'bottom',
        //                highlight: true,
        //                tips: {
        //                    trackMouse: true,
        //                    width: 140,
        //                    height: 28,
        //                    renderer: function (storeItem, item) {
        //                        this.setTitle(storeItem.get('KORNAME') + ' , ' + ' 접속수: ' + storeItem.get('CNT'));
        //                    }
        //                },
        //                label: {
        //                    display: 'insideEnd',
        //                    field: 'CNT',
        //                    renderer: Ext.util.Format.numberRenderer('0'),
        //                    orientation: 'horizontal',
        //                    color: '#333',
        //                    'text-anchor': 'middle'
        //                },
        //                xField: 'KORNAME',
        //                yField: ['CNT']
        //            }]
        //        });


        //        /* 그리드 차트 담기는 패널 정의 */
        //        var mainPanel = Ext.create('Ext.form.Panel', {
        //            flex: 1.0,
        //            bodyPadding: 5,
        //            border: 0,
        //            region: 'center',
        //            layout: {
        //                type: 'hbox',
        //                align: 'stretch',
        //                padding: 0
        //            },
        //            items: [menuStatsGrid, menuStatsChart]// 
        //        });

        /* 뷰포트 정의 */
        var mainViewport = Ext.create('Ext.Viewport', {
            layout: {
                type: 'border',
                padding: 0
            },
            border: 0,
            renderTo: Ext.getBody(),
            items: [{
                region: 'north',
                height: 45,
                border: 0,
                layout: {
                    type: 'vbox',
                    align: 'stretch'
                },
                items: [{
                    height: 45,
                    border: 0,
                    padding: '10 20 5 5',
                    contentEl: 'menu-title'
                }]
            }, menuStatsGrid] // 랜더링이 되는곳 정의
        });

        menuStatsStore.load({
            params: {
                startDt: Ext.util.Format.date(Ext.getCmp('startDt').getValue(), 'Ymd') + '000000',
                endDt: Ext.util.Format.date(Ext.getCmp('endDt').getValue(), 'Ymd') + '235959'
            }
        });

        menuStatsTotCntStore.load({
            params: {
                startDt: Ext.util.Format.date(Ext.getCmp('startDt').getValue(), 'Ymd') + '000000',
                endDt: Ext.util.Format.date(Ext.getCmp('endDt').getValue(), 'Ymd') + '235959'
            }
        });
    });

</script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<div id="menu-title" class="title_box x-hide-display" align="center" style="text-align:left;">
	<span style="font-weight:bold; margin-left:10px;">
    <img src="<%=Page.ResolveUrl("/Images") %>/icons/chart.png" align="absmiddle"/>&nbsp;&nbsp;
    메뉴별 통계
    </span>
</div>
</asp:Content>
