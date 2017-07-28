<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	사용자별 통계
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PreHeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">

    Ext.onReady(function () {
        Ext.BLANK_IMAGE_URL = '/Scripts/extjs/s.gif';
        Ext.tip.QuickTipManager.init();

        
        /* 사용자별 통계 TotalCount 모델 정의 */
        var userStatsTotCntModel = Ext.define('userStatsTotCntModel', {
            extend: 'Ext.data.Model',
            idProperty: 'TOT_CNT',
            fields: [
                { name: 'TOT_CNT', type: 'int' }
            ]
        });

        /* 사용자별 통계 TotalCount 프록시 정의 */
        var userStatsTotCntProxy =
        {
            type: 'ajax',
            url: '<%=Page.ResolveUrl("~/SysStats")%>/GetUserStatsTotCnt',
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

        /* 사용자별 통계 TotalCount 저장소 정의 */
        var userStatsTotCntStore = Ext.create('Ext.data.Store', {
            id: 'userStatsTotCntStore',
            model: 'userStatsTotCntModel',
            autoDestroy: true,
            autoLoad: false,
            proxy: userStatsTotCntProxy,
            listeners: {
                load: function (store, records, successful, operation) {
                    if (records.length != 0) {
                        Ext.getCmp('totalCnt').setValue('Total :  ' + '<span style="color:red">' + records[0].data.TOT_CNT + '</span>');
                    }
                }
            }
        });


        /* 사용자별 통계 모델 정의 */
        var userStatsModel = Ext.define('userStatsModel', {
            extend: 'Ext.data.Model',
            idProperty: 'EMPNO',
            fields: [
                { name: 'EMPNO', type: 'string' },
                { name: 'KORNAME', type: 'string' },
                { name: 'CNT', type: 'int' }
            ]
        });

        /* 사용자별 통계 프록시 정의 */
        var userStatsProxy =
        {
            type: 'ajax',
            url: '<%=Page.ResolveUrl("~/SysStats")%>/GetUserStatsList',
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

        /* 사용자별 통계 저장소 정의 */
        var userStatsStore = Ext.create('Ext.data.Store', {
            id: 'userStatsStore',
            model: 'userStatsModel',
            autoDestroy: true,
            autoLoad: false,
            proxy: userStatsProxy,
            listeners: {
                load: function (store, records, successful, operation) {
                }
            }
        });


        /* 사용자별 통계 그리드 컬럼 정의 */
        var userStatsColumns =
        [
            { header: '사번', flex: 0.5, minWidth: 60, dataIndex: 'EMPNO', align: 'center' },
            { header: '이름', flex: 0.5, minWidth: 60, dataIndex: 'KORNAME', align: 'center' },
            { header: '접속수', flex: 0.6, minWidth: 70, dataIndex: 'CNT', align: 'center' }
        ];

        /* 사용자별 통계 그리드 tbar 아이템 정의 */
        var userStatsTbarItems =
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
                    userStatsStore.load({
                        params: {
                            startDt: sDt,
                            endDt: eDt
                        }
                    });
                    userStatsTotCntStore.load({
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


        /* 사용자별 통계 그리드 정의 */
        var userStatsGrid = Ext.create('Ext.grid.Panel', {
            id: 'userStatsGrid',
            name: 'userStatsGrid',
            frame: true,
            flex: 1,
            region: 'center',
            columns: userStatsColumns,
            store: userStatsStore,
            tbar: userStatsTbarItems,
            listeners: {
                selectionchange: function (view, selections, options) {
                }
            }
        });

        //////////////////////////////////////////////// 그리드 종료 ////////////////////////////////////////////////


//        var userStatsChart = Ext.create('Ext.chart.Chart', {
//            flex: 1,
//            region: 'east',
//            animate: true,
//            store: userStatsStore,
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
//            items: [userStatsGrid, userStatsChart]// 
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
            }, userStatsGrid] // 랜더링이 되는곳 정의
        });

        userStatsStore.load({
            params: {
                startDt: Ext.util.Format.date(Ext.getCmp('startDt').getValue(), 'Ymd') + '000000',
                endDt: Ext.util.Format.date(Ext.getCmp('endDt').getValue(), 'Ymd') + '235959'
            }
        });

        userStatsTotCntStore.load({
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
    사용자별 통계
    </span>
</div>
</asp:Content>
