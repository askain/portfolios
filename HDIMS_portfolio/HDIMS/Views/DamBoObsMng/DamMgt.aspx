<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	관리단 부서관리
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

        function getMsg() {
            var retMsg;
            var cnt = parseInt(Ext.getCmp('HID_CHILD_CNT').getValue());

            if (cnt > 0) {
                retMsg = "선택하신 항목과</br>자식 노드까지 삭제됩니다.<br/>데이터를 삭제하시겠습니까?";
            }
            else if (cnt == 0) {
                retMsg = "데이터를 삭제하시겠습니까?";
            }
            return retMsg;
        }

        function chkBlank(id, msg) {
            if (trim(Ext.getCmp(id).getValue()) == '') {
                Ext.Msg.alert('Info', msg);
                return false;
            }
            return true;
        }

        function setRightPanel() {
            Ext.getCmp('MGTCD1').setValue('');
            Ext.getCmp('MGTNM1').setValue(' ');
            Ext.getCmp('MGTCOMMENT1').setValue('');
            Ext.getCmp('MGTLVL1').setValue('');
            Ext.getCmp('MGTORD1').setValue(' ');
            Ext.getCmp('USEYN1').setValue('Y');

            Ext.getCmp('MGTNM2').setValue('');
            Ext.getCmp('MGTCOMMENT2').setValue('');
            Ext.getCmp('MGTLVL2').setValue(' ');
            Ext.getCmp('MGTORD2').setValue(' ');
            Ext.getCmp('USEYN2').setValue('Y');
            damMgtRightUpPanel.show();
            damMgtRightUpPanel.setTitle('부서 수정');
            damMgtRightDownPanel.show();
            damMgtRightDownPanel.setTitle('부서 추가');
        }

        ///////////////////////////////  메뉴 관리 왼쪽 tree 시작 ////////////////////////////

        /* 모델 정의 */
        Ext.define('MgtOrdModel', {
            extend: 'Ext.data.Model',
            fields: [
                        { name: 'NODECOUNT', type: 'int' }
                    ]
        });

        /* 저장소 정의 */
        var ordStore = Ext.create('Ext.data.Store', {
            autoDestroy: true,
            model: 'MgtOrdModel',
            proxy: {
                type: 'ajax',
                url: '/DamBoObsMng/GetDamMgtNodeCount',
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
                        Ext.getCmp('MGTORD2').setValue(ordNum);
                    }
                }
            },
            autoLoad: false
        });

        function initFormValue() {
            Ext.getCmp('MGTCD1').setValue("");
            Ext.getCmp('MGTNM1').setValue("");
            Ext.getCmp('MGTCOMMENT1').setValue("");
            Ext.getCmp('MGTLVL1').setValue("");
            Ext.getCmp('HID_MGTLVL1').setValue("");
            Ext.getCmp('MGTORD1').setValue("");
            Ext.getCmp('USEYN1').setValue("");

            Ext.getCmp('MGTNM2').setValue("");
            Ext.getCmp('MGTCOMMENT2').setValue("");
            Ext.getCmp('MGTLVL2').setValue("");
            Ext.getCmp('MGTORD2').setValue("");
            Ext.getCmp('USEYN2').setValue("");
            Ext.getCmp('PARCD2').setValue("");
        };

        var damMgtModel = Ext.define('DamMgtModel', {
            extend: 'Ext.data.Model',
            idProperty: 'MGTCD',
            fields: [
                { name: 'MGTCD', type: 'string' },
                { name: 'MGTNM', type: 'string' },
                { name: 'USEYN', type: 'string' },
                { name: 'MGTORD', type: 'int' },
                { name: 'MGTCOMMENT', type: 'string' },
                { name: 'PARCD', type: 'string' },
                { name: 'MGTLVL', type: 'int' },
                { name: 'CHILD_CNT', type: 'int' }
            ]
        });

        var damMgtStore = Ext.create('Ext.data.TreeStore', {
            proxy: {
                type: 'ajax',
                url: '/DamBoObsMng/GetDamMgtTreeList',
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
            defaultRootId: "0000",
            model: "DamMgtModel",
            folderSort: true,
            autoLoad: false,
            sorters: [{
                property: 'MGTORD',
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
            store: damMgtStore,
            collapsible: false,
            split: true,
            rootVisible: false,
            autoScroll: true,
            margins: '0 5 5 0',
            displayField: 'MGTNM',
            root: {
                expanded: true
            },
            listeners: {
                itemclick: function (view, record, item, index, e, options) {
                    initFormValue();
                    var mgtLvl;
                    switch (record.data.MGTLVL) {
                        case 1:
                            mgtLvl = "[대분류]";
                            break;
                        case 2:
                            mgtLvl = "[중분류]";
                            break;
                        case 3:
                            mgtLvl = "[소분류]";
                            break;
                        default:
                            mgtLvl = "[Root]";
                            break;
                    }


                    // MGTLVL=1(본사) 클릭시 추가만 가능
                    if (mgtLvl == "[대분류]") {
                        Ext.getCmp('delete1').hide();
                    }
                    else {
                        Ext.getCmp('delete1').show();
                    }

                    // Root 클릭시 추가만 가능
                    if (mgtLvl == "[Root]") {
                        damMgtRightUpPanel.hide();
                        Ext.getCmp('displayUpDown').hide();
                    }
                    else {
                        damMgtRightUpPanel.show();
                        Ext.getCmp('displayUpDown').show();
                    }

                    // 최하위 DETPTH 클릭시 수정만 가능
                    if (parseInt(record.data.MGTLVL) > 2) {
                        damMgtRightDownPanel.hide();
                    }
                    else {
                        damMgtRightDownPanel.show();
                    }

                    // field Value Settign(Root 아닐때)
                    if (record.data.MGTCD != 'root') {
                        Ext.getCmp('MGTCD1').setValue(record.data.MGTCD);
                        Ext.getCmp('MGTNM1').setValue(record.data.MGTNM);
                        Ext.getCmp('MGTCOMMENT1').setValue(record.data.MGTCOMMENT);
                        Ext.getCmp('MGTLVL1').setValue(record.data.MGTLVL);
                        Ext.getCmp('HID_MGTLVL1').setValue(record.data.MGTLVL);
                        Ext.getCmp('MGTORD1').setValue(record.data.MGTORD);
                        Ext.getCmp('USEYN1').setValue(record.data.USEYN);
                        Ext.getCmp('HID_CHILD_CNT').setValue(record.data.CHILD_CNT);

                        //메뉴 수정 Title Setting
                        damMgtRightUpPanel.setTitle(record.data.MGTNM + ' ' + mgtLvl);

                        //메뉴 추가 부모ID Setting
                        var parent = record.data.MGTCD;
                        if (parent == 'root') {
                            parent = null;
                        }
                        //alert(parent);
                        Ext.getCmp('PARCD2').setValue(parent);
                    }

                    var part;
                    switch (mgtLvl) {
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
                    if (record.data.MGTNM == "Root") {
                        record.data.MGTNM = '';
                    }
                    // 메뉴 추가 Title Setting
                    damMgtRightDownPanel.setTitle(part);

                    // 메뉴 추가 DEPTH Setting
                    Ext.getCmp('MGTLVL2').setValue(parseInt(record.data.MGTLVL) + 1);

                    // 메뉴 추가 순서 Setting
                    var currId = record.data.MGTCD;
                    ordStore.load({
                        params: {
                            mgtcd: currId
                        }
                    });

                    // 메뉴 추가 사용여부 Setting
                    Ext.getCmp('USEYN2').setValue('Y');
                }
            }
        });

        /* 메뉴 왼쪽 패널 정의 */
        var damMgtLeftPanel = Ext.create('Ext.panel.Panel', {
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
        var damMgtMidPanel = Ext.create('Ext.panel.Panel', {
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
        var damMgtRightSpPanel = Ext.create('Ext.panel.Panel', {
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
        var damMgtRightUpTbarItems = [
            '->',
            {
                text: '<span style="vertical-align:middle">저 장</span>',
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/disk.png',
                id: 'save1',
                formBind: true,
                disabled: false,
                tooltip: '저 장',
                formBind: true,
                handler: function () {
                    var val1 = trim(Ext.getCmp('MGTNM1').getValue());
                    var val2 = trim(Ext.getCmp('MGTCOMMENT1').getValue());
                    var val3 = trim(Ext.getCmp('MGTLVL1').getValue());
                    var val4 = trim(Ext.getCmp('MGTORD1').getValue());

                    if (val1 == '' && val2 == '' && val3 == '' && val4 == '') {
                        Ext.Msg.alert('Info', '트리를 선택하세요.'); return;
                    }

                    if (!chkBlank('MGTNM1', '부서명을 입력하세요.')) return;
                    if (!chkBlank('MGTCOMMENT1', '부서약자를 입력하세요.')) return;
                    if (!chkBlank('MGTORD1', '순서를 입력하세요.')) return;
                    if (!chkBlank('USEYN1', '사용여부를 입력하세요.')) return;

                    Ext.Msg.show({
                        title: 'Message',
                        msg: '데이터를 저장하시겠습니까?',
                        width: 150,
                        buttons: Ext.Msg.OKCANCEL,
                        icon: Ext.Msg.INFO,
                        fn: function (btn) {
                            if (btn == "ok") {
                                Ext.getCmp('HID_MGT_INSERT_UPDATE').setValue('Update');
                                damMgtRightUpPanel.getForm().submit({
                                    success: function (form, action) {
                                        damMgtStore.load();
                                        setRightPanel();
                                    },
                                    failure: function (form, action) {
                                        Ext.Msg.alert('Failed', '메뉴 수정 에러');
                                    }
                                });
                            }
                        }
                    });
                }
            },
            {
                text: '<span style="vertical-align:middle">삭 제</span>',
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/minus-circle-frame.png',
                id: 'delete1',
                formBind: true,
                disabled: false,
                tooltip: '삭 제',
                formBind: true,
                handler: function () {
                    var val1 = trim(Ext.getCmp('MGTNM1').getValue());
                    var val2 = trim(Ext.getCmp('MGTCOMMENT1').getValue());
                    var val3 = trim(Ext.getCmp('MGTLVL1').getValue());
                    var val4 = trim(Ext.getCmp('MGTORD1').getValue());

                    if (val1 == '' && val2 == '' && val3 == '' && val4 == '') {
                        Ext.Msg.alert('Info', '트리를 선택하세요.'); return;
                    }

                    if (!chkBlank('MGTNM1', '부서명을 입력하세요.')) return;
                    if (!chkBlank('MGTORD1', '순서를 입력하세요.')) return;

                    Ext.Msg.show({
                        title: 'Message',
                        msg: getMsg(),
                        width: 150,
                        buttons: Ext.Msg.OKCANCEL,
                        icon: Ext.Msg.INFO,
                        fn: function (btn) {
                            if (btn == "ok") {
                                Ext.getCmp('HID_MGT_INSERT_UPDATE').setValue('Delete');
                                damMgtRightUpPanel.getForm().submit({
                                    success: function (form, action) {
                                        damMgtStore.load();
                                        setRightPanel();
                                    },
                                    failure: function (form, action) {
                                        Ext.Msg.alert('Failed', '메뉴 삭제 에러');
                                    }
                                });
                            }
                        }
                    });
                }
            }
        ];

        var damMgtRightUpPanel = Ext.create('Ext.form.Panel', {
            id: 'damMgtRightUpPanel',
            name: 'damMgtRightUpPanel',
            url: '/DamBoObsMng/UpdateDeleteDamMgt',
            frame: true,
            title: '부서 수정',
            region: 'north',
            maxWidth: 330,
            height: 240,
            fbar: damMgtRightUpTbarItems,
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
                title: '부서 수정',
                items: [{
                    xtype: 'textfield',
                    fieldLabel: '부서ID',
                    hidden: true,
                    labelWidth: 80,
                    width: 296,
                    id: 'MGTCD1',
                    name: 'MGTCD1',
                    allowBlank: false
                }, {
                    xtype: 'textfield',
                    fieldLabel: '부서명',
                    labelWidth: 80,
                    width: 296,
                    id: 'MGTNM1',
                    name: 'MGTNM1',
                    allowBlank: false
                }, {
                    xtype: 'textfield',
                    fieldLabel: '부서 약자',
                    labelWidth: 80,
                    width: 296,
                    id: 'MGTCOMMENT1',
                    name: 'MGTCOMMENT1'
                }, {
                    xtype: 'textfield',
                    fieldLabel: 'LEVEL',
                    labelWidth: 80,
                    width: 296,
                    disabled: true,
                    id: 'MGTLVL1',
                    name: 'MGTLVL1'
                }, {
                    xtype: 'hiddenfield',
                    id: 'HID_MGTLVL1',
                    name: 'HID_MGTLVL1'
                }, {
                    xtype: 'textfield',
                    fieldLabel: '순서',
                    labelWidth: 80,
                    width: 296,
                    id: 'MGTORD1',
                    name: 'MGTORD1',
                    allowBlank: false
                }, {
                    xtype: 'combobox',
                    fieldLabel: '사용여부',
                    labelWidth: 80,
                    width: 296,
                    id: 'USEYN1',
                    name: 'USEYN1',
                    value: 'Y',
                    store: [
                        ['Y', '사용'],
                        ['N', '사용안함']
                    ]
                }, {
                    xtype: 'hiddenfield',
                    id: 'HID_CHILD_CNT',
                    name: 'HID_CHILD_CNT'
                }, {
                    xtype: 'hiddenfield',
                    id: 'HID_MGT_INSERT_UPDATE',
                    name: 'HID_MGT_INSERT_UPDATE'
                }]
            }]
        });


        /////////////////////////////// [[  메뉴 관리 오른쪽 상단 패널 종료 ]] ////////////////////////////


        /////////////////////////////// << 메뉴 관리 오른쪽 하단 패널 시작 >> ////////////////////////////

        /* 메뉴 관리 오른쪽 하단 bbar 아이템 정의 */
        var damMgtRightDownTbarItems = [
            '->',
            {
                text: '<span style="vertical-align:middle">저장</span>',
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/disk.png',
                id: 'save2',
                tooltip: '데이터 저장',
                handler: function () {
                    var val1 = trim(Ext.getCmp('MGTNM2').getValue());
                    var val2 = trim(Ext.getCmp('MGTCOMMENT2').getValue());
                    var val3 = trim(Ext.getCmp('MGTLVL2').getValue());
                    var val4 = trim(Ext.getCmp('MGTORD2').getValue());

                    if (val1 == '' && val2 == '' && val3 == '' && val4 == '') {
                        Ext.Msg.alert('Info', '트리를 선택하세요.'); return;
                    }

                    if (!chkBlank('MGTNM2', '부서명을 입력하세요.')) return;
                    if (!chkBlank('MGTCOMMENT2', '부서약자를 입력하세요.')) return;
                    if (!chkBlank('MGTLVL2', 'LEVEL을 입력하세요.')) return;
                    if (!chkBlank('MGTORD2', '순서를 입력하세요.')) return;
                    if (!chkBlank('USEYN2', '사용여부를 입력하세요.')) return;

                    Ext.Msg.show({
                        title: 'Message',
                        msg: '데이터를 저장하시겠습니까?',
                        width: 150,
                        buttons: Ext.Msg.OKCANCEL,
                        icon: Ext.Msg.INFO,
                        fn: function (btn) {
                            if (btn == "ok") {
                                damMgtRightDownPanel.getForm().submit({
                                    success: function (form, action) {
                                        damMgtStore.load();
                                        setRightPanel();
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

        var damMgtRightDownPanel = Ext.create('Ext.form.Panel', {
            id: 'damMgtRightDownPanel',
            name: 'damMgtRightDownPanel',
            url: '/DamBoObsMng/InsertDamMgt',
            frame: true,
            title: '부서 추가',
            region: 'north',
            maxWidth: 330,
            height: 240,
            fbar: damMgtRightDownTbarItems,
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
                title: '부서 추가',
                width: 310,
                items: [{
                    xtype: 'textfield',
                    fieldLabel: '부서명',
                    labelWidth: 80,
                    width: 296,
                    id: 'MGTNM2',
                    name: 'MGTNM2'
                }, {
                    xtype: 'textfield',
                    fieldLabel: '부서약자',
                    labelWidth: 80,
                    width: 296,
                    id: 'MGTCOMMENT2',
                    name: 'MGTCOMMENT2'
                }, {
                    xtype: 'textfield',
                    fieldLabel: 'LEVEL',
                    labelWidth: 80,
                    width: 296,
                    id: 'MGTLVL2',
                    name: 'MGTLVL2',
                    allowBlank: false
                }, {
                    xtype: 'textfield',
                    fieldLabel: '순서',
                    labelWidth: 80,
                    width: 296,
                    id: 'MGTORD2',
                    name: 'MGTORD2',
                    allowBlank: false
                }, {
                    xtype: 'combobox',
                    fieldLabel: '사용여부',
                    labelWidth: 80,
                    width: 296,
                    id: 'USEYN2',
                    name: 'USEYN2',
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
                    id: 'PARCD2',
                    name: 'PARCD2',
                    hidden: true
                }]
            }]
        });

        /////////////////////////////// << 메뉴 관리 오른쪽 하단 패널 종료 >> ////////////////////////////

        /* 메뉴 오른쪽 패널 정의 */
        var damMgtRightPanel = Ext.create('Ext.panel.Panel', {
            flex: 0.2,
            border: 0,
            region: 'center',
            layout: {
                type: 'vbox',
                align: 'stretch',
                padding: 0
            },
            items: [damMgtRightUpPanel,
                   {
                       xtype: 'displayfield',
                       id: 'displayUpDown',
                       name: 'displayUpDown',
                       value: '',
                       height: 20
                   }, damMgtRightDownPanel]
        });


        ///////////////////////////////  *****  메뉴 관리 오른쪽 그리스 종료  ***** ////////////////////////////


        /* 메뉴 메인 패널 정의 */
        var damMgtPanel = Ext.create('Ext.panel.Panel', {
            flex: 1.0,
            bodyPadding: 5,
            border: 0,
            region: 'center',
            layout: {
                type: 'hbox',
                align: 'stretch',
                padding: 0
            },
            items: [damMgtLeftPanel, damMgtMidPanel, damMgtRightPanel, damMgtRightSpPanel]
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
            }, damMgtPanel]
        });

    });
    
</script>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<div id="menu-title" class="title_box x-hide-display" align="center" style="text-align:left;">
	<span style="font-weight:bold; margin-left:10px;">
    <img src="<%=Page.ResolveUrl("/Images") %>/icons/gear.png" align="absmiddle"/>&nbsp;&nbsp;
    관리단 부서관리
    </span>
</div>
</asp:Content>
