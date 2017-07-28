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
    var obsTp = 'wl';   //콤보 변경시 댐별 관측국 변경 사용 wl: 수위국, rf: 우량국
    var LATITUDE = null;
    Ext.onReady(function () {
        Ext.BLANK_IMAGE_URL = '/Scripts/extjs/s.gif';
        Ext.tip.QuickTipManager.init();

        function rowSelect(type) {
            var selection = wlObsManagementGrid.getSelectionModel().getSelection();
            var selectIdx = wlObsManagementStore.indexOf(selection[0]);

            if (type == 'Insert') { alert('등록되었습니다'); }
            else { alert('제거되었습니다.'); }

            wlObsManagementGrid.getView().select(selectIdx);
        }

        function setCommit() {
            var storeCount = wlDownRightObsManagementStore.getCount();
            for (var i = 0; i < storeCount; i++) {
                record = wlDownRightObsManagementStore.getAt(i);
                if (record.dirty == true) {
                    record.commit();
                }
            }
        }

        function loadObsMngList() {
            var damCd = Ext.getCmp("damNm").getValue();
            //var obsCd = Ext.getCmp("obsNm").getValue();

            wlObsManagementStore.load({
                params: {
                    damCd: damCd,
                    //obsCd: obsCd,
                    obsTp: obsTp
                }
            });

            wlObsManagementGrid.getSelectionModel().getSelection();
        }

        var switchRadio = function (tp) {
            var damVal = Ext.getCmp("damNm").getValue();

            var wlUrl = '/WLObsManagement';
            var rfUrl = '/RFObsManagement/?damValue=' + damVal;
            var url = '<%=Page.ResolveUrl("~/Code") %>';
            if (tp == 'wl') {
                url = url + wlUrl;
            } else {
                url = url + rfUrl;
            }
            document.location.href = url;
        }

        function getUseTP(value, metaData, record, rowIndex, colIndex, store) {
            var val;
            switch (value) {
                case "U":
                    val = '<div style="color:blue">상류</div>';
                    break;
                case "D":
                    val = '<div style="color:green">하류</div>';
                    break;
            }
            return val;
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
                    if (records && records.length > 0) {
                        var damValue = '<%=ViewData["damVal"] %>';
                        if (damValue == "" && records && records.length > 0) {
                            Ext.getCmp('damNm').setValue(records[0].data.KEY);
                        }
                        else {
                            Ext.getCmp('damNm').setValue(damValue);
                        }
                    }
                }
            },
            sorters: [{ property: 'ORDERNUM', direction: 'ASC'}],
            autoLoad: true
        });

        /* 관측국 콤보 저장소 정의 */
        //        var obsCodeStore = Ext.create('Ext.data.Store', {
        //            autoDestroy: true, model: 'CodeMode',
        //            proxy: {
        //                type: 'ajax',
        //                //url: '<%=Page.ResolveUrl("~/Common")%>/ObsCodeList',
        //                url: '<%=Page.ResolveUrl("~/Code")%>/GetObsMngCodeList',
        //                reader: { type: 'json', root: 'Data' },
        //                listeners: {
        //                    exception: function (proxy, response, operation) {
        //                        var json = Ext.decode(response.responseText);
        //                        Ext.MessageBox.show({
        //                            title: 'ERROR',
        //                            msg: json.msg,
        //                            icon: Ext.MessageBox.ERROR,
        //                            buttons: Ext.Msg.OK
        //                        });
        //                    }
        //                }
        //            },
        //            listeners: {
        //                load: function (store, records, successful, operation) {
        //                    if (records && records.length > 0) {
        //                        Ext.getCmp('obsNm').setValue(records[0].data.KEY);
        //                    }
        //                    else {
        //                        Ext.getCmp('obsNm').setValue('');
        //                    }
        //                }
        //            },
        //            sorters: [{ property: 'ORDERNUM', direction: 'ASC'}],
        //            autoLoad: false
        //        });

        var wlObsManagementModel = Ext.define('wlObsManagementModel', {
            extend: 'Ext.data.Model',
            idProperty: 'NROBCD',
            fields: [
                        { name: 'DAMCD', type: 'string' },
                        { name: 'DAMNM', type: 'string' },
                        { name: 'OBSCD', type: 'string' },
                        { name: 'OBSNM', type: 'string' },
                        { name: 'OBSTP', type: 'string' },
                        { name: 'WLOBCD', type: 'string' },
                        { name: 'NROBCD', type: 'string' },
                        { name: 'NROBNM', type: 'string' },
                        { name: 'NRDIST', type: 'string' },
                        { name: 'REGCOUNT', type: 'int' },
                        { name: 'UPDOWNTP', type: 'string' },
                        { name: 'LATITUDE', type: 'string' }
                    ]
        });

        var wlObsManagementColumns = [
        { id: 'OBSCD', header: '댐명', flex: 0.4, minWidth: 50, sortable: false, align: 'center', dataIndex: 'DAMNM' },
        { header: '관측국명', flex: 0.4, minWidth: 50, sortable: false, align: 'center', dataIndex: 'OBSNM' },
        { header: '관측국코드', hidden: true, flex: 0.4, minWidth: 50, sortable: false, align: 'center', dataIndex: 'OBSCD' },
        { header: '현재등록된 인근관측국', flex: 1.8, minWidth: 220, sortable: false, align: 'center', dataIndex: 'NROBNM' },
        { header: '등록수', flex: 0.4, minWidth: 50, sortable: false, align: 'center', dataIndex: 'REGCOUNT'}//,
        //{ header: '댐코드', hidden: true, flex: 0.4, minWidth: 50, sortable: false, align: 'center', dataIndex: 'DAMCD' }
        ];

        var wlObsManagementStore = Ext.create('Ext.data.Store', {
            id: 'wlObsManagementStore',
            model: 'wlObsManagementModel',
            autoDestroy: true,
            proxy: {
                type: 'ajax',
                url: '<%=Page.ResolveUrl("~/Code")%>/GetObsManagementList',
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
                    if (records && records.length == 0) {
                        Ext.Msg.alert('Message', '조회 결과가 없습니다.');
                    }

                    //추가 : 자기 댐이 아닌경우는 수정이 안돼도록 함.
                    var damcd = Ext.getCmp('damNm').getValue();
                    if (MGTDAM != 'MAIN' && MGTDAM.indexOf(damcd) == -1) {
                        Ext.getCmp('addDownMidUpper').disable();
                        Ext.getCmp('addDownMidLower').disable();
                        Ext.getCmp('moveItem').disable();
                        Ext.getCmp('saveList').disable();
                    } else {
                        Ext.getCmp('addDownMidUpper').enable();
                        Ext.getCmp('addDownMidLower').enable();
                        Ext.getCmp('moveItem').enable();
                        Ext.getCmp('saveList').enable();
                    }
                }
            },
            autoLoad: false
        });

        var wlObsManagementTbarItems =
        [{
            xtype: 'displayfield',
            name: 'dispfld2',
            value: '',
            width: 10
        }, {
            xtype: 'radiogroup',
            id: 'radio',
            width: 110,
            height: 25,
            vertical: false,
            items: [
                {
                    boxLabel: '수위',
                    name: 'obsType',
                    id: 'wl',
                    checked: true,
                    listeners: {
                        change: function (field, check, oldval) {
                            if (check == true) {
                                switchRadio('wl');
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
                                switchRadio('rf');
                            }
                        }
                    }
                }
            ]
        }, {
            xtype: 'combobox',
            id: 'damNm',
            fieldLabel: '댐명',
            labelWidth: 40,
            labelAlign: 'right',
            width: 190,
            displayField: 'VALUE',
            valueField: 'KEY',
            store: damCodeStore,
            queryMode: 'local'//,
            //listeners: {
            //                change: function (field, newValue, oldValue, options) {
            //                    obsCodeStore.load({
            //                        params: {
            //                            damCd: newValue,
            //                            ObsTp: obsTp
            //                        }
            //                    });
            //                }
            //}
        },
        //        {
        //            xtype: 'combobox',
        //            id: 'obsNm',
        //            fieldLabel: '관측국',
        //            labelWidth: 50,
        //            labelAlign: 'right',
        //            width: 180,
        //            displayField: 'VALUE',
        //            valueField: 'KEY',
        //            store: obsCodeStore,
        //            queryMode: 'local'
        //        }, 
        {
        xtype: 'displayfield',
        name: 'dispfld1',
        value: '',
        width: 10
    }, {
        xtype: 'button',
        name: 'submit1',
        text: '<span style="font-weight: bold;vertical-align:middle">조 회</span>',
        icon: '<%=Page.ResolveUrl("~/Images") %>/icons/magnifier.png',
        width: 70,
        valign: 'top',
        height: 24,
        handler: function () {

            var damCd = Ext.getCmp("damNm").getValue();
            //var obsCd = Ext.getCmp("obsNm").getValue();

            wlObsManagementStore.load({
                params: {
                    damCd: damCd,
                    //obsCd: obsCd,
                    obsTp: obsTp
                }
            });

            // 인근 관측국 관리 리스트 조회시 좌측 하단의 리스트 로드
            //Ext.getCmp('downLeftDamNm').setValue(damCd);

        }
    }]


    /* 상단의 그리드 정의 */
    var wlObsManagementGrid = Ext.create('Ext.grid.Panel', {
        id: 'wlObsManagementGrid',
        name: 'wlObsManagementGrid',
        frame: true,
        flex: 1,
        region: 'center',
        tbar: wlObsManagementTbarItems,
        columns: wlObsManagementColumns,
        store: wlObsManagementStore,
        columnLines: true,
        listeners: {
            load: function (store, rocords, options) {
            },
            selectionchange: function (view, recs, options) {
                if (recs != null && recs.length > 0) {

                    LATITUDE = recs[0].get('LATITUDE');
                    if (LATITUDE == '') {
                        Ext.Msg.alert('Message', '위도, 경도가 없습니다. 위도, 경도를 입력해 주세요.');
                        wlDownRightObsManagementStore.load({ params: { damCode: "xxxxxxx", obsCode: "xxxxxxx", obsTp: obsTp} });
                        wlDownLeftObsManagementStore.load({ params: { damCode: "xxxxxxx", obsTp: obsTp} });
                        return;
                    }

                    wlDownRightObsManagementStore.load({
                        params: {
                            damCode: recs[0].get('DAMCD'),
                            obsCode: recs[0].get('OBSCD'),
                            obsTp: obsTp
                        }
                    });

                    //Ext.getCmp('downLeftDamNm').setValue(recs[0].get('DAMCD'));
                    downLeftDamCodeStore.load({
                        params: {
                            firstvalue: "ALL"
                        }
                    });
                    //상단그리드의 선택된 관측소가 선택
                    wlDownLeftObsManagementStore.load({
                        params: {
                            damCd: '', //recs[0].get('DAMCD'),
                            obsCd: '',
                            obsTp: obsTp,
                            obCd: recs[0].get("OBSCD") // 인근관측소 테이블에 저장된 WLOBCD
                        }
                    });

                }
            }
        }
    });

    ////////////////////////////////////인근관측국 등록수정 시작////////////////////////////////////////
    ////// ***********************  하단 왼쪽 그리드 콤보 바인딩 시작 *********************** //////

    /* 하단 왼쪽 코드 모델 정의 - Ext.define() 함수를 이용한 클래스 정의 */
    Ext.define('downLeftCodeModel', {
        extend: 'Ext.data.Model',
        fields: [
                { name: 'KEY', type: 'string' },
                { name: 'VALUE', type: 'string' },
                { name: 'ORDERNUM', type: 'int' }
            ]
    });

    /* 하단 왼쪽 댐 콤보 저장소 정의 */
    var downLeftDamCodeStore = Ext.create('Ext.data.Store', {
        autoDestroy: true,
        model: 'downLeftCodeModel',
        proxy: {
            type: 'ajax',
            //url: '<%=Page.ResolveUrl("~/Common")%>/DamCodeList',
            url: '<%=Page.ResolveUrl("~/Code")%>/GetObsMngDamCodeList',
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
                    Ext.getCmp('downLeftDamNm').setValue(records[0].data.KEY);
                }
                else {
                    Ext.Msg.alert('Message', '조회 결과가 없습니다.');
                }
            }
        },
        //sorters: [{ property: 'ORDERNUM', direction: 'ASC'}],
        autoLoad: false
    });

    downLeftDamCodeStore.load({
        params: {
            firstvalue: "ALL"
        }
    });

    /* 하단 왼쪽 관측국 콤보 저장소 정의 */
    //    var downLeftObsCodeStore = Ext.create('Ext.data.Store', {
    //        autoDestroy: true,
    //        model: 'downLeftCodeModel',
    //        proxy: {
    //            type: 'ajax',
    //            //url: '<%=Page.ResolveUrl("~/Common")%>/ObsCodeList',
    //            url: '<%=Page.ResolveUrl("~/Code")%>/GetObsMngCodeList',
    //            reader: { type: 'json', root: 'Data' },
    //            listeners: {
    //                exception: function (proxy, response, operation) {
    //                    var json = Ext.decode(response.responseText);
    //                    Ext.MessageBox.show({
    //                        title: 'ERROR',
    //                        msg: json.msg,
    //                        icon: Ext.MessageBox.ERROR,
    //                        buttons: Ext.Msg.OK
    //                    });
    //                }
    //            }
    //        },
    //        sorters: [{ property: 'ORDERNUM', direction: 'ASC'}],
    //        autoLoad: false,
    //        listeners: {
    //            load: function (store, records, successful, operation) {
    //                if (records && records.length > 0) {
    //                    //Ext.getCmp('downLeftObsNm').setValue(records[0].data.KEY);
    //                }
    //            }
    //        }
    //    });

    ////// ***********************  하단 왼쪽 그리드 콤보 바인딩 종료 *********************** //////


    // *********************************  하단 왼쪽 그리드 시작 ************************************* //


    /* 하단 왼쪽 그리드 모델 정의 - Ext.define() 함수를 이용한 클래스 정의 */
    Ext.define('wlDownLeftObsManagementModel', {
        extend: 'Ext.data.Model',
        idProperty: 'OBSCD',
        fields: [
                    { name: 'DAMCD', type: 'string' },
                    { name: 'DAMNM', type: 'string' },
                    { name: 'WLOBSCD', type: 'string' },
                    { name: 'OBSNM', type: 'string' },
                    { name: 'NRDIST', type: 'string' }
                ]
    });

    /* 하단 왼쪽 관측국 그리드 저장소 정의 */
    var wlDownLeftObsManagementStore = Ext.create('Ext.data.Store', {
        id: 'wlDownLeftObsManagementStore',
        model: 'wlDownLeftObsManagementModel',
        autoDestroy: true,
        proxy: {
            type: 'ajax',
            url: '<%=Page.ResolveUrl("~/Code")%>/GetDownLeftObsManagementList',
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
                if (records && records.length == 0 && LATITUDE != '') {
                    Ext.Msg.alert('Message', '조회 결과가 없습니다...');
                }
            }
        },
        autoLoad: false
    });

    /* 하단 왼쪽 관측국 그리드 컬럼 정의 */
    var wlDownLeftObsManagementColumns = [
        { header: '댐명', flex: 0.4, minWidth: 50, sortable: false, align: 'center', dataIndex: 'DAMNM' },
        { header: '관측국명', flex: 0.4, minWidth: 50, sortable: false, align: 'center', dataIndex: 'OBSNM' },
    //{ header: '관측국코드', hidden: true, flex: 0.4, minWidth: 50, sortable: false, align: 'center', dataIndex: 'OBSCD' },
        {header: '인근관측국 거리 (Km)', flex: 0.4, minWidth: 50, sortable: false, align: 'center', dataIndex: 'NRDIST' }
        ];

    /* 하단 왼쪽 관측국 그리드 아이템 정의 */
    var wlDownLeftObsItems = [
        {
            xtype: 'displayfield',
            name: 'dispfld6',
            icon: '<%=Page.ResolveUrl("/Images") %>/icons/gear.png',
            value: '&nbsp;인근 관측국 목록',
            width: 160,
            height: 18
        }, '->', {
            xtype: 'combobox',
            id: 'downLeftDamNm',
            name: 'downLeftDamNm',
            fieldLabel: '댐명',
            labelWidth: 34,
            labelAlign: 'right',
            width: 156,
            displayField: 'VALUE',
            valueField: 'KEY',
            store: downLeftDamCodeStore,
            queryMode: 'local'
            //            ,
            //            listeners: {
            //                change: function (field, newValue, oldValue, options) {
            //                    downLeftObsCodeStore.load({
            //                        params: {
            //                            DamCd: newValue,
            //                            ObsTp: obsTp
            //                        }
            //                    });
            //                }
            //            }
        },
    //        {
    //            xtype: 'combobox',
    //            id: 'downLeftObsNm',
    //            name: 'downLeftObsNm',
    //            fieldLabel: '관측국',
    //            labelWidth: 50,
    //            labelAlign: 'right',
    //            width: 170,
    //            displayField: 'VALUE',
    //            valueField: 'KEY',
    //            store: downLeftObsCodeStore,
    //            queryMode: 'local'
    //        }, 
         {
         xtype: 'displayfield',
         name: 'dispfld2',
         value: '',
         width: 10
     }, {
         xtype: 'button',
         name: 'submit2',
         text: '<span style="font-weight: bold;vertical-align:middle">조 회</span>',
         icon: '<%=Page.ResolveUrl("~/Images") %>/icons/magnifier.png',
         width: 70,
         valign: 'top',
         height: 24,
         handler: function () {
             var damCd = Ext.getCmp("downLeftDamNm").getValue();
             //var obsCd = Ext.getCmp("downLeftObsNm").getValue();
             var recs = wlObsManagementGrid.getSelectionModel().getSelection();
             if (recs != null && recs.length > 0) {
                 wlDownLeftObsManagementStore.load({
                     params: {
                         damCd: damCd,
                         //obsCd: obsCd,
                         obsTp: obsTp,
                         obCd: recs[0].get("OBSCD") // 인근관측소 테이블에 저장된 WLOBCD
                     }
                 });
             } else {
                 alert("상단그리드의 관측국을 선택하셔야 합니다.");
             }

         }
     }];

    /* 하단 왼쪽 관측국 그리드 정의 */
    var wlDownLeftObsGrid = Ext.create('Ext.grid.Panel', {
        id: 'wlDownLeftObsGrid',
        name: 'wlDownLeftObsGrid',
        frame: true,
        region: 'west',
        collapsible: false,
        collapsed: false,
        split: true,
        flex: 0.8,
        store: wlDownLeftObsManagementStore,
        columns: wlDownLeftObsManagementColumns,
        tbar: wlDownLeftObsItems,
        columnLines: true,
        multiSelect: true
    });

    // *********************************  하단 왼쪽 그리드 종료 ************************************* //


    // *********************************  하단 중앙 패널 시작 ************************************* //
    function addLeftToRightGrid(utype) {
        var mrecs = wlObsManagementGrid.getSelectionModel().getSelection();
        var lrecs = wlDownLeftObsGrid.getSelectionModel().getSelection();
        if (mrecs != null && mrecs.length > 0 && lrecs != null && lrecs.length > 0) {
            var mrec = mrecs[0];

            for (var i = 0; i < lrecs.length; i++) {
                var rec = lrecs[i];

                for (var j = 0; j < wlDownRightObsManagementStore.getCount(); j++) {
                    if (rec.data.WLOBSCD == wlDownRightObsManagementStore.getAt(j).data.NROBCD) {
                        Ext.Msg.alert('Message', '이미 등록된 관측국이 있습니다. 확인해주세요.');
                        return;
                    }
                }
            }

            for (var i = 0; i < lrecs.length; i++) {
                var rec = lrecs[i];
                var id = mrec.get("OBSCD") + "_" + rec.data.OBSCD;

                var rec1 = new wlDownRightObsManagementModel({
                    ID: '',
                    NROBNM: rec.data.OBSNM,
                    NROBCD: rec.data.WLOBSCD,
                    OBSCD: mrec.get("OBSCD"),
                    UPDOWNTP: utype,
                    NRDIST: rec.data.NRDIST,
                    OBSTP: 'WL',
                    WLOBCD: mrec.get("OBSCD")
                }), edit = wlDownRightObsGrid.editing;

                defaultEditable: true;
                rec1.set("ID", id);
                wlDownRightObsManagementStore.insert(0, rec1);
                edit.startEditByPosition({
                    row: 0,
                    column: 0
                });
            }
            wlDownRightObsManagementStore.sync();
            rowSelect('Insert');
        }
        else {
            Ext.Msg.alert('Message', '상단과 좌측 하단의 리스트를 선택하셔야합니다.');
            return;
        }
    }

    var wlDownMidObsPanel = Ext.create('Ext.panel.Panel', {
        id: 'wlDownMidObsPanel',
        name: 'wlDownMidObsPanel',
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
        items: [
            {
                xtype: 'displayfield',
                name: 'dispfld4',
                value: '',
                height: 75
            }, {
                xtype: 'button',
                id: 'addDownMidUpper',
                text: '<span style="font-weight: bold;vertical-align:middle">등록(상류)</span>',
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/arrow.png',
                width: 90,
                valign: 'top',
                height: 24,
                handler: function () {
                    addLeftToRightGrid('U');
                }
            }, {
                xtype: 'displayfield',
                name: 'dispfld4',
                value: '',
                height: 1
            }, {
                xtype: 'button',
                id: 'addDownMidLower',
                text: '<span style="font-weight: bold;vertical-align:middle">등록(하류)</span>',
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/arrow.png',
                width: 90,
                valign: 'top',
                height: 24,
                handler: function () {
                    addLeftToRightGrid('D');
                }
            }, {
                xtype: 'displayfield',
                name: 'dispfld4',
                value: '',
                height: 1
            }, {
                xtype: 'button',
                id: 'moveItem',
                text: '<span style="font-weight: bold;vertical-align:middle">제 거</span>',
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/arrow-180.png',
                width: 90,
                valign: 'top',
                height: 24,
                handler: function () {
                    //var mrecs = wlObsManagementGrid.getSelectionModel().getSelection();
                    var recs = wlDownRightObsGrid.getSelectionModel().getSelection();
                    if (recs != null && recs.length > 0) {
                        for (var i = 0; i < recs.length; i++) {
                            wlDownRightObsManagementStore.remove(recs[i]);
                        }
                        wlDownRightObsManagementStore.sync();
                        rowSelect('Delete');
                    }
                    else {
                        Ext.Msg.alert('Message', '우측 하단의 리스트를 선택하셔야합니다.');
                        return;
                    }
                }
            }, {
                xtype: 'displayfield',
                name: 'dispfld4',
                value: '',
                height: 30
            }, {
                xtype: 'button',
                id: 'saveList',
                text: '<span style="font-weight: bold;vertical-align:middle">저 장</span>',
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/disk.png',
                width: 90,
                valign: 'top',
                height: 24,
                hidden: true,
                handler: function () {
                    Ext.Msg.show({
                        title: 'Message',
                        msg: '데이터를 저장하시겠습니까?',
                        width: 150,
                        buttons: Ext.Msg.OKCANCEL,
                        icon: Ext.Msg.INFO,
                        fn: function (btn) {
                            if (btn == "ok") {
                                var mrecs = wlObsManagementGrid.getSelectionModel().getSelection();
                                //var lrecs = wlDownLeftObsGrid.getSelectionModel().getSelection();
                                if (mrecs != null && mrecs.length > 0) {
                                    wlDownRightObsManagementStore.sync();
                                }
                                else {
                                    Ext.Msg.alert('message', '상단의 리스트를 선택해 주세요.');
                                }
                                //wlDownRightObsManagementStore.sync();
                                //                                var recs = wlObsManagementGrid.getSelectionModel().getSelection();                                
                                //                                wlDownRightObsManagementStore.load({
                                //                                    params: {
                                //                                        damCode: recs[0].get('DAMCD'),
                                //                                        obsCode: recs[0].get('OBSCD'),
                                //                                        obsTp: obsTp
                                //                                    }
                                //                                });
                            }
                        }
                    });
                }
            }]
    });

    // *********************************  하단 중앙 패널 종료 ************************************* //


    // *********************************  하단 오른쪽 그리드 시작 ************************************* //

    /* 하단 오른쪽 그리드 모델 정의 - Ext.define() 함수를 이용한 클래스 정의 */
    Ext.define('wlDownRightObsManagementModel', {
        extend: 'Ext.data.Model',
        idProperty: 'ID',
        fields: [
        //{ name: 'ID', type: 'string' },
                    {name: 'NROBNM', type: 'string' },
                    { name: 'NROBCD', type: 'string' },
                    { name: 'OBSNM', type: 'string' },
                    { name: 'OBSCD', type: 'string' },
                    { name: 'UPDOWNTP', type: 'string' },
                    { name: 'NRDIST', type: 'string' },
                    { name: 'OBSTP', type: 'string' },
                    { name: 'WLOBCD', type: 'string' }
                ]
    });

    var wlDownRightObsManagementStore = Ext.create('Ext.data.Store', {
        id: 'wlDownRightObsManagementStore',
        model: 'wlDownRightObsManagementModel',
        autoDestroy: true,
        remoteSort: true,
        autoLoad: false,
        autoSync: false,
        simpleSortMode: true,
        nocache: true,
        proxy: {
            type: 'ajax',
            api: {
                read: '<%=Page.ResolveUrl("~/Code")%>/GetDownRightObsManagementList',
                create: '<%=Page.ResolveUrl("~/Code")%>/InsertUpdateObsManagement',
                update: '<%=Page.ResolveUrl("~/Code")%>/InsertUpdateObsManagement',
                destroy: '<%=Page.ResolveUrl("~/Code")%>/DeleteObsManagement'
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
                    setCommit(); loadObsMngList();
                }
                if (operation.action == 'update') {
                    setCommit(); loadObsMngList();
                }
                if (operation.action == 'destroy') {
                    setCommit(); loadObsMngList();
                }
            }
        }
    });


    /* 하단 오른쪽 관측국 그리드 컬럼 정의 */
    var wlDownRightObsManagementColumns = [
        { header: '인근관측국명', flex: 0.4, minWidth: 50, sortable: false, align: 'center', dataIndex: 'NROBNM' },
        { header: '상 / 하류', flex: 0.4, minWidth: 50, sortable: false, align: 'center', dataIndex: 'UPDOWNTP', renderer: getUseTP,
            field: {
                xtype: 'combobox',
                id: '',
                typeAhead: true,
                triggerAction: 'all',
                selectOnTab: true,
                lazyRender: true,
                store: [
                    ['U', '상류'],
                    ['D', '하류']
                ]
            }
        },
        { header: '인근관측국 거리 (Km)', flex: 0.4, minWidth: 50, sortable: false, align: 'center', dataIndex: 'NRDIST',
            field: {
                type: 'numberfield'
            }
        }];

    /* 하단 오른쪽 관측국 그리드 아이템 정의 */
    var wlDownRightObsItems = [
        {
            xtype: 'displayfield',
            name: 'dispfld6',
            icon: '<%=Page.ResolveUrl("/Images") %>/icons/gear.png',
            value: '&nbsp;등록된 인근 관측국 목록',
            width: 160,
            height: 24
        }
    //        , '->', {
    //            xtype: 'button',
    //            name: 'submit3',
    //            text: '<span style="font-weight: bold;vertical-align:middle">인근관측국 설정 저장</span>',
    //            icon: '<%=Page.ResolveUrl("~/Images") %>/icons/disk.png',
    //            width: 160,
    //            valign: 'top',
    //            height: 24,
    //            handler: function () {
    //                Ext.Msg.show({
    //                    title: 'Message',
    //                    msg: '데이터를 저장하시겠습니까?',
    //                    width: 150,
    //                    buttons: Ext.Msg.OKCANCEL,
    //                    icon: Ext.Msg.INFO,
    //                    fn: function (btn) {
    //                        if (btn == "ok") {
    //                            wlDownRightObsManagementStore.sync();
    //                        }
    //                    }
    //                });
    //            }
    //        }, {
    //            icon: '<%=Page.ResolveUrl("~/Images") %>/icons/minus-circle-frame.png',
    //            text: '<span style="font-weight: bold;vertical-align:middle">삭제</span>',
    //            valign: 'top',
    //            height: 24,
    //            handler: function () {
    //                var recs = wlDownRightObsGrid.getSelectionModel().getSelection();
    //                if (recs != null && recs.length > 0) {
    //                    wlDownRightObsManagementStore.remove(recs[0]);
    //                }
    //            }
    //        }
        ];

    /* 하단 오른쪽 관측국 그리드 정의 */
    var wlDownRightObsGrid = Ext.create('Edit.Grid', {
        id: 'wlDownRightObsGrid',
        name: 'wlDownRightObsGrid',
        frame: true,
        region: 'east',
        collapsible: false,
        collapsed: false,
        split: true,
        flex: 0.8,
        store: wlDownRightObsManagementStore,
        columns: wlDownRightObsManagementColumns,
        tbar: wlDownRightObsItems,
        columnLines: true,
        multiSelect: true,
        listeners: {
            beforeedit: function (editor, e, options) {
                if (editor.colIdx == 0) {
                    editor.cancel = true;
                }
            }
        }
    });

    // *********************************  하단 오른쪽 그리드 종료 ************************************* //



    /* 하단의 그리드 담기는 패널 정의 */
    var editObsManagement = Ext.create('Ext.form.Panel', {
        title: '인근관측국 등록 / 수정 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' + '<span style="color:red;">** 인근관측국 등록시 상단의 리스트와 좌측 하단의 리스트를 선택하셔야 합니다. **</span>',
        flex: 0.8,
        bodyPadding: 5,
        border: 0,
        region: 'south',
        layout: {
            type: 'hbox',
            align: 'stretch',
            padding: 0
        },
        items: [wlDownLeftObsGrid, wlDownMidObsPanel, wlDownRightObsGrid]// 
    });


    /////////////////인근관측국 등록수정 종료//////////////////

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
            border: 3,
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
        }, wlObsManagementGrid, editObsManagement]
    });
});

</script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<div id="menu-title" class="title_box x-hide-display" align="center" style="text-align:left;">
	<span style="font-weight:bold; margin-left:10px;">
    <img src="<%=Page.ResolveUrl("/Images") %>/icons/gear.png" align="absmiddle"/>&nbsp;&nbsp;
    수위 인근관측국 관리
    </span>
</div>
</asp:Content>
