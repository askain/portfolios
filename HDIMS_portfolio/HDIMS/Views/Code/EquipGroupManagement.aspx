<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EquipGroupManagement
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PreHeadContent" runat="server">
<script src="../../Scripts/code/editgrid.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

<script type="text/javascript">
    var equipGroupCd;

    Ext.onReady(function () {
        Ext.BLANK_IMAGE_URL = '/Scripts/extjs/s.gif';
        Ext.tip.QuickTipManager.init();

        // ******** function 시작 ******** // 

        function setCursor(value, metaData, record, rowIndex, colIndex, store, view) {
            return '<div style="cursor:pointer;width:100%;">' + value + '</div>';
        }

        function setCommit() {
            var storeCount = equipGroupStore.getCount();
            for (var i = 0; i < storeCount; i++) {
                record = equipGroupStore.getAt(i);
                if (record.dirty == true) {
                    record.commit();
                }
            }
            equipGroupStore.load();
        }

        function setGroupCDNum() {
            var number;
            var storeCount = equipGroupStore.getCount();
            for (var i = 0; i < storeCount; i++) {
                record = equipGroupStore.getAt(i).data.GROUPCD;
                if (storeCount < 9) {
                    number = "0" + (storeCount + 1);
                }
                else {
                    number = storeCount + 1;
                }
            }
            return number;
        }

        // ******** function 종료 ******** // 


        /* 모델 정의 */
        Ext.define('equipGroupModel', {
            extend: 'Ext.data.Model',
            idProperty: 'GROUPCD',
            fields: [
                { name: 'GROUPCD', type: 'string' },
                { name: 'GROUPNM', type: 'string' },
                { name: 'GROUPEXPLAIN', type: 'string' }
            ]
        });

        var equipGroupProxy =
        { type: 'ajax',
            api: {
                read: '<%=Page.ResolveUrl("~/Code")%>/GetEquipGroupManagementList',
                create: '<%=Page.ResolveUrl("~/Code")%>/InsertUpdateEquipGroupManagement',
                update: '<%=Page.ResolveUrl("~/Code")%>/InsertUpdateEquipGroupManagement',
                destroy: '<%=Page.ResolveUrl("~/Code")%>/DeleteEquipGroupManagement'
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

        /* 저장소 정의 */
        var equipGroupStore = Ext.create('Ext.data.Store', {
            id: 'equipGroupStore',
            model: 'equipGroupModel',
            autoDestroy: true,
            autoLoad: true,
            proxy: equipGroupProxy,
            listeners: {
                load: function (store, records, successful) {
                },
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

        /* 그리드 컬럼 정의 */
        var equipGroupColumns =
        [
            { header: '그룹코드<span style="color:red;"> *</span>', flex: 0.4, minWidth: 60, dataIndex: 'GROUPCD', align: 'center',
                field: {
                    xtype: 'textfield',
                    allowBlank: false
                }
            },
            { header: '그룹명', flex: 0.8, minWidth: 90, dataIndex: 'GROUPNM', align: 'center', renderer: setCursor,
                field: {
                    xtype: 'textfield'
                }
            },
            { header: '<span style="text-align:center">설 명</span>', flex: 4.0, width: 220, dataIndex: 'GROUPEXPLAIN', align: 'center', renderer: setCursor,
                field: {
                    xtype: 'textfield'
                }
            }
        ];


        /* 그리드 tbar 아이템 정의 */
        var equipGroupTbarItems = [
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
                    var rec = new equipGroupModel({
                        GROUPCD: setGroupCDNum(),
                        GROUPNM: '',
                        GROUPEXPLAIN: ''
                    }), edit = equipGroupGrid.editing;

                    rec.setDirty();
                    edit.cancelEdit();
                    equipGroupStore.insert(0, rec);
                    edit.startEditByPosition({
                        row: 0,
                        column: 0
                    });
                }
            }, '-',
            {
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/minus-circle-frame.png',
                text: '<span style="vertical-align:middle">삭제</span>',
                disabled: true,
                itemId: 'delete',
                tooltip: '행 삭제',
                scope: this,
                handler: function () {
                    var selection = equipGroupGrid.getSelectionModel().getSelection()[0];
                    if (selection) {
                        equipGroupStore.remove(selection);
                    }
                }
            }, '-',
            {
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/disk.png',
                text: '<span style="vertical-align:middle">저장</span>',
                itemId: 'save',
                tooltip: '데이터 저장',
                handler: function () {

                    var storeCount = equipGroupStore.getCount();
                    for (var i = 0; i < storeCount; i++) {
                        record = equipGroupStore.getAt(i);
                        if (record.dirty == true) {
                            if (equipGroupStore.getAt(i).data.GROUPCD == '') {

                                Ext.Msg.alert("Message", "권한코드는 필수 입력 항목입니다.");
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
                                equipGroupStore.sync();
                            }
                        }
                    });
                }
            }, '-',
            {
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/navigation-000-frame.png',
                text: '<span style="vertical-align:middle">조회</span>',
                itemId: 'search',
                tooltip: '다시 조회',
                handler: function () {
                    equipGroupStore.load();
                }
            }
        ];

        /* 그리드 정의 */
        var equipGroupGrid = Ext.create('Edit.Grid', {
            id: 'equipGroupGrid',
            name: 'equipGroupGrid',
            frame: true,
            flex: 1,
            region: 'center',
            columns: equipGroupColumns,
            store: equipGroupStore,
            tbar: equipGroupTbarItems,
            listeners: {
                beforeedit: function (editor, e, options) {
                    var commitStoreCount = equipGroupStore.getTotalCount();
                    var totalStoreCount = equipGroupStore.getCount();
                    var addRowCount = totalStoreCount - commitStoreCount;

                    if (editor.rowIdx >= addRowCount) {
                        if (editor.colIdx == 0) editor.cancel = true;
                    }
                },
                selectionchange: function (view, selections, options) {
                    this.down('#delete').setDisabled(selections.length == 0);
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
            }, equipGroupGrid] // 랜더링이 되는곳 정의
        });

    });

</script>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<div id="menu-title" class="title_box x-hide-display" align="center" style="text-align:left;">
	<span style="font-weight:bold; margin-left:10px;">
    <img src="<%=Page.ResolveUrl("/Images") %>/icons/gear.png" align="absmiddle"/>&nbsp;&nbsp;
    설비상태그룹 관리
    </span>
</div>
</asp:Content>
