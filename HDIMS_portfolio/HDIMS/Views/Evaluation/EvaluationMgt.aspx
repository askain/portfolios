<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	이상자료 유무 이력
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PreHeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript" src="/Scripts/common/renderers.js"></script>
<script type="text/javascript">
    var currDate = new Date();
    var prevDate = Ext.Date.add(currDate, Ext.Date.HOUR, -2);

    Ext.onReady(function () {
        Ext.BLANK_IMAGE_URL = '/Scripts/extjs/s.gif';
        Ext.tip.QuickTipManager.init();

        Ext.apply(Ext.form.field.VTypes, {
            daterange: function (val, field) {
                var date = field.parseDate(val);

                if (!date) {
                    return false;
                }
                if (field.startDateField && (!this.dateRangeMax || (date.getTime() != this.dateRangeMax.getTime()))) {
                    var start = field.up().down('#' + field.startDateField);
                    start.setMaxValue(date);
                    start.validate();
                    this.dateRangeMax = date;
                }
                else if (field.endDateField && (!this.dateRangeMin || (date.getTime() != this.dateRangeMin.getTime()))) {
                    var end = field.up().down('#' + field.endDateField);
                    end.setMinValue(date);
                    end.validate();
                    this.dateRangeMin = date;
                }

                return true;
            },

            daterangeText: '시작날짜가 끝날짜보다 커야 합니다.'
        });

        function loadObsList() {
            var damTp = Ext.getCmp("comboDamType").getValue();
            var damCd = Ext.getCmp('comboDamNm').getValue();
            var startDt = Ext.util.Format.date(Ext.getCmp('startdt').getValue(), 'Ymd');
            var endDt = Ext.util.Format.date(Ext.getCmp('enddt').getValue(), 'Ymd');
            var searchTerm = Ext.getCmp('comboSearchTerm').getValue();
            var searchText = Ext.getCmp('txtSearch').getValue();

            if (searchTerm != 'ALL' && searchText == '') {
                Ext.Msg.alert('message', '검색어를 입력하세요.');
                return;
            }

            dataStore.proxy.extraParams = {
                damTp: damTp,
                damcd: damCd,
                startdt: startDt,
                enddt: endDt,
                term: searchTerm,
                txt: searchText,
                start: 0
            };
            dataStore.loadPage(1);
        }

        //        function setCommit() {
        //            var storeCount = dataStore.getCount();
        //            for (var i = 0; i < storeCount; i++) {
        //                record = dataStore.getAt(i);
        //                if (record.dirty == true) {
        //                    record.commit();
        //                }
        //            }
        //            loadObsList();
        //        }

        function renderChkYn(val, meta, rec, row, col, store) {
            if (val == "Y") {
                return "정상";
            } else if (val == "N") {
                return "이상";
            } else {
                return "미등록";
            }
        }

        function renderDataTp(value, metaData, record, rowIndex, colIndex, store, view) {
            var retVal;
            switch (value) {
                case 'D':
                    retVal = '댐운영자료';
                    break;
                case 'R':
                    retVal = '우량';
                    break;
                case 'W':
                    retVal = '수위';
                    break;
            }

            return retVal;
        }

        ////////////////////////////////////////////////////  댐, 관측국, 구분 콤보 조회 시작  //////////////////////////////////////////////////// 
        var damTypeComboModel = Ext.define('damTypeComboModel', {
            extend: 'Ext.data.Model',
            idProperty: 'DAMTYPE',
            fields: [
                { name: 'DAMTYPE', type: 'string' },
                { name: 'DAMTPNM', type: 'string' }
           ]
        });

        var damTypeComboProxy =
        {
            type: 'ajax',
            url: '<%=Page.ResolveUrl("~/DamBoObsMng")%>/GetDamTypeCombo',
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

        // Tbar의 댐구분 콤보
        var damTypeComboStore = Ext.create('Ext.data.Store', {
            id: 'damTypeComboStore',
            model: 'damTypeComboModel',
            autoDestroy: true,
            remoteSort: true,
            autoLoad: true,
            autoSync: false,
            simpleSortMode: true,
            proxy: damTypeComboProxy,
            listeners: {
                load: function (store, records, successful, operation) {
                    if (records && records.length > 0) {
                        Ext.getCmp('comboDamType').setValue(records[0].data.DAMTYPE);
                    }
                }
            },
            sorters: [{ property: 'DAMTYPE', direction: 'ASC'}],
            autoLoad: true
        });

        var damTypeModel = Ext.define('damTypeModel', {
            extend: 'Ext.data.Model',
            idProperty: 'DAMTYPE',
            fields: [
                { name: 'DAMTYPE', type: 'string' },
                { name: 'DAMTPNM', type: 'string' }
           ]
        });

        var damTypeProxy =
        {
            type: 'ajax',
            url: '<%=Page.ResolveUrl("~/DamBoObsMng")%>/GetDamType/?type=all',
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

        //리스트 안의 댐구분 콤보
        var damTypeStore = Ext.create('Ext.data.Store', {
            id: 'damTypeStore',
            model: 'damTypeModel',
            autoDestroy: true,
            remoteSort: true,
            autoLoad: false,
            autoSync: false,
            simpleSortMode: true,
            proxy: damTypeProxy,
            listeners: {
                load: function (store, records, successful, operation) {
                    if (records && records.length > 0) {

                    }
                }
            },
            sorters: [{ property: 'DAMTYPE', direction: 'ASC'}]
        });

        /* 코드 모델 정의 - Ext.define() 함수를 이용한 클래스 정의 */
        var CodeModel = Ext.define('CodeModel', {
            extend: 'Ext.data.Model',
            fields: [
                { name: 'KEY', type: 'string' },
                { name: 'VALUE', type: 'string' },
                { name: 'ORDERNUM', type: 'int' }
            ]
        });

        var damCodeProxy =
        {
            type: 'ajax',
            url: encodeURI('<%=Page.ResolveUrl("~/DamBoObsMng")%>/DamCodeList/'),
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

        /* 댐 콤보 저장소 정의 */
        var damCodeStore = Ext.create('Ext.data.Store', {
            autoDestroy: true,
            model: 'CodeModel',
            proxy: damCodeProxy,
            listeners: {
                load: function (store, records, successful, operation) {
                    if (records && records.length > 0) {
                        Ext.getCmp('comboDamNm').setValue(records[0].data.KEY);
                    }
                }
            },
            sorters: [{ property: 'ORDERNUM', direction: 'ASC'}],
            autoLoad: false
        });
        damCodeStore.load({
            params: {
                firstValue: "전체"
            }
        });

        ////////////////////////////////////////////////////  댐, 관측국, 구분 콤보 조회 종료  //////////////////////////////////////////////////// 

        /* 관측국 모델 정의 */
        var dataModel = Ext.define('dataModel', {
            extend: 'Ext.data.Model',
            idProperty: 'ID',
            fields: [
                { name: 'OBSDT', type: 'string' },
                { name: 'DAMCD', type: 'string' },
                { name: 'DAMNM', type: 'string' },
                { name: 'DATATP', type: 'string' },
                { name: 'EMPNO', type: 'string' },
                { name: 'EMPNM', type: 'string' },
                { name: 'CHKYN', type: 'string' },
                { name: 'CHKOBSDT', type: 'string' },
                { name: 'NOTE', type: 'string' }
            ]
        });

        /* 관측국 프록시 정의 */
        var obsProxy =
        {
            type: 'ajax',
            api: {
                read: '<%=Page.ResolveUrl("~/Evaluation")%>/GetEvaluationList',
                create: '<%=Page.ResolveUrl("~/Evaluation")%>/GetEvaluationList',
                update: '<%=Page.ResolveUrl("~/Evaluation")%>/GetEvaluationList'
            },
            reader: {
                type: 'json',
                root: 'Data'
            },
            writer: {
                allowSingle: false,
                writeAllFields: true,
                root: 'Data'
            },
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

        /* 관측국 저장소 정의 */
        var dataStore = Ext.create('Ext.data.Store', {
            id: 'dataStore',
            model: 'dataModel',
            autoDestroy: true,
            proxy: obsProxy,
            pageSize: 50,
            remoteSort: true,
            reader: new Ext.data.JsonReader({
                root: 'Data',
                totalProperty: 'total',
                id: 'ID'
            }),
            listeners: {
                add: function (store, records, operation) {
                },
                update: function (records, operation) {
                },
                remove: function (proxy, operation) {
                },
                write: function (proxy, operation) {
                    if (operation.action == 'read') {
                    }
                    if (operation.action == 'create') {
                        setCommit();
                    }
                    if (operation.action == 'update') {
                        setCommit();
                    }
                    if (operation.action == 'destroy') {
                        setCommit();
                    }
                }
            }
        });

        /* 관측국 그리드 컬럼 정의 */
        var dataColumns =
        [
            { header: '확일일자', flex: 0.5, minWidth: 60, dataIndex: 'OBSDT', align: 'center', renderer: formatDate },
            { header: '댐명', flex: 0.5, minWidth: 60, dataIndex: 'DAMNM', align: 'center' },
            { header: '구분', flex: 0.5, minWidth: 60, dataIndex: 'DATATP', align: 'center', renderer: renderDataTp },
            { header: '담당자명', flex: 0.6, minWidth: 70, dataIndex: 'EMPNM', align: 'center' },
            { header: '확인여부', flex: 0.6, minWidth: 70, dataIndex: 'CHKYN', align: 'center', renderer: renderChkYn },
            { header: '확인일시', flex: 0.5, minWidth: 60, dataIndex: 'CHKOBSDT', align: 'center', renderer: formatDate },
            { header: '비고', flex: 0.5, minWidth: 60, dataIndex: 'NOTE', align: 'center' }
        ];

        /* 관측국 그리드 tbar 아이템 정의 */
        var TbarItems =
        [
            {
                xtype: 'combobox',
                id: 'comboDamType',
                fieldLabel: '댐구분',
                labelWidth: 60,
                labelAlign: 'right',
                width: 190,
                typeAhead: true,
                triggerAction: 'all',
                selectOnTab: true,
                valueField: 'DAMTYPE',
                displayField: 'DAMTPNM',
                store: damTypeComboStore,
                queryMode: 'local',
                listeners: {
                    change: function (field, newValue, oldValue, options) {
                        //alert(newValue);
                        if (newValue != "") {
                            damCodeStore.load({
                                params: {
                                    firstValue: "전체",
                                    DamType: newValue
                                }
                            });
                        } else {
                            damCodeStore.load({
                                params: {
                                    firstValue: "전체"
                                }
                            });
                        }

                    }
                }
            },
            {
                xtype: 'combobox',
                id: 'comboDamNm',
                fieldLabel: '댐명',
                labelWidth: 60,
                labelAlign: 'right',
                width: 190,
                typeAhead: true,
                triggerAction: 'all',
                selectOnTab: true,
                valueField: 'KEY',
                displayField: 'VALUE',
                store: damCodeStore,
                queryMode: 'local',
                listeners: {
                    change: function (field, newValue, oldValue, options) {
                    }
                }
            }, ' ',
            {
                xtype: 'datefield'
                    , name: 'startdt'
                    , id: 'startdt'
                    , width: 90
                    , vtype: 'daterange'
                    , endDateField: 'enddt'
                    , format: 'Y-m-d'
                    , align: 'right'
            }, {
                xtype: 'displayfield'
                    , name: 'dispfld01'
                    , value: '~'
                    , width: 10
            }, {
                xtype: 'datefield'
                    , name: 'enddt'
                    , id: 'enddt'
                    , width: 90
                    , vtype: 'daterange'
                    , startDateField: 'startdt'
                    , format: 'Y-m-d'
                    , align: 'right'
            }, ' ',
            {
                xtype: 'combobox',
                id: 'comboSearchTerm',
                fieldLabel: '검색조건',
                labelWidth: 60,
                labelAlign: 'right',
                width: 190,
                typeAhead: true,
                triggerAction: 'all',
                selectOnTab: true,
                value: 'ALL',
                store: [
                    ['EMPNM', '담당자명'],
                    ['EMPNO', '담당자번호']
                ],
                queryMode: 'local',
                listeners: {
                    change: function (field, newValue, oldValue, options) {
                    }
                }
            }, ' ',
            {
                xtype: 'textfield',
                id: 'txtSearch',
                name: 'txtSearch',
                tabIndex: 1
            },
            {
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/navigation-000-frame.png',
                text: '<span style="vertical-align:middle">조회</span>',
                itemId: 'search',
                tooltip: '조회',
                handler: function () {
                    loadObsList();
                }
            }, ' ',
        //            {
        //                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/disk.png',
        //                text: '<span style="vertical-align:middle">저장</span>',
        //                itemId: 'save',
        //                tooltip: '데이터 저장',
        //                handler: function () {
        //                    Ext.Msg.show({
        //                        title: 'Message',
        //                        msg: '데이터를 저장하시겠습니까?',
        //                        width: 150,
        //                        buttons: Ext.Msg.OKCANCEL,
        //                        icon: Ext.Msg.INFO,
        //                        fn: function (btn) {
        //                            if (btn == "ok") {
        //                                dataStore.sync();
        //                            }
        //                        }
        //                    });
        //                }
        //            },
            '->'
        ];


        /* 관측국 그리드 정의 */
        var dataGrid = Ext.create('Ext.grid.Panel', {
            id: 'dataGrid',
            name: 'dataGrid',
            frame: true,
            flex: 1,
            region: 'center',
            columns: dataColumns,
            store: dataStore,
            tbar: TbarItems,
            bbar: Ext.create('Ext.PagingToolbar', {
                store: dataStore,
                displayInfo: true,
                displayMsg: '{0} - {1} of {2}',
                emptyMsg: "검색결과가 없습니다."
            }),
            columnLines: true,
            listeners: {
                selectionchange: function (view, selections, options) {
                }
            }
        });

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
                height: 35,
                border: 0,
                layout: {
                    type: 'vbox',
                    align: 'stretch'
                },
                items: [{
                    height: 35,
                    border: 0,
                    padding: '0 20 5 5',
                    contentEl: 'menu-title'
                }]
            }, dataGrid] // 랜더링이 되는곳 정의
        });

        Ext.getCmp('enddt').setValue(currDate);
        Ext.getCmp('startdt').setValue(prevDate);
        loadObsList();
    });

</script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<div id="menu-title" class="title_box x-hide-display" align="center" style="text-align:left;">
	<span style="font-weight:bold; margin-left:10px;">
    <img src="<%=Page.ResolveUrl("~/Images") %>/icons/gear.png" align="absmiddle"/>&nbsp;&nbsp;
    이상자료 유무 이력
    </span>
</div>
</asp:Content>
