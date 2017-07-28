<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="HDIMS.Utils.Login"%>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	관측국 관리
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PreHeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<%
    EmpData empdata = new EmpData();

    string MGTDAM = empdata.GetEmpData(6).Trim();
%>
<script type="text/javascript">
    var MGTDAM = '<%=MGTDAM %>';
    Ext.onReady(function () {
        Ext.BLANK_IMAGE_URL = '/Scripts/extjs/s.gif';
        Ext.tip.QuickTipManager.init();

        function loadObsList() {
            var damTp = Ext.getCmp("comboDamType").getValue();
            var damCd = Ext.getCmp('comboDamNm').getValue();
            var searchTerm = Ext.getCmp('comboSearchTerm').getValue();
            var searchText = Ext.getCmp('txtSearch').getValue();

            if (searchTerm != 'ALL' && searchText == '') {
                Ext.Msg.alert('message', '검색어를 입력하세요.');
                return;
            }

            obsStore.proxy.extraParams = {
                damTp: damTp,
                damCd: damCd,
                term: searchTerm,
                txt: searchText,
                start: 0
            };
            obsStore.loadPage(1);
        }

        function setCommit() {
            var storeCount = obsStore.getCount();
            for (var i = 0; i < storeCount; i++) {
                record = obsStore.getAt(i);
                if (record.dirty == true) {
                    record.commit();
                }
            }
            loadObsList();
        }

        function getUseYN(val, meta, rec, row, col, store) {
            if (val == "Y") {
                return "<input type='checkbox' checked onclick='return false'/>";
            } else {
                return "<input type='checkbox' onclick='return false'/>";
            }
        }

        function getReportYN(val, meta, rec, row, col, store) {
            if (val == "Y") {
                return "<input type='checkbox' checked onclick='return false'/>";
            } else {
                return "<input type='checkbox' onclick='return false'/>";
            }
        }

        function setDisplay(value, metaData, record, rowIndex, colIndex, store, view) {
            var retVal;

            switch (value) {
                case 'WL':
                    retVal = '수위국';
                    break;
                case 'RF':
                    retVal = '우량국';
                    break;
                case 'WR':
                    retVal = '수위우량국';
                    break;
                case 'AL':
                    retVal = '경보국';
                    break;
                case 'QL':
                    retVal = '수질국';
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
        var obsModel = Ext.define('obsModel', {
            extend: 'Ext.data.Model',
            idProperty: 'OBSCD',
            fields: [
                { name: 'DAMCD', type: 'string' },
                { name: 'DAMNM', type: 'string' },
                { name: 'OBSCD', type: 'string' },
                { name: 'OBSNM', type: 'string' },
                { name: 'USEYN', type: 'string' },
                { name: 'REPORTYN', type: 'string' },
                { name: 'OBSTP', type: 'string' }
            ]
        });

        /* 관측국 프록시 정의 */
        var obsProxy =
        {
            type: 'ajax',
            api: {
                read: '<%=Page.ResolveUrl("~/DamBoObsMng")%>/GetObsList',
                create: '<%=Page.ResolveUrl("~/DamBoObsMng")%>/SaveObsMst',
                update: '<%=Page.ResolveUrl("~/DamBoObsMng")%>/SaveObsMst'
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
        var obsStore = Ext.create('Ext.data.Store', {
            id: 'obsStore',
            model: 'obsModel',
            autoDestroy: true,
            autoLoad: true,
            proxy: obsProxy,
            pageSize: 50,
            remoteSort: true,
            reader: new Ext.data.JsonReader({
                root: 'data',
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
        var obsColumns =
        [
            { header: '댐명', flex: 0.5, minWidth: 60, dataIndex: 'DAMNM', align: 'center' },
            { header: '관측국명', flex: 0.5, minWidth: 60, dataIndex: 'OBSNM', align: 'center' },
            { header: '관측국코드', flex: 0.5, minWidth: 60, dataIndex: 'OBSCD', align: 'center' },
            { header: '구분', flex: 0.6, minWidth: 70, dataIndex: 'OBSTP', align: 'center', renderer: setDisplay },
            { header: '검색대상', flex: 0.5, minWidth: 60, dataIndex: 'USEYN', align: 'center', renderer: getUseYN },
            { header: '분석대상', flex: 0.5, minWidth: 60, dataIndex: 'REPORTYN', align: 'center', renderer: getReportYN }
        ];

        /* 관측국 그리드 tbar 아이템 정의 */
        var obsTbarItems =
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
                    ['ALL', '전 체'],
                    ['DAMNM', '댐명'],
                    ['OBSNM', '관측소명']
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
            {
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/disk.png',
                text: '<span style="vertical-align:middle">저장</span>',
                itemId: 'save',
                tooltip: '데이터 저장',
                handler: function () {
                    Ext.Msg.show({
                        title: 'Message',
                        msg: '데이터를 저장하시겠습니까?',
                        width: 150,
                        buttons: Ext.Msg.OKCANCEL,
                        icon: Ext.Msg.INFO,
                        fn: function (btn) {
                            if (btn == "ok") {
                                obsStore.sync();
                            }
                        }
                    });
                }
            },
            '->'
        ];


        /* 관측국 그리드 정의 */
        var obsGrid = Ext.create('Ext.grid.Panel', {
            id: 'obsGrid',
            name: 'obsGrid',
            frame: true,
            flex: 1,
            region: 'center',
            columns: obsColumns,
            store: obsStore,
            tbar: obsTbarItems,
            bbar: Ext.create('Ext.PagingToolbar', {
                store: obsStore,
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

        obsGrid.on("cellclick", function (gg, cell, colIndex, model) {
            //추가 : 자기 댐이 아닌경우는 수정이 안돼도록 함.
            var rowItem = this.getStore().getAt(gg.indexOf(model));
            if (MGTDAM != 'MAIN' && MGTDAM.indexOf(rowItem.get('DAMCD')) == -1) {
                return false;
            }

            if (colIndex == 4) {
                var val = model.get("USEYN");
                if (val == "Y") model.set("USEYN", "N");
                else model.set("USEYN", "Y");
            } else if (colIndex == 5) {
                var val = model.get("REPORTYN");
                if (val == "Y") model.set("REPORTYN", "N");
                else model.set("REPORTYN", "Y");
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
            }, obsGrid] // 랜더링이 되는곳 정의
        });

    });

</script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<div id="menu-title" class="title_box x-hide-display" align="center" style="text-align:left;">
	<span style="font-weight:bold; margin-left:10px;">
    <img src="<%=Page.ResolveUrl("~/Images") %>/icons/gear.png" align="absmiddle"/>&nbsp;&nbsp;
    관측국 관리
    </span>
</div>
</asp:Content>
