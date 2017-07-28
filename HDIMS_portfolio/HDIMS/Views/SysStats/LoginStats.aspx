<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	로그인 통계
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PreHeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">
    var totalCount;
    var type = 'month';

    Ext.onReady(function () {
        
        Ext.tip.QuickTipManager.init();

        /////////////////////////// fnuction 시작  ///////////////////////////

        var switchRadio = function (tp) {
            if (tp == 'month') {
                type = 'month';

                loginStatsGrid.reconfigure(MonthloginStatsStore, MonthloginStatsColumns);
                Ext.getCmp('loginMonthChart').show();
                Ext.getCmp('loginDayChart').hide();
            }
            else {
                type = 'day';

                loginStatsGrid.reconfigure(DayloginStatsStore, DayloginStatsColumns);
                Ext.getCmp('loginMonthChart').hide();
                Ext.getCmp('loginDayChart').show();
            }

            var date = Ext.util.Format.date(Ext.getCmp('dt').getValue(), 'Ym');
            type == 'month' ? MonthloginStatsStore.load({ params: { dt: date} }) : DayloginStatsStore.load({ params: { dt: date} });

            loginStatsTotCntStore.load({
                params: {
                    dt: Ext.util.Format.date(Ext.getCmp('dt').getValue(), 'Ym'),
                    tp: type
                }
            });
        }

        /////////////////////////// fnuction 종료  ///////////////////////////



        ////////////////////////////////////******************** 월별 로그인 통계 시작 *********************////////////////////////////////////////

        /* 월별 로그인 통계 모델 정의 */
        var MonthloginStatsModel = Ext.define('MonthloginStatsModel', {
            extend: 'Ext.data.Model',
            idProperty: 'MONTH',
            fields: [
                { name: 'MONTH', type: 'string' },
                { name: 'CNT', type: 'int' }
            ]
        });

        /* 월별 로그인 통계 프록시 정의 */
        var MonthloginStatsProxy =
        {
            type: 'ajax',
            url: '<%=Page.ResolveUrl("~/SysStats")%>/GetMonthLoginStatsList',
            reader: { type: 'json', root: 'Data' }
            //            ,
            //            listeners: {
            //                exception: function (proxy, response, operation) {
            //                    var json = Ext.decode(response.responseText);
            //                    Ext.MessageBox.show({
            //                        title: 'ERROR',
            //                        //msg: json.msg,
            //                        msg: operation.getError(),
            //                        icon: Ext.MessageBox.ERROR,
            //                        buttons: Ext.Msg.OK
            //                    });
            //                }
            //            }
        };

        /* 월별 로그인 통계 저장소 정의 */
        var MonthloginStatsStore = Ext.create('Ext.data.Store', {
            id: 'MonthloginStatsStore',
            model: 'MonthloginStatsModel',
            autoDestroy: true,
            autoLoad: false,
            proxy: MonthloginStatsProxy,
            listeners: {
                load: function (store, records, successful, operation) {
                }
            }
        });


        /* 월별 로그인 통계 그리드 컬럼 정의 */
        var MonthloginStatsColumns =
        [
            { header: '월별', flex: 0.5, minWidth: 60, dataIndex: 'MONTH', align: 'center' },
            { header: '접속수', flex: 0.6, minWidth: 70, dataIndex: 'CNT', align: 'center' }
        ];

        /* 로그인 통계 그리드 tbar 아이템 정의 */
        var loginStatsTbarItems =
        [
            '',
            {
                xtype: 'datefield',
                name: 'dt',
                id: 'dt',
                width: 110,
                value: new Date(),
                maxValue: new Date(),
                format: 'Y-m-d'
            }, {
                xtype: 'radiogroup',
                id: 'radio',
                width: 110,
                height: 25,
                vertical: false,
                padding: '0 0 0 10',
                items: [
                    {
                        boxLabel: '월별',
                        name: 'dateType',
                        id: 'month',
                        checked: true,
                        listeners: {
                            change: function (field, check, oldval) {
                                if (check == true) {
                                    switchRadio('month');
                                }
                            }
                        }
                    }, {
                        boxLabel: '일별',
                        name: 'dateType',
                        id: 'day',
                        listeners: {
                            change: function (field, check, oldval) {
                                if (check == true) {
                                    switchRadio('day');
                                }
                            }
                        }
                    }
                ]
            }, '',
            {
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/navigation-000-frame.png',
                text: '<span style="vertical-align:middle">조회</span>',
                itemId: 'search',
                tooltip: '조회',
                handler: function () {
                    var date = Ext.util.Format.date(Ext.getCmp('dt').getValue(), 'Ym');
                    type == 'month' ? MonthloginStatsStore.load({ params: { dt: date} }) : DayloginStatsStore.load({ params: { dt: date} });

                    loginStatsTotCntStore.load({
                        params: {
                            dt: Ext.util.Format.date(Ext.getCmp('dt').getValue(), 'Ym'),
                            tp: type
                        }
                    });
                }
            }, '', {
                xtype: 'displayfield',
                id: 'totalCnt',
                name: 'totalCnt',
                value: '',
                style: { padding: '0 0 4 0' },
                width: 300,
                height: 20
            }
        ];


        ////////////////////////////////////******************** 월별 로그인 통계 종료 *********************////////////////////////////////////////


        ////////////////////////////////////******************** 일별 로그인 통계 시작 *********************////////////////////////////////////////

        /* 일별 로그인 통계 모델 정의 */
        var DayloginStatsModel = Ext.define('DayloginStatsModel', {
            extend: 'Ext.data.Model',
            idProperty: 'Day',
            fields: [
                { name: 'DAY', type: 'string' },
                { name: 'CNT', type: 'int' }
            ]
        });

        /* 일별 로그인 통계 프록시 정의 */
        var DayloginStatsProxy =
        {
            type: 'ajax',
            url: '<%=Page.ResolveUrl("~/SysStats")%>/GetDayLoginStatsList',
            reader: { type: 'json', root: 'Data' }
            //            ,
            //            listeners: {
            //                exception: function (proxy, response, operation) {
            //                    var json = Ext.decode(response.responseText);
            //                    Ext.MessageBox.show({
            //                        title: 'ERROR',
            //                        //msg: json.msg,
            //                        msg: operation.getError(),
            //                        icon: Ext.MessageBox.ERROR,
            //                        buttons: Ext.Msg.OK
            //                    });
            //                }
            //            }
            ////////////////////// exception listendrs 주석을 제거하면 controller 에서 에러가 발생하지 않음에도 불구하고 exception 걸림 이유는 모름
        };

        /* 일별 로그인 통계 저장소 정의 */
        var DayloginStatsStore = Ext.create('Ext.data.Store', {
            id: 'DayloginStatsStore',
            model: 'DayloginStatsModel',
            autoDestroy: true,
            autoLoad: false,
            proxy: DayloginStatsProxy,
            listeners: {
                load: function (store, records, successful, operation) {
                }
            }
        });


        /* 일별 로그인 통계 그리드 컬럼 정의 */
        var DayloginStatsColumns =
        [
            { header: '일별', flex: 0.5, minWidth: 60, dataIndex: 'DAY', align: 'center' },
            { header: '접속수', flex: 0.6, minWidth: 70, dataIndex: 'CNT', align: 'center' }
        ];

        ////////////////////////////////////******************** 일별 로그인 통계 종료 *********************////////////////////////////////////////





        ////////////////////////////////////////////////////////////  그리드 //////////////////////////////////////////////////////////////////////

        /* 로그인 통계 그리드 정의 */
        var loginStatsGrid = Ext.create('Ext.grid.Panel', {
            id: 'loginStatsGrid',
            name: 'loginStatsGrid',
            frame: true,
            flex: 1,
            region: 'west',
            columns: MonthloginStatsColumns,
            store: MonthloginStatsStore,
            tbar: loginStatsTbarItems,
            listeners: {
                selectionchange: function (view, selections, options) {
                }
            }
        });

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        var loginMonthChart = Ext.create('Ext.chart.Chart', {
            id: 'loginMonthChart',
            flex: 1,
            region: 'east',
            animate: true,
            store: MonthloginStatsStore,
            axes: [{
                type: 'Numeric',
                position: 'bottom',
                fields: ['CNT'],
                label: {
                    renderer: Ext.util.Format.numberRenderer('0,0')
                },
                title: '접속수',
                grid: true,
                minimum: 0
            }, {
                type: 'Category',
                position: 'left',
                fields: ['MONTH'],
                title: '월별'
            }],
            series: [{
                type: 'bar',
                axis: 'bottom',
                highlight: true,
                tips: {
                    trackMouse: true,
                    width: 140,
                    height: 28,
                    renderer: function (storeItem, item) {
                        this.setTitle(storeItem.get('MONTH') + ' 월, ' + ' 접속수: ' + storeItem.get('CNT'));
                    }
                },
                label: {
                    display: 'insideEnd',
                    field: 'CNT',
                    renderer: Ext.util.Format.numberRenderer('0'),
                    orientation: 'horizontal',
                    color: '#333',
                    'text-anchor': 'middle'
                },
                xField: 'MONTH',
                yField: ['CNT']
            }]
        });

        var loginDayChart = Ext.create('Ext.chart.Chart', {
            id: 'loginDayChart',
            flex: 1,
            region: 'east',
            animate: true,
            hidden: true,
            store: DayloginStatsStore,
            axes: [{
                type: 'Numeric',
                position: 'bottom',
                fields: ['CNT'],
                label: {
                    renderer: Ext.util.Format.numberRenderer('0,0')
                },
                title: '접속수',
                grid: true,
                minimum: 0
            }, {
                type: 'Category',
                position: 'left',
                fields: ['DAY'],
                title: '일별'
            }],
            series: [{
                type: 'bar',
                axis: 'bottom',
                highlight: true,
                tips: {
                    trackMouse: true,
                    width: 140,
                    height: 28,
                    renderer: function (storeItem, item) {
                        this.setTitle(storeItem.get('DAY') + ' 일, ' + ' 접속수: ' + storeItem.get('CNT'));
                    }
                },
                label: {
                    display: 'insideEnd',
                    field: 'CNT',
                    renderer: Ext.util.Format.numberRenderer('0'),
                    orientation: 'horizontal',
                    color: '#333',
                    'text-anchor': 'middle'
                },
                xField: 'DAY',
                yField: ['CNT']
            }]
        });

        /* 하단의 차트 담기는 패널 정의 */
        var mainPanel = Ext.create('Ext.form.Panel', {
            flex: 1.0,
            bodyPadding: 5,
            border: 0,
            region: 'center',
            layout: {
                type: 'hbox',
                align: 'stretch',
                padding: 0
            },
            items: [loginStatsGrid, loginMonthChart, loginDayChart]// 
        });

        //////////////================================ total count 시작 ======================================//////////////


        /* 로그인 통계 TotalCount 모델 정의 */
        var loginStatsTotCntModel = Ext.define('loginStatsTotCntModel', {
            extend: 'Ext.data.Model',
            idProperty: 'TOT_CNT',
            fields: [
                { name: 'TOT_CNT', type: 'int' }
            ]
        });

        /* 로그인 통계 TotalCount 프록시 정의 */
        var loginStatsTotCntProxy =
        {
            type: 'ajax',
            url: '<%=Page.ResolveUrl("~/SysStats")%>/GetLoginStatsTotCnt',
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

        /* 로그인 통계 TotalCount 저장소 정의 */
        var loginStatsTotCntStore = Ext.create('Ext.data.Store', {
            id: 'loginStatsTotCntStore',
            model: 'loginStatsTotCntModel',
            autoDestroy: true,
            autoLoad: false,
            proxy: loginStatsTotCntProxy,
            listeners: {
                load: function (store, records, successful, operation) {
                    if (records.length != 0) {
                        Ext.getCmp('totalCnt').setValue('Total :  ' + '<span style="color:red">' + records[0].data.TOT_CNT + '</span>');
                    }
                }
            }
        });


        loginStatsTotCntStore.load({
            params: {
                dt: Ext.util.Format.date(Ext.getCmp('dt').getValue(), 'Ym'),
                tp: type
            }
        });

        //////////////================================ total count 종료 ======================================//////////////


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
            }, mainPanel]
        });

        var date = Ext.util.Format.date(Ext.getCmp('dt').getValue(), 'Ym');
        //type == 'month' ? MonthloginStatsStore.load({ params: { dt: date} }) : DayloginStatsStore.load({ params: { dt: date} });
        MonthloginStatsStore.load({ params: { dt: date} });
        DayloginStatsStore.load({ params: { dt: date} });

    });

</script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<div id="menu-title" class="title_box x-hide-display" align="center" style="text-align:left;">
	<span style="font-weight:bold; margin-left:10px;">
    <img src="<%=Page.ResolveUrl("/Images") %>/icons/chart.png" align="absmiddle"/>&nbsp;&nbsp;
    로그인 통계
    </span>
</div>
</asp:Content>
