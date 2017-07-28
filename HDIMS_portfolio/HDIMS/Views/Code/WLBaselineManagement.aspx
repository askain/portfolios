<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="HDIMS.Utils.Login"%>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	이상기준치 관리(수위)
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PreHeadContent" runat="server">
<script src="../../Scripts/code/editgrid.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<%
    EmpData empdata = new EmpData();

    string MGTDAM = empdata.GetEmpData(6).Trim();
%>
<script type="text/javascript">
    var MGTDAM = '<%=MGTDAM %>';
    var exTp = 'W';     //검정코드 현황 범례 팝업 사용 W: 수위, R: 우량
    var obsTp = 'wl';   //콤보 변경시 댐별 관측국 변경 사용 WL: 수위국, RF: 우량국

    Ext.onReady(function () {
        Ext.BLANK_IMAGE_URL = '/Scripts/extjs/s.gif';
        Ext.tip.QuickTipManager.init();

        function setCursor(value, metaData, record, rowIndex, colIndex, store, view) {
            return '<div style="cursor:pointer;width:100%;">' + value + '</div>';
        }

        function setCommit() {
            var storeCount = wlBaselineStore.getCount();
            for (var i = 0; i < storeCount; i++) {
                record = wlBaselineStore.getAt(i);
                if (record.dirty == true) {
                    record.commit();
                }
            }
        }

        var switchRadio = function (tp) {
            var damVal = Ext.getCmp("damNm").getValue();
            var obsVal = Ext.getCmp("obsNm").getValue();

            var wlUrl = '/WLBaselineManagement';
            var rfUrl = '/RFBaselineManagement/?damValue=' + damVal;
            var url = '<%=Page.ResolveUrl("~/Code") %>';
            if (tp == 'wl') {
                url = url + wlUrl;
            } else {
                url = url + rfUrl;
            }
            document.location.href = url;
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
                url: '<%=Page.ResolveUrl("~/Common")%>/DamCodeList',
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

                    if ('<%=ViewData["damVal"] %>' == "") {
                        Ext.getCmp('damNm').setValue(records[0].data.KEY);
                    }
                    else {
                        Ext.getCmp('damNm').setValue('<%=ViewData["damVal"] %>');
                    }
                }
            },
            autoLoad: true
        });

        /* 관측국 콤보 저장소 정의 */
        var obsCodeStore = Ext.create('Ext.data.Store', {
            autoDestroy: true, model: 'CodeMode',
            proxy: {
                type: 'ajax',
                url: '<%=Page.ResolveUrl("~/Common")%>/ObsCodeList',
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
                        Ext.getCmp('obsNm').setValue(records[0].data.KEY);
                    }
                }
            },
            autoLoad: false
        });

        var searchPanel = Ext.create('Ext.form.Panel', {
            bodyPadding: 0,
            frame: true,
            height: 100,
            bodyPadding: 5,
            layout: {
                type: 'table',
                columns: 8
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
                    change: function (field, newValue, oldValue, options) {
                        obsCodeStore.load({
                            params: {
                                DamCode: newValue,
                                ObsTp: obsTp,
                                firstValue: 'ALL',
                                Tp: 'includeWR'
                            }
                        });
                    }
                }
            }, {
                xtype: 'combo',
                id: 'obsNm',
                fieldLabel: '관측국',
                labelWidth: 50,
                labelAlign: 'right',
                width: 180,
                displayField: 'VALUE',
                valueField: 'KEY',
                store: obsCodeStore,
                queryMode: 'local'
            }, {
                xtype: 'displayfield',
                name: 'dispfld10',
                value: '',
                width: 10
            }, {
                xtype: 'radiogroup',
                id: 'radio',
                width: 110,
                vertical: false,
                colspan: 2,
                margin: '1 0 0 10',
                items: [
                    {
                        boxLabel: '수위',
                        name: 'obsType',
                        id: 'wl',
                        checked: true,
                        listeners: {
                            change: function (field, check, oldval) {
                                if (check == true) {
                                    switchRadio(field.getId());
                                }
                            }
                        }
                    }, {
                        boxLabel: '우량',
                        name: 'obsType',
                        id: 'rf',
                        listeners: {
                            change: function (field, check, oldval) {
                                if (check == true) {
                                    switchRadio(field.getId());
                                }
                            }
                        }
                    }
                ]
            }, {
                xtype: 'displayfield',
                name: 'dispfld10',
                value: '',
                width: 70
            }, {
                xtype: 'displayfield',
                name: 'dispfld10',
                value: '',
                width: 2
            }, {
                xtype: 'displayfield',
                name: 'dispfld10',
                value: '',
                width: 70
            }, {
                xtype: 'displayfield',
                name: 'dispfld10',
                margin: '0 0 0 34',
                value: '<span style="color:red; vertical-align:top">※ 이상치 기준값 변경은 이후 검정코드 산정에 영향을 줍니다</span>',
                width: 336,
                colspan: 2
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
                                wlBaselineStore.sync();
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
                    var obsCd = fp.findField("obsNm").getValue();

                    wlBaselineStore.load({
                        params: {
                            damCd: damCd,
                            obsCd: obsCd,
                            obsTp: obsTp
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
                        windowURL: '<%=Page.ResolveUrl("~/Code")%>/WLRF_Legend/?type=' + exTp,
                        centerScreen: 1,
                        resizable: 1
                    });
                }
            }]
        });


        Ext.define('wlBaselineManagementModel', {
            extend: 'Ext.data.Model',
            idProperty: 'OBSCD',
            fields: [
                        { name: 'DAMCD', type: 'string' },
                        { name: 'DAMNM', type: 'string' },
                        { name: 'OBSCD', type: 'string' },
                        { name: 'OBSNM', type: 'string' },
                        { name: 'OBSTP', type: 'string' },
                        { name: 'WLOBCD', type: 'string' },
                        { name: 'WL_EXCODE0952', type: 'string' },
                        { name: 'WL_EXCODE0953', type: 'string' },
                        { name: 'WL_EXCODE0954', type: 'string' },
                        { name: 'WL_EXCODE0961', type: 'string' },
                        { name: 'WL_EXCODE0971', type: 'string' },
                        { name: 'WL_EXCODE0972', type: 'string' }
                    ]
        });

        var wlBaselineColumns = [
        { header: '댐명', flex: 0.5, minWidth: 60, sortable: false, align: 'center', dataIndex: 'DAMNM', renderer: setCursor,
            field: {
                xtype: 'panel'
            }
        },
        { header: '관측국명', flex: 0.5, minWidth: 60, sortable: false, align: 'center', dataIndex: 'OBSNM',
            field: {
                xtype: 'panel'
            }
        },
        { header: '관측국코드', hidden: true, flex: 0.5, minWidth: 60, sortable: false, align: 'center', dataIndex: 'OBSCD',
            field: {
                xtype: 'panel'
            }
        },
        { header: '수위국코드', hidden: true, flex: 0.5, minWidth: 60, sortable: false, align: 'center', dataIndex: 'WLOBCD',
            field: {
                xtype: 'panel'
            }
        },
        { header: '수위 기울기 급변(배)',
            columns: [
                    { header: '0952(기울기 -1배)', width: 150, sortable: true, align: 'center', dataIndex: 'WL_EXCODE0952', renderer: setCursor,
                        field: {
                            xtype: 'textfield'
                        }
                    },
                    { header: '0953(기울기 3배)', width: 150, sortable: true, align: 'center', dataIndex: 'WL_EXCODE0953', renderer: setCursor,
                        field: {
                            xtype: 'textfield'
                        }
                    }
                ]
        },
        { header: '수위 급변(mm)',
            columns: [
                { header: '0954(수위급변)', width: 150, sortable: true, align: 'center', dataIndex: 'WL_EXCODE0954', renderer: setCursor,
                    field: {
                        xtype: 'textfield'
                    }
                }
            ]
        },
        { header: '수위 불변(시간)',
            columns: [
                { header: '0961(수위불변)', width: 150, sortable: true, align: 'center', dataIndex: 'WL_EXCODE0961', renderer: setCursor,
                    field: {
                        xtype: 'textfield'
                    }
                }
            ]
        },
        { header: '수위 상하한(mm)',
            columns: [
                { header: '0971(수위상한)', width: 150, sortable: true, align: 'center', dataIndex: 'WL_EXCODE0971', renderer: setCursor,
                    field: {
                        xtype: 'textfield'
                    }
                },
                { header: '0972(수위하한)', width: 150, sortable: true, align: 'center', dataIndex: 'WL_EXCODE0972', renderer: setCursor,
                    field: {
                        xtype: 'textfield'
                    }
                }
            ]
        }];

        var wlBaselineStore = Ext.create('Ext.data.Store', {
            id: 'wlBaselineStore',
            model: 'wlBaselineManagementModel',
            autoDestroy: true,
            remoteSort: true,
            autoLoad: false,
            autoSync: false,
            simpleSortMode: true,
            proxy: {
                type: 'ajax',
                api: {
                    read: '<%=Page.ResolveUrl("~/Code")%>/GetBaselineManagementList',
                    update: '<%=Page.ResolveUrl("~/Code")%>/InsertUpdateBaselineManagement'
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
                },
                load: function (store, records, successful, operation) {
                    if (records && records.length == 0) {
                        Ext.Msg.alert('Message', '조회 결과가 없습니다.');
                    }
                }
            }
        });


        var baselineGrid = Ext.create('Edit.Grid', {
            id: 'grid',
            name: 'grid',
            flex: 1,
            region: 'center',
            columns: wlBaselineColumns,
            store: wlBaselineStore,
            columnLines: true,
            listeners: {
                beforeedit: function (editor, e, options) {
                    var store = this.store;
                    var commitStoreCount = store.getTotalCount();
                    var totalStoreCount = store.getCount();
                    var addRowCount = totalStoreCount - commitStoreCount;

                    if (editor.rowIdx >= addRowCount) {
                        if (editor.colIdx == 0 || editor.colIdx == 1) editor.cancel = true;
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
                height: 120,
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
                }, searchPanel]
            }, baselineGrid]//, examGrid
        });

    });

</script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <div id="menu-title" class="title_box x-hide-display" align="center" style="text-align:left;">
	    <span style="font-weight:bold; margin-left:10px;">
        <img src="<%=Page.ResolveUrl("/Images") %>/icons/gear.png" align="absmiddle"/>&nbsp;&nbsp;
       이상기준치관리
        </span>
    </div>
</asp:Content>



