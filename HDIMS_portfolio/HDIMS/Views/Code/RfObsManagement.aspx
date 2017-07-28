<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="HDIMS.Utils.Login"%>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	이상기준치 관리(우량)
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
    var obsTp = 'rf';   //콤보 변경시 댐별 관측국 변경 사용 rf: 수위국, rf: 우량국
    var LATITUDE = null;

    Ext.onReady(function () {
        Ext.BLANK_IMAGE_URL = '/Scripts/extjs/s.gif';
        Ext.tip.QuickTipManager.init();

        function rowSelect(type) {
            var selection = rfObsManagementGrid.getSelectionModel().getSelection();
            var selectIdx = rfObsManagementStore.indexOf(selection[0]);

            if (type == 'Insert') { alert('등록되었습니다'); }
            else { alert('제거되었습니다.'); }

            rfObsManagementGrid.getView().select(selectIdx);
        }

        function setCommit() {
            var storeCount = rfDownRightObsManagementStore.getCount();
            for (var i = 0; i < storeCount; i++) {
                record = rfDownRightObsManagementStore.getAt(i);
                if (record.dirty == true) {
                    record.commit();
                }
            }
        }

        function loadObsMngList() {
            var damCd = Ext.getCmp("damNm").getValue();
            //var obsCd = Ext.getCmp("obsNm").getValue();

            rfObsManagementStore.load({
                params: {
                    damCd: damCd,
                    //obsCd: obsCd,
                    obsTp: obsTp
                }
            });
        }

        var switchRadio = function (tp) {
            var damVal = Ext.getCmp("damNm").getValue();

            var wlUrl = '/WLObsManagement/?damValue=' + damVal;
            var rfUrl = '/RFObsManagement';
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
                    var damValue = '<%=ViewData["damVal"] %>';

                    if (damValue == '') {
                        Ext.getCmp('damNm').setValue(records[0].data.KEY);
                    }
                    else {
                        Ext.getCmp('damNm').setValue(damValue);
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

        var rfObsManagementModel = Ext.define('rfObsManagementModel', {
            extend: 'Ext.data.Model',
            idProperty: 'NROBCD',
            fields: [
                        { name: 'DAMCD', type: 'string' },
                        { name: 'DAMNM', type: 'string' },
                        { name: 'OBSCD', type: 'string' },
                        { name: 'OBSNM', type: 'string' },
                        { name: 'OBSTP', type: 'string' },
                        { name: 'RFOBCD', type: 'string' },
                        { name: 'NROBCD', type: 'string' },
                        { name: 'NROBNM', type: 'string' },
                        { name: 'NRDIST', type: 'string' },
                        { name: 'NRQUAD', type: 'string' },
                        { name: 'REGCOUNT', type: 'int' },
                        { name: 'LATITUDE', type: 'string' }
                    ]
        });

        var rfObsManagementColumns = [
        { id: 'OBSCD', header: '댐명', flex: 0.4, minWidth: 50, sortable: false, align: 'center', dataIndex: 'DAMNM' },
        { header: '관측국명', flex: 0.4, minWidth: 50, sortable: false, align: 'center', dataIndex: 'OBSNM' },
        { header: '관측국코드', hidden: true, flex: 0.4, minWidth: 50, sortable: false, align: 'center', dataIndex: 'OBSCD' },
        { header: '현재등록된 인근관측국', flex: 1.8, minWidth: 220, sortable: false, align: 'center', dataIndex: 'NROBNM' },
        { header: '등록수', flex: 0.4, minWidth: 50, sortable: false, align: 'center', dataIndex: 'REGCOUNT' }
        ];

        var rfObsManagementStore = Ext.create('Ext.data.Store', {
            id: 'rfObsManagementStore',
            model: 'rfObsManagementModel',
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
                        Ext.getCmp('moveItem').disable();
                        Ext.getCmp('saveList').disable();
                    } else {
                        Ext.getCmp('addDownMidUpper').enable();
                        Ext.getCmp('moveItem').enable();
                        Ext.getCmp('saveList').enable();
                    }
                }
            },
            autoLoad: false
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
        //        var downLeftObsCodeStore = Ext.create('Ext.data.Store', {
        //            autoDestroy: true,
        //            model: 'downLeftCodeModel',
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
        //            sorters: [{ property: 'ORDERNUM', direction: 'ASC'}],
        //            autoLoad: false,
        //            listeners: {
        //                load: function (store, records, successful, operation) {
        //                    if (records && records.length > 0) {
        //                        Ext.getCmp('downLeftObsNm').setValue(records[0].data.KEY);
        //                    }
        //                }
        //            }
        //        });

        ////// ***********************  하단 왼쪽 그리드 콤보 바인딩 종료 *********************** //////


        // *********************************  하단 왼쪽 그리드 시작 ************************************* //


        /* 하단 왼쪽 그리드 모델 정의 - Ext.define() 함수를 이용한 클래스 정의 */
        Ext.define('rfDownLeftObsManagementModel', {
            extend: 'Ext.data.Model',
            idProperty: 'OBSCD',
            fields: [
                        { name: 'DAMCD', type: 'string' },
                        { name: 'DAMNM', type: 'string' },
                        { name: 'RFOBSCD', type: 'string' },
                        { name: 'OBSNM', type: 'string' },
                        { name: 'NRQUAD', type: 'string' },
                        { name: 'NRDIST', type: 'string' }
                ]
        });

        /* 하단 왼쪽 관측국 그리드 저장소 정의 */
        var rfDownLeftObsManagementStore = Ext.create('Ext.data.Store', {
            id: 'rfDownLeftObsManagementStore',
            model: 'rfDownLeftObsManagementModel',
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
            autoLoad: false
        });

        /* 하단 왼쪽 관측국 그리드 컬럼 정의 */
        var rfDownLeftObsManagementColumns = [
            { header: '댐명', flex: 0.4, minWidth: 50, sortable: false, align: 'center', dataIndex: 'DAMNM' },
            { header: '관측국명', flex: 0.4, minWidth: 50, sortable: false, align: 'center', dataIndex: 'OBSNM' },
            { header: '사분면', flex: 0.4, minWidth: 50, sortable: false, align: 'center', dataIndex: 'NRQUAD' },
            { header: '인근관측국 거리 (Km)', flex: 0.4, minWidth: 50, sortable: false, align: 'center', dataIndex: 'NRDIST' }
        ];

        /* 하단 왼쪽 관측국 그리드 아이템 정의 */
        var rfDownLeftObsItems = [
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
            queryMode: 'local'//,
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
             var recs = rfObsManagementGrid.getSelectionModel().getSelection();
             if (recs != null && recs.length > 0) {
                 rfDownLeftObsManagementStore.load({
                     params: {
                         damCd: damCd,
                         //obsCd: obsCd,
                         obsTp: obsTp,
                         obCd: recs[0].get("OBSCD") // 인근관측소 테이블에 저장된 RFOBCD
                     }
                 });
             } else {
                 alert("상단그리드의 관측국을 선택하셔야 합니다.");
             }
         }
     }];

        /* 하단 왼쪽 관측국 그리드 정의 */
        var rfDownLeftObsGrid = Ext.create('Ext.grid.Panel', {
            id: 'rfDownLeftObsGrid',
            name: 'rfDownLeftObsGrid',
            frame: true,
            region: 'west',
            collapsible: false,
            collapsed: false,
            split: true,
            flex: 0.8,
            store: rfDownLeftObsManagementStore,
            columns: rfDownLeftObsManagementColumns,
            tbar: rfDownLeftObsItems,
            multiSelect: true,
            columnLines: true
        });

        // *********************************  하단 왼쪽 그리드 종료 ************************************* //


        // *********************************  하단 중앙 패널 시작 ************************************* //
        function addLeftToRightRightGrid() {
            var mrecs = rfObsManagementGrid.getSelectionModel().getSelection();
            var lrecs = rfDownLeftObsGrid.getSelectionModel().getSelection();

            if (mrecs != null && mrecs.length > 0 && lrecs != null && lrecs.length > 0) {
                var mrec = mrecs[0];

                for (var i = 0; i < lrecs.length; i++) {
                    var rec = lrecs[i];
                    //alert('왼쪽: ' + i + ':' + rec.data.OBSNM); alert('왼쪽: ' + i + ':' + rec.data.RFOBSCD);
                    for (var j = 0; j < rfDownRightObsManagementStore.getCount(); j++) {
                        //alert('오른쪽: ' + j + ':' + rfDownRightObsManagementStore.getAt(j).data.NROBNM); alert('오른쪽: ' + j + ':' + rfDownRightObsManagementStore.getAt(j).data.NROBCD);
                        if (rec.data.RFOBSCD == rfDownRightObsManagementStore.getAt(j).data.NROBCD) {
                            Ext.Msg.alert('Message', '이미 등록된 관측국이 있습니다. 확인해주세요.');
                            return;
                        }
                    }
                }

                for (var i = 0; i < lrecs.length; i++) {
                    var rec = lrecs[i];
                    var id = mrec.get("OBSCD") + "_" + rec.data.OBSCD;

                    var rec1 = new rfDownRightObsManagementModel({
                        ID: '',
                        NROBNM: rec.data.OBSNM,
                        NROBCD: rec.data.RFOBSCD,
                        OBSCD: mrec.get("OBSCD"),
                        NRDIST: rec.data.NRDIST,
                        NRQUAD: rec.data.NRQUAD,
                        OBSTP: 'RF',
                        RFOBCD: mrec.get("OBSCD")
                    }), edit = rfDownRightObsGrid.editing;

                    defaultEditable: true;
                    rec1.set("ID", id);
                    rfDownRightObsManagementStore.insert(0, rec1);
                    edit.startEditByPosition({
                        row: 0,
                        column: 0
                    });
                }
                rfDownRightObsManagementStore.sync();
                rowSelect('Insert');
            }
            else {
                Ext.Msg.alert('Message', '상단과 좌측 하단의 리스트를 선택하셔야합니다.');
                return;
            }
        }

        var rfDownMidObsPanel = Ext.create('Ext.panel.Panel', {
            id: 'rfDownMidObsPanel',
            name: 'rfDownMidObsPanel',
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
                height: 110
            }, {
                xtype: 'button',
                id: 'addDownMidUpper',
                text: '<span style="font-weight: bold;vertical-align:middle">등 록</span>',
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/arrow.png',
                width: 90,
                valign: 'top',
                height: 24,
                handler: function () {
                    addLeftToRightRightGrid();
                }
            }, {
                xtype: 'displayfield',
                name: 'dispfld5',
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
                    //                    var recs = rfDownRightObsGrid.getSelectionModel().getSelection();
                    //                    if (recs != null && recs.length > 0) {
                    //                        rfDownRightObsManagementStore.remove(recs[0]);
                    //                    }
                    //var mrecs = rfObsManagementGrid.getSelectionModel().getSelection();
                    var recs = rfDownRightObsGrid.getSelectionModel().getSelection();
                    if (recs != null && recs.length > 0) {
                        for (var i = 0; i < recs.length; i++) {
                            rfDownRightObsManagementStore.remove(recs[i]);
                        }
                        rfDownRightObsManagementStore.sync();
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
                                var mrecs = rfObsManagementGrid.getSelectionModel().getSelection();
                                //var lrecs = wlDownLeftObsGrid.getSelectionModel().getSelection();
                                if (mrecs != null && mrecs.length > 0) {
                                    rfDownRightObsManagementStore.sync();
                                }
                                else {
                                    Ext.Msg.alert('message', '상단의 리스트를 선택해 주세요.');
                                }
                                //                                rfDownRightObsManagementStore.load({
                                //                                    params: {
                                //                                        damCode: mrec.get('DAMCD'),
                                //                                        obsCode: mrec.get('OBSCD'),
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
        Ext.define('rfDownRightObsManagementModel', {
            extend: 'Ext.data.Model',
            idProperty: 'ID',
            fields: [
                    { name: 'ID', type: 'string' },
                    { name: 'NROBNM', type: 'string' },
                    { name: 'NROBCD', type: 'string' },
                    { name: 'OBSNM', type: 'string' },
                    { name: 'OBSCD', type: 'string' },
                    { name: 'UPDOWNTP', type: 'string' },
                    { name: 'NRDIST', type: 'string' },
                    { name: 'NRQUAD', type: 'string' },
                    { name: 'OBSTP', type: 'string' },
                    { name: 'RFOBCD', type: 'string' }
                ]
        });

        var rfDownRightObsManagementStore = Ext.create('Ext.data.Store', {
            id: 'rfDownRightObsManagementStore',
            model: 'rfDownRightObsManagementModel',
            autoDestroy: true,
            remoteSort: true,
            autoLoad: false,
            autoSync: false,
            nocache: true,
            simpleSortMode: true,
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
        var rfDownRightObsManagementColumns = [
        { header: '인근관측국명', flex: 0.4, minWidth: 50, sortable: false, align: 'center', dataIndex: 'NROBNM' },
        { header: '사분면', flex: 0.4, minWidth: 50, sortable: false, align: 'center', dataIndex: 'NRQUAD',
            field: {
                xtype: 'combobox',
                id: '',
                typeAhead: true,
                triggerAction: 'all',
                selectOnTab: true,
                lazyRender: true,
                store: [
                    ['1', '1사분면'],
                    ['2', '2사분면'],
                    ['3', '3사분면'],
                    ['4', '4사분면']
                ]
            }
        },
        { header: '인근관측국 거리 (Km)', flex: 0.4, minWidth: 50, sortable: false, align: 'center', dataIndex: 'NRDIST',
            field: {
                type: 'numberfield'
            }
        }];

        /* 하단 오른쪽 관측국 그리드 아이템 정의 */
        var rfDownRightObsItems = [
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
        //                            rfDownRightObsManagementStore.sync();
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
        //                var selection = rfDownRightObsGrid.getSelectionModel().getSelection()[0];
        //                if (selection) {
        //                    rfDownRightObsManagementStore.remove(selection);
        //                }
        //            }
        //        }
        ];

        /* 하단 오른쪽 관측국 그리드 정의 */
        var rfDownRightObsGrid = Ext.create('Edit.Grid', {
            id: 'rfDownRightObsGrid',
            name: 'rfDownRightObsGrid',
            frame: true,
            region: 'east',
            collapsible: false,
            collapsed: false,
            split: true,
            flex: 0.8,
            store: rfDownRightObsManagementStore,
            columns: rfDownRightObsManagementColumns,
            tbar: rfDownRightObsItems,
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
            items: [rfDownLeftObsGrid, rfDownMidObsPanel, rfDownRightObsGrid]// 
        });


        /////////////////인근관측국 등록수정 종료//////////////////

        var rfObsManagementTbarItems =
        [{
            xtype: 'displayfield',
            name: 'dispfld1',
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
                    checked: true,
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
            labelWidth: 60,
            labelAlign: 'right',
            width: 190,
            displayField: 'VALUE',
            valueField: 'KEY',
            store: damCodeStore,
            queryMode: 'local'//,
            //            listeners: {
            //                change: function (field, newValue, oldValue, options) {
            //                    obsCodeStore.load({
            //                        params: {
            //                            DamCd: newValue,
            //                            ObsTp: obsTp
            //                        }
            //                    });
            //                }
            //            }
        }
        //        , {
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
        //        }
        , {
            xtype: 'displayfield',
            name: 'dispfld2',
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

                rfObsManagementStore.load({
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
        var rfObsManagementGrid = Ext.create('Ext.grid.Panel', {
            id: 'rfObsManagementGrid',
            name: 'rfObsManagementGrid',
            frame: true,
            flex: 1,
            region: 'center',
            tbar: rfObsManagementTbarItems,
            columns: rfObsManagementColumns,
            store: rfObsManagementStore,
            columnLines: true,
            listeners: {
                selectionchange: function (view, recs, options) {
                    if (recs != null && recs.length > 0) {

                        LATITUDE = recs[0].get('LATITUDE');
                        if (LATITUDE == '') {
                            Ext.Msg.alert('Message', '위도, 경도가 없습니다. 위도, 경도를 입력해 주세요.');
                            rfDownRightObsManagementStore.load({ params: { damCode: "xxxxxxx", obsCode: "xxxxxxx", obsTp: obsTp} });
                            rfDownLeftObsManagementStore.load({ params: { damCode: "xxxxxxx", obsTp: obsTp} });
                            return;
                        }

                        rfDownRightObsManagementStore.load({
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
                        rfDownLeftObsManagementStore.load({
                            params: {
                                damCd: '', //recs[0].get('DAMCD'),
                                obsCd: '',
                                obsTp: obsTp,
                                obCd: recs[0].get("OBSCD") // 인근관측소 테이블에 저장된 RFOBCD
                            }
                        });

                    }
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
            }, rfObsManagementGrid, editObsManagement]
        });
    });

</script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<div id="menu-title" class="title_box x-hide-display" align="center" style="text-align:left;">
	<span style="font-weight:bold; margin-left:10px;">
    <img src="<%=Page.ResolveUrl("/Images") %>/icons/gear.png" align="absmiddle"/>&nbsp;&nbsp;
    우량 인근관측국 관리
    </span>
</div>
</asp:Content>
