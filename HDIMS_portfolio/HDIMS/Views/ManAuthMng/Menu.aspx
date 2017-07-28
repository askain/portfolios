<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	메뉴 관리
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PreHeadContent" runat="server">
<script src="../../Scripts/code/editgrid.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

<script type="text/javascript">
    var ordNum;
    Ext.onReady(function () {
        Ext.BLANK_IMAGE_URL = '/Scripts/extjs/s.gif';

        Ext.tip.QuickTipManager.init();

        function chkBlank(id, msg) {
            if (trim(Ext.getCmp(id).getValue()) == '') {
                Ext.Msg.alert('Info', msg);
                return false;
            }
            return true;
        }
        ///////////////////////////////  메뉴 관리 왼쪽 tree 시작 ////////////////////////////

        /* 모델 정의 */
        Ext.define('testModel', {
            extend: 'Ext.data.Model',
            fields: [
                        { name: 'NODECOUNT', type: 'int' }
                    ]
        });

        /* 저장소 정의 */
        var ordStore = Ext.create('Ext.data.Store', {
            autoDestroy: true,
            model: 'testModel',
            proxy: {
                type: 'ajax',
                url: '/ManAuthMng/GetNodeCount',
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
                load: function (store, records, successful) {
                    if (records != '') {
                        ordNum = records[0].data.NODECOUNT + 1;
                        Ext.getCmp('ord2').setValue(ordNum);
                    }
                }
            },
            autoLoad: false
        });

        function initFormValue() {
            Ext.getCmp('menuId1').setValue("");
            Ext.getCmp('menuNm1').setValue("");
            Ext.getCmp('uri1').setValue("");
            Ext.getCmp('depth1').setValue("");
            Ext.getCmp('ord1').setValue("");
            Ext.getCmp('flag1').setValue("");

            Ext.getCmp('menuNm2').setValue("");
            Ext.getCmp('uri2').setValue("");
            Ext.getCmp('depth2').setValue("");
            Ext.getCmp('ord2').setValue("");
            Ext.getCmp('flag2').setValue("");
            Ext.getCmp('parentId2').setValue("");
        };

        var menuStore = Ext.create('Ext.data.TreeStore', {
            proxy: {
                type: 'ajax',
                url: '/Common/AdminMenuList/?authcode=01',
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
            root: {
                text: "메뉴",
                id: 'id',
                expanded: true
            },
            fields: [
                { name: 'text', type: 'string' },
                { name: 'link', type: 'string' },
                { name: 'id', type: 'string' },
                { name: 'leaf', type: 'boolean' },
                { name: 'depth', type: 'string' },
                { name: 'ord', type: 'string' },
                { name: 'flag', type: 'string' },
                { name: 'parentId', type: 'string' }
            ],
            folderSort: true,
            autoLoad: false,
            sorters: [{
                property: 'ord',
                direction: 'ASC'
            }]
        });

        var treePanel = Ext.create('Ext.tree.Panel', {
            id: 'treePanel',
            region: 'west',
            useArrows: false,
            frame: true,
            flex: 2.5,
            minWidth: 150,
            store: menuStore,
            collapsible: false,
            split: true,
            rootVisible: true,
            autoScroll: true,
            margins: '0 5 5 0',
            root: {
                expanded: true
            },
            listeners: {
                itemclick: function (view, record, item, index, e, options) {
                    initFormValue();
                    var depth;
                    switch (record.data.depth) {
                        case "1":
                            depth = "[대분류]";
                            break;
                        case "2":
                            depth = "[중분류]";
                            break;
                        case "3":
                            depth = "[소분류]";
                            break;
                        default:
                            depth = "[Root]";
                            break;
                    }

                    // Root 클릭시 추가만 가능
                    if (depth == "[Root]") {
                        menuRightUpPanel.hide();
                        Ext.getCmp('displayUpDown').hide();
                    }
                    else {
                        menuRightUpPanel.show();
                        Ext.getCmp('displayUpDown').show();
                    }

                    // 최하위 DETPTH 클릭시 수정만 가능
                    if (parseInt(record.data.depth) > 2) {
                        menuRightDownPanel.hide();
                    }
                    else {
                        menuRightDownPanel.show();
                    }

                    // field Value Settign(Root 아닐때)
                    if (record.data.id != 'root') {
                        Ext.getCmp('menuId1').setValue(record.data.id);
                        Ext.getCmp('menuNm1').setValue(record.data.text);
                        Ext.getCmp('uri1').setValue(record.data.link);
                        Ext.getCmp('depth1').setValue(record.data.depth);
                        Ext.getCmp('ord1').setValue(record.data.ord);
                        Ext.getCmp('flag1').setValue(record.data.flag);

                        //메뉴 수정 Title Setting
                        menuRightUpPanel.setTitle(record.data.text + ' ' + depth);

                        //메뉴 추가 부모ID Setting
                        var parent = record.data.id;
                        if (parent == 'root') {
                            parent = null;
                        }
                        //alert(parent);
                        Ext.getCmp('parentId2').setValue(parent);
                    }

                    var part;
                    switch (depth) {
                        case "[Root]":
                            part = "[대분류]";
                            break;
                        case "[대분류]":
                            part = "[중분류]";
                            break;
                        case "[중분류]":
                            part = "[소분류]";
                            break;
                    }
                    if (record.data.text == "Root") {
                        record.data.text = '';
                    }
                    // 메뉴 추가 Title Setting
                    menuRightDownPanel.setTitle(part);

                    // 메뉴 추가 DEPTH Setting
                    Ext.getCmp('depth2').setValue(parseInt(record.data.depth) + 1);

                    // 메뉴 추가 순서 Setting
                    var currId = record.data.id;
                    ordStore.load({
                        params: {
                            id: currId
                        }
                    });

                    // 메뉴 추가 사용여부 Setting
                    Ext.getCmp('flag2').setValue('Y');
                }
            }
        });

        /* 메뉴 왼쪽 패널 정의 */
        var menuLeftPanel = Ext.create('Ext.panel.Panel', {
            flex: 1.0,
            maxWidth: 280,
            border: 0,
            region: 'west',
            layout: {
                type: 'hbox',
                align: 'stretch',
                padding: 0
            },
            items: [treePanel]
        });

        ///////////////////////////////  메뉴 관리 왼쪽 tree 종료 ////////////////////////////


        ///////////////////////////////  ***** 메뉴 관리 오른쪽 패널 시작  ***** ////////////////////////////



        /* 트리와 그리드 중간 공백 패널 정의 */
        var menuMidPanel = Ext.create('Ext.panel.Panel', {
            flex: 0.02,
            border: 0,
            region: 'center',
            layout: {
                type: 'hbox',
                align: 'stretch',
                padding: 0
            },
            items: []
        });

        /* 오른쪽 그리드와 그리드 오른쪽 화면끝 공백 패널 정의 */
        var menuRightSpPanel = Ext.create('Ext.panel.Panel', {
            flex: 0.06,
            border: 0,
            region: 'east',
            layout: {
                type: 'hbox',
                align: 'stretch',
                padding: 0
            }
        });


        /////////////////////////////// [[ 메뉴 관리 오른쪽 상단 패널 시작 ]] ////////////////////////////


        /* 메뉴 관리 오른쪽 상단 bbar 아이템 정의 */
        var menuRightUpTbarItems = [
            '->',
            {
                text: '<span style="vertical-align:middle">저장</span>',
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/disk.png',
                itemId: 'save1',
                formBind: true,
                disabled: false,
                tooltip: '데이터 저장',
                formBind: true,
                handler: function () {
                    var val1 = trim(Ext.getCmp('menuNm1').getValue());
                    var val2 = trim(Ext.getCmp('uri1').getValue());
                    var val3 = trim(Ext.getCmp('depth1').getValue());
                    var val4 = trim(Ext.getCmp('ord1').getValue());

                    if (val1 == '' && val2 == '' && val3 == '' && val4 == '') {
                        Ext.Msg.alert('Info', '트리를 선택하세요.'); return;
                    }

                    if (!chkBlank('menuNm1', '메뉴명을 입력하세요.')) return;
                    if (!chkBlank('uri1', '메뉴URI를 입력하세요.')) return;
                    if (!chkBlank('depth1', 'DEPTH를 입력하세요.')) return;
                    if (!chkBlank('ord1', '순서를 입력하세요.')) return;

                    Ext.Msg.show({
                        title: 'Message',
                        msg: '데이터를 저장하시겠습니까?',
                        width: 150,
                        buttons: Ext.Msg.OKCANCEL,
                        icon: Ext.Msg.INFO,
                        fn: function (btn) {
                            if (btn == "ok") {
                                menuRightUpPanel.getForm().submit({
                                    success: function (form, action) {
                                        menuStore.load();
                                    },
                                    failure: function (form, action) {
                                        Ext.Msg.alert('Failed', '메뉴 수정 에러');
                                    }
                                });
                            }
                        }
                    });
                }
            }
        ];

        var menuRightUpPanel = Ext.create('Ext.form.Panel', {
            id: 'menuRightUpPanel',
            name: 'menuRightUpPanel',
            url: '/ManAuthMng/UpdateMenu',
            frame: true,
            title: '메뉴 수정',
            region: 'north',
            maxWidth: 330,
            height: 240,
            fbar: menuRightUpTbarItems,
            layout: {
                type: 'vbox',
                padding: 5
            },
            defaults: {
                anchor: '0',
                padding: '5px'
            },
            items: [{
                xtype: 'fieldset',
                width: 310,
                title: '메뉴 수정',
                items: [{
                    xtype: 'textfield',
                    fieldLabel: '메뉴ID',
                    hidden: true,
                    labelWidth: 80,
                    width: 296,
                    id: 'menuId1',
                    name: 'menuId1',
                    allowBlank: false
                }, {
                    xtype: 'textfield',
                    fieldLabel: '메뉴명',
                    labelWidth: 80,
                    width: 296,
                    id: 'menuNm1',
                    name: 'menuNm1',
                    allowBlank: false
                }, {
                    xtype: 'textfield',
                    fieldLabel: '메뉴URI',
                    labelWidth: 80,
                    width: 296,
                    id: 'uri1',
                    name: 'uri1'
                }, {
                    xtype: 'textfield',
                    fieldLabel: 'DEPTH',
                    labelWidth: 80,
                    width: 296,
                    disabled: true,
                    id: 'depth1',
                    name: 'depth1',
                    allowBlank: false
                }, {
                    xtype: 'textfield',
                    fieldLabel: '순서',
                    labelWidth: 80,
                    width: 296,
                    id: 'ord1',
                    name: 'ord1',
                    allowBlank: false
                }, {
                    xtype: 'combobox',
                    fieldLabel: '사용여부',
                    labelWidth: 80,
                    width: 296,
                    id: 'flag1',
                    name: 'flag1',
                    value: 'Y',
                    store: [
                        ['Y', '사용'],
                        ['N', '사용안함']
                    ]
                }]
            }]
        });


        /////////////////////////////// [[  메뉴 관리 오른쪽 상단 패널 종료 ]] ////////////////////////////


        /////////////////////////////// << 메뉴 관리 오른쪽 하단 패널 시작 >> ////////////////////////////

        /* 메뉴 관리 오른쪽 하단 bbar 아이템 정의 */
        var menuRightDownTbarItems = [
            '->',
            {
                text: '<span style="vertical-align:middle">저장</span>',
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/disk.png',
                itemId: 'save2',
                tooltip: '데이터 저장',
                handler: function () {
                    var val1 = trim(Ext.getCmp('menuNm2').getValue());
                    var val2 = trim(Ext.getCmp('uri2').getValue());
                    var val3 = trim(Ext.getCmp('depth2').getValue());
                    var val4 = trim(Ext.getCmp('ord2').getValue());

                    if (val1 == '' && val2 == '' && val3 == '' && val4 == '') {
                        Ext.Msg.alert('Info', '트리를 선택하세요.'); return;
                    }

                    if (!chkBlank('menuNm2', '메뉴명을 입력하세요.')) return;
                    if (!chkBlank('uri2', '메뉴URI를 입력하세요.')) return;
                    if (!chkBlank('depth2', 'DEPTH를 입력하세요.')) return;
                    if (!chkBlank('ord2', '순서를 입력하세요.')) return;

                    Ext.Msg.show({
                        title: 'Message',
                        msg: '데이터를 저장하시겠습니까?',
                        width: 150,
                        buttons: Ext.Msg.OKCANCEL,
                        icon: Ext.Msg.INFO,
                        fn: function (btn) {
                            if (btn == "ok") {
                                menuRightDownPanel.getForm().submit({
                                    success: function (form, action) {
                                        menuStore.load();
                                    },
                                    failure: function (form, action) {
                                        Ext.Msg.alert('Failed', '메뉴 추가 에러');
                                    }
                                });
                            }
                        }
                    });
                }
            }
        ];

        var menuRightDownPanel = Ext.create('Ext.form.Panel', {
            id: 'menuRightDownPanel',
            name: 'menuRightDownPanel',
            url: '/ManAuthMng/InsertMenu',
            frame: true,
            title: '메뉴 추가',
            region: 'north',
            maxWidth: 330,
            height: 240,
            fbar: menuRightDownTbarItems,
            layout: {
                type: 'vbox',
                padding: 5
            },
            defaults: {
                anchor: '0',
                padding: '5px'
            },
            items: [{
                xtype: 'fieldset',
                title: '메뉴 추가',
                width: 310,
                items: [{
                    xtype: 'textfield',
                    fieldLabel: '메뉴명',
                    labelWidth: 80,
                    width: 296,
                    id: 'menuNm2',
                    name: 'menuNm2',
                    allowBlank: false
                }, {
                    xtype: 'textfield',
                    fieldLabel: '메뉴URI',
                    labelWidth: 80,
                    width: 296,
                    id: 'uri2',
                    name: 'uri2'
                }, {
                    xtype: 'textfield',
                    fieldLabel: 'DEPTH',
                    labelWidth: 80,
                    width: 296,
                    id: 'depth2',
                    name: 'depth2',
                    allowBlank: false
                }, {
                    xtype: 'textfield',
                    fieldLabel: '순서',
                    labelWidth: 80,
                    width: 296,
                    id: 'ord2',
                    name: 'ord2',
                    allowBlank: false
                }, {
                    xtype: 'combobox',
                    fieldLabel: '사용여부',
                    labelWidth: 80,
                    width: 296,
                    id: 'flag2',
                    name: 'flag2',
                    value: 'Y',
                    store: [
                        ['Y', '사용'],
                        ['N', '사용안함']
                    ]
                }, {
                    xtype: 'textfield',
                    fieldLabel: '부모ID',
                    labelWidth: 80,
                    width: 296,
                    id: 'parentId2',
                    name: 'parentId2',
                    hidden: true
                }]
            }]
        });

        /////////////////////////////// << 메뉴 관리 오른쪽 하단 패널 종료 >> ////////////////////////////

        /* 메뉴 오른쪽 패널 정의 */
        var menuRightPanel = Ext.create('Ext.panel.Panel', {
            flex: 0.2,
            border: 0,
            region: 'center',
            layout: {
                type: 'vbox',
                align: 'stretch',
                padding: 0
            },
            items: [menuRightUpPanel,
                   {
                       xtype: 'displayfield',
                       id: 'displayUpDown',
                       name: 'displayUpDown',
                       value: '',
                       height: 20
                   }, menuRightDownPanel]
        });


        ///////////////////////////////  *****  메뉴 관리 오른쪽 그리스 종료  ***** ////////////////////////////


        /* 메뉴 메인 패널 정의 */
        var menuMainPanel = Ext.create('Ext.panel.Panel', {
            flex: 1.0,
            bodyPadding: 5,
            border: 0,
            region: 'center',
            layout: {
                type: 'hbox',
                align: 'stretch',
                padding: 0
            },
            items: [menuLeftPanel, menuMidPanel, menuRightPanel, menuRightSpPanel]
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
            }, menuMainPanel]
        });


    });

</script>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<div id="menu-title" class="title_box x-hide-display" align="center" style="text-align:left;">
	<span style="font-weight:bold; margin-left:10px;">
    <img src="<%=Page.ResolveUrl("/Images") %>/icons/gear.png" align="absmiddle"/>&nbsp;&nbsp;
    메뉴 관리
    </span>
</div>
</asp:Content>
