<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	댐 품질등급 관리
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
<script src="../../Scripts/colorfield/colorfield.js" type="text/javascript"></script>
<script type="text/javascript">

    Ext.onReady(function () {
        Ext.BLANK_IMAGE_URL = '/Scripts/extjs/s.gif';
        Ext.tip.QuickTipManager.init();

        var ExColumnRenderer = function (value, metaData, record, rowIndex, colIndex, store) {
            var ret = '';
            switch (value) {
                case 'COMMON': ret = '공통'; break;
                case 'RWL': ret = '저수위'; break;
				case 'OSPILWL': ret = '방수로수위'; break;
                case 'IQTY': ret = '유입량'; break;
                case 'ETCIQTY2': ret = '기타유입량2'; break;
                case 'EDQTY': ret = '발전방류량'; break;
                case 'ETCEDQTY': ret = '기타발전방류량'; break;
                case 'SPDQTY': ret = '수문방류량'; break;
                case 'ETCDQTY1': ret = '기타방류량1'; break;
                case 'ETCDQTY2': ret = '기타방류량2'; break;
                case 'ETCDQTY3': ret = '기타방류량3'; break;
                case 'OTLTDQTY': ret = '아울렛방류량'; break;
                case 'ITQTY1': ret = '취수1'; break;
                case 'ITQTY2': ret = '취수2'; break;
                case 'ITQTY3': ret = '취수3'; break;
            }
            return ret;
        };

        Ext.define('Writer.Grid', {
            extend: 'Ext.grid.Panel',
            alias: 'widget.examGrid',

            initComponent: function () {

                this.editing = Ext.create('Ext.grid.plugin.CellEditing');

                Ext.apply(this, {
                    frame: true,
                    plugins: [this.editing],
                    dockedItems: [{
                        xtype: 'toolbar',
                        items: [
                        {
                            xtype: 'displayfield',
                            id: 'dpf',
                            name: 'dispfld2',
                            width: 110,
                            value: '<table><tr><td style="color:red;padding-bottom:2">* 추가시 필수입력</td></tr></table>'
                        }
                        , '->',
                        {
                            icon: '<%=Page.ResolveUrl("~/Images") %>/icons/plus-circle-frame.png',
                            text: '<span style="vertical-align:middle">추가</span>',
                            itemId: 'add',
                            tooltip: '행 추가',
                            scope: this,
                            handler: this.onAddClick
                        }, '-', {
                            icon: '<%=Page.ResolveUrl("~/Images") %>/icons/minus-circle-frame.png',
                            text: '<span style="vertical-align:middle">삭제</span>',
                            disabled: true,
                            itemId: 'delete',
                            tooltip: '행 삭제',
                            scope: this,
                            handler: this.onDeleteClick
                        }, '-', {
                            icon: '<%=Page.ResolveUrl("~/Images") %>/icons/disk.png',
                            text: '<span style="vertical-align:middle">저장</span>',
                            itemId: 'save',
                            tooltip: '데이터 저장',
                            scope: this,
                            handler: this.onSaveClick
                        }, '-', {
                            icon: '<%=Page.ResolveUrl("~/Images") %>/icons/navigation-000-frame.png',
                            text: '<span style="vertical-align:middle">조회</span>',
                            itemId: 'search',
                            tooltip: '다시 조회',
                            scope: this,
                            handler: this.onSearchClick
                        }]
                    }],
                    columns: [
                        { header: '검정코드<span style="color:red;"> *</span>', flex: 0.4, minWidth: 70, sortable: false, align: 'center', dataIndex: 'EXCD',
                            field: {
                                xtype: 'textfield',
                                allowBlank: false
                            }
                        }, { text: '항목<span style="color:red;"> *</span>', flex: 0.6, minWidth: 60, sortable: false, align: 'center', dataIndex: 'EXCOLUMN', renderer: ExColumnRenderer,
                            field: {
                                xtype: 'combobox',
                                typeAhead: true,
                                triggerAction: 'all',
                                selectOnTab: true,
                                allowBlank: false,
                                store: [
                                    ['COMMON', '공통'],
                                    ['RWL', '저수위'],
									['OSPILWL', '방수로수위'],
                                    ['IQTY', '유입량'],
                                    ['ETCIQTY2', '기타유입량2'],
                                    ['EDQTY', '발전방류량'],
                                    ['ETCEDQTY', '기타발전방류량'],
                                    ['SPDQTY', '수문방류량'],
                                    ['ETCDQTY1', '기타방류량1'],
                                    ['ETCDQTY2', '기타방류량2'],
                                    ['ETCDQTY3', '기타방류량3'],
                                    ['OTLTDQTY', '아울렛방류량'],
                                    ['ITQTY1', '취수1'],
                                    ['ITQTY2', '취수2'],
                                    ['ITQTY3', '취수3']
                                ],
                                lazyRender: true
                            }
                        }, { text: '우선순위<span style="color:red;"> *</span>', flex: 0.5, minWidth: 60, sortable: false, align: 'center', dataIndex: 'EXORD', renderer: setCursor,
                            field: {
                                xtype: 'textfield'
                            }
                        }, { header: '<div style="text-align:center;">설명</div>', flex: 3.2, minWidth: 150, sortable: false, align: 'left', dataIndex: 'EXCONT', renderer: setCursor,
                            field: {
                                xtype: 'textfield'
                            }
                        }, { header: '<div style="text-align:center;">요약</div>', flex: 1.5, minWidth: 80, sortable: false, align: 'left', dataIndex: 'EXNOTE', renderer: setCursor,
                            field: {
                                xtype: 'textfield'
                            }
                        }, { header: '<div style="text-align:center;">수행주기(10분)</div>', width: 100, sortable: false, align: 'center', dataIndex: 'PROCINT',
                            field: { type: 'numberfield' }
                        }, { header: '사용여부', flex: 0.7, minWidth: 60, sortable: false, align: 'center', dataIndex: 'EXYN', renderer: getUseYN,
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
                        }, { header: '항목명', flex: 0.5, minWidth: 60, sortable: false, align: 'center', dataIndex: 'EXCOLUMNNM', id: 'EXCOLUMNNM', hidden: true,
                            field: {
                                xtype: 'textfield'
                            }
                        }, { header: '색상', flex: 0.5, minWidth: 60, sortable: false, align: 'center', dataIndex: 'EXCOLOR', id: 'EXCOLOR', renderer: getColor,
                            field: {
                                xtype: 'colorfield',
                                id: 'cf',
                                name: 'cf',
                                value: 'FFFFFF'
                            }
                        }, { header: '<div style="text-align:center;">최종수행시간</div>', width: 120, sortable: false, align: 'center', dataIndex: 'PROCDT', renderer: setProcDt
                        }],
                    listeners: {
                        beforeedit: function (editor, e, options) {
                            var commitStoreCount = examStore.getTotalCount();
                            var totalStoreCount = examStore.getCount();
                            var addRowCount = totalStoreCount - commitStoreCount;

                            if (editor.rowIdx >= addRowCount) {
                                if (editor.colIdx == 0 || editor.colIdx == 1) editor.cancel = true;
                            }
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

                var storeCount = examStore.getCount();
                for (var i = 0; i < storeCount; i++) {
                    record = examStore.getAt(i);
                    if (record.dirty == true) {
                        if (examStore.getAt(i).data.EXCD == '' || examStore.getAt(i).data.EXCOLUMN == '' || examStore.getAt(i).data.EXORD == '') {
                            Ext.Msg.alert("Message", "검정코드, 항목, 우선순위는</br> 필수 입력 항목입니다.");
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
                            examStore.sync();
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
                examStore.load();
            },

            onAddClick: function () {

                var rec = new examManagementModel({
                    EXCD: '',
                    EXTP: 'C',
                    EXORD: '',
                    EXCONT: '',
                    EXNOTE: '',
                    EXYN: 'Y',
                    EXCOLOR: 'FFFFFF',
                    PROCINT: 10
                }), edit = this.editing;

                rec.setDirty();
                defaultEditable: true;
                this.store.insert(0, rec);
                edit.startEditByPosition({
                    row: 0,
                    column: 0
                });
            }
        });

        function setCursor(value, metaData, record, rowIndex, colIndex, store, view) {
            return '<div style="cursor:pointer;width:100%;">' + value + '</div>';
        }

        function setProcDt(value, meta, record, rowIndex) {
            return convStrToDateMin(value);
        }

        function getUseYN(value, metaData, record, rowIndex, colIndex, store) {
            return value == "Y" ? '<div style="cursor:pointer;width:100%;">' + "사용함" + '</div>' : '<div style="cursor:pointer;width:100%;">' + "사용안함+ '</div>'";
        }

        function getColor(value) {
            var retVal = '<div id="test" name="test" style="cursor:pointer;width:100%;background-color:#' + value + '">' + value + '</div>';
            return retVal;
        }

        function setCommit() {
            var storeCount = examStore.getCount();
            for (var i = 0; i < storeCount; i++) {
                record = examStore.getAt(i);
                if (record.dirty == true) {

                    var exColumnNm = ExColumnRenderer(record.get("EXCOLUMN"));
                    record.set("EXCOLUMNNM", exColumnNm);

                    record.commit();
                }
            }
            examStore.load();
        }

        Ext.define('examManagementModel', {
            extend: 'Ext.data.Model',
            idProperty: 'ID',
            fields: [
                    { name: 'EXCD', type: 'string' },
                    { name: 'EXCOLUMN', type: 'string' },
                    { name: 'EXORD', type: 'string' },
                    { name: 'EXCONT', type: 'string' },
                    { name: 'EXNOTE', type: 'string' },
                    { name: 'EXYN', type: 'string' },
                    { name: 'EXCOLOR', type: 'string' },
                    { name: 'EXCOLUMNNM', type: 'string' },
                    { name: 'PROCDT', type: 'string' },
                    { name: 'PROCINT', type: 'int' }
           ]
            //            validations: [{
            //                type: 'length',
            //                field: 'EXCD',
            //                min: 1
            //            }, {
            //                type: 'length',
            //                field: 'EXTP',
            //                min: 1
            //            }, {
            //                type: 'length',
            //                field: 'EXORD',
            //                min: 1
            //            }, {
            //                type: 'length',
            //                field: 'EXCONT',
            //                min: 1
            //            }, {
            //                type: 'length',
            //                field: 'EXCOLOR',
            //                min: 1
            //            }, {
            //                type: 'length',
            //                field: 'EXYN',
            //                min: 1
            //            }, {
            //                type: 'length',
            //                field: 'EXNOTE',
            //                min: 1
            //            }],
        });

        var examStore = Ext.create('Ext.data.Store', {
            id: 'examStore',
            model: 'examManagementModel',
            autoDestroy: true,
            remoteSort: false,
            autoLoad: true,
            autoSync: false,
            simpleSortMode: true,
            proxy: {
                type: 'ajax',
                api: {
                    read: '<%=Page.ResolveUrl("~/Code")%>/GetDAMExamManagementList',
                    create: '<%=Page.ResolveUrl("~/Code")%>/InsertDAMExamManagement',
                    update: '<%=Page.ResolveUrl("~/Code")%>/UpdateDAMExamManagement',
                    destroy: '<%=Page.ResolveUrl("~/Code")%>/DeleteDAMExamManagement'
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
                        if (json) {
                            Ext.MessageBox.show({
                                //title: 'REMOTE EXCEPTION',
                                //msg: json.msg,
                                title: '에러',
                                msg: json.msg,
                                icon: Ext.MessageBox.ERROR,
                                buttons: Ext.Msg.OK
                            });
                        }
                    }
                }
            },
            listeners: {
                load: function (store, records, successful, operation, options) {
                },
                add: function (store, records, operation) {
                },
                update: function (store, record, operation) {
                    var procInt = parseInt(record.get("PROCINT"));
                    if (procInt == NaN || procInt < 10 || procInt % 10 != 0) {
                        alert("수행주기는 10분단위로 입력하셔야 합니다.");
                        record.reject();
                    }
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
                xtype: 'examGrid',
                flex: 1,
                region: 'center',
                store: examStore,
                listeners: {
                    selectionchange: function (selModel, selected) {
                    }
                }
            }
            //            , { xtype: 'colorfield',
            //                id: 'cf2',
            //                name: 'cf2',
            //                value: 'ffffff',
            //                region: 'south',
            //                menuListeners: {
            //                    select: function (e, color) {
            //                        //examStore.getAt(rowindex).data.EXCOLOR = color;
            //                        this.setValue(color);
            //                    }
            //                }
            //            }
            ]
        });
    });
</script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div id="menu-title" class="title_box x-hide-display" align="center" style="text-align:left;">
	<span style="font-weight:bold; margin-left:10px;">
    <img src="<%=Page.ResolveUrl("/Images") %>/icons/gear.png" align="absmiddle"/>&nbsp;&nbsp;
    댐 검정코드 관리
    </span>
</div>
</asp:Content>
