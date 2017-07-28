<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	수계 관리
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PreHeadContent" runat="server">
<script src="../../Scripts/code/editgrid.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

<script type="text/javascript">
    Ext.onReady(function () {
        Ext.BLANK_IMAGE_URL = '/Scripts/extjs/s.gif';
        Ext.tip.QuickTipManager.init();

        // ******** function 시작 ******** // 
        function getUseYN(val, meta, rec, row, col, store) {
            if (val == "Y") {
                return "<input type='checkbox' checked/>";
            } else {
                return "<input type='checkbox'/>";
            }
        }

        function setCursor(value, metaData, record, rowIndex, colIndex, store, view) {
            return '<div style="cursor:pointer;width:100%;">' + value + '</div>';
        }

        function setCommit() {
            var storeCount = WKStore.getCount();
            for (var i = 0; i < storeCount; i++) {
                record = WKStore.getAt(i);
                if (record.dirty == true) {
                    record.commit();
                }
            }
            WKStore.load();
        }

        //새코드를 생성
        function setGroupCDNum() {
            var number;
            var storeCount = WKStore.getCount();
            for (var i = 0; i < storeCount; i++) {
                record = WKStore.getAt(i).data.GROUPCD;
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
        Ext.define('WKModel', {
            extend: 'Ext.data.Model',
            idProperty: 'WKCD',
            fields: [
                { name: 'WKCD', type: 'string' },
                { name: 'WKNM', type: 'string' },
                { name: 'ORD', type: 'string' },
                { name: 'OUTYN', type: 'string' }
            ]
        });

        var WKProxy =
        { type: 'ajax',
            api: {
                read: '/DamBoObsMng/GetWKList',
                create: '/DamBoObsMng/InsertWK',
                update: '/DamBoObsMng/UpdateWK',
                destroy: '/DamBoObsMng/DeleteWK'
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
        var WKStore = Ext.create('Ext.data.Store', {
            id: 'WKStore',
            model: 'WKModel',
            autoDestroy: true,
            autoLoad: true,
            proxy: WKProxy,
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
        var WKColumns =
        [
            { header: '코드<span style="color:red;"> *</span>', flex: 0.4, minWidth: 60, dataIndex: 'WKCD', align: 'center',
                field: {
                    xtype: 'textfield',
                    allowBlank: false
                }
            },
            { header: '수계명', flex: 0.8, minWidth: 90, dataIndex: 'WKNM', align: 'center', renderer: setCursor,
                field: {
                    xtype: 'textfield'
                }
            },
            { header: '순서', width: 100, dataIndex: 'ORD', align: 'center', renderer: setCursor,
                field: {
                    xtype: 'numberfield'
                }
            },
            { header: '사용여부', width: 100, dataIndex: 'OUTYN', align: 'center', renderer: getUseYN
            }
        ];


        /* 그리드 tbar 아이템 정의 */
        var WKTbarItems = [
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
                    var rec = new WKModel({
                        GROUPCD: setGroupCDNum(),
                        GROUPNM: '',
                        GROUPEXPLAIN: ''
                    }), edit = WKGrid.editing;

                    rec.setDirty();
                    edit.cancelEdit();
                    WKStore.insert(0, rec);
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
                    var selection = WKGrid.getSelectionModel().getSelection()[0];
                    if (selection) {
                        WKStore.remove(selection);
                    }
                }
            }, '-',
            {
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/disk.png',
                text: '<span style="vertical-align:middle">저장</span>',
                itemId: 'save',
                tooltip: '데이터 저장',
                handler: function () {

                    var storeCount = WKStore.getCount();
                    for (var i = 0; i < storeCount; i++) {
                        record = WKStore.getAt(i);
                        if (record.dirty == true) {
                            if (WKStore.getAt(i).data.WKCD == '' || WKStore.getAt(i).data.WKNM == '') {

                                Ext.Msg.alert("Message", "코드와 구분명은 필수 입력 항목입니다.");
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
                                WKStore.sync();
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
                    WKStore.load();
                }
            }
        ];

        /* 그리드 정의 */
        var WKGrid = Ext.create('Edit.Grid', {
            id: 'WKGrid',
            name: 'WKGrid',
            frame: true,
            flex: 1,
            region: 'center',
            width: 500,
            columns: WKColumns,
            store: WKStore,
            tbar: WKTbarItems,
            columnLines: true,
            listeners: {
                beforeedit: function (editor, e, options) {
                    var commitStoreCount = WKStore.getTotalCount();
                    var totalStoreCount = WKStore.getCount();
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
        WKGrid.on("cellclick", function (gg, rowIndex, colIndex, rec) {
            if (colIndex == 3) {
                var val = rec.get("OUTYN");
                if (val == "Y") rec.set("OUTYN", "N");
                else rec.set("OUTYN", "Y");
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
            }, WKGrid] // 랜더링이 되는곳 정의
        });

    });

</script>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<div id="menu-title" class="title_box x-hide-display" align="center" style="text-align:left;">
	<span style="font-weight:bold; margin-left:10px;">
    <img src="<%=Page.ResolveUrl("/Images") %>/icons/gear.png" align="absmiddle"/>&nbsp;&nbsp;
    수계 관리
    </span>
</div>
</asp:Content>
