<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	보정코드 관리
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PreHeadContent" runat="server">
<script src="../../Scripts/code/editgrid.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

<script type="text/javascript">

    Ext.onReady(function () {
        Ext.BLANK_IMAGE_URL = '/Scripts/extjs/s.gif';
        Ext.tip.QuickTipManager.init();

        function setCursor(value, metaData, record, rowIndex, colIndex, store, view) {
            return '<div style="cursor:pointer;width:100%;">' + value + '</div>';
        }

        function getUseTP(value, metaData, record, rowIndex, colIndex, store) {
            var val;
            switch (value) {
                case "WL":
                    val = '<div style="color:blue">수위</div>';
                    break;
                case "RF":
                    val = '<div style="color:green">우량</div>';
                    break;
                case "DD":
                    val = "댐자료";
                    break;
            }
            return val;
        }

        function getUseYN(value, metaData, record, rowIndex, colIndex, store) {
            var val = '사용함';
            switch (value) {
                case "Y":
                    val = '사용함';
                    break;
                case "N":
                    val = '사용안함';
                    break;
            }
            return '<div style="cursor:pointer;width:100%;">' + val + '</div>';
            //return value == "Y" ? "사용함" : "사용안함";
        }

        function setCommit() {
            var storeCount = revisionManagementStore.getCount();
            for (var i = 0; i < storeCount; i++) {
                record = revisionManagementStore.getAt(i);
                if (record.dirty == true) {
                    record.commit();
                }
            }
            revisionManagementStore.load();
        }


        //var revisionManagementModel = Ext.create('Ext.data.Model', {
        Ext.define('revisionManagementModel', {
            extend: 'Ext.data.Model',
            idProperty: 'ID',
            fields: [
                { name: 'EDEXWAY', type: 'string' },
                { name: 'EDTP', type: 'string' },
                { name: 'EDORD', type: 'string' },
                { name: 'EDEXWAYCONT', type: 'string' },
                { name: 'EDNOTE', type: 'string' },
                { name: 'EDYN', type: 'string' }
           ]
        });

        var revisionManagementStore = Ext.create('Ext.data.Store', {
            id: 'revisionManagementStore',
            model: 'revisionManagementModel',
            autoDestroy: true,
            autoLoad: true,
            autoSync: false,
            remoteSort: true,
            simpleSortMode: true,
            proxy: {
                type: 'ajax',
                api: {
                    read: '<%=Page.ResolveUrl("~/Code")%>/GetRevisionManagementList',
                    create: '<%=Page.ResolveUrl("~/Code")%>/InsertUpdateRevisionManagement',
                    update: '<%=Page.ResolveUrl("~/Code")%>/InsertUpdateRevisionManagement',
                    destroy: '<%=Page.ResolveUrl("~/Code")%>/DeleteRevisionManagement'
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

        var revisionManagementColumns = [
        { header: '보정코드<span style="color:red;"> *</span>', flex: 0.4, minWidth: 70, sortable: false, align: 'center', dataIndex: 'EDEXWAY',
            field: {
                type: 'textfield',
                allowBlank: false
            }
        }, { header: '구분<span style="color:red;"> *</span>', flex: 0.6, minWidth: 60, sortable: false, align: 'center', dataIndex: 'EDTP', renderer: getUseTP,
            field: {
                xtype: 'combobox',
                typeAhead: true,
                triggerAction: 'all',
                selectOnTab: true,
                lazyRender: true,
                allowBlank: false,
                store: [
                    ['WL', '수위'],
                    ['RF', '우량'],
                    ['DD', '댐자료']
                ]
            }
        }, { header: '우선순위<span style="color:red;"> *</span>', flex: 0.5, minWidth: 60, sortable: false, align: 'center', dataIndex: 'EDORD', renderer: setCursor,
            field: {
                xtype: 'textfield',
                allowBlank: false
            }
        }, { header: '<div style="text-align:center;">설명</div>', flex: 3.2, minWidth: 150, sortable: false, align: 'left', dataIndex: 'EDEXWAYCONT', renderer: setCursor,
            field: {
                xtype: 'textfield'
            }
        }, { header: '<div style="text-align:center;">요약</div>', flex: 1.5, minWidth: 80, sortable: false, align: 'left', dataIndex: 'EDNOTE', renderer: setCursor,
            field: {
                xtype: 'textfield'
            }
        }, { header: '사용여부', flex: 0.7, minWidth: 60, sortable: false, align: 'center', dataIndex: 'EDYN', renderer: getUseYN,
            field: {
                xtype: 'combobox',
                typeAhead: true,
                triggerAction: 'all',
                selectOnTab: true,
                lazyRender: true,
                store: [
                    ['Y', '사용함'],
                    ['N', '사용안함']
                ]
            }
        }];

        var revisionManagementTbarItems = [
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
                var rec = new revisionManagementModel({
                    ABCOLUMN: '',
                    ABCONT: '',
                    ABYN: 'Y',
                    ABCOLOR: 'FFFFFF'
                }), edit = revisionManagementGrid.editing;

                rec.setDirty();
                revisionManagementStore.insert(0, rec);
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
                var selection = revisionManagementGrid.getSelectionModel().getSelection()[0];
                if (selection) {
                    revisionManagementStore.remove(selection);
                }
            }
        }, '-', {
            icon: '<%=Page.ResolveUrl("~/Images") %>/icons/disk.png',
            text: '<span style="vertical-align:middle">저장</span>',
            itemId: 'save',
            tooltip: '데이터 저장',
            scope: this,
            handler: function () {

                var storeCount = revisionManagementStore.getCount();
                for (var i = 0; i < storeCount; i++) {
                    record = revisionManagementStore.getAt(i);
                    if (record.dirty == true) {
                        if (revisionManagementStore.getAt(i).data.EDEXWAY == '' || revisionManagementStore.getAt(i).data.EDTP == '' || revisionManagementStore.getAt(i).data.EDORD == '') {
                            Ext.Msg.alert("Message", "보정코드, 구분, 우선순위는</br> 필수 입력 항목입니다.");
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
                            revisionManagementStore.sync();
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
                revisionManagementStore.load();
            }
        }];

        var revisionManagementGrid = Ext.create('Edit.Grid', {
            id: 'grid',
            name: 'grid',
            flex: 1,
            region: 'center',
            frame: true,
            columns: revisionManagementColumns,
            store: revisionManagementStore,
            tbar: revisionManagementTbarItems,
            listeners: {
                beforeedit: function (editor, e, options) {
                    var commitStoreCount = revisionManagementStore.getTotalCount();
                    var totalStoreCount = revisionManagementStore.getCount();
                    var addRowCount = totalStoreCount - commitStoreCount;

                    if (editor.rowIdx >= addRowCount) {
                        if (editor.colIdx == 0 || editor.colIdx == 1) editor.cancel = true;
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
            }, revisionManagementGrid]
        });
    });

</script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<div id="menu-title" class="title_box x-hide-display" align="center" style="text-align:left;">
	<span style="font-weight:bold; margin-left:10px;">
    <img src="<%=Page.ResolveUrl("/Images") %>/icons/gear.png" align="absmiddle"/>&nbsp;&nbsp;
    보정코드 관리
    </span>
</div>
</asp:Content>
