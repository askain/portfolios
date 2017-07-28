<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="HDIMS.Utils.Login"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	댐/보 저장항목 관리
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

    Ext.onReady(function () {
        Ext.BLANK_IMAGE_URL = '/Scripts/extjs/s.gif';
        Ext.tip.QuickTipManager.init();

        //////////////////////// function 시작 /////////////////////////

        function getUseYN(val, meta, rec, row, col, store) {
            if (val == "Y") {
                return "<input type='checkbox' checked/>";
            } else {
                return "<input type='checkbox'/>";
            }
        }

        function getDamYN(val, meta, rec, row, col, store) {
            if (val == "Y") {
                return "<input type='checkbox' checked/>";
            } else {
                return "<input type='checkbox'/>";
            }
        }

        function getWlYN(val, meta, rec, row, col, store) {
            if (val == "Y") {
                return "<input type='checkbox' checked/>";
            } else {
                return "<input type='checkbox'/>";
            }
        }

        function getRfYN(val, meta, rec, row, col, store) {
            if (val == "Y") {
                return "<input type='checkbox' checked/>";
            } else {
                return "<input type='checkbox'/>";
            }
        }

        function setWkNm(value, metaData, record, rowIndex, colIndex, store, view) {

            var retVal = 'X';

            switch (value) {
                case '1':
                    retVal = '한강수계';
                    break;
                case '2':
                    retVal = '낙동강수계';
                    break;
                case '3':
                    retVal = '금강수계';
                    break;
                case '4':
                    retVal = '섬진강수계';
                    break;
                case '5':
                    retVal = '영산강수계';
                    break;
                case '6':
                    retVal = '임진강유역';
                    break;
                case '7':
                    retVal = '기타수계';
                    break;
            }

            return '<div style="cursor:pointer;width:100%">' + retVal + '</div>';
        }

        function setDamType(value, metaData, record, rowIndex, colIndex, store, view) {
            var retVal = '';
            var crec = damTypeComboStore.findRecord("DAMTYPE", value);
            if (crec) retVal = crec.get("DAMTPNM");

            return '<div style="cursor:pointer;width:100%">' + retVal + '</div>';
        }

        function setDamMgt(value, metaData, record, rowIndex, colIndex, store, view) {
            var crec = damMgtStore.findRecord("MGTCD", value);
            if (crec)
                return trim(crec.get("MGTNM").replace("└", "").replace("─", "").replace("→", ""));
            return "미선택";
        }

        function setCommit() {
            var storeCount = damColMgtStore.getCount();
            for (var i = 0; i < storeCount; i++) {
                record = damColMgtStore.getAt(i);
                if (record.dirty == true) {
                    record.commit();
                }
            }
            damColMgtStore.load({
                params: {
                    wkCd: Ext.getCmp('comboWk').getValue(),
                    damTp: Ext.getCmp('comboDamType').getValue()
                }
            });
        }

        //////////////////////// function 종료 /////////////////////////


        //*********************** 수계, 댐구분 combo 정의 시작 ***********************//

        var wkComboModel = Ext.define('wkComboModel', {
            extend: 'Ext.data.Model',
            idProperty: 'WKCD',
            fields: [
                { name: 'WKCD', type: 'string' },
                { name: 'WKNM', type: 'string' }
           ]
        });

        var wkComboProxy =
        {
            type: 'ajax',
            url: '<%=Page.ResolveUrl("~/DamBoObsMng")%>/GetWkCombo',
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

        var wkComboStore = Ext.create('Ext.data.Store', {
            id: 'wkComboStore',
            model: 'wkComboModel',
            autoDestroy: true,
            remoteSort: true,
            autoLoad: true,
            autoSync: false,
            simpleSortMode: true,
            proxy: wkComboProxy,
            listeners: {
                load: function (store, records, successful, operation) {
                    if (records && records.length > 0) {
                        Ext.getCmp('comboWk').setValue(records[0].data.WKCD);
                    }
                }
            },
            sorters: [{ property: 'WKCD', direction: 'ASC'}],
            autoLoad: true
        });

        // ----------------------------------------- //

        var damTypeComboModel = Ext.define('damTypeComboModel', {
            extend: 'Ext.data.Model',
            idProperty: 'DAMTYPE',
            fields: [
                { name: 'DAMTYPE', type: 'string' },
                { name: 'DAMTPNM', type: 'string' }
           ]
        });

        var damTypeComboProxy =
        {
            type: 'ajax',
            url: '<%=Page.ResolveUrl("~/DamBoObsMng")%>/GetDamTypeCombo',
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

        // Tbar의 댐구분 콤보
        var damTypeComboStore = Ext.create('Ext.data.Store', {
            id: 'damTypeComboStore',
            model: 'damTypeComboModel',
            autoDestroy: true,
            remoteSort: true,
            autoLoad: true,
            autoSync: false,
            simpleSortMode: true,
            proxy: damTypeComboProxy,
            listeners: {
                load: function (store, records, successful, operation) {
                    if (records && records.length > 0) {
                        Ext.getCmp('comboDamType').setValue(records[0].data.DAMTYPE);
                    }
                }
            },
            sorters: [{ property: 'DAMTYPE', direction: 'ASC'}],
            autoLoad: true
        });


        //*********************** 수계, 댐구분 combo 정의 종료 ***********************//


        var wkModel = Ext.define('wkModel', {
            extend: 'Ext.data.Model',
            idProperty: 'WKCD',
            fields: [
                { name: 'WKCD', type: 'string' },
                { name: 'WKNM', type: 'string' }
           ]
        });

        var wkProxy =
        {
            type: 'ajax',
            url: '<%=Page.ResolveUrl("~/DamBoObsMng")%>/GetWk',
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
        //수계
        var wkStore = Ext.create('Ext.data.Store', {
            id: 'wkStore',
            model: 'wkModel',
            autoDestroy: true,
            remoteSort: true,
            autoLoad: true,
            autoSync: false,
            simpleSortMode: true,
            proxy: wkProxy,
            listeners: {
                load: function (store, records, successful, operation) {
                    if (records && records.length > 0) {
                    }
                }
            },
            sorters: [{ property: 'WKCD', direction: 'ASC'}],
            autoLoad: true
        });


        var damTypeModel = Ext.define('damTypeModel', {
            extend: 'Ext.data.Model',
            idProperty: 'DAMTYPE',
            fields: [
                { name: 'DAMTYPE', type: 'string' },
                { name: 'DAMTPNM', type: 'string' }
           ]
        });

        var damTypeProxy =
        {
            type: 'ajax',
            url: '<%=Page.ResolveUrl("~/DamBoObsMng")%>/GetDamType/?type=all',
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

        //리스트 안의 댐구분 콤보
        var damTypeStore = Ext.create('Ext.data.Store', {
            id: 'damTypeStore',
            model: 'damTypeModel',
            autoDestroy: true,
            remoteSort: true,
            autoLoad: false,
            autoSync: false,
            simpleSortMode: true,
            proxy: damTypeProxy,
            listeners: {
                load: function (store, records, successful, operation) {
                    if (records && records.length > 0) {
                    }
                }
            },
            sorters: [{ property: 'DAMTYPE', direction: 'ASC'}]
        });


        var DamColModel = Ext.define('DamColModel', {
            extend: 'Ext.data.Model',
            idProperty: 'DAMCD',
            fields: [
                { name: 'DAMCD', type: 'string' },
                { name: 'DAMNM', type: 'string' },
                { name: 'RWL', type: 'string' },
                { name: 'RSQTY', type: 'string' },
                { name: 'RSRT', type: 'string' },
                { name: 'OSPILWL', type: 'string' },
                { name: 'IQTY', type: 'string' },
                { name: 'ETCIQTY1', type: 'string' },
                { name: 'ETCIQTY2', type: 'string' },
                { name: 'ETQTY', type: 'string' },
                { name: 'TDQTY', type: 'string' },
                { name: 'EDQTY', type: 'string' },
                { name: 'ETCEDQTY', type: 'string' },
                { name: 'SPDQTY', type: 'string' },
                { name: 'OTLTDQTY', type: 'string' },
                { name: 'ETCDQTY1', type: 'string' },
                { name: 'ETCDQTY2', type: 'string' },
                { name: 'ETCDQTY3', type: 'string' },
                { name: 'ITQTY1', type: 'string' },
                { name: 'ITQTY2', type: 'string' },
                { name: 'ITQTY3', type: 'string' }
           ]
        });

        var damColMgtProxy =
        {
            type: 'ajax',
            api: {
                read: '<%=Page.ResolveUrl("~/DamBoObsMng")%>/GetDamColMgtList',
                create: '<%=Page.ResolveUrl("~/DamBoObsMng")%>/SaveDamColMgt',
                update: '<%=Page.ResolveUrl("~/DamBoObsMng")%>/SaveDamColMgt'
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

        var damColMgtStore = Ext.create('Ext.data.Store', {
            id: 'damColMgtStore',
            model: 'DamColModel',
            autoDestroy: true,
            remoteSort: false,
            autoLoad: true,
            autoSync: false,
            simpleSortMode: true,
            proxy: damColMgtProxy,
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
            }
        });

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

        var damColMgtColumns = [

        { header: '댐명', flex: 0.5, minWidth: 60, align: 'center', dataIndex: 'DAMNM', renderer: Ext.util.Format.uppercase }
        , { header: '댐코드', flex: 0.5, minWidth: 60, align: 'center', dataIndex: 'DAMCD', renderer: Ext.util.Format.uppercase }
        , { header: '저수위', flex: 0.3, minWidth: 30, align: 'center', dataIndex: 'RWL', field: { type: 'textfield'} }
        , { header: '저수량', flex: 0.3, minWidth: 30, align: 'center', dataIndex: 'RSQTY',  field: { type: 'textfield'} }
        , { header: '저수율', flex: 0.3, minWidth: 30, align: 'center', dataIndex: 'RSRT',  field: { type: 'textfield'} }
        , { header: '방수로수위', flex: 0.3, minWidth: 30, align: 'center', dataIndex: 'OSPILWL',  field: { type: 'textfield'} }
        , { header: '유입량', flex: 0.3, minWidth: 30, align: 'center', dataIndex: 'IQTY',  field: { type: 'textfield'} }
        , { header: '기타유입1', flex: 0.3, minWidth: 30, align: 'center', dataIndex: 'ETCIQTY1', field: { type: 'textfield'} }
        , { header: '기타유입2', flex: 0.3, minWidth: 30, align: 'center', dataIndex: 'ETCIQTY2', field: { type: 'textfield'} }
        , { header: '공용량', flex: 0.3, minWidth: 30, align: 'center', dataIndex: 'ETQTY',  field: { type: 'textfield'} }
        , { header: '총방류량', flex: 0.3, minWidth: 30, align: 'center', dataIndex: 'TDQTY',  field: { type: 'textfield'} }
        , { header: '발전방류', flex: 0.3, minWidth: 30, align: 'center', dataIndex: 'EDQTY', field: { type: 'textfield'} }
        , { header: '기타발전방류', flex: 0.3, minWidth: 30, align: 'center', dataIndex: 'ETCEDQTY', field: { type: 'textfield'} }
        , { header: '수문방류', flex: 0.3, minWidth: 30, align: 'center', dataIndex: 'SPDQTY', field: { type: 'textfield'} }
        , { header: '아울렛방류', flex: 0.3, minWidth: 30, align: 'center', dataIndex: 'OTLTDQTY', field: { type: 'textfield'} }
        , { header: '기타방류1', flex: 0.3, minWidth: 30, align: 'center', dataIndex: 'ETCDQTY1', field: { type: 'textfield'} }
        , { header: '기타방류2', flex: 0.3, minWidth: 30, align: 'center', dataIndex: 'ETCDQTY2', field: { type: 'textfield'} }
        , { header: '기타방류3', flex: 0.3, minWidth: 30, align: 'center', dataIndex: 'ETCDQTY3', field: { type: 'textfield'} }
        , { header: '취수1', flex: 0.3, minWidth: 30, align: 'center', dataIndex: 'ITQTY1', field: { type: 'textfield'} }
        , { header: '취수2', flex: 0.3, minWidth: 30, align: 'center', dataIndex: 'ITQTY2', field: { type: 'textfield'} }
        , { header: '취수3', flex: 0.3, minWidth: 30, align: 'center', dataIndex: 'ITQTY3', field: { type: 'textfield'} }
        ];

        var damBoTbarItems =
        [
            {
                xtype: 'combobox',
                id: 'comboWk',
                fieldLabel: '수계',
                labelWidth: 60,
                labelAlign: 'right',
                width: 190,
                typeAhead: true,
                triggerAction: 'all',
                selectOnTab: true,
                valueField: 'WKCD',
                displayField: 'WKNM',
                store: wkComboStore,
                queryMode: 'local',
                listeners: {
                    change: function (field, newValue, oldValue, options) {
                    }
                }
            }, {
                xtype: 'combobox',
                id: 'comboDamType',
                fieldLabel: '댐구분',
                labelWidth: 60,
                labelAlign: 'right',
                width: 190,
                typeAhead: true,
                triggerAction: 'all',
                selectOnTab: true,
                valueField: 'DAMTYPE',
                displayField: 'DAMTPNM',
                store: damTypeComboStore,
                queryMode: 'local',
                listeners: {
                    change: function (field, newValue, oldValue, options) {
                    }
                }
            }, ' ',
            {
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/navigation-000-frame.png',
                text: '<span style="vertical-align:middle">조회</span>',
                itemId: 'search',
                tooltip: '조회',
                scope: this,
                handler: function () {
                    damColMgtStore.load({
                        params: {
                            wkCd: Ext.getCmp('comboWk').getValue(),
                            damTp: Ext.getCmp('comboDamType').getValue()
                        }
                    });
                }
            }, '->'
            , '-'
            , {
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/disk.png',
                text: '<span style="vertical-align:middle">저장</span>',
                itemId: 'save',
                tooltip: '데이터 저장',
                scope: this,
                handler: function () {

                    var storeCount = damColMgtStore.getCount();
                    for (var i = 0; i < storeCount; i++) {
                        record = damColMgtStore.getAt(i);
                        if (record.dirty == true) {
                            if (damColMgtStore.getAt(i).data.DAMCD == '' || damColMgtStore.getAt(i).data.DAMNM == '') {

                                Ext.Msg.alert("Message", "댐코드와 댐명은 필수 입력 항목입니다.");
                                return;
                            }
                        }
                    }
                    //필수입력의 컬럼에서 다른 id로 포커스 이동 
                    //뻘짓거리지만 필수입력 사항만 입력후 저장시 필수 입력의 포커스가 늦게 빠져서 저장이 원활하게 되지 않는 경우를 우회함 
                    //Ext.getCmp("dpf").focus(false);
                    //Ext.getCmp("dpf").setWidth(120);

                    Ext.Msg.show({
                        title: 'Message',
                        msg: '데이터를 저장하시겠습니까?',
                        width: 150,
                        buttons: Ext.Msg.OKCANCEL,
                        icon: Ext.Msg.INFO,
                        fn: function (btn) {
                            if (btn == "ok") {
                                //Dirty가 없을경우에도 저장될 수 있도록 처리한다.
                                //새로운 댐,보등을 저장할 경우 처리
                                damColMgtStore.getAt(0).setDirty();
                                damColMgtStore.sync();
                            }
                        }
                    });
                }
            }
        ];

        var damColMgtGrid = Ext.create('Edit.Grid', {
            id: 'damColMgtGrid',
            name: 'damColMgtGrid',
            flex: 1,
            region: 'center',
            frame: true,
            columns: damColMgtColumns,
            store: damColMgtStore,
            tbar: damBoTbarItems,
            columnLines: true,
            listeners: {
                beforeedit: function (editor, e, options) {

                    var commitStoreCount = damColMgtStore.getTotalCount();
                    var totalStoreCount = damColMgtStore.getCount();
                    var addRowCount = totalStoreCount - commitStoreCount;

                    if (editor.rowIdx >= addRowCount) {
                        if (editor.colIdx == 0 || editor.colIdx == 1) editor.cancel = true;
                    }

                    //추가 : 자기 댐이 아닌경우는 수정이 안돼도록 함.
                    var rowItem = this.getStore().getAt(editor.rowIdx);
                    if (MGTDAM != 'MAIN' && MGTDAM.indexOf(rowItem.get('DAMCD')) == -1) {
                        return false;
                    }
                },
                selectionchange: function (view, selections, options) {
                    //this.down('#delete').setDisabled(selections.length === 0);
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
            }, damColMgtGrid]
        });
    });

</script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<div id="menu-title" class="title_box x-hide-display" align="center" style="text-align:left;">
	<span style="font-weight:bold; margin-left:10px;">
    <img src="<%=Page.ResolveUrl("/Images") %>/icons/balance.png" align="absmiddle"/>&nbsp;&nbsp;
    댐/보 저장항목 관리
    </span>
</div>
</asp:Content>
