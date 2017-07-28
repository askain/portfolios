<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EquipManagement
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PreHeadContent" runat="server">
<script src="../../Scripts/code/editgrid.js" type="text/javascript"></script>
<script src="../../Scripts/colorfield/colorfield.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">

    Ext.onReady(function () {
        Ext.BLANK_IMAGE_URL = '/Scripts/extjs/s.gif';
        Ext.tip.QuickTipManager.init();

        function setCursor(value, metaData, record, rowIndex, colIndex, store, view) {
            return '<div style="cursor:pointer;width:100%;">' + value + '</div>';
        }

        function getUseYN(value, metaData, record, rowIndex, colIndex, store) {
            return value == "Y" ? '<div style="cursor:pointer;width:100%;">' + "사용함" + '</div>' : '<div style="cursor:pointer;width:100%;">' + "사용안함" + '</div>';
        }

        function getColor(value, metaData, record, rowIndex, colIndex, store) {
            var retVal = '<div style="cursor:pointer;width:100%;background-color:#' + value + '">' + value + '</div>';
            return retVal;
        }

        function setGroupNm(value, metaData, record, rowIndex, colIndex, store, view) {
            var crec = equipGroupComboStore.findRecord("GROUPCD", value); 
            if (crec) {
                return '<div style="cursor:pointer;width:100%;">' + crec.get("GROUPNM") + '</div>';
            }
            return '미선택';
        }

        function setCommit() {
            var storeCount = equipManagementStore.getCount();
            for (var i = 0; i < storeCount; i++) {
                record = equipManagementStore.getAt(i);
                if (record.dirty == true) {
                    record.commit();
                }
            }
            equipManagementStore.load();
        }


        /* 그룹명 콤보 모델 정의 */
        var equipGroupComboModel = Ext.define('equipGroupComboModel', {
            extend: 'Ext.data.Model',
            idProperty: 'GROUPCD',
            fields: [
                { name: 'GROUPCD', type: 'string' },
                { name: 'GROUPNM', type: 'string' },
                { name: 'GROUPEXPLAIN', type: 'string' }
            ]
        });

        /* 리스트내 그룹명 저장소 정의 */
        var equipGroupComboStore = Ext.create('Ext.data.Store', {
            autoDestroy: true,
            model: 'equipGroupComboModel',
            proxy: {
                type: 'ajax',
                url: '<%=Page.ResolveUrl("~/Code")%>/GetEquipGroupCombo',
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
            },
            listeners: {
                load: function (store, records, successful, operation) {
                    if (records && records.length > 0) {
                    }
                }
            },
            autoLoad: true
        });


        var equipManagementColumns = [
        { id: 'ABCOLUMN', header: '<div style="text-align:center;">코드<span style="color:red;"> *</span></div>', flex: 1.0, minWidth: 100, sortable: false, align: 'left', dataIndex: 'ABCOLUMN', renderer: Ext.util.Format.uppercase,
            field: {
                type: 'textfield',
                allowBlank: false
            }
        }, { header: '<div style="text-align:center;">세부사항</div>', flex: 3.4, minWidth: 200, sortable: false, align: 'left', dataIndex: 'ABCONT', renderer: setCursor,
            field: {
                xtype: 'textfield'
            }
        }, { header: '사용여부', flex: 0.6, minWidth: 70, sortable: false, align: 'center', dataIndex: 'ABYN', renderer: getUseYN,
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
        }, { header: '색상', flex: 0.5, minWidth: 60, sortable: false, align: 'center', dataIndex: 'ABCOLOR', id: 'ABCOLOR', renderer: getColor,
            field: {
                xtype: 'colorfield',
                id: 'cf',
                name: 'cf',
                value: 'ffffff'
            }
        }, { header: '그룹명', flex: 0.7, minWidth: 90, align: 'center', dataIndex: 'GROUPCD', renderer: setGroupNm,
            field: {
                xtype: 'combobox',
                typeAhead: true,
                triggerAction: 'all',
                selectOnTab: true,
                valueField: 'GROUPCD',
                displayField: 'GROUPNM',
                store: equipGroupComboStore
            }
        }, { header: '<div style="text-align:center;">줄임말</div>', flex: 1.0, minWidth: 100, sortable: false, align: 'left', dataIndex: 'ABCOMMENT', renderer: setCursor,
            field: {
                type: 'textfield'
            }
        }];

        var equipManagementTbarItems = [
        {
            xtype: 'displayfield',
            id: 'dpf',
            name: 'dispfld2',
            width: 110,
            value: '<table><tr><td style="color:red;padding-bottom:2">* 추가시 필수입력</td></tr></table>'
        }
        ,
        '->',
        {
            icon: '<%=Page.ResolveUrl("~/Images") %>/icons/plus-circle-frame.png',
            text: '<span style="vertical-align:middle">추가</span>',
            itemId: 'add',
            tooltip: '행 추가',
            scope: this,
            handler: function () {
                var rec = new equipManagementModel({
                    ABCOLUMN: '',
                    ABCONT: '',
                    ABYN: 'Y',
                    ABCOLOR: 'FFFFFF',
                    GROUPCD: equipGroupComboStore.getAt(0).data.GROUPCD,
                    GROUPNM: equipGroupComboStore.getAt(0).data.GROUPNM,
                    ABCOMMENT: ''
                }), edit = equipManagementGrid.editing;

                rec.setDirty();
                defaultEditable: true;
                equipManagementStore.insert(0, rec);
                edit.startEditByPosition({
                    row: 0,
                    column: 0
                });
            }
        }, '-', {
            icon: '<%=Page.ResolveUrl("~/Images") %>/icons/minus-circle-frame.png',
            text: '<span style="vertical-align:middle">삭제</span>',
            disabled: true,
            itemId: 'delete',
            tooltip: '행 삭제',
            scope: this,
            handler: function () {
                var selection = equipManagementGrid.getSelectionModel().getSelection()[0];
                if (selection) {
                    equipManagementStore.remove(selection);
                }
            }
        }, '-', {
            icon: '<%=Page.ResolveUrl("~/Images") %>/icons/disk.png',
            text: '<span style="vertical-align:middle">저장</span>',
            itemId: 'save',
            tooltip: '데이터 저장',
            scope: this,
            handler: function () {

                var storeCount = equipManagementStore.getCount();
                for (var i = 0; i < storeCount; i++) {
                    record = equipManagementStore.getAt(i);
                    if (record.dirty == true) {
                        if (equipManagementStore.getAt(i).data.ABCOLUMN == '') {
                            Ext.Msg.alert("Message", "코드는 필수 입력 항목입니다.");
                            return;
                        }
                    }
                }
                //필수입력의 컬럼에서 다른 id로 포커스 이동 
                //뻘짓거리지만 필수입력 사항만 입력후 저장시 필수 입력의 포커스가 늦게 빠져서 저장이 원활하게 되지 않는 경우를 우회함 
                Ext.getCmp("dpf").focus(false);
                Ext.getCmp("dpf").setWidth(120);

                Ext.Msg.show({
                    title: 'Message',
                    msg: '데이터를 저장하시겠습니까?',
                    width: 150,
                    buttons: Ext.Msg.OKCANCEL,
                    icon: Ext.Msg.INFO,
                    fn: function (btn) {
                        if (btn == "ok") {
                            equipManagementStore.sync();
                        }
                    }
                });
            }
        }, '-', {
            icon: '<%=Page.ResolveUrl("~/Images") %>/icons/navigation-000-frame.png',
            text: '<span style="vertical-align:middle">조회</span>',
            itemId: 'search',
            tooltip: '다시 조회',
            scope: this,
            handler: function () {
                equipManagementStore.load();
            }
        }];

        Ext.define('equipManagementModel', {
            extend: 'Ext.data.Model',
            idProperty: 'ABCOLUMN',
            fields: [
                    { name: 'ABCOLUMN', type: 'string' },
                    { name: 'ABCONT', type: 'string' },
                    { name: 'ABYN', type: 'string' },
                    { name: 'ABCOLOR', type: 'string' },
                    { name: 'GROUPCD', type: 'string' },
                    { name: 'GROUPNM', type: 'string' },
                    { name: 'ABCOMMENT', type: 'string' }
           ]
        });

        //설비상태코드관리 Store
        var equipManagementStore = Ext.create('Ext.data.Store', {
            id: 'equipManagementStore',
            model: 'equipManagementModel',
            autoDestroy: true,
            autoLoad: true,
            autoSync: false,
            remoteSort: true,
            simpleSortMode: true,
            proxy: {
                type: 'ajax',
                api: {
                    read: '<%=Page.ResolveUrl("~/Code")%>/GetEquipManagementList',
                    create: '<%=Page.ResolveUrl("~/Code")%>/InsertEquipManagement',
                    update: '<%=Page.ResolveUrl("~/Code")%>/UpdateEquipmanagement',
                    destroy: '<%=Page.ResolveUrl("~/Code")%>/DeleteEquipManagement'
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

        var equipManagementGrid = Ext.create('Edit.Grid', {
            id: 'grid',
            name: 'grid',
            flex: 1,
            region: 'center',
            frame: true,
            columns: equipManagementColumns,
            store: equipManagementStore,
            tbar: equipManagementTbarItems,
            listeners: {
                beforeedit: function (editor, e, options) {
                    var commitStoreCount = equipManagementStore.getTotalCount();
                    var totalStoreCount = equipManagementStore.getCount();
                    var addRowCount = totalStoreCount - commitStoreCount;

                    if (editor.rowIdx >= addRowCount) {
                        if (editor.colIdx == 0) editor.cancel = true;
                    }
                },
                selectionchange: function (view, selections, options) {
                    this.down('#delete').setDisabled(selections.length === 0);
                }
            }
        });

        var mainViewport = Ext.create('Ext.Viewport', {
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
            }, equipManagementGrid]
        });
    });

</script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<div id="menu-title" class="title_box x-hide-display" align="center" style="text-align:left;">
	<span style="font-weight:bold; margin-left:10px;">
    <img src="<%=Page.ResolveUrl("/Images") %>/icons/gear.png" align="absmiddle"/>&nbsp;&nbsp;
    설비상태코드 관리
    </span>
</div>
</asp:Content>
