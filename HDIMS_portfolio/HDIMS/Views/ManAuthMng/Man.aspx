<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	담당자관리
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PreHeadContent" runat="server">
<script src="../../Scripts/code/editgrid.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

<script type="text/javascript">

    Ext.onReady(function () {
        Ext.BLANK_IMAGE_URL = '/Scripts/extjs/s.gif';

        Ext.tip.QuickTipManager.init();

        function setCommit() {
            var storeCount = manStore.getCount();
            for (var i = 0; i < storeCount; i++) {
                record = manStore.getAt(i);
                if (record.dirty == true) {
                    record.commit();
                }
            }

            var authcd = Ext.getCmp('comboAuthList').getValue();
            manStore.load({
                params: {
                    authCode: authcd
                }
            });
        }

        function setDisplay(value, metaData, record, rowIndex, colIndex, store, view) {
            //var retVal = 'X';

            if (value != '') {
                for (var i = 0; i < authComboStore.getCount(); i++) {
                    if (value == authComboStore.getAt(i).data.AUTHCODE) {
                        return '<div style="cursor:pointer;width:100%">' + authComboStore.getAt(i).data.AUTHNAME + '</div>';
                    }
                }
            }
            else {
                var retVal = '<div style="cursor:pointer;width:100%">' + 'X' + '</div>';
            }

            return retVal;
        }

        function setDamMgt(value, metaData, record, rowIndex, colIndex, store, view) {
            var crec = damMgtStore.findRecord("MGTCD", value);
            if (crec)
                return trim(crec.get("MGTNM").replace("└", "").replace("─", "").replace("→", ""));
            return "미선택";
        }

        /* 권한 콤보 모델 정의 */
        var authModel = Ext.define('authModel', {
            extend: 'Ext.data.Model',
            idProperty: 'AUTHCODE',
            fields: [
                { name: 'AUTHCODE', type: 'string' },
                { name: 'AUTHNAME', type: 'string' }
            ]
        });

        /* 리스트내 권한 저장소 정의 */
        var authStore = Ext.create('Ext.data.Store', {
            autoDestroy: true,
            model: 'authModel',
            proxy: {
                type: 'ajax',
                url: '<%=Page.ResolveUrl("~/ManAuthMng")%>/GetAuthList',
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
                        //Ext.getCmp('comboAuthList').setValue(records[0].data.AUTHCODE);
                    }
                }
            },
            //sorters: [{ property: 'AUTHCODE', direction: 'ASC'}],
            autoLoad: true
        });

        /* 권한 콤보 저장소 정의 */
        var authComboStore = Ext.create('Ext.data.Store', {
            autoDestroy: true,
            model: 'authModel',
            proxy: {
                type: 'ajax',
                url: '<%=Page.ResolveUrl("~/ManAuthMng")%>/GetManAuthCombo',
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
                        Ext.getCmp('comboAuthList').setValue(records[0].data.AUTHCODE);


                        // authStore가 로드 되기 전에 메인그리드가 로드 되어버려서 
                        // 권한컬럼이 비어버리는 문제가 발생하여 
                        // 타이밍을 늦추기위해 메인그리드 로드를 여기에 위치시킴
                        var authcd = Ext.getCmp('comboAuthList').getValue();
                        manStore.load({
                            params: {
                                authCode: authcd
                            }
                        });
                    }
                }
            },
            sorters: [{ property: 'AUTHCODE', direction: 'ASC'}],
            autoLoad: true
        });

        // 관리단 모델 정의
        Ext.define('damMgtModel', {
            extend: 'Ext.data.Model',
            idProperty: 'MGTCD',
            fields: [
                    { name: 'MGTCD', type: 'string' },
                    { name: 'MGTNM', type: 'string' },
                    { name: 'USEYN', type: 'string' },
                    { name: 'MGTCOMMENT', type: 'string' }
           ]
        });

        //
        var damMgtStore = Ext.create('Ext.data.Store', {
            id: 'damMgtStore',
            model: 'damMgtModel',
            autoDestroy: true,
            autoLoad: true,
            proxy: {
                type: 'ajax',
                api: {
                    read: '<%=Page.ResolveUrl("~/DamBoObsMng")%>/GetDamMgtList/'
                },
                reader: {
                    type: 'json',
                    root: 'Data'
                },
                extraParams: {
                    allnm: '미선택'
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
        });

        /* 담당자 모델 정의 */
        Ext.define('manModel', {
            extend: 'Ext.data.Model',
            idProperty: 'EMPNO',
            fields: [
                { name: 'EMPNO', type: 'string' },
                { name: 'PASSWD', type: 'string' },
                { name: 'KORNAME', type: 'string' },
                { name: 'DEPT', type: 'string' },
                { name: 'MGTCD', type: 'string' },
                { name: 'MGTNM', type: 'string' },
                { name: 'AUTHCODE', type: 'string' },
                { name: 'AUTHNAME', type: 'string' },
                { name: 'EMAIL', type: 'string' },
                { name: 'HPTEL', type: 'string' }     
            ]
        });

        /* 담당자 프록시 정의 */
        var manProxy =
        {
            type: 'ajax',
            api: {
                read: '<%=Page.ResolveUrl("~/ManAuthMng")%>/GetManList',
                update: '<%=Page.ResolveUrl("~/ManAuthMng")%>/InsertUpdateMan'
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

        /* 담당자 저장소 정의 */
        var manStore = Ext.create('Ext.data.Store', {
            id: 'manStore',
            model: 'manModel',
            pageSize: 50,
            autoDestroy: true,
            autoLoad: false,
            proxy: manProxy,
            remoteSort: true,
            reader: new Ext.data.JsonReader({
                root: 'data',
                totalProperty: 'total',
                id: 'EMPNO'
            }),
            listeners: {
                load: function (store, records, successful, operation) {
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
                    if (operation.action == 'update') {
                        setCommit();
                    }
                }
            }
        });

        var mgt;
        /* 담당자 그리드 컬럼 정의 */
        var manColumns =
        [
            { header: '사번', flex: 0.5, minWidth: 60, dataIndex: 'EMPNO', align: 'center' },
            { header: '비밀번호', flex: 0.5, width: 60, dataIndex: 'PASSWD', align: 'center', field: { xtype: 'textfield', allowBlank: false} },
            { header: '이름', flex: 0.5, minWidth: 60, dataIndex: 'KORNAME', align: 'center' },
            { header: '부서명', flex: 0.6, minWidth: 70, dataIndex: 'DEPT', align: 'center' },
            { header: '관리단', flex: 0.7, minWidth: 70, align: 'center', dataIndex: 'MGTCD', renderer: setDamMgt,
                field: {
                    xtype: 'combobox',
                    id: 'cboxmgt',
                    typeAhead: true,
                    triggerAction: 'all',
                    selectOnTab: true,
                    valueField: 'MGTCD',
                    displayField: 'MGTNM',
                    store: damMgtStore
                }
            },
            { header: '권한', flex: 0.6, minWidth: 70, dataIndex: 'AUTHCODE', align: 'center', renderer: setDisplay,
                field: {
                    xtype: 'combobox',
                    id: 'comboAuth',
                    name: 'comboAuth',
                    typeAhead: true,
                    triggerAction: 'all',
                    selectOnTab: true,
                    displayField: 'AUTHNAME',
                    valueField: 'AUTHCODE',
                    store: authStore,
                    queryMode: 'local'
                }
            },
            { header: '이메일', flex: 0.5, width: 60, dataIndex: 'EMAIL', align: 'center', field: 'textfield' },
            { header: '핸드폰', flex: 0.5, width: 60, dataIndex: 'HPTEL', align: 'center', field: 'textfield' }
        ];

        //Ext.getCmp('cboxmgt').setValue(mgt);

        /* 담당자 그리드 tbar 아이템 정의 */
        var manTbarItems =
        [
            {
                xtype: 'combobox',
                id: 'comboAuthList',
                fieldLabel: '권한',
                labelWidth: 60,
                labelAlign: 'right',
                width: 190,
                typeAhead: true,
                triggerAction: 'all',
                selectOnTab: true,
                valueField: 'AUTHCODE',
                displayField: 'AUTHNAME',
                store: authComboStore,
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
                store: [
                    ['ALL', '전체'],
                    ['EMPNO', '사번'],
                    ['KORNAME', '이름'],
                    ['DEPT', '부서명']
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
            }, ' ',
            {
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/navigation-000-frame.png',
                text: '<span style="vertical-align:middle">조회</span>',
                itemId: 'search',
                tooltip: '조회',
                handler: function () {
                    var authcd = Ext.getCmp('comboAuthList').getValue();
                    var searchTerm = Ext.getCmp('comboSearchTerm').getValue();
                    var searchText = Ext.getCmp('txtSearch').getValue();

                    if (searchTerm != 'ALL' && searchText == '') {
                        Ext.Msg.alert('message', '검색어를 입력하세요.');
                        return;
                    }

                    manStore.proxy.extraParams = {
                        authCode: authcd,
                        term: searchTerm,
                        txt: searchText,
                        start: 0
                    };

                    manStore.loadPage(1);
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
                                manStore.sync();
                            }
                        }
                    });
                }
            },
            '->'
        ];

        /* 담당자 그리드 정의 */
        var manGrid = Ext.create('Edit.Grid', {
            id: 'manGrid',
            name: 'manGrid',
            frame: true,
            flex: 1,
            region: 'center',
            columns: manColumns,
            store: manStore,
            tbar: manTbarItems,
            bbar: Ext.create('Ext.PagingToolbar', {
                store: manStore,
                displayInfo: true,
                displayMsg: '{0} - {1} of {2}',
                emptyMsg: "검색결과가 없습니다."
            }),
            listeners: {
                beforeedit: function (editor, e, options) {
                    var commitStoreCount = manStore.getTotalCount();
                    var totalStoreCount = manStore.getCount();
                    var addRowCount = totalStoreCount - commitStoreCount;

                    if (editor.rowIdx >= addRowCount) {
                        if (editor.colIdx == 0 || editor.colIdx == 2 || editor.colIdx == 3) editor.cancel = true;
                    }
                },
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
            }, manGrid] // 랜더링이 되는곳 정의
        });

        Ext.getCmp('comboSearchTerm').setValue('ALL');
    });

</script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<div id="menu-title" class="title_box x-hide-display" align="center" style="text-align:left;">
	<span style="font-weight:bold; margin-left:10px;">
    <img src="<%=Page.ResolveUrl("/Images") %>/icons/gear.png" align="absmiddle"/>&nbsp;&nbsp;
    담당자 관리
    </span>
</div>
</asp:Content>
