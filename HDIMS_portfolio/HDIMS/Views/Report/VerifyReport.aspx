<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	검보정 보고서
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
        var switchUrl = function (tp) {
            //            var wlUrl = '/WaterLevelSearch';
            //            var rfUrl = '/RainFallSearch';
            //            var url = '<%=Page.ResolveUrl("~/DataSearch") %>';
            //            if (tp == 'wl') {
            //                url = url + wlUrl;
            //            } else {
            //                url = url + rfUrl;
            //            }
            //            document.location.href = url;
        }
        //        var loadChartPanel = function () {
        //            var url = '/chart/chart1.aspx';
        //            Ext.get("main-panel").update("<iframe width=100% height=100% frameborder=0 src='" + url + "' scrolling='no'></iframe>");
        //        }

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

        var bd = Ext.getBody(),
        form = false,
        rec = false,
        selectedStoreItem = false,
        selectItem = function (storeItem) {
            /*
            var name = storeItem.get('company'),
            series = lineChart.series.get(0),
            i, items, l;

            series.highlight = true;
            series.unHighlightItem();
            series.cleanHighlights();
            for (i = 0, items = series.items, l = items.length; i < l; i++) {
            if (name == items[i].storeItem.get('company')) {
            selectedStoreItem = items[i].storeItem;
            series.highlightItem(items[i]);
            break;
            }
            }
            series.highlight = false;
            */
        },
        //폼에 의해서 수정된 필드 수정하기
        updateRecord = function (rec) {
            var name, series, i, l, items, json = [{
                'Name': 'Price',
                'Data': rec.get('price')
            }, {
                'Name': 'Revenue %',
                'Data': rec.get('revenue %')
            }, {
                'Name': 'Growth %',
                'Data': rec.get('growth %')
            }, {
                'Name': 'Product %',
                'Data': rec.get('product %')
            }, {
                'Name': 'Market %',
                'Data': rec.get('market %')
            }];
            chs.loadData(json);
            selectItem(rec);
        },
        createListeners = function () {
            return {
                buffer: 200,
                change: function (field, newValue, oldValue, listener) {
                    form.updateRecord(rec);
                    updateRecord(rec);
                }
            };
        };

        /** DUBMMRF(TRMDV=10,30), DUBHRRF (10분자료,30분자료,시간자료)
        1. 댐코드,댐명,관측국코드, 관측국명,측정일시,원시자료[누가우량,시간우량,등급],추정치(누가우량,우량],기타범례
        확인자, 확인일시, 보정자, 보정일시
        2. Damcd, Damnm, Obscd, Obsnm,Obsdt,OrigAcurf,OrigRf,OrigQlvl,UdtAcurf,UdtRf,EtcExam
        ,ChkUserId,ChkUserNm,ChkDt, EdUserId,EdUserNm,EdDt
        3. DUBMMRF 의 EXEDLVL(검보정레벨) 을 어떻게 처리할까? 댐운영시스템(DDIS)와 호환성 고려할 것.
        */
        var myData = {};
        var ds = Ext.create('Ext.data.ArrayStore', {
            fields: [
                { name: 'company' },
                { name: 'price', type: 'float' },
                { name: 'revenue %', type: 'float' },
                { name: 'growth %', type: 'float' },
                { name: 'product %', type: 'float' },
                { name: 'market %', type: 'float' }
            ],
            data: myData
        });

        //create radar dataset model.
        var chs = Ext.create('Ext.data.JsonStore', {
            fields: ['Name', 'Data'],
            data: [
                { 'Name': 'Price', 'Data': 100 },
                { 'Name': 'Revenue %', 'Data': 100 },
                { 'Name': 'Growth %', 'Data': 100 },
                { 'Name': 'Product %', 'Data': 100 },
                { 'Name': 'Market %', 'Data': 100 }
            ]
        });



        var gridPanel = Ext.create('Ext.grid.Panel', {
            id: 'WaterLevelStat', autoWidth: true,
            flex: 1,
            store: ds,
            title: '검보정 보고서',
            region: 'center',
            columns: [
                { id: 'company', text: '댐명', flex: 1.2, sortable: true, align: 'center', dataIndex: 'price' },
                { text: '2011년 7월 1일 ~ 2011년 7월 31일',
                    columns: [
                    { text: '1', width: 60, sortable: false, align: 'center', dataIndex: 'revenue %' },
                    { text: '2', width: 60, sortable: false, align: 'center', dataIndex: 'growth %' },
                    { text: '3', width: 60, sortable: false, align: 'center', dataIndex: 'product %' },
                    { text: '4', width: 60, sortable: false, align: 'center', dataIndex: 'growth %' },
                    { text: '5', width: 60, sortable: false, align: 'center', dataIndex: 'revenue %' },
                    { text: '6', width: 60, sortable: false, align: 'center', dataIndex: 'growth %' },
                    { text: '7', width: 60, sortable: false, align: 'center', dataIndex: 'product %' },
                    { text: '8', width: 60, sortable: false, align: 'center', dataIndex: 'revenue %' },
                    { text: '9', width: 60, sortable: false, align: 'center', dataIndex: 'growth %' },
                    { text: '10', width: 60, sortable: false, align: 'center', dataIndex: 'product %' },
                    { text: '11', width: 60, sortable: false, align: 'center', dataIndex: 'growth %' },
                    { text: '12', width: 60, sortable: false, align: 'center', dataIndex: 'product %' }
                  ]
                },
                { text: '전체', flex: 1, sortable: false, align: 'center', dataIndex: 'growth %' },
                { text: '정상', flex: 1, sortable: true, align: 'center', dataIndex: 'product %' },
                { text: '오결측', flex: 1.4, sortable: true, dataIndex: 'price', align: 'center' },
                { text: '신뢰도', flex: 1, sortable: true, align: 'center', dataIndex: 'revenue %' }
            ],
            listeners: {
                selectionchange: function (model, records) {
                    var json, name, i, l, items, series, fields;
                    if (records[0]) {
                        /*
                        rec = records[0];
                        form = form || this.up('form').getForm();
                        fields = form.getFields();
                        fields.each(function (field) {
                        field.suspendEvents();
                        });
                        form.loadRecord(rec);
                        updateRecord(rec);
                        fields.each(function (field) {
                        field.resumeEvents();
                        });
                        */
                    }
                }
            }
        });

        var damComboList = [
            ['AL', '소양강', 'The Heart of Dixie'],
            ['AK', '횡성댐', 'The Land of the Midnight Sun'],
            ['AZ', '평화의댐', 'The Grand Canyon State'],
            ['AR', '부암댐', 'The Natural State']
        ];

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
        var dateTypes = [
            ['obdt', '측정일시'],
            ['chdt', '확인일시'],
            ['vrdt', '보정일시']
        ];

        var searchForm = Ext.create('Ext.form.Panel', {
            url: '<%=Page.ResolveUrl("") %>/Statistics/DamDataVerify',
            bodyPadding: 0,
            frame: true,
            height: 100,
            bodyPadding: 5,
            layout: {
                type: 'table'
                , columns: 19
            },
            items: [{
                xtype: 'combo'
                    , id: 'ObsNM'
				    , fieldLabel: '수계'
                    , labelWidth: 60
                    , labelAlign: 'right'
                    , width: 190
                    , multiSelect: true
                    , displayField: 'Value'
                    , valueField: 'Key'
				    , store: obsCodeStore
                    , queryMode: 'remote'
                    , emptyText: '전체'
                    , forceSelection: true
                    , colspan: 3
                //, listeners: {
                //    change: function (field, newValue, oldValue, options) {
                //        alert(newValue);
                //    }
                //}
            }, {
                xtype: 'combo'
                    , id: 'DanNM'
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
                xtype: 'displayfield'
                    , name: 'dispfld02'
                    , value: ''
                    , width: 8
            }, {
                xtype: 'displayfield'
                    , name: 'dispfld02'
                    , value: '검색기간:'
                    , width: 52
                    , style: 'text-align:right'
            }, {
                xtype: 'displayfield'
                    , name: 'dispfld10'
                    , value: ''
                    , width: 10
            }, {
                xtype: 'datefield'
                    , name: 'startdt'
                    , id: 'startdt'
                    , width: 120
                    , vtype: 'daterange'
                    , endDateField: 'enddt'
                    , format: 'Y-m-d'
                    , colspan: 2
            }, {
                xtype: 'displayfield'
                    , name: 'dispfld01'
                    , value: ''
                    , width: 10
            }, {
                xtype: 'displayfield'
                    , name: 'dispfld01'
                    , value: '~'
                    , width: 10
            }, {
                xtype: 'displayfield'
                    , name: 'dispfld01'
                    , value: ''
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
                    , colspan: 2
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
                //, colspan: 2
                , formBind: true
                , handler: function () {
                    rdOpen();
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
                    , value: ''
                    , width: 8
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
                style: 'text-align:center',
                listeners: {
                    change: function (field, newValue, oldValue, options) {
                        if (newValue == true) {
                            switchUrl('');
                        }
                    }
                }
            }, {
                xtype: 'displayfield'
                    , name: 'dispfld02'
                    , value: ''
                    , width: 90
            }, {
                xtype: 'displayfield'
                    , name: 'dispfld02'
                    , value: ''
                    , width: 8
            }, {
                xtype: 'displayfield'
                    , name: 'dispfld02'
                    , value: '자료구분:'
                    , width: 52
                    , style: 'text-align:right'
            }, {
                xtype: 'displayfield'
                    , name: 'dispfld22'
                    , value: ''
                    , width: 10
            }, {
                xtype: 'radiofield',
                name: 'radio4',
                value: 'day',
                //labelSeparator: '',
                //hideEmptyLabel: false,
                boxLabel: '일',
                checked: true,
                width: 60,
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
                name: 'radio4',
                value: 'month',
                //labelSeparator: '',
                //hideEmptyLabel: false,
                boxLabel: '월',
                width: 60,
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
                name: 'radio4',
                value: '3month',
                //labelSeparator: '',
                //hideEmptyLabel: false,
                boxLabel: '분기',
                width: 60,
                style: 'text-align:center',
                colspan: 3,
                listeners: {
                    change: function (field, newValue, oldValue, options) {
                        if (newValue == true) {
                            switchUrl('');
                        }
                    }
                }
            }, {
                xtype: 'radiofield',
                name: 'radio4',
                value: 'year',
                //labelSeparator: '',
                //hideEmptyLabel: false,
                boxLabel: '년',
                width: 60,
                style: 'text-align:center',
                listeners: {
                    change: function (field, newValue, oldValue, options) {
                        if (newValue == true) {
                            switchUrl('');
                        }
                    }
                }
            }, {
                xtype: 'displayfield'
                    , name: 'dispfld22'
                    , value: ''
                    , width: 60
            }, {
                xtype: 'displayfield'
                    , name: 'dispfld02'
                    , value: ''
                    , width: 30
            }
            ]
        });

        /**
        제목창 : north,
        내용 : center
        - 검색창 : north
        - 목록 : center
        - 그래프 : south
        modal window : 범례
        */

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
            }, {
                id: 'reportPanel',
                region: 'center',
                contentEl: 'report-panel'
            }
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
    <img src="<%=Page.ResolveUrl("/Images") %>/icons/application-browser.png" align="absmiddle"/>&nbsp;&nbsp;
    검보정 보고서
    </span>
</div>
<div id="report-panel" class="x-hide-display">
<%
    string mrdpath = "";
    string param = " /rcontype [RDAGENT] /rf http://rdserver.kwater.or.kr:8080/RDServer/rdagent.jsp /rsn [hdims]";
 %>
<object id=Rdviewer name=Rdviewer width=100% height=100% classid="clsid:ADB6D20D-80A1-4aa4-88AE-B2DC820DA076" codebase="<%=Page.ResolveUrl("~/Cab") %>/rdviewer50.cab#version=5,0,0,183"></object>
<script type="text/javascript">
    function getServerUrl() {
        var protocol = document.location.protocol;
        var domain = document.domain;
        var port = document.location.port;
        var url = "http://" + domain;
        if (port != "") url += ":" + port;
        return url;
    }
    var mrdPath = "/mrd/verify.mrd";
    var mrdUrl = getServerUrl() + mrdPath;
    //alert(mrdUrl);
    function rdOpen() {
        Rdviewer.AutoAdjust = true;
        Rdviewer.ZoomDefault();
        Rdviewer.ApplyLicense("http://rdserver.kwater.or.kr:8080/RDServer/rdagent.jsp");
        Rdviewer.FileOpen(mrdUrl, "<%=param%>");
    }
    document.getElementById("Rdviewer").height = document.documentElement.clientHeight - 125;
    
</script>
</div>
</asp:Content>
