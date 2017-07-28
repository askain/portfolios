<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="HDIMS.Utils.Login"%>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	댐이상기준치 관리
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PreHeadContent" runat="server">
<script src="../../Scripts/code/editgrid.js" type="text/javascript"></script>
<style type="text/css">
    .myObs 
    {
        background-color:Red;
    }
</style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<%
    EmpData empdata = new EmpData();

    string MGTDAM = empdata.GetEmpData(6).Trim();
%>
<script type="text/javascript">
    var MGTDAM = '<%=MGTDAM %>';
    var exTp = 'D';

    Ext.onReady(function () {
        Ext.BLANK_IMAGE_URL = '/Scripts/extjs/s.gif';
        Ext.tip.QuickTipManager.init();

        function setCommit() {
            var storeCount = damBaselineStore.getCount();
            for (var i = 0; i < storeCount; i++) {
                record = damBaselineStore.getAt(i);
                if (record.dirty == true) {
                    record.commit();
                }
            }
        }

        /* 코드 모델 정의 - Ext.define() 함수를 이용한 클래스 정의 */
        Ext.define('CodeMode', {
            extend: 'Ext.data.Model',
            fields: [
                { name: 'KEY', type: 'string' },
                { name: 'VALUE', type: 'string' },
                { name: 'ORDERNUM', type: 'int' }
            ]
        });

        /* 댐 콤보 저장소 정의 */
        var damCodeStore = Ext.create('Ext.data.Store', {
            autoDestroy: true, model: 'CodeMode',
            proxy: {
                type: 'ajax',
                url: encodeURI('<%=Page.ResolveUrl("~/Common")%>/DamCodeList/?firstValue=전체'),
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
                        Ext.getCmp('damNm').setValue(records[0].data.KEY);
                    }
                }
            },
            sorters: [{ property: 'ORDERNUM', direction: 'ASC'}],
            autoLoad: true
        });

        var searchPanel = Ext.create('Ext.form.Panel', {
            bodyPadding: 0,
            frame: true,
            height: 100,
            bodyPadding: 5,
            layout: {
                type: 'hbox'
            },
            items: [{
                xtype: 'combo',
                id: 'damNm',
                fieldLabel: '댐명',
                labelWidth: 60,
                labelAlign: 'right',
                width: 190,
                displayField: 'VALUE',
                valueField: 'KEY',
                store: damCodeStore,
                queryMode: 'local',
                listeners: {
                    change: function (field, newValue, oldValue, options) { }
                }
            }, {
                xtype: 'displayfield',
                name: 'dispfld10',
                value: '',
                width: 10
            }, {
                xtype: 'button',
                name: 'submit2',
                text: '<span style="font-weight: bold;vertical-align:middle">이상기준치저장</span>',
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/disk--arrow.png',
                width: 120,
                height: 24,
                handler: function () {
                    Ext.Msg.show({
                        title: 'Message',
                        msg: '데이터를 저장하시겠습니까?',
                        width: 150,
                        buttons: Ext.Msg.OKCANCEL,
                        icon: Ext.Msg.INFO,
                        fn: function (btn) {
                            if (btn == "ok") {
                                damBaselineStore.sync();
                            }
                        }
                    });
                }
            }, {
                xtype: 'displayfield',
                name: 'dispfld10',
                value: '',
                width: 2
            }, {
                xtype: 'button',
                name: 'submit1',
                text: '<span style="font-weight: bold;vertical-align:middle">조 회</span>',
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/magnifier.png',
                width: 70,
                valign: 'top',
                height: 24,
                handler: function () {
                    var fp = searchPanel.getForm();
                    var damCd = fp.findField("damNm").getValue();

                    damBaselineStore.load({
                        params: {
                            damCd: damCd
                        }
                    });
                }
            }, {
                xtype: 'displayfield',
                name: 'dispfld10',
                value: '',
                width: 2
            }, {
                xtype: 'button',
                name: 'submit2',
                text: '<span style="font-weight: bold;vertical-align:middle">범 례</span>',
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/application-table.png',
                width: 70,
                valign: 'top',
                height: 24,
                handler: function () {
                    jQuery.popupWindow2({
                        windowName: "",
                        width: 1000,
                        height: 700,
                        windowURL: '<%=Page.ResolveUrl("~/Code")%>/DAM_Legend',
                        centerScreen: 1,
                        resizable: 1
                    });
                }
            }, {
                xtype: 'displayfield',
                name: 'dispfld10',
                value: '',
                width: 10
            }, {
                xtype: 'displayfield',
                name: 'dispfld10',
                value: '<span style="color:red; vertical-align:top">※ 이상치 기준값 변경은 이후 품질등급 산정에 영향을 줍니다</span>',
                width: 336
            }]
        });

        var comboYNField = {
            xtype: 'combobox',
            typeAhead: true,
            triggerAction: 'all',
            selectOnTab: true,
            store: [
                ['Y', '사용함'],
                ['N', '사용안함']
            ],
            lazyRender: true
        };

        Ext.define('damBaselineManagementModel', {
            extend: 'Ext.data.Model',
            idProperty: 'DAMCD',
            fields: [
                        { name: 'DAMCD', type: 'string' },
                        { name: 'DAMNM', type: 'string' },
                        { name: 'RWLNOCHGDT', type: 'string' },
                        { name: 'RWLSDCHG', type: 'string' },
                        { name: 'OSPILWLUSE', type: 'string' },
                        { name: 'OSPILWLNOCHGDT', type: 'string' },
                        { name: 'OSPILWLSDCHG', type: 'string' },
                        { name: 'IQTYMAX', type: 'string' },
                        { name: 'ETCIQTY2MAX', type: 'string' },
                        { name: 'EDQTYUSE', type: 'string' },
                        { name: 'EDQTYMAX', type: 'string' },
                        { name: 'EDQTYMIN', type: 'string' },
                        { name: 'ETCEDQTYUSE', type: 'string' },
                        { name: 'ETCEDQTYMAX', type: 'string' },
                        { name: 'ETCEDQTYMIN', type: 'string' },
                        { name: 'SPDQTYUSE', type: 'string' },
                        { name: 'SPDQTYMAX', type: 'string' },
                        { name: 'SPDQTYMIN', type: 'string' },
                        { name: 'ETCDQTY1USE', type: 'string' },
                        { name: 'ETCDQTY1MAX', type: 'string' },
                        { name: 'ETCDQTY1MIN', type: 'string' },
                        { name: 'ETCDQTY2USE', type: 'string' },
                        { name: 'ETCDQTY2MAX', type: 'string' },
                        { name: 'ETCDQTY2MIN', type: 'string' },
                        { name: 'ETCDQTY3USE', type: 'string' },
                        { name: 'ETCDQTY3MAX', type: 'string' },
                        { name: 'ETCDQTY3MIN', type: 'string' },
                        { name: 'OTLTDQTYUSE', type: 'string' },
                        { name: 'OTLTDQTYMAX', type: 'string' },
                        { name: 'OTLTDQTYMIN', type: 'string' },
                        { name: 'ITQTY1USE', type: 'string' },
                        { name: 'ITQTY1MAX', type: 'string' },
                        { name: 'ITQTY1MIN', type: 'string' },
                        { name: 'ITQTY2USE', type: 'string' },
                        { name: 'ITQTY2MAX', type: 'string' },
                        { name: 'ITQTY2MIN', type: 'string' },
                        { name: 'ITQTY3USE', type: 'string' },
                        { name: 'ITQTY3MAX', type: 'string' },
                        { name: 'ITQTY3MIN', type: 'string' }
                    ]
        });

        var damBaselineColumns = [
        { header: '댐명', flex: 0.5, height: 80, minWidth: 150, sortable: false, align: 'center', dataIndex: 'DAMNM',
            field: {
                xtype: 'panel'
            }
        },
        { header: '저수위<br/>(무변동:시간,급변:mm)',
            columns: [
                    { header: '무변동', width: 60, sortable: true, align: 'center', dataIndex: 'RWLNOCHGDT', /*renderer: setCursor,*/
                        field: {
                            xtype: 'textfield'
                        }
                    },
                    { header: '급변', width: 60, sortable: true, align: 'center', dataIndex: 'RWLSDCHG', /*renderer: setCursor,*/
                        field: {
                            xtype: 'textfield'
                        }
                    }
                ]
        },
        { header: '방수로수위<br/>(무변동:시간,급변:mm)',
            columns: [
                    { header: '사용여부', width: 60, sortable: true, align: 'center', dataIndex: 'OSPILWLUSE', renderer: getUseYN,
                        field: comboYNField
                    },
                    { header: '무변동', width: 60, sortable: true, align: 'center', dataIndex: 'OSPILWLNOCHGDT', /*renderer: setCursor,*/
                        field: {
                            xtype: 'textfield'
                        }
                    },
                    { header: '급변', width: 60, sortable: true, align: 'center', dataIndex: 'OSPILWLSDCHG', /*renderer: setCursor,*/
                        field: {
                            xtype: 'textfield'
                        }
                    }
                ]
        },
        { header: '총유입량<br/>(단위:배수)',
            columns: [
                { header: '상한', width: 70, sortable: true, align: 'center', dataIndex: 'IQTYMAX', /*renderer: setCursor,*/
                    field: {
                        xtype: 'textfield'
                    }
                }
            ]
        },
        { header: '발전방류량(단위:배수)<br/>[예:상한:2,하한:0.2]',
            columns: [
                { header: '사용여부', width: 60, sortable: true, align: 'center', dataIndex: 'EDQTYUSE', renderer: getUseYN,
                    field: comboYNField
                },
                { header: '상한', width: 60, sortable: true, align: 'center', dataIndex: 'EDQTYMAX', /*renderer: setCursor,*/
                    field: {
                        xtype: 'textfield'
                    }
                },
                { header: '하한', width: 60, sortable: true, align: 'center', dataIndex: 'EDQTYMIN', /*renderer: setCursor,*/
                    field: {
                        xtype: 'textfield'
                    }
                }
            ]
        },
        { header: '기타발전방류량(단위:배수)<br/>[예:상한:2,하한:0.2]',
            columns: [
                { header: '사용여부', width: 60, sortable: true, align: 'center', dataIndex: 'ETCEDQTYUSE', renderer: getUseYN,
                    field: comboYNField
                },
                { header: '상한', width: 60, sortable: true, align: 'center', dataIndex: 'ETCEDQTYMAX', /*renderer: setCursor,*/
                    field: {
                        xtype: 'textfield'
                    }
                },
                { header: '하한', width: 60, sortable: true, align: 'center', dataIndex: 'ETCEDQTYMIN', /*renderer: setCursor,*/
                    field: {
                        xtype: 'textfield'
                    }
                }
            ]
        },
        { header: '수문방류량(단위:배수)<br/>[예:상한:2,하한:0.2]',
            columns: [
                { header: '사용여부', width: 60, sortable: true, align: 'center', dataIndex: 'SPDQTYUSE', renderer: getUseYN,
                    field: comboYNField
                },
                { header: '상한', width: 60, sortable: true, align: 'center', dataIndex: 'SPDQTYMAX', /*renderer: setCursor,*/
                    field: {
                        xtype: 'textfield'
                    }
                },
                { header: '하한', width: 60, sortable: true, align: 'center', dataIndex: 'SPDQTYMIN', /*renderer: setCursor,*/
                    field: {
                        xtype: 'textfield'
                    }
                }
            ]
        },
        { header: '기타방류량1(단위:배수)<br/>[예:상한:2,하한:0.2]',
            columns: [
                { header: '사용여부', width: 60, sortable: true, align: 'center', dataIndex: 'ETCDQTY1USE', renderer: getUseYN,
                    field: comboYNField
                },
                { header: '상한', width: 60, sortable: true, align: 'center', dataIndex: 'ETCDQTY1MAX', /*renderer: setCursor,*/
                    field: {
                        xtype: 'textfield'
                    }
                },
                { header: '하한', width: 60, sortable: true, align: 'center', dataIndex: 'ETCDQTY1MIN', /*renderer: setCursor,*/
                    field: {
                        xtype: 'textfield'
                    }
                }
            ]
        },
        { header: '기타방류량2(단위:배수)<br/>[예:상한:2,하한:0.2]',
            columns: [
                { header: '사용여부', width: 60, sortable: true, align: 'center', dataIndex: 'ETCDQTY2USE', renderer: getUseYN,
                    field: comboYNField
                },
                { header: '상한', width: 60, sortable: true, align: 'center', dataIndex: 'ETCDQTY2MAX', /*renderer: setCursor,*/
                    field: {
                        xtype: 'textfield'
                    }
                },
                { header: '하한', width: 60, sortable: true, align: 'center', dataIndex: 'ETCDQTY2MIN', /*renderer: setCursor,*/
                    field: {
                        xtype: 'textfield'
                    }
                }
            ]
        },
        { header: '기타방류량3(단위:배수)<br/>[예:상한:2,하한:0.2]',
            columns: [
                { header: '사용여부', width: 60, sortable: true, align: 'center', dataIndex: 'ETCDQTY3USE', renderer: getUseYN,
                    field: comboYNField
                },
                { header: '상한', width: 60, sortable: true, align: 'center', dataIndex: 'ETCDQTY3MAX', /*renderer: setCursor,*/
                    field: {
                        xtype: 'textfield'
                    }
                },
                { header: '하한', width: 60, sortable: true, align: 'center', dataIndex: 'ETCDQTY3MIN', /*renderer: setCursor,*/
                    field: {
                        xtype: 'textfield'
                    }
                }
            ]
        },
        { header: '아울렛방류량(단위:배수)<br/>[예:상한:2,하한:0.2]',
            columns: [
                { header: '사용여부', width: 60, sortable: true, align: 'center', dataIndex: 'OTLTDQTYUSE', renderer: getUseYN,
                    field: comboYNField
                },
                { header: '상한', width: 60, sortable: true, align: 'center', dataIndex: 'OTLTDQTYMAX', /*renderer: setCursor,*/
                    field: {
                        xtype: 'textfield'
                    }
                },
                { header: '하한', width: 60, sortable: true, align: 'center', dataIndex: 'OTLTDQTYMIN', /*renderer: setCursor,*/
                    field: {
                        xtype: 'textfield'
                    }
                }
            ]
        },
        { header: '취수1(단위:배수)<br/>[예:상한:2,하한:0.2]',
            columns: [
                { header: '사용여부', width: 60, sortable: true, align: 'center', dataIndex: 'ITQTY1USE', renderer: getUseYN,
                    field: comboYNField
                },
                { header: '상한', width: 60, sortable: true, align: 'center', dataIndex: 'ITQTY1MAX', /*renderer: setCursor,*/
                    field: {
                        xtype: 'textfield'
                    }
                },
                { header: '하한', width: 60, sortable: true, align: 'center', dataIndex: 'ITQTY1MIN', /*renderer: setCursor,*/
                    field: {
                        xtype: 'textfield'
                    }
                }
            ]
        },
        { header: '취수2(단위:배수)<br/>[예:상한:2,하한:0.2]',
            columns: [
                { header: '사용여부', width: 60, sortable: true, align: 'center', dataIndex: 'ITQTY2USE', renderer: getUseYN,
                    field: comboYNField
                },
                { header: '상한', width: 60, sortable: true, align: 'center', dataIndex: 'ITQTY2MAX', /*renderer: setCursor,*/
                    field: {
                        xtype: 'textfield'
                    }
                },
                { header: '하한', width: 60, sortable: true, align: 'center', dataIndex: 'ITQTY2MIN', /*renderer: setCursor,*/
                    field: {
                        xtype: 'textfield'
                    }
                }
            ]
        },
        { header: '취수3(단위:배수)<br/>[예:상한:2,하한:0.2]',
            columns: [
                { header: '사용여부', width: 60, sortable: true, align: 'center', dataIndex: 'ITQTY3USE', renderer: getUseYN,
                    field: comboYNField
                },
                { header: '상한', width: 60, sortable: true, align: 'center', dataIndex: 'ITQTY3MAX', /*renderer: setCursor,*/
                    field: {
                        xtype: 'textfield'
                    }
                },
                { header: '하한', width: 60, sortable: true, align: 'center', dataIndex: 'ITQTY3MIN', /*renderer: setCursor,*/
                    field: {
                        xtype: 'textfield'
                    }
                }
            ]
        }
        ];

        var damBaselineStore = Ext.create('Ext.data.Store', {
            id: 'damBaselineStore',
            model: 'damBaselineManagementModel',
            autoDestroy: true,
            remoteSort: false,
            autoLoad: true,
            autoSync: false,
            simpleSortMode: true,
            proxy: {
                type: 'ajax',
                api: {
                    read: '<%=Page.ResolveUrl("~/Code")%>/GetDAMBaselineManagementList',
                    update: '<%=Page.ResolveUrl("~/Code")%>/InsertUpdateDAMBaselineManagement'
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
//                                ,
//                                load: function (store, records, successful, operation, eOpts) {
//                                    if (MGTDAM != 'MAIN' && records) {
//                                        for (var i = 0; i < records.length; i++) {
//                                            if (MGTDAM.indexOf(records[i].get('DAMCD')) != -1) {
//                                                alert(records[i].get('DAMCD'));
//                                                //alert(Ext.getCmp('grid').getView().getRow(i));
//                                                Ext.getCmp('grid').getView().addRowCls(records[i], 'myObs');
//                                            }
//                                        }
//                                    }
//                                }
            }
        });

        function setCursor(value, metaData, record, rowIndex, colIndex, store, view) {
            return '<div style="cursor:pointer;width:100%;">' + value + '</div>';
        }

        function getUseYN(value, metaData, record, rowIndex, colIndex, store) {
            return value == "Y" ? '<div style="cursor:pointer;width:100%;">' + "사용함" + '</div>' : '<div style="cursor:pointer;width:100%;">' + "사용안함" + '</div>';
        }

        function getColor(value) {
            var retVal = '<div id="test" name="test" style="cursor:pointer;width:100%;background-color:#' + value + '">' + value + '</div>';
            return retVal;
        }

        var baselineGrid = Ext.create('Edit.Grid', {
            id: 'grid',
            name: 'grid',
            flex: 1,
            region: 'center',
            columns: damBaselineColumns,
            store: damBaselineStore,
            columnLines: true,
//            viewConfig: {
//                showPreview: true, // custom property     
//                enableRowBody: true,
//                getRowClass: function (record, rowIndex, rowParams, store) {
//                    if (MGTDAM != 'MAIN') {
//                        if (MGTDAM.indexOf(record.get("DAMCD")) != -1) {
//                            alert(record.get("DAMCD"));
//                            return "h2";
//                        }
//                        return undefined;
//                    }
//                }
//            },
            listeners: {
                beforeedit: function (editor, e, options) {
                    var store = this.store;
                    var commitStoreCount = store.getTotalCount();
                    var totalStoreCount = store.getCount();
                    var addRowCount = totalStoreCount - commitStoreCount;

                    if (editor.rowIdx >= addRowCount) {
                        if (editor.colIdx == 0) editor.cancel = true;
                    }

                    //추가 : 자기 댐이 아닌경우는 수정이 안돼도록 함.
                    var rowItem = this.getStore().getAt(editor.rowIdx);
                    if (MGTDAM != 'MAIN' && MGTDAM.indexOf(rowItem.get('DAMCD')) == -1) {
                        return false;
                    }
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
                height: 86,
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
                }, searchPanel]
            }, baselineGrid]
        });
    });

</script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <div id="menu-title" class="title_box x-hide-display" align="center" style="text-align:left;">
	    <span style="font-weight:bold; margin-left:10px;">
        <img src="<%=Page.ResolveUrl("/Images") %>/icons/gear.png" align="absmiddle"/>&nbsp;&nbsp;
       댐 이상기준치관리
        </span>
    </div>
</asp:Content>
