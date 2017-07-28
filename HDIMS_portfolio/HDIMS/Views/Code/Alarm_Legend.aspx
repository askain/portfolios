<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	AlarmManagement
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
            alias: 'widget.alarmGrid',

            initComponent: function () {

                this.editing = Ext.create('Ext.grid.plugin.CellEditing');

                Ext.apply(this, {
                    frame: true,
                    columns: [
                        { id: 'ALARMCD', header: '알람코드', flex: 0.4, minWidth: 50, sortable: false, align: 'center', dataIndex: 'ALARMCD',
                            field: {
                                type: 'numberfield'
                            }
                        }, { id: 'ALARMNM', header: '알람명', flex: 0.4, minWidth: 50, sortable: false, align: 'center', dataIndex: 'ALARMNM',
                            field: {
                                type: 'textfield'
                            }
                        }, { header: '알람설명', flex: 3.8, minWidth: 200, sortable: false, align: 'left', dataIndex: 'ALARMCONT',
                            field: {
                                xtype: 'textfield'
                            }
                        }],
                    listeners: {
                        beforeedit: function (editor, e, options) {
                            var commitStoreCount = alarmStore.getTotalCount();
                            var totalStoreCount = alarmStore.getCount();
                            var addRowCount = totalStoreCount - commitStoreCount;

                            if (editor.rowIdx >= addRowCount) {
                                if (editor.colIdx == 0) editor.cancel = true;
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
                            alarmStore.sync();
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
                alarmStore.load();
            },

            onAddClick: function () {

                var rec = new alarmManagementModel({
                    ALARMCD: '',
                    ALARMCONT: '',
                    ALARMNM: ''
                }), edit = this.editing;

                this.store.insert(0, rec);
                edit.startEditByPosition({
                    row: 0,
                    column: 0
                });
            }
        });

        function getUseYN(value, metaData, record, rowIndex, colIndex, store) {
            return value == "Y" ? "사용함" : "사용안함";
        }

        function getColor(value, metaData, record, rowIndex, colIndex, store) {
            var retVal = '<div style="background-color:#' + value + '">' + value + '</div>';
            return retVal;
        }

        function setCommit() {
            var storeCount = alarmStore.getCount();
            for (var i = 0; i < storeCount; i++) {
                record = alarmStore.getAt(i);
                if (record.dirty == true) {
                    record.commit();
                }
            }
            alarmStore.load();
        }

        Ext.define('alarmManagementModel', {
            extend: 'Ext.data.Model',
            idProperty: 'ALARMCD',
            fields: [
                    { name: 'ALARMCD', type: 'number' },
                    { name: 'ALARMCONT', type: 'string' },
                    { name: 'ALARMNM', type: 'string' }
           ],
            validations: [
                { type: 'length', field: 'ALARMCD', min: 0, max: 3 }
           ]
        });

        var alarmStore = Ext.create('Ext.data.Store', {
            id: 'alarmStore',
            model: 'alarmManagementModel',
            autoDestroy: true,
            remoteSort: true,
            autoLoad: true,
            autoSync: false,
            simpleSortMode: true,
            proxy: {
                type: 'ajax',
                api: {
                    read: '<%=Page.ResolveUrl("~/Code")%>/GetAlarmManagementList',
                    create: '<%=Page.ResolveUrl("~/Code")%>/InsertAlarmManagement',
                    update: '<%=Page.ResolveUrl("~/Code")%>/UpdateAlarmManagement',
                    destroy: '<%=Page.ResolveUrl("~/Code")%>/DeleteAlarmManagement'
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
                height: 35,
                border: 3,
                layout: {
                    type: 'vbox'
                    , align: 'stretch'
                },
                items: [{
                    height: 35,
                    border: 0,
                    padding: '0 20 5 5',
                    contentEl: 'menu-title'
                }]
            }, {
                id: 'grid',
                name: 'grid',
                xtype: 'alarmGrid',
                flex: 1,
                region: 'center',
                store: alarmStore,
                listeners: {
                    selectionchange: function (selModel, selected) {
                    }
                }
            }]
        });
    });

</script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div id="menu-title" class="title_box x-hide-display" align="center" style="text-align:left;">
	<span style="font-weight:bold; margin-left:10px;">
    <img src="<%=Page.ResolveUrl("/Images") %>/icons/gear.png" align="absmiddle"/>&nbsp;&nbsp;
    댐운영자료 알람관리
    </span>
</div>
</asp:Content>
