<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	수위자료 통계
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
    .x-panel-framed {
        padding: 0;
    }
</style>
<script type="text/javascript">
    var currDate = new Date();
    var prevDate = Ext.Date.add(currDate, Ext.Date.DAY, -6);

    Ext.onReady(function () {
        Ext.BLANK_IMAGE_URL = '/Scripts/extjs/s.gif';
        function switchUrl() {
        }
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

        var bd = Ext.getBody();

        /* 코드 모델 정의 */
        Ext.define('CodeMode', {
            extend: 'Ext.data.Model',
            fields: [
                { name: 'KEY', type: 'string' },
                { name: 'VALUE', type: 'string' },
                { name: 'ORDERNUM', type: 'int' }
            ]
        });
        //댐구분 모델
        var damTypeModel = Ext.define('damTypeModel', {
            extend: 'Ext.data.Model',
            idProperty: 'DAMTYPE',
            fields: [
                { name: 'DAMTYPE', type: 'string' },
                { name: 'DAMTPNM', type: 'string' }
           ]
        });

        //댐구분 Proxy
        var damTypeProxy =
        {
            type: 'ajax',
            url: encodeURI('<%=Page.ResolveUrl("~/DamBoObsMng")%>/GetDamType?firstvalue=전체'),
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
        //댐구분 Store
        var damTypeStore = Ext.create('Ext.data.Store', {
            id: 'damTypeStore',
            model: 'damTypeModel',
            autoDestroy: true,
            autoLoad: false,
            proxy: damTypeProxy,
            listeners: {
                load: function (store, records, successful, operation) {
                    if (records && records.length > 0) {
                        var temp = records[0].data.DAMTYPE;
                        Ext.getCmp('damtype').setValue(temp);
                    }
                }
            }
        });
        damTypeStore.load({
            params: {
                type: 'all' //보 포함
            }
        });
        /* 댐코드 저장소 정의 */
        var damCodeStore = Ext.create('Ext.data.Store', {
            autoDestroy: true, model: 'CodeMode',
            proxy: {
                type: 'ajax',
                url: encodeURI('<%=Page.ResolveUrl("~/Common")%>/DamCodeList?firstvalue=전체'),
                reader: { type: 'json', root: 'Data' }
            },
            listeners: {
                load: function (store, records, successful, operation) {
                    if (records && records.length > 0) {
                        var damcd = records[0].data.KEY;
                        Ext.getCmp('damcd').setValue(damcd);
                    }
                }
            }
        });
        /* 관측국 저장소 정의 */
        var obsCodeStore = Ext.create('Ext.data.Store', {
            autoDestroy: true, model: 'CodeMode',
            proxy: {
                type: 'ajax',
                url: '<%=Page.ResolveUrl("~/Common")%>/ObsCodeList?firstvalue=전체',
                reader: { type: 'json', root: 'Data' }
            },
            listeners: {
                load: function (store, records, successful, operation) {
                    if (records && records.length > 0) {
                        var damcd = records[0].data.KEY;
                        Ext.getCmp('obscd').setValue(damcd);
                    }
                }
            }
        });

        //품질등급 저장소 정의 */
        var exCodeStore = Ext.create('Ext.data.Store', {
            autoDestroy: true, model: 'Common.ExCodeModel',
            proxy: {
                type: 'ajax',
                url: encodeURI('<%=Page.ResolveUrl("~/Common")%>/GetExCodeList/?ExTp=W&firstvalue=전체'),
                reader: { type: 'json', root: 'Data' }
            },
            sorters: [{ property: 'EXORD', direction: 'ASC'}],
            autoLoad: true,
            listeners: {
                load: function (store, records, successful, operation) {
                    if (records && records.length > 0) {
                        var excd = records[0].data.EXCD;
                        Ext.getCmp('excd').setValue(excd);
                    }
                }
            }
        });
        //기타범례 저장소 정의 */
        var etcCodeStore = Ext.create('Ext.data.Store', {
            autoDestroy: true, model: 'Common.EtcCodeModel',
            proxy: {
                type: 'ajax',
                url: '<%=Page.ResolveUrl("~/Common")%>/GetEtcCodeList/?EtcTp=W',
                reader: { type: 'json', root: 'Data' }
            },
            sorters: [{ property: 'ETCCD', direction: 'ASC'}],
            autoLoad: true
        });

        var dateTypes = [
            ['D', '일별'],
            ['M', '월별'],
            ['Y', '년별']
        ];

        var searchForm = Ext.create('Ext.form.Panel', {
            bodyPadding: 0,
            frame: true,
            height: 100,
            bodyPadding: 5,
            layout: {
                type: 'table'
                , columns: 8
            },
            items: [{
                xtype: 'combobox',
                id: 'damtype',
                name: 'damtype',
                fieldLabel: '댐구분',
                labelWidth: 60,
                labelAlign: 'right',
                width: 190,
                height: 26,
                triggerAction: 'all',
                valueField: 'DAMTYPE',
                displayField: 'DAMTPNM',
                store: damTypeStore,
                queryMode: 'local',
                listeners: {
                    change: function (field, newValue, oldValue, options) {
                        damCodeStore.load({
                            params: {
                                DamType: newValue
                            }
                        });
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
                    , displayField: 'VALUE'
                    , valueField: 'KEY'
				    , store: damCodeStore
                    , queryMode: 'local'
                    , multiSelect: true
                    , forceSelection: true
                    , listeners: {
                        change: function (field, newValue, oldValue, options) { //[object Object], 1003110 - 현재선택 코드, 1012110 - 이전선택 코드, [object Object]
                            obsCodeStore.proxy.url = '<%=Page.ResolveUrl("~/Common")%>/ObsCodeList/?DamCode=' + newValue + '&ObsTp=wl&firstvalue=ALL';
                            obsCodeStore.load();
                        }
                    }

            }, {
                xtype: 'combo'
                    , id: 'obscd'
                    , name: 'obscd'
				    , fieldLabel: '관측국'
                    , labelWidth: 60
                    , labelAlign: 'right'
                    , width: 190
                    , displayField: 'VALUE'
                    , valueField: 'KEY'
				    , store: obsCodeStore
                    , queryMode: 'local'
                    , multiSelect: true
                    , forceSelection: true
            }, {
                xtype: 'datefield'
                    , name: 'startdt'
                    , fieldLabel: '검색기간'
                    , id: 'startdt'
                    , labelWidth: 60
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
                    , width: 10
            }, {
                xtype: 'button'
                , name: 'submit1'
                , text: '<span style="font-weight: bold">조 회</span>'
                , icon: '<%=Page.ResolveUrl("~/Images") %>/icons/magnifier.png'
                , width: 70
                , height: 24
                , formBind: true
                , handler: function () {
                    run(1); // 리스트 로드
                }
            }, {
                xtype: 'combo'
                    , id: 'excd'
                    , name: 'excd'
				    , fieldLabel: '품질등급'
                    , labelWidth: 60
                    , labelAlign: 'right'
                    , width: 190
                    , queryMode: 'local'
                    , displayField: 'EXCONT'
                    , valueField: 'EXCD'
				    , store: exCodeStore
                    , multiSelect: true
            }, {
                xtype: 'combo'
                    , id: 'etccd'
                    , name: 'etccd'
				    , fieldLabel: '기타범례'
                    , labelWidth: 60
                    , labelAlign: 'right'
                    , width: 190
                    , emptyText: '전체'
                    , displayField: 'ETCTITLE'
                    , valueField: 'ETCCD'
                    , queryMode: 'local'
                    , forceSelection: true
				    , store: etcCodeStore
            }, { xtype: 'combo'
                    , id: 'dtype'
                    , name: 'dtype'
				    , fieldLabel: '구 분'
                    , labelWidth: 60
                    , labelAlign: 'right'
                    , width: 190
                    , queryMode: 'local'
                    , forceSelection: true
				    , store: dateTypes
                    , value: 'D'
            }, { xtype: 'combo'
                    , id: 'displaytype'
                    , name: 'displaytype'
				    , fieldLabel: '출 력'
                    , labelWidth: 60
                    , labelAlign: 'right'
                    , width: 190
                    , queryMode: 'local'
                    , forceSelection: true
				    , store: [['', '댐'], ['OBSCD', '관측국']]
                    , value: ''
            }, { xtype: 'displayfield'
                    , name: 'dispfld02'
                    , value: ''
                    , width: 10
                    , colspan: 2
            }, {
                xtype: 'displayfield'
                    , name: 'dispfld22'
                    , value: ''
                    , width: 10
            }, {
                xtype: 'button'
                , name: 'submit1'
                , text: '<span style="font-weight: bold">엑셀</span>'
                , icon: '<%=Page.ResolveUrl("~/Images") %>/icons/document-excel-table.png'
                , width: 70
                , height: 24
                , formBind: true
                , handler: function () {
                    run(2); // 엑셀 다운로드
                }
            }
            ]
        });


        function run(flag) {
            var fp = searchForm.getForm();
            var damType = Ext.getCmp("damtype").getValue();
            var damCd = fp.findField("damcd").getValue().toString();
            damCd = damCd.split(',').join(':');
            var obsCd = fp.findField("obscd").getValue().toString();
            obsCd = obsCd.split(',').join(':');
            var startDt = Ext.util.Format.date(fp.findField("startdt").getValue(), 'Ymd');
            var endDt = Ext.util.Format.date(fp.findField("enddt").getValue(), 'Ymd');
            var dtype = fp.findField("dtype").getValue();
            var excd = fp.findField("excd").getValue().toString();
            excd = excd.split(',').join(':');
            var etccd = fp.findField("etccd").getValue();
            var displayType = Ext.getCmp("displaytype").getValue();

            if (dtype == "D" && (damCd == null || damCd == "")) {
                Ext.Msg.alert('Message', "일별자료 통계시 반드시 댐/을 선택하셔야 합니다.");
                return false;
            }

            if (flag == 1) {
                mainGrid.el.mask('로딩중입니다...', 'loadingMask');
                Ext.get("pivotPanel").update("");
                Ext.Ajax.request({
                    url: '/Statistics/GetWaterLevelStatList',
                    params: {
                        damtype: damType,
                        damcd: damCd,
                        obscd: obsCd,
                        sdate: startDt,
                        edate: endDt,
                        excd: excd,
                        etccd: etccd,
                        dtype: dtype,
                        displaytype: displayType
                    },
                    success: function (response) {
                        var jsonData = Ext.JSON.decode(response.responseText);
                        var grid = Ext.create("Ext.grid.Panel", {
                            id: 'grid',
                            name: 'grid',
                            autoDestory: true,
                            renderTo: 'pivotPanel',
                            stripeRows: true,
                            selType: 'cellmodel',
                            region: 'center',
                            overCls: 'grid-over-cls',
                            forceFit: true,
                            stateful: false,
                            width: $(document).width(),
                            height: $(document).height() - 120,
                            store: {
                                data: jsonData.Data,
                                fields: jsonData.Model,
                                proxy: {
                                    type: 'memory'
                                },
                                sorters: [{ property: 'DAMNM', direction: 'ASC' }, { property: 'OBSDT', direction: 'ASC'}]
                            },
                            columns: jsonData.Head,
                            listeners: {}
                        });
                        mainGrid.render();
                        mainGrid.el.unmask();
                    }
                });
            } else if (flag == 2) {
                try {
                    if (Ext.getCmp('grid').getStore().count == 0) { }
                } catch (e) {
                    Ext.Msg.alert('Message', "먼저 조회를 하셔야 합니다.");
                    return false;
                }
                var url = '/Statistics/GetWaterLevelStatExcel?';
                var params = "damtype=" + damType + "&damcd=" + damCd + "&obscd=" + obsCd + "&sdate=" + startDt + "&edate=" + endDt + "&excd=" + excd + "&etccd=" + etccd + "&dtype=" + dtype + "&displaytype=" + displayType;
                document.location = url + params;
            }
        }

        /**
        제목창 : north,
        내용 : center
        - 검색창 : north
        - 목록 : center
        - 그래프 : south
        modal window : 범례
        */
        var mainGrid = Ext.create('Ext.panel.Panel', {
            region: 'center',
            labelAlign: 'center',
            bodyStyle: 'overflow:auto',
            autoScroll: false,
            contentEl: 'pivotPanel'
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
            }, mainGrid
            ],
            renderTo: bd
        });

        Ext.getCmp('enddt').setValue(currDate);
        Ext.getCmp('startdt').setValue(prevDate);
    });

</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="menu-title" class="title_box x-hide-display" align="center" style="text-align:left;">
	<span style="font-weight:bold; margin-left:10px;">
    <img src="<%=Page.ResolveUrl("/Images") %>/icons/television-image.png" align="absmiddle"/>&nbsp;&nbsp;
    수위자료 통계
    </span>
</div>
<div id="pivotPanel"></div>
</asp:Content>