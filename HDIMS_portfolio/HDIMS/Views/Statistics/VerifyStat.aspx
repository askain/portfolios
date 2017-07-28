<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	검보정 통계
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
    .x-panel-framed {
        padding: 0;
    }
</style>
<script type="text/javascript">
    var currDate = new Date();

    //var prevDate = new Date();

    Ext.onReady(function () {
        Ext.BLANK_IMAGE_URL = '/Scripts/extjs/s.gif';
        var bd = Ext.getBody();
        // 'VTypes' 추가하기
        Ext.apply(Ext.form.field.VTypes, {
            daterange: function (val, field) {
                var date = field.parseDate(val);

                if (!date) {
                    return false;
                }
                if (field.startDateField && (!this.dateRangeMax || (date.getTime() != this.dateRangeMax.getTime()))) {
                    var start = field.up('form').down('#' + field.startDateField);
                    start.setMaxValue(date);
                    start.validate();
                    this.dateRangeMax = date;
                }
                else if (field.endDateField && (!this.dateRangeMin || (date.getTime() != this.dateRangeMin.getTime()))) {
                    var end = field.up('form').down('#' + field.endDateField);
                    end.setMinValue(date);
                    end.validate();
                    this.dateRangeMin = date;
                }

                return true;
            },

            daterangeText: '시작날짜가 끝날짜보다 커야 합니다.'
        });

        var gridPanel = Ext.create('Ext.panel.Panel', {
            region: 'center',
            labelAlign: 'center',
            bodyStyle: 'overflow:auto',
            autoScroll: true,
            contentEl: 'pivotPanel'
        });

        //        var gridPanel = Ext.create('Ext.grid.Panel', {
        //            id: 'WaterLevelStat', autoWidth: true,
        //            flex: 1,
        //            store: ds,
        //            title: '검보정 통계',
        //            region: 'center',
        //            columns: [
        //                { id: 'company', text: '댐명', flex: 1.2, sortable: true, align: 'center', dataIndex: 'price' },
        //                { text: '2011년 7월 1일 ~ 2011년 7월 31일',
        //                    columns: [
        //                    { text: '1', width:60, sortable: false, align: 'center', dataIndex: 'revenue %'},
        //                    { text: '2', width:60, sortable: false, align: 'center', dataIndex: 'growth %'},
        //                    { text: '3', width:60, sortable: false, align: 'center', dataIndex: 'product %'},
        //                    { text: '4', width:60, sortable: false, align: 'center', dataIndex: 'growth %'},
        //                    { text: '5', width:60, sortable: false, align: 'center', dataIndex: 'revenue %'},
        //                    { text: '6', width:60, sortable: false, align: 'center', dataIndex: 'growth %'},
        //                    { text: '7', width:60, sortable: false, align: 'center', dataIndex: 'product %'},
        //                    { text: '8', width:60, sortable: false, align: 'center', dataIndex: 'revenue %'},
        //                    { text: '9', width:60, sortable: false, align: 'center', dataIndex: 'growth %'},
        //                    { text: '10', width:60, sortable: false, align: 'center', dataIndex: 'product %'},
        //                    { text: '11', width:60, sortable: false, align: 'center', dataIndex: 'growth %'},
        //                    { text: '12', width:60, sortable: false, align: 'center', dataIndex: 'product %'}
        //                  ]
        //                },
        //                { text: '전체', flex: 1, sortable: false, align: 'center', dataIndex: 'growth %' },
        //                { text: '정상', flex: 1, sortable: true, align: 'center', dataIndex: 'product %' },
        //                { text: '오결측', flex: 1.4, sortable: true, dataIndex: 'price', align: 'center' },
        //                { text: '신뢰도', flex: 1, sortable: true, align: 'center', dataIndex: 'revenue %' }
        //            ],
        //            listeners: {
        //                selectionchange: function (model, records) {
        //                    var json, name, i, l, items, series, fields;
        //                    if (records[0]) {
        //                        /*
        //                        rec = records[0];
        //                        form = form || this.up('form').getForm();
        //                        fields = form.getFields();
        //                        fields.each(function (field) {
        //                        field.suspendEvents();
        //                        });
        //                        form.loadRecord(rec);
        //                        updateRecord(rec);
        //                        fields.each(function (field) {
        //                        field.resumeEvents();
        //                        });
        //                        */
        //                    }
        //                }
        //            }
        //        });

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
        /* 관측국 저장소 정의 */
        var obsCodeStore = Ext.create('Ext.data.Store', {
            autoDestroy: true, model: 'CodeMode',
            proxy: {
                type: 'ajax',
                url: '<%=Page.ResolveUrl("~/Common")%>/ObsCodeList',
                reader: { type: 'json', root: 'Data' }
            },
            sorters: [{ property: 'Ordernum', direction: 'ASC'}],
            autoLoad: true
        });


        //*********************** 수계, 댐구분 combo 정의 시작 ***********************//

        var wkComboModel = Ext.define('wkComboModel', {
            extend: 'Ext.data.Model',
            idProperty: 'WKCD',
            fields: [
                { name: 'WKCD', type: 'string' },
                { name: 'WKNM', type: 'string' }
           ]
        });

        var wkComboProxy =
        {
            type: 'ajax',
            url: '<%=Page.ResolveUrl("~/DamBoObsMng")%>/GetWkCombo',
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
        }

        var wkComboStore = Ext.create('Ext.data.Store', {
            id: 'wkComboStore',
            model: 'wkComboModel',
            autoDestroy: true,
            remoteSort: true,
            autoLoad: true,
            autoSync: false,
            simpleSortMode: true,
            proxy: wkComboProxy,
            listeners: {
                load: function (store, records, successful, operation) {
                    if (records && records.length > 0) {
                        Ext.getCmp('comboWk').setValue(records[0].data.WKCD);
                    }
                }
            },
            sorters: [{ property: 'WKCD', direction: 'ASC'}],
            autoLoad: true
        });

        //*********************** 수계, 댐구분 combo 정의  끝  ***********************//

        var dateTypes = [
            ['D', '일별'],
            ['M', '월별'],
        /* ['Q', '분기'],*/
            ['Y', '년별']
        ];

        var searchForm = Ext.create('Ext.form.Panel', {
            url: '<%=Page.ResolveUrl("") %>/Statistics/DamDataVerify',
            bodyPadding: 0,
            frame: true,
            height: 100,
            bodyPadding: 5,
            layout: {
                type: 'table'
                , columns: 12
            },
            items: [{
                xtype: 'combobox',
                id: 'comboWk',
                fieldLabel: '수계',
                labelWidth: 60,
                labelAlign: 'right',
                width: 190,
                colspan: 3,
                typeAhead: true,
                triggerAction: 'all',
                selectOnTab: true,
                valueField: 'WKCD',
                displayField: 'WKNM',
                store: wkComboStore,
                queryMode: 'local',
                listeners: {
                    change: function (field, newValue, oldValue, options) {
                    }
                }
            }, {
                xtype: 'combo'
                    , id: 'damcd'
                    , name: 'damcd'
				    , fieldLabel: '댐명'
                    , labelWidth: 60
                    , labelAlign: 'right'
                    , width: 190
                //, multiSelect: true
                    , typeAhead: true
                    , displayField: 'Value'
                    , valueField: 'Key'
				    , store: damCodeStore
                    , queryMode: 'remote'
                    , forceSelection: true
                    , emptyText: '전체'
                    , colspan: 2
                    , listeners: {
                        change: function (field, newValue, oldValue, options) { //[object Object], 1003110 - 현재선택 코드, 1012110 - 이전선택 코드, [object Object]
                            obsCodeStore.proxy.url = '<%=Page.ResolveUrl("~/Common")%>/ObsCodeList/?DamCode=' + newValue + '&ObsTp=wl';
                            obsCodeStore.load();
                        }
                    }

            }, {
                xtype: 'datefield'
                    , name: 'startdt'
                    , id: 'startdt'
                    , fieldLabel: '검색기간'
                    , labelWidth: 70
                    , labelAlign: 'right'
                    , width: 190
                    , vtype: 'daterange'
                    , endDateField: 'enddt'
                    , format: 'Y-m-d'
            }, {
                xtype: 'displayfield'
                    , name: 'dispfld01'
                    , value: '~'
                    , width: 10
            }, {
                xtype: 'datefield'
                    , name: 'enddt'
                    , id: 'enddt'
                    , width: 120
                    , vtype: 'daterange'
                    , startDateField: 'startdt'
                    , format: 'Y-m-d'
                    , align: 'right'
            }, {
                xtype: 'displayfield'
                    , name: 'dispfld22'
                    , value: ''
                    , width: 30
            }, {                              //360 검색구분 조회전까지
                xtype: 'button'
                , name: 'submit1'
                //, rowspan: 2
                , text: '<span style="font-weight: bold">조 회</span>'
                , icon: '<%=Page.ResolveUrl("~/Images") %>/icons/magnifier.png'
                , width: 70
                , height: 24
                , formBind: true
                , handler: function () {
                    var fp = searchForm.getForm();
                    var wk = fp.findField("comboWk").getValue(); //수계
                    var damCd = fp.findField("damcd").getValue();
                    var startDt = Ext.util.Format.date(fp.findField("startdt").getValue(), 'Ymd');
                    var endDt = Ext.util.Format.date(fp.findField("enddt").getValue(), 'Ymd');

                    var analyzeType = null;  //fp.findField("analyzeType").getValue();

                    var dtype = fp.findField("dtype").getValue();
                    if (dtype == "D" && (damCd == null || damCd == "")) {
                        alert("일별자료 통계시 반드시 댐을 선택하셔야 합니다.");
                        return false;
                    }
                    //alert(damCd + "," + obsCd + "," + startDt + "," + endDt + "," + dtype + "," + excd + "," + etccd);
                    gridPanel.el.mask('로딩중입니다...', 'loadingMask');
                    Ext.get("pivotPanel").update("");
                    Ext.Ajax.request({
                        url: '/Statistics/GetVerifyStatList',
                        params: {
                            wk: wk,
                            damcd: damCd,
                            sdate: startDt,
                            edate: endDt,
                            analyzetype: analyzeType,
                            dtype: dtype
                        },
                        success: function (response) {
                            var jsonData = Ext.JSON.decode(response.responseText);
                            var grid = Ext.create("Ext.grid.Panel", {
                                autoDestory: true,
                                renderTo: 'pivotPanel',
                                stripeRows: true,
                                selType: 'cellmodel',
                                region: 'center',
                                overCls: 'grid-over-cls',
                                forceFit: true,
                                width: $(document).width(),
                                height: $(document).height() - 120,
                                store: {
                                    data: jsonData.Data,
                                    fields: jsonData.Model,
                                    proxy: {
                                        type: 'memory'
                                    },
                                    sorters: [{ property: 'DAMNM', direction: 'ASC'}]
                                },
                                columns: jsonData.Head,
                                listeners: {

                                }
                            });
                            grid.render();
                            gridPanel.el.unmask();
                        }
                    });
                }
            }, {
                xtype: 'displayfield'
                        , name: 'dispfld02'
                        , value: ''
                        , width: 3
            }, {
                xtype: 'displayfield'
                        , name: 'dispfld02'
                        , value: ''
                        , width: 100
            }, {
                xtype: 'displayfield'
                        , name: 'dispfld02'
                        , value: '분석구분:'
                        , width: 52
            }, {
                xtype: 'radiofield',
                name: 'radio1',
                value: 'Dam',
                //labelSeparator: '',
                //hideEmptyLabel: false,
                boxLabel: '댐별분석',
                checked: true,
                width: 130,
                style: 'text-align:center',
                listeners: {
                    change: function (field, newValue, oldValue, options) {
                        if (newValue == true) {
                            switchUrl('');
                        }
                    }
                }
            }, {
                xtype: 'radiofield',
                name: 'radio1',
                value: 'Obs',
                //labelSeparator: '',
                //hideEmptyLabel: false,
                boxLabel: '관측국별분석',
                width: 100,
                colspan: 3,
                style: 'text-align:center',
                listeners: {
                    change: function (field, newValue, oldValue, options) {
                        if (newValue == true) {
                            switchUrl('');
                        }
                    }
                }
            }, {
                xtype: 'combo'
                        , id: 'dtype'
                        , name: 'dtype'
				        , fieldLabel: '구 분'
                        , labelWidth: 70
                        , labelAlign: 'right'
                        , width: 190
                        , queryMode: 'local'
                        , forceSelection: true
				        , store: dateTypes
                        , value: 'D'
            }, {
                xtype: 'displayfield'
                        , width: 3
            }, {
                xtype: 'displayfield'
                        , width: 3
            }, {
                xtype: 'displayfield'
                        , width: 3
            }, {
                xtype: 'button'
                    , name: 'submit1'
                //, rowspan: 2
                    , text: '<span style="font-weight: bold">그래프</span>'
                    , icon: '<%=Page.ResolveUrl("~/Images") %>/icons/document-block.png'
                    , width: 70
                    , height: 24
                //, colspan: 2
                    , formBind: true
                    , handler: function () {
                        if (searchForm.getForm().isValid()) {
                            searchForm.getForm().submit({
                                waitTitle: '잠시만 기다려 주십시요',
                                waitMsg: '데이터를 로딩중입니다...'
                                //,
                                //                            success: function (form, action) {
                                //                                alert("success");
                                //                            },
                                //                            failure: function (form, action) {
                                //                                alert("failure");
                                //                            }
                            });
                        }
                    }
            }, {
                xtype: 'displayfield'
                        , name: 'dispfld02'
                        , value: ''
                        , width: 3
            }, {
                xtype: 'button'
                    , name: 'submit1'
                //, rowspan: 2
                    , text: '<span style="font-weight: bold">보고서</span>'
                    , icon: '<%=Page.ResolveUrl("~/Images") %>/icons/report-paper.png'
                    , width: 70
                    , height: 24
                //, colspan: 2
                    , formBind: true
                    , handler: function () {
                        if (searchForm.getForm().isValid()) {
                            searchForm.getForm().submit({
                                waitTitle: '잠시만 기다려 주십시요',
                                waitMsg: '데이터를 로딩중입니다...'
                                //,
                                //                            success: function (form, action) {
                                //                                alert("success");
                                //                            },
                                //                            failure: function (form, action) {
                                //                                alert("failure");
                                //                            }
                            });
                        }
                    }
            }
            ]
    });

    var mainForm = Ext.create('Ext.Viewport', {
        layout: {
            type: 'border'
                , padding: 0
        },
        border: 0,
        items: [{
            region: 'north',
            height: 120,
            border: 3,
            layout: {
                type: 'vbox'
                    , align: 'stretch'
            },
            items: [{
                height: 45,
                border: 0,
                padding: '10 20 5 5',
                contentEl: 'menu-title'
            }, searchForm]
        }, gridPanel
        //            , {
        //                id: 'main-panel',
        //                title: '수위 이상자료 그래프',
        //                collapsible: true,
        //                collapeMode: 'mini',
        //                split: true,
        //                region: 'south',
        //                flex: 0.6
        //            }
            ],
        renderTo: bd
    });

    var gp = Ext.getCmp('WaterLevelStat');
    //Chart Loading
    //loadChartPanel();
    //시작날짜, 끝날짜 조정
    //alert(Ext.util.Format.date(Ext.getCmp('enddt').getValue(), 'Y-m-d'));
    Ext.getCmp('enddt').setValue(currDate);
    Ext.getCmp('startdt').setValue(currDate);
    //alert(Ext.BLANK_IMAGE_URL);

});

</script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div id="menu-title" class="title_box x-hide-display" align="center" style="text-align:left;">
	<span style="font-weight:bold; margin-left:10px;">
    <img src="<%=Page.ResolveUrl("/Images") %>/icons/table-select-all.png" align="absmiddle"/>&nbsp;&nbsp;
    검보정 통계
    </span>
</div>
<div id="pivotPanel"></div>
</asp:Content>
