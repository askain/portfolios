<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	권한관리
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PreHeadContent" runat="server">
<script src="../../Scripts/code/editgrid.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

<script type="text/javascript">
    var authCd;

    Ext.onReady(function () {
        Ext.BLANK_IMAGE_URL = '/Scripts/extjs/s.gif';
        Ext.tip.QuickTipManager.init();

        // ******** function 시작 ******** // 

        function setCursor(value, metaData, record, rowIndex, colIndex, store, view) {
            return '<div style="cursor:pointer;width:100%;">' + value + '</div>';
        }

        function setCommit() {
            var storeCount = authStore.getCount();
            for (var i = 0; i < storeCount; i++) {
                record = authStore.getAt(i);
                if (record.dirty == true) {
                    record.commit();
                }
            }
            authStore.load();
        }

        function setRegMenuCommit() {

            var storeCount = authDownRightStore.getCount();
            for (var i = 0; i < storeCount; i++) {
                record = authDownRightStore.getAt(i);
                if (record.dirty == true) {
                    record.commit();
                }
            }
        }

        // ******** function 종료 ******** // 


        ////////////////////////     상단 권한 CRUD 그리드 시작      ////////////////////////

        /* 모델 정의 */
        Ext.define('authModel', {
            extend: 'Ext.data.Model',
            idProperty: 'AUTHCODE',
            fields: [
                { name: 'AUTHCODE', type: 'string' },
                { name: 'AUTHNAME', type: 'string' },
                { name: 'AUTHEXPLAN', type: 'string' }
            ]
        });

        var authProxy =
        { type: 'ajax',
            api: {
                read: '<%=Page.ResolveUrl("~/ManAuthMng")%>/GetAuthList',
                create: '<%=Page.ResolveUrl("~/ManAuthMng")%>/InsertUpdateAuth',
                update: '<%=Page.ResolveUrl("~/ManAuthMng")%>/InsertUpdateAuth',
                destroy: '<%=Page.ResolveUrl("~/ManAuthMng")%>/DeleteAuth'
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
        var authStore = Ext.create('Ext.data.Store', {
            id: 'authStore',
            model: 'authModel',
            autoDestroy: true,
            autoLoad: true,
            proxy: authProxy,
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
        var authColumns =
        [
            { header: '권한코드<span style="color:red;"> *</span>', flex: 0.4, minWidth: 60, dataIndex: 'AUTHCODE', align: 'center',
                field: {
                    xtype: 'textfield',
                    allowBlank: false
                }
            },
            { header: '권한명', flex: 0.8, minWidth: 90, dataIndex: 'AUTHNAME', align: 'center', renderer: setCursor,
                field: {
                    xtype: 'textfield'
                }
            },
            { header: '<span style="text-align:center">설 명</span>', flex: 4.0, width: 220, dataIndex: 'AUTHEXPLAN', align: 'center', renderer: setCursor,
                field: {
                    xtype: 'textfield'
                }
            }
        ];


        /* 그리드 tbar 아이템 정의 */
        var authTbarItems = [
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
                    var rec = new authModel({
                        LVCODE: '',
                        LVNAME: '',
                        LVEXPLAN: ''
                    }), edit = authGrid.editing;

                    rec.setDirty();
                    edit.cancelEdit();
                    authStore.insert(0, rec);
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
                    var selection = authGrid.getSelectionModel().getSelection()[0];
                    if (selection) {
                        authStore.remove(selection);
                    }
                }
            }, '-',
            {
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/disk.png',
                text: '<span style="vertical-align:middle">저장</span>',
                itemId: 'save',
                tooltip: '데이터 저장',
                handler: function () {

                    var storeCount = authStore.getCount();
                    for (var i = 0; i < storeCount; i++) {
                        record = authStore.getAt(i);
                        if (record.dirty == true) {
                            if (authStore.getAt(i).data.AUTHCODE == '') {

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
                                authStore.sync();
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
                    authStore.load();
                }
            }
        ];

        /* 그리드 정의 */
        var authGrid = Ext.create('Edit.Grid', {
            id: 'authGrid',
            name: 'authGrid',
            frame: true,
            flex: 1,
            region: 'center',
            columns: authColumns,
            store: authStore,
            tbar: authTbarItems,
            listeners: {
                beforeedit: function (editor, e, options) {
                    var commitStoreCount = authStore.getTotalCount();
                    var totalStoreCount = authStore.getCount();
                    var addRowCount = totalStoreCount - commitStoreCount;

                    if (editor.rowIdx >= addRowCount) {
                        if (editor.colIdx == 0) editor.cancel = true;
                    }
                },
                selectionchange: function (view, selections, options) {
                    if (selections.length != 0) {
                        authCd = selections[0].data.AUTHCODE;
                    }
                    this.down('#delete').setDisabled(selections.length == 0);
                    //메뉴 리스트 조회
                    authDownLeftStore.load({
                        params: {
                            authCode: authCd
                        }
                    });

                    //등록된 메뉴 리스트 조회
                    authDownRightStore.load({
                        params: {
                            authCode: authCd
                        }
                    });
                }
            }
        });
        ////////////////////////     상단 권한 CRUD 그리드 종료     ////////////////////////





        //////////////////////////////////////////////// ********    하단 시작    ********  ////////////////////////////////////////////////



        // *********************************  하단 왼쪽 메뉴 그리드 시작 ************************************* //


        /* 모델 정의 */
        Ext.define('authDownLeftModel', {
            extend: 'Ext.data.Model',
            idProperty: 'MENU_ID',
            fields: [
                { name: 'MENU_ID', type: 'int' },
                { name: 'B_MENU_NAME', type: 'string' },
                { name: 'MENU_NAME', type: 'string' },
                { name: 'AUTHCODE', type: 'string' }
            ]
        });

        var authDownLeftProxy =
        {
            type: 'ajax',
            url: '<%=Page.ResolveUrl("~/ManAuthMng")%>/GetMenuMngList',
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

        /* 하단 왼쪽 프로그램 리스트 그리드 저장소 정의 */
        var authDownLeftStore = Ext.create('Ext.data.Store', {
            autoDestroy: true,
            model: 'authDownLeftModel',
            proxy: authDownLeftProxy,
            listeners: {
                load: function (store, records, successful, operation) {
                    if (records && records.length > 0) {

                    }
                }
            },
            sorters: [{ property: 'MENU_ORD', direction: 'ASC'}],
            autoLoad: false
        });

        /* 하단 왼쪽 프로그램 리스트 그리드 컬럼 정의 */
        var authDownLeftColumns =
        [
            { header: '상위 메뉴명', flex: 0.5, minWidth: 80, dataIndex: 'B_MENU_NAME', align: 'center' },
            { header: '메뉴명', flex: 0.7, minWidth: 100, dataIndex: 'MENU_NAME', align: 'center' }
        ];

        /* 하단 왼쪽 프로그램 리스트 그리드 정의 */
        var authDownLeftGrid = Ext.create('Ext.grid.Panel', {
            id: 'authDownLeftGrid',
            name: 'authDownLeftGrid',
            frame: true,
            title: '메뉴 리스트',
            region: 'west',
            multiSelect: true,
            collapsible: false,
            collapsed: false,
            split: true,
            flex: 0.6,
            store: authDownLeftStore,
            columns: authDownLeftColumns,
            listeners: {
                selectionchange: function (view, selections, options) {
                }
            }
        });

        // *********************************  하단 왼쪽 메뉴 그리드 종료 ************************************* //


        // *********************************  [하단 중앙 추가,제거,저장 패널 시작] ************************************* //

        var authDownMidItems =
        [
            {
                xtype: 'displayfield',
                name: 'dispfld2',
                value: '',
                height: 115
            }, {
                xtype: 'button',
                id: 'addDownMidUpper',
                text: '<span style="font-weight: bold;vertical-align:middle">등록</span>',
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/arrow.png',
                width: 70,
                valign: 'top',
                height: 24,
                handler: function () {
                    if (authDownLeftStore.getCount() == 0) {
                        Ext.Msg.alert('Message', '등록할 항목이 없습니다.');
                        return;
                    }
                    var recs = authDownLeftGrid.getSelectionModel().getSelection();
                    if (recs.length > 0) {

                        for (var i = 0; i < recs.length; i++) {
                            var rec = recs[i];

                            authDownLeftStore.remove(rec);

                            var rec2 = new authDownRightModel({
                                MENU_ID: '',
                                B_MENU_NAME: rec.data.B_MENU_NAME,
                                MENU_NAME: rec.data.MENU_NAME,
                                AUTHCODE: authCd
                            });

                            authDownRightStore.insert(0, rec2);
                            authDownRightStore.getAt(0).set('MENU_ID', rec.data.MENU_ID);
                        }

                        authDownRightStore.sync();

                    } else {
                        Ext.Msg.alert('Message', '메뉴 리스트를 선택하셔야합니다.');
                    }

                }
            }, {
                xtype: 'displayfield',
                name: 'dispfld2',
                value: '',
                height: 1
            }, {
                xtype: 'button',
                id: 'addDownMidBtn',
                text: '<span style="font-weight: bold;vertical-align:middle">제거</span>',
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/arrow-180.png',
                width: 70,
                valign: 'top',
                height: 24,
                handler: function () {
                    if (authDownRightStore.getCount() == 0) {
                        Ext.Msg.alert('Message', '제거할 항목이 없습니다.');
                        return;
                    }
                    var recs = authDownRightGrid.getSelectionModel().getSelection();
                    if (recs.length > 0) {

                        for (var i = 0; i < recs.length; i++) {
                            var rec = recs[i];
                            authDownRightStore.remove(rec);
                            authDownLeftStore.insert(0, rec);
                        }

                        authDownRightStore.sync();
                    } else {
                        Ext.Msg.alert('Message', '등록된 메뉴 리스트를 선택하셔야합니다.');
                    }

                }
            }, {
                xtype: 'displayfield',
                name: 'dispfld3',
                value: '',
                height: 15
            }/*, {
                xtype: 'button',
                id: 'delDownMid',
                text: '<span style="font-weight: bold;vertical-align:middle">저장</span>',
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/disk.png',
                width: 70,
                valign: 'top',
                height: 24,
                handler: function () {
                    if (confirm("저장하시겠습니까")) {
                        var cnt = authDownRightStore.getCount();
                        alert("new : " + authDownRightStore.getNewRecords().length);
                        alert("update : " + authDownRightStore.getUpdatedRecords().length);
                        alert("remove : " + authDownRightStore.getRemovedRecords().length);
                        //authDownRightStore.sync();
                    }
                }
            }*/
        ]

        var authDownMidPanel = Ext.create('Ext.panel.Panel', {
            id: 'authDownCenterPanel',
            name: 'authDownCenterPanel',
            region: 'center',
            border: 0,
            collapsible: false,
            collapsed: false,
            split: true,
            flex: 0.2,
            layout: {
                type: 'vbox',
                align: 'center'
            },
            items: authDownMidItems,
            listeners: {
                selectionchange: function (model, records) {
                    var json, name, i, l, items, series, fields;
                    if (records[0]) {
                    }
                }
            }
        });


        // *********************************  [하단 중앙 추가,제거,저장 패널 시작]  ************************************* //



        // *********************************  하단 오른쪽 메뉴 등록 그리드 시작 ************************************* //


        /* 하단 오른쪽 모델 정의 */
        var authDownRightModel = Ext.define('authDownRightModel', {
            extend: 'Ext.data.Model',
            idProperty: 'ID',
            fields: [
                { name: 'MENU_ID', type: 'int' },
                { name: 'B_MENU_NAME', type: 'string' },
                { name: 'MENU_NAME', type: 'string' },
                { name: 'AUTHCODE', type: 'string' }
            ]
        });

        var authDownRightProxy =
        {
            type: 'ajax',
            api: {
                read: '<%=Page.ResolveUrl("~/ManAuthMng")%>/GetRegMenuMngList',
                create: '<%=Page.ResolveUrl("~/ManAuthMng")%>/InsertAuthMenu',
                update: '<%=Page.ResolveUrl("~/ManAuthMng")%>/InsertAuthMenu',
                destroy: '<%=Page.ResolveUrl("~/ManAuthMng")%>/DeleteAuthMenu'
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
        }

        /* 하단 오른쪽 메뉴 리스트 그리드 저장소 정의 */
        var authDownRightStore = Ext.create('Ext.data.Store', {
            id: 'authDownRightStore',
            model: 'authDownRightModel',
            autoDestroy: true,
            remoteSort: true,
            autoLoad: false,
            autoSync: false,
            simpleSortMode: true,
            proxy: authDownRightProxy,
            listeners: {
                load: function (store, records, successful) {
                },
                update: function (records, operation) {
                },
                remove: function (proxy, operation) {
                },
                write: function (proxy, operation) {
                    if (operation.action == 'read') {
                    }
                    if (operation.action == 'create') {
                        setRegMenuCommit();
                    }
                    if (operation.action == 'update') {
                        setRegMenuCommit();
                    }
                    if (operation.action == 'destroy') {
                        setRegMenuCommit();
                    }
                }

            }
        });

        /* 하단 오른쪽 메뉴 리스트 그리드 컬럼 정의 */
        var authDownRightColumns =
        [
            { header: '상위 메뉴명', flex: 0.6, minWidth: 80, dataIndex: 'B_MENU_NAME', align: 'center' },
            { header: '메뉴명', flex: 0.8, minWidth: 100, dataIndex: 'MENU_NAME', align: 'center' }
        ];


        /* 하단 오른쪽 메뉴 리스트 그리드 정의 */
        var authDownRightGrid = Ext.create('Ext.grid.Panel', {
            id: 'authDownRightGrid',
            name: 'authDownRightGrid',
            frame: true,
            title: '등록된 메뉴 리스트',
            region: 'east',
            multiSelect: true,
            collapsible: false,
            collapsed: false,
            split: true,
            flex: 0.61,
            store: authDownRightStore,
            columns: authDownRightColumns,
            listeners: {
                selectionchange: function (view, selections, options) {
                }
            }
        });


        // *********************************  하단 오른쪽 메뉴 등록 그리드 종료 ************************************* //


        /* 화면 하단의 패널 정의 */
        var authDownPanel = Ext.create('Ext.panel.Panel', {
            title: '메뉴 등록 / 수정  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' + '<span style="color:red;">** 메뉴 등록시 메뉴 리스트를 선택해주세요. **</span>',
            flex: 0.8,
            bodyPadding: 5,
            border: 0,
            region: 'south',
            layout: {
                type: 'hbox',
                align: 'stretch',
                padding: 0
            },
            items: [authDownLeftGrid, authDownMidPanel, authDownRightGrid]
        });

        //////////////////////////////////////////////// ********    하단 종료    ********  ////////////////////////////////////////////////


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
            }, authGrid, authDownPanel] // 랜더링이 되는곳 정의
        });

    });

</script>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<div id="menu-title" class="title_box x-hide-display" align="center" style="text-align:left;">
	<span style="font-weight:bold; margin-left:10px;">
    <img src="<%=Page.ResolveUrl("/Images") %>/icons/gear.png" align="absmiddle"/>&nbsp;&nbsp;
    권한 관리
    </span>
</div>
</asp:Content>
