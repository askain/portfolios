<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	스케줄러 모니터링
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PreHeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">

    Ext.onReady(function () {
        Ext.BLANK_IMAGE_URL = '/Scripts/extjs/s.gif';
        Ext.tip.QuickTipManager.init();

        /* 스케줄러 모니터링 모델 정의 */
        Ext.define('schedulerModel', {
            extend: 'Ext.data.Model',
            fields: [
                { name: 'SHTP', type: 'string' },
                { name: 'SHTPNM', type: 'string' },
                { name: 'STARTDT', type: 'string' },
                { name: 'ENDDT', type: 'string' },
                { name: 'NOTE', type: 'string' }
            ]
        });

        /* 검보정기 모니터링 모델 정의 */
        Ext.define('examModel', {
            extend: 'Ext.data.Model',
            fields: [
                { name: 'EXTP', type: 'string' },
                { name: 'EXTPNM', type: 'string' },
                { name: 'STARTDT', type: 'string' },
                { name: 'ENDDT', type: 'string' },
                { name: 'NOTE', type: 'string' }
            ]
        });

        /* 스케줄러 모니터링 프록시 정의 */
        var schedulerProxy =
        {
            type: 'ajax',
            url: '<%=Page.ResolveUrl("~/SysStats")%>/GetSchedulerLogList',
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

        /* 검보정기 모니터링 프록시 정의 */
        var examProxy =
        {
            type: 'ajax',
            url: '<%=Page.ResolveUrl("~/SysStats")%>/GetExamLogList',
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

        /* 스케줄러 모니터링 저장소 정의 */
        var schedulerStore = Ext.create('Ext.data.Store', {
            id: 'schedulerStore',
            model: 'schedulerModel',
            autoDestroy: true,
            autoLoad: false,
            proxy: schedulerProxy,
            listeners: {
                load: function (store, records, successful, operation) {
                    //                    if (records.length != 0) {
                    //                        Ext.getCmp('totalCnt').setValue('Total :  ' + '<span style="color:red">' + records[0].data.TOT_CNT + '</span>');
                    //                    }
                }
            }
        });
        /* 검보정기 모니터링 저장소 정의 */
        var examStore = Ext.create('Ext.data.Store', {
            id: 'examStore',
            model: 'examModel',
            autoDestroy: true,
            autoLoad: false,
            proxy: examProxy,
            listeners: {
                load: function (store, records, successful, operation) {
                    //                    if (records.length != 0) {
                    //                        Ext.getCmp('totalCnt').setValue('Total :  ' + '<span style="color:red">' + records[0].data.TOT_CNT + '</span>');
                    //                    }
                }
            }
        });

        /* 스케줄러 모니터링 그리드 컬럼 정의 */
        var schedulerColumns =
        [
            { header: '스케줄러 구분', flex: 0.1, minWidth: 200, dataIndex: 'SHTPNM', align: 'center' },
            { header: '시작일시', flex: 0.1, minWidth: 200, dataIndex: 'STARTDT', align: 'center', renderer: convStrToDateMin },
            { header: '종료일시', flex: 0.1, minWidth: 200, dataIndex: 'ENDDT', align: 'center', renderer: convStrToDateMin },
            { header: '상태', flex: 0.7, minWidth: 200, dataIndex: 'NOTE', align: 'center' }
        ];
        /* 검보정기 모니터링 그리드 컬럼 정의 */
        var examColumns =
        [
            { header: '스케줄러 구분', flex: 0.1, minWidth: 200, dataIndex: 'EXTPNM', align: 'center' },
            { header: '시작일시', flex: 0.1, minWidth: 200, dataIndex: 'STARTDT', align: 'center', renderer: convStrToDateMin },
            { header: '종료일시', flex: 0.1, minWidth: 200, dataIndex: 'ENDDT', align: 'center', renderer: convStrToDateMin },
            { header: '상태', flex: 0.7, minWidth: 200, dataIndex: 'NOTE', align: 'center' }
        ];


        /* 스케줄러 모니터링 그리드 tbar 아이템 정의 */
        var schedulerTbarItems =
        [
            '',
            {
                xtype: 'displayfield',
                value: '<img src="<%=Page.ResolveUrl("/Images") %>/icons/clock-small.png" align="absmiddle"/>&nbsp;&nbsp;스케줄러 모니터링',
                width: 180
            }, {
                xtype: 'datefield',
                name: 'sch_startDt',
                id: 'sch_startDt',
                width: 110,
                value: new Date(),
                maxValue: new Date(),
                format: 'Y-m-d'
            }, {
                xtype: 'displayfield',
                name: 'dispfld01',
                value: '',
                width: 10
            }, '', {
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/navigation-000-frame.png',
                text: '<span style="vertical-align:middle">조회</span>',
                itemId: 'search',
                tooltip: '조회',
                handler: function () {
                    schedulerStore.load({
                        params: {
                            startDt: Ext.util.Format.date(Ext.getCmp('sch_startDt').getValue(), 'Ymd') + '000000',
                            endDt: Ext.util.Format.date(Ext.getCmp('sch_startDt').getValue(), 'Ymd') + '240000'
                        }
                    });                   
                }
            }
        ];

        /* 검보정기 모니터링 그리드 tbar 아이템 정의 */
        var examTbarItems =
        [
            '',
            {
                xtype: 'displayfield',
                value: '<img src="<%=Page.ResolveUrl("/Images") %>/icons/clock-small.png" align="absmiddle"/>&nbsp;&nbsp;검보정기 모니터링',
                width: 180
            }, {
                xtype: 'datefield',
                name: 'ex_startDt',
                id: 'ex_startDt',
                width: 110,
                value: new Date(),
                maxValue: new Date(),
                format: 'Y-m-d'
            }, {
                xtype: 'displayfield',
                name: 'dispfld01',
                value: '',
                width: 10
            }, '', {
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/navigation-000-frame.png',
                text: '<span style="vertical-align:middle">조회</span>',
                itemId: 'search',
                tooltip: '조회',
                handler: function () {
                    examStore.load({
                        params: {
                            startDt: Ext.util.Format.date(Ext.getCmp('ex_startDt').getValue(), 'Ymd') + '000000',
                            endDt: Ext.util.Format.date(Ext.getCmp('ex_startDt').getValue(), 'Ymd') + '240000'
                        }
                    });
                }
            }
        ];


        //////////////////////////////////////////////// 그리드 시작 ////////////////////////////////////////////////


        /* 스캐줄러 모니터링 그리드 정의 */
        var schedulerGrid = Ext.create('Ext.grid.Panel', {
            id: 'schedulerGrid',
            name: 'schedulerGrid',
            frame: true,
            flex: 1,
            //region: 'center',
            columns: schedulerColumns,
            store: schedulerStore,
            tbar: schedulerTbarItems
        });
        /* 검보정기 모니터링 그리드 정의 */
        var examGrid = Ext.create('Ext.grid.Panel', {
            id: 'examGrid',
            name: 'examGrid',
            frame: true,
            flex: 1,
            //region: 'center',
            columns: examColumns,
            store: examStore,
            tbar: examTbarItems
        });

        //////////////////////////////////////////////// 그리드 종료 ////////////////////////////////////////////////

        /* 뷰포트 정의 */
        var mainViewport = Ext.create('Ext.Viewport', {
            layout: {
                type: 'vbox',
                padding: 0
            },
            defaults: { width: '100%' },
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
            }, examGrid, schedulerGrid] // 랜더링이 되는곳 정의
        });

        schedulerStore.load({
            params: {
                startDt: Ext.util.Format.date(Ext.getCmp('sch_startDt').getValue(), 'Ymd') + '000000',
                endDt: Ext.util.Format.date(Ext.getCmp('sch_startDt').getValue(), 'Ymd') + '240000'
            }
        });
        examStore.load({
            params: {
                startDt: Ext.util.Format.date(Ext.getCmp('ex_startDt').getValue(), 'Ymd') + '000000',
                endDt: Ext.util.Format.date(Ext.getCmp('ex_startDt').getValue(), 'Ymd') + '240000'
            }
        });
    });

</script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<div id="menu-title" class="title_box x-hide-display" align="center" style="text-align:left;">
	<span style="font-weight:bold; margin-left:10px;">
    <img src="<%=Page.ResolveUrl("/Images") %>/icons/alarm-clock--exclamation.png" align="absmiddle"/>&nbsp;&nbsp;
    스케줄러 모니터링
    </span>
</div>
</asp:Content>
