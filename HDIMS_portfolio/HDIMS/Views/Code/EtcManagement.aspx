<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EtcManagement
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
<script src="../../Scripts/colorfield/colorfield.js" type="text/javascript"></script>
<script type="text/javascript">
    var rowindex;

    Ext.onReady(function () {
        Ext.BLANK_IMAGE_URL = '/Scripts/extjs/s.gif';
        Ext.tip.QuickTipManager.init();

        Ext.define('Writer.Grid', {
            extend: 'Ext.grid.Panel',
            alias: 'widget.etcGrid',

            initComponent: function () {

                this.editing = Ext.create('Ext.grid.plugin.CellEditing');

                Ext.apply(this, {
                    frame: true,
                    plugins: [this.editing],
                    dockedItems: [{
                        xtype: 'toolbar',
                        items: ['->', 
                        {
                            icon: '<%=Page.ResolveUrl("~/Images") %>/icons/plus-circle-frame.png',
                            text: '<span style="vertical-align:middle">추가</span>',
                            itemId: 'add',
                            tooltip: '행 추가',
                            scope: this,
                            handler: this.onAddClick
                        }, '-', {
                            icon: '<%=Page.ResolveUrl("~/Images") %>/icons/minus-circle-frame.png',
                            text: '<span style="vertical-align:middle">삭제</span>',
                            disabled: true,
                            itemId: 'delete',
                            tooltip: '행 삭제',
                            scope: this,
                            handler: this.onDeleteClick
                        }, '-', {
                            icon: '<%=Page.ResolveUrl("~/Images") %>/icons/disk.png',
                            text: '<span style="vertical-align:middle">저장</span>',
                            itemId: 'save',
                            tooltip: '데이터 저장',
                            scope: this,
                            handler: this.onSaveClick
                        }, '-', {
                            icon: '<%=Page.ResolveUrl("~/Images") %>/icons/navigation-000-frame.png',
                            text: '<span style="vertical-align:middle">조회</span>',
                            itemId: 'search',
                            tooltip: '다시 조회',
                            scope: this,
                            handler: this.onSearchClick
                        }]
                    }],
                    columns: [
                        { id: 'ETCCD', header: '코드', flex: 0.4, minWidth: 80, sortable: false, align: 'center', dataIndex: 'ETCCD',
                            field: {
                                type: 'textfield'
                            }
                        }, { text: '구분', flex: 0.6, minWidth: 60, sortable: false, align: 'center', dataIndex: 'ETCTP', renderer: getUseTP,
                            field: {
                                xtype: 'combobox',
                                typeAhead: true,
                                triggerAction: 'all',
                                selectOnTab: true,
                                lazyRender: true,
                                store: [
                                    ['W', '수위'],
                                    ['R', '우량'],
                                    ['C', '공통']
                                ]
                            }
                        }, { text: '<div style="text-align:center;">범례요약</div>', flex: 2.0, minWidth: 100, sortable: false, align: 'left', dataIndex: 'ETCTITLE',
                            field: {
                                xtype: 'textfield'
                            }
                        }, { header: '<div style="text-align:center;">내용</div>', flex: 3.4, minWidth: 150, sortable: false, align: 'left', dataIndex: 'ETCCONT',
                            field: {
                                xtype: 'textfield'
                            }
                        }, { header: '사용여부', flex: 0.6, minWidth: 60, sortable: false, align: 'center', dataIndex: 'ETCYN', renderer: getUseYN,
                            field: {
                                xtype: 'combobox',
                                typeAhead: true,
                                triggerAction: 'all',
                                selectOnTab: true,
                                store: [
                                    ['Y', '사용함'],
                                    ['N', '사용안함']
                                ],
                                lazyRender: true
                            }
                        }, { header: '색상', flex: 0.5, minWidth: 60, sortable: false, align: 'center', dataIndex: 'ETCCOLOR', id: 'ETCCOLOR', renderer: getColor,
                            field: {
                                xtype: 'colorfield',
                                id: 'cf',
                                name: 'cf',
                                value: 'ffffff'
                            }
                        }],
                         listeners: {
                             beforeedit: function (editor, e, options) {
                                var commitStoreCount = etcStore.getTotalCount();
                                var totalStoreCount = etcStore.getCount();
                                var addRowCount = totalStoreCount - commitStoreCount;

                                if (editor.rowIdx >= addRowCount) {
                                    if (editor.colIdx == 0 || editor.colIdx == 1) editor.cancel = true;
                                }
                                rowindex = editor.rowIdx;
                            }
                        }
                });
                this.callParent();
                this.getSelectionModel().on('selectionchange', this.onSelectChange, this);
            },

            onSelectChange: function (selModel, selections) {
                this.down('#delete').setDisabled(selections.length === 0);
            },

            onSaveClick: function () {

                Ext.Msg.show({
                    title: 'Message',
                    msg: '데이터를 저장하시겠습니까?',
                    width: 150,
                    buttons: Ext.Msg.OKCANCEL,
                    icon: Ext.Msg.INFO,
                    fn: function (btn) {
                        if (btn == "ok") {
                            etcStore.sync();
                        }
                    }
                });
            },

            onDeleteClick: function () {
                var selection = this.getView().getSelectionModel().getSelection()[0];
                if (selection) {
                    this.store.remove(selection);
                }
            },

            onSearchClick: function () {
                etcStore.load();
            },

            onAddClick: function () {

                var rec = new etcManagementModel({
                    ETCCD: '',
                    ETCTP: 'C',
                    ETCTITLE: '',
                    ETCCONT: '',
                    ETCYN: 'Y',
                    ETCCOLOR: 'FFFFFF'
                }), edit = this.editing;

                this.store.insert(0, rec);
                edit.startEditByPosition({
                    row: 0,
                    column: 0
                });
            }
        });

        function getUseTP(value, metaData, record, rowIndex, colIndex, store) {
            var val;
            switch (value) {
                case "W":
                    val = '<div style="color:blue">수위</div>';
                    break;
                case "R":
                    val = '<div style="color:green">우량</div>';
                    break;
                case "C":
                    val = "공통";
                    break;
            }
            return val;
        }

        function getUseYN(value, metaData, record, rowIndex, colIndex, store) {
            return value == "Y" ? "사용함" : "사용안함";
        }

        function getColor(value, metaData, record, rowIndex, colIndex, store) {
            var retVal = '<div style="background-color:#' + value + '">' + value + '</div>';
            return retVal;
        }

        function setCommit() {
            var storeCount = etcStore.getCount();
            for (var i = 0; i < storeCount; i++) {
                record = etcStore.getAt(i);
                if (record.dirty == true) {
                    record.commit();
                }
            }
            etcStore.load();
        }

        Ext.define('etcManagementModel', {
            extend: 'Ext.data.Model',
            idProperty: 'ETCCD',
            fields: [
                    { name: 'ETCCD', type: 'string' },
                    { name: 'ETCTP', type: 'string' },
                    { name: 'ETCTITLE', type: 'string' },
                    { name: 'ETCCONT', type: 'string' },
                    { name: 'ETCYN', type: 'string' },
                    { name: 'ETCCOLOR', type: 'string' }
           ]
            //            validations: [{
            //                type: 'length',
            //                field: 'EXCD',
            //                min: 1
            //            }, {
            //                type: 'length',
            //                field: 'EXTP',
            //                min: 1
            //            }, {
            //                type: 'length',
            //                field: 'EXORD',
            //                min: 1
            //            }, {
            //                type: 'length',
            //                field: 'EXCONT',
            //                min: 1
            //            }, {
            //                type: 'length',
            //                field: 'EXCOLOR',
            //                min: 1
            //            }, {
            //                type: 'length',
            //                field: 'EXYN',
            //                min: 1
            //            }, {
            //                type: 'length',
            //                field: 'EXNOTE',
            //                min: 1
            //            }],
        });

        var etcStore = Ext.create('Ext.data.Store', {
            id: 'etcStore',
            model: 'etcManagementModel',
            autoDestroy: true,
            remoteSort: true,
            autoLoad: true,
            autoSync: false,
            simpleSortMode: true,
            proxy: {
                type: 'ajax',
                api: {
                    read: '<%=Page.ResolveUrl("~/Code")%>/GetEtcManagementList',
                    create: '<%=Page.ResolveUrl("~/Code")%>/InsertEtcManagement',
                    update: '<%=Page.ResolveUrl("~/Code")%>/UpdateEtcManagement',
                    destroy: '<%=Page.ResolveUrl("~/Code")%>/DeleteEtcManagement'
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
            },
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

        var main = Ext.create('Ext.Viewport', {
            layout: {
                type: 'border'
            , padding: 0
            },
            border: 0,
            renderTo: Ext.getBody(),
            items: [{
                region: 'north',
                height: 45,
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
                }]
            }, {
                id: 'grid',
                name: 'grid',
                xtype: 'etcGrid',
                flex: 1,
                region: 'center',
                store: etcStore,
                listeners: {
                    selectionchange: function (selModel, selected) {
                    }
                }
            }]
        });
    });

</script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<div id="menu-title" class="title_box x-hide-display" align="center" style="text-align:left;">
	<span style="font-weight:bold; margin-left:10px;">
    <img src="<%=Page.ResolveUrl("/Images") %>/icons/gear.png" align="absmiddle"/>&nbsp;&nbsp;
    기타범례 관리
    </span>
</div>
</asp:Content>
