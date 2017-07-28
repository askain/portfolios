Ext.define('Verify.DamDataGrid', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.damdatagrid',

    requires: [
        'Ext.grid.plugin.CellEditing',
        'Ext.form.field.Text',
        'Ext.toolbar.TextItem',
        'Ext.grid.*',
        'Ext.data.*',
        'Ext.util.*',
        'Ext.state.*',
        'Ext.selection.*'
    ],

    initComponent: function () {

        this.editing = Ext.create('Ext.grid.plugin.CellEditing');
        
        Ext.apply(this, {
            iconCls: 'icon-grid',
            frame: true,
            plugins: [this.editing]
        });
        this.callParent();
        //this.getSelectionModel().on('selectionchange', this.onSelectChange, this);
    },
    /*
    onSelectChange: function (selModel, selections) {
    this.down('#delete').setDisabled(selections.length === 0);
    },
    */
    onSync: function () {
        //alert("onSync");
        this.store.sync();
    }
    /*
    onDeleteClick: function () {
    var selection = this.getView().getSelectionModel().getSelection()[0];
    if (selection) {
    this.store.remove(selection);
    }
    },

    onAddClick: function () {
    var rec = new Writer.Person({
    first: '',
    last: '',
    email: ''
    }), edit = this.editing;

    edit.cancelEdit();
    this.store.insert(0, rec);
    edit.startEditByPosition({
    row: 0,
    column: 1
    });
    }*/
});

Ext.onReady(function () {
    Ext.BLANK_IMAGE_URL = '/Scripts/extjs/s.gif';

    var getChartData = function () {
        var data = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
        var sdata = "<series>";
        var gd1 = "<graph gid=\"1\">";
        var gd2 = "<graph gid=\"2\">";
        var gd3 = "<graph gid=\"3\">";
        var gd4 = "<graph gid=\"4\">";
        var gd5 = "<graph gid=\"5\">";
        var gd6 = "<graph gid=\"6\">";
        var gd7 = "<graph gid=\"7\">";
        var gd8 = "<graph gid=\"8\">";
        var gd9 = "<graph gid=\"9\">";
        var gd10 = "<graph gid=\"10\">";
        data += "<chart>";
        var cnt = damDataStore.getCount();
        if (cnt > 0) {
            j = 0;
            for (var i = cnt - 1; i >= 0; i--) {
                var cm = damDataStore.getAt(i);
                sdata += "<value xid=\"" + j + "\">" + Ext.Date.format(Ext.Date.parse(cm.get("OBSDT"), "YmdHi"), "Y-m-d H:i") + "</value>";
                gd1 += "<value xid=\"" + j + "\">" + cm.get("RWL") + "</value>";
                gd2 += "<value xid=\"" + j + "\">" + cm.get("RSQTY") + "</value>";
                gd3 += "<value xid=\"" + j + "\">" + cm.get("RSRT") + "</value>";
                gd4 += "<value xid=\"" + j + "\">" + cm.get("IQTY") + "</value>";
                gd5 += "<value xid=\"" + j + "\">" + cm.get("ETCIQTY1") + "</value>";
                gd6 += "<value xid=\"" + j + "\">" + cm.get("ETCIQTY2") + "</value>";
                gd7 += "<value xid=\"" + j + "\">" + cm.get("TDQTY") + "</value>";
                gd8 += "<value xid=\"" + j + "\">" + cm.get("EDQTY") + "</value>";
                gd9 += "<value xid=\"" + j + "\">" + cm.get("SPDQTY") + "</value>";
                gd10 += "<value xid=\"" + j + "\">" + cm.get("DAMBSARF") + "</value>";
                j++;
            }
            data += sdata + "</series>";
            data += "<graphs>";
            data += gd1 + "</graph>";
            data += gd2 + "</graph>";
            data += gd3 + "</graph>";
            data += gd4 + "</graph>";
            data += gd5 + "</graph>";
            data += gd6 + "</graph>";
            data += gd7 + "</graph>";
            data += gd8 + "</graph>";
            data += gd9 + "</graph>";
            data += gd10 + "</graph>";
            data += "</graphs>";
            searchFlag = 2;
        } else {
            //series,value
            var CDHM = Ext.Date.format(currDate, "YmdHi");
            data += "<series>";
            data += "<value xid=\"0\">" + CDHM + "</value>";
            data += "</series>";
            //graphs,graph, value
            data += "<graphs>";
            data += "<graph gid=\"1\">";
            data += "<value xid=\"0\"></value>";
            data += "</graph>";
            data += "<graph gid=\"2\">";
            data += "<value xid=\"0\"></value>";
            data += "</graph>";
            data += "<graph gid=\"3\">";
            data += "<value xid=\"0\"></value>";
            data += "</graph>";
            data += "</graphs>";
            searchFlag = 1;
        }
        data += "</chart>";
        return data;
    };

    var initChartPanel = function () {
        var chartWidth = '100%';
        var chartHeight = '100%';

        swfobject.embedSWF("/Scripts/amcharts/flash/amline.swf", "ChartViewPanel", chartWidth, chartHeight, "8.0.0", "/Scripts/amcharts/flash/expressInstall.swf", flashVars, params);
    };
    var loadChartPanel = function () {
        var data = getChartData();
        flashMovie.setData(data);
    };

    function setCommit() {
        var storeCount = damDataStore.getCount();
        for (var i = 0; i < storeCount; i++) {
            record = damDataStore.getAt(i);
            if (record.dirty == true) {
                record.commit();
            }
        }
    };
    // 'VTypes' 추가하기
    Ext.apply(Ext.form.field.VTypes, {
        daterange: function (val, field) {
            var date = field.parseDate(val);

            if (!date) {
                return false;
            }
            if (field.startDateField && (!this.dateRangeMax || (date.getTime() != this.dateRangeMax.getTime()))) {
                var start = field.up('form').down('#' + field.startDateField);
                start.setMaxValue(date);
                start.validate();
                this.dateRangeMax = date;
            }
            else if (field.endDateField && (!this.dateRangeMin || (date.getTime() != this.dateRangeMin.getTime()))) {
                var end = field.up('form').down('#' + field.endDateField);
                end.setMinValue(date);
                end.validate();
                this.dateRangeMin = date;
            }

            return true;
        },

        daterangeText: '시작날짜가 끝날짜보다 커야 합니다.'
    });

    var bd = Ext.getBody();
    var formatDate = function (v) {
        var cText = "";
        if (v) {
            if (v.length >= 8) {
                cText = v.substring(0, 4) + "-" + v.substring(4, 6) + "-" + v.substring(6, 8);
            }
            if (v.length >= 10) {
                cText += " " + v.substring(8, 10);
            }
            if (v.length >= 12) {
                cText += ":" + v.substring(10, 12);
            }
        }
        return cText;
    };

    /* 모델 정의 */
    Ext.define('DamDataModel', {
        extend: 'Ext.data.Model',
        fields: [
                    { name: 'ID', type: 'string' }
                , { name: 'OBSDT', type: 'string' }
                , { name: 'DAMCD', type: 'string' }
                , { name: 'DAMNM', type: 'string' }
                , { name: 'RWL', type: 'float' }
                , { name: 'RSQTY', type: 'float' }
                , { name: 'RSRT', type: 'float' }
                , { name: 'IQTY', type: 'float' }
                , { name: 'OSPILWL', type: 'float' }
                , { name: 'ETCIQTY1', type: 'float' }
                , { name: 'ETCIQTY2', type: 'float' }
                , { name: 'ETQTY', type: 'float' }
                , { name: 'TDQTY', type: 'float' }
                , { name: 'EDQTY', type: 'float' }
                , { name: 'ETCEDQTY', type: 'float' }
                , { name: 'SPDQTY', type: 'float' }
                , { name: 'ETCDQTY1', type: 'float' }
                , { name: 'ETCDQTY2', type: 'float' }
                , { name: 'ETCDQTY3', type: 'float' }
                , { name: 'OTLTDQTY', type: 'float' }
                , { name: 'ITQTY1', type: 'float' }
                , { name: 'ITQTY2', type: 'float' }
                , { name: 'ITQTY3', type: 'float' }
                , { name: 'DAMBSARF', type: 'float' }
                , { name: 'EDEXLVL', type: 'string' }
                , { name: 'CONTROLDT', type: 'string' }
                , { name: 'EXEMPNO', type: 'string' }
                , { name: 'EXEMPNM', type: 'string' }
                , { name: 'EXDT', type: 'string' }
                , { name: 'CHKEMPNO', type: 'string' }
                , { name: 'CHKEMPNM', type: 'string' }
                , { name: 'CHKDT', type: 'string' }
                , { name: 'REASON', type: 'string' }
            ],
        idProperty: 'ID'
    });
    /* 저장소 정의 */
    damDataStore = Ext.create('Ext.data.Store', {
        id: 'damDataStore',
        autoDestroy: true,
        model: 'DamDataModel',
        autoLoad: false,
        pageSize: pageSize,
        purgePageCount: 0,
        buffered: true,
        proxy: Ext.create("Ext.data.proxy.Ajax", {
            type: 'ajax',
            api: {
                read: '/Verify/GetDamDataVerifyList',
                create: '',
                update: '/Verify/UpdateDamDataVerify',
                destory: ''
            },
            reader: {
                type: 'json',
                totalProperty: 'totalCount',
                root: 'Data'
            },
            writer: {
                allowSingle: false,
                writeAllFields: true,
                root: 'Data'
            },
            listeners: {
                exception: function (proxy, response, operation) {
                    Ext.MessageBox.show({
                        title: '에러발생',
                        msg: operation.getError(),
                        icon: Ext.MessageBox.ERROR,
                        buttons: Ext.Msg.OK
                    });
                }
            },
            simpleSortMode: true
        }),
        sorters: [{
            property: 'OBSDT',
            direction: 'DESC'
        }],
        listeners: {
            load: function (store, records, successful) {
                recontrolGrid(damDataGrid, 0);
                loadChartPanel();
            },
            write: function (proxy, operation) {
                if (operation.action == 'update') {
                    var storeCount = damDataStore.getCount();
                    for (var i = 0; i < storeCount; i++) {
                        record = damDataStore.getAt(i);
                        if (record.dirty == true) {
                            record.commit();
                        }
                    }
                }
            }

        }
    });

    //댐구분 모델
    var damTypeModel = Ext.define('damTypeModel', {
        extend: 'Ext.data.Model',
        idProperty: 'DAMTYPE',
        fields: [
                { name: 'DAMTYPE', type: 'string' },
                { name: 'DAMTPNM', type: 'string' }
           ]
    });

    //댐구분 Proxy
    var damTypeProxy =
    {
        type: 'ajax',
        url: '/DamBoObsMng/GetDamType',
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
    };

    var damTypeStore = Ext.create('Ext.data.Store', {
        id: 'damTypeStore',
        model: 'damTypeModel',
        autoDestroy: true,
        autoLoad: false,
        proxy: damTypeProxy,
        listeners: {
            load: function (store, records, successful, operation) {
                if (records && records.length > 0) {
                    var damType = records[0].data.DAMTYPE;
                    Ext.getCmp('damType').setValue(damType);
                    damCodeStore.load({
                        params: {
                            DamType: damType
                        }
                    });
                }
            }
        },
        sorters: [{ property: 'DAMTYPE', direction: 'ASC'}]
    });

    damTypeStore.load({
        params: {
            type: 'excBo' //보 제외
        }
    });

    /* 코드 모델 정의 */
    Ext.define('CodeMode', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'KEY', type: 'string' },
            { name: 'VALUE', type: 'string' },
            { name: 'ORDERNUM', type: 'int' }
        ]
    });

    /* 댐코드 저장소 정의 */
    var damCodeStore = Ext.create('Ext.data.Store', {
        autoDestroy: true, model: 'CodeMode',
        proxy: {
            type: 'ajax',
            url: '/Common/DamCodeList',
            reader: { type: 'json', root: 'Data' }
        },
        listeners: {
            load: function (store, records, successful, operation) {
                if (records && records.length > 0) {
                    var damCd = records[0].data.KEY;
                    Ext.getCmp('damCd').setValue(damCd);
                }
            }
        },
        sorters: [{ property: 'ORDERNUM', direction: 'ASC'}],
        autoLoad: false
    });
    /* 보정방법 */
    var edExWayStore = Ext.create('Ext.data.Store', {
        autoDestroy: true, model: 'CodeMode',
        proxy: {
            type: 'ajax',
            url: '/Common/GetEdExWayList/?edtp=WL',
            reader: { type: 'json', root: 'Data' }
        },
        sorters: [{ property: 'ORDERNUM', direction: 'ASC'}],
        autoLoad: true
    });
    //보정등급 저장소
    var edExLvlStore = Ext.create('Ext.data.Store', {
        autoDestroy: true, model: 'CodeMode',
        proxy: {
            type: 'ajax',
            url: '/Common/GetEdExLvlList/',
            reader: { type: 'json', root: 'Data' }
        },
        sorters: [{ property: 'ORDERNUM', direction: 'ASC'}],
        autoLoad: true
    });
    var cnrsnText = Ext.create('Ext.form.field.Text', {
        id: 'cnrsnText'
            , name: 'cnrsnText'
            , fieldLabel: '사유'
            , labelWidth: 60
            , width: 340
            , labelAlign: 'right'
            , colspan: 2
    });
    var cndsText = Ext.create('Ext.form.field.Text', {
        id: 'cndsText'
            , name: 'cndsText'
            , fieldLabel: '내역'
            , labelWidth: 60
            , width: 340
            , labelAlign: 'right'
            , colspan: 2
    });
    //alert(Ext.getCmp("cnrsnText"));
    var dateTypes = [
            ['', '전체'],
            ['obdt', '측정일시'],
            ['vrdt', '보정일시']
        ];
    var dataTypes = [
                    ['1', '1분'], ['10', '10분'], ['30', '30분'], ['60', '60분']
                ];
    var targetColumns = [
            ['RWL', '저수위'],
            ['OSPILWL', '방수로수위'],
            ['ETCIQTY1', '기타유입량1'],
            ['ETCIQTY2', '기타유입량2'],
            ['EDQTY', '발전방류량'],
            ['ETCEDQTY', '기타발전방류량'],
            ['SPDQTY', '수문방류량'],
            ['ETCDQTY1', '기타방류량1'],
            ['ETCDQTY2', '기타방류량2'],
            ['ETCDQTY3', '기타방류량3'],
            ['ITQTY1', '취수1'],
            ['ITQTY2', '취수2'],
            ['ITQTY3', '취수3']
    ];

    var TargetColumn = {
        xtype: 'combo'
        , id: 'TargetColumn'
        , name: 'TargetColumn'
		, fieldLabel: '적용대상'
        , labelWidth: 60
        , labelAlign: 'right'
        , width: 250
		, store: targetColumns
        , value: 'RWL'
    };

    //    //품질등급 저장소 정의 */
    //    var exCodeStore = Ext.create('Ext.data.Store', {
    //        autoDestroy: true, model: 'Common.ExCodeModel',
    //        proxy: {
    //            type: 'ajax',
    //            url: '/Common/GetExCodeList',
    //            reader: { type: 'json', root: 'Data' }
    //        },
    //        sorters: [{ property: 'EXORD', direction: 'ASC'}],
    //        autoLoad: true
    //    });
    //    //기타범례 저장소 정의 */
    //    var etcCodeStore = Ext.create('Ext.data.Store', {
    //        autoDestroy: true, model: 'Common.EtcCodeModel',
    //        proxy: {
    //            type: 'ajax',
    //            url: '/Common/GetEtcCodeList',
    //            reader: { type: 'json', root: 'Data' }
    //        },
    //        sorters: [{ property: 'ETCCD', direction: 'ASC'}],
    //        autoLoad: true
    //    });

    var run = function (flag) {
        var fp = searchForm.getForm();
        var damCd = fp.findField("damCd").getValue();
        var startDt = Ext.util.Format.date(fp.findField("startdt").getValue(), 'Ymd');
        var startHt = Ext.util.Format.date(fp.findField("startht").getValue(), 'H');
        var endDt = Ext.util.Format.date(fp.findField("enddt").getValue(), 'Ymd');
        var endHt = Ext.util.Format.date(fp.findField("endht").getValue(), 'H');
        var dataTp = fp.findField("dataTp").getValue();
        var exEmpno = ""; //fp.findField("exempno").getValue();
        if (dataTp == "1") {
            startDt = startDt + startHt + "0000";
            endDt = endDt + endHt + "5959";
        } else if (dataTp == "10" || dataTp == "30") {
            startDt = startDt + startHt + "00";
            endDt = endDt + endHt + "59";
        } else {
            startDt += startHt;
            endDt += endHt;
        };
        if (damCd == null || damCd == "") {
            Ext.Msg.alert('Message', "반드시 댐을 선택하셔야 합니다.");
            return;
        }

        if (flag == 1) {
            damDataStore.proxy.extraParams = {
                damCd: damCd,
                startDt: startDt,
                endDt: endDt,
                dataTp: dataTp
            };
            damDataStore.load();
        } else if (flag == 2) {
            try {
                if (damDataStore.getCount() == 0) { throw new Exception(); }
            } catch (e) {
                Ext.Msg.alert('Message', "먼저 조회를 하셔야 합니다.");
                return false;
            }
            var url = '/Verify/GetDamDataVerifyExcel?';
            var params = {
                damCd: damCd,
                startDt: startDt,
                endDt: endDt,
                dataTp: dataTp
            };
            document.location = url + Ext.urlEncode(params); ;

        }
    };

    //사용자정보 저장소 정의 */
    var userInfoStore = Ext.create('Ext.data.Store', {
        autoDestroy: true, model: 'Common.UserInfo',
        proxy: {
            type: 'ajax',
            url: '/Common/GetUserInfoList',
            reader: { type: 'json', root: 'Data' }
        },
        sorters: [{ property: 'EMPNM', direction: 'ASC'}],
        autoLoad: true
    });


    //////////////////////// 전일평균값 시작 ////////////////////////////////

    function GetAvg(params) {

        //alert(Ext.urlEncode(params));

        //평균값 구하기
        Ext.Ajax.request({
            url: '/Verify/GetDamDataAvg',
            params: params,
            success: function (response) {
                var jsonData = Ext.JSON.decode(response.responseText);
                resultValue = jsonData.Data[0];
                Ext.getCmp("ua_exvl").setValue(resultValue);
            }
        }); //ext.ajax.request
    }

    //일괄수정에 포함시킴..
    var AvgCalcYesterdayButton = {
        xtype: 'button',
        text: '<span style="font-weight: bold;vertical-align:middle">전일평균 계산</span>',
        icon: '/Images/icons/calculator--arrow.png',
        valign: 'top',
        width: 140,
        height: 24,
        colspan: 2,
        //tooltip: '현재 시간으로부터 24시간전까지의 데이터의 평균을 산출합니다.',
        handler: function () {
            //수정값입력과 품질등급, 기타범례, 사유, 적용버튼
            var recs = damDataGrid.getSelectionModel().getSelection();
            var resultValue = 0;

            var rec = recs[0];
            var damCd = rec.get("DAMCD");
            var startDt = Ext.Date.format(prevDate, 'Ymd') + prevHour + "00";
            var endDt = Ext.Date.format(currDate, 'Ymd') + currHour + "00";
            var target = Ext.getCmp("TargetColumn").getValue();

            var params = {
                damcd: damCd,
                startdt: startDt,
                enddt: endDt,
                targetcolumn: target
            };

            GetAvg(params);
        }
    };

    //////////////////////// 전일평균값 끝 ///////////////////////////////

    //////////////////////// 지정시간평균값 시작 ///////////////////////////////


    var AvgCalcSelectedButton = {
        xtype: 'button',
        text: '<span style="font-weight: bold;vertical-align:middle">지정시간평균 계산</span>',
        icon: '/Images/icons/calculator--arrow.png',
        valign: 'top',
        width: 140,
        height: 24,
        colspan: 2,
        handler: function () {
            //수정값입력과 품질등급, 기타범례, 사유, 적용버튼
            var recs = damDataGrid.getSelectionModel().getSelection();
            var resultValue = 0;

            var rec = recs[0];
            var damCd = rec.get("DAMCD");
            var target = Ext.getCmp("TargetColumn").getValue();

            var symd = Ext.Date.format(Ext.getCmp('AvgCalcSelectedstartdt').getValue(), 'Ymd');
            var sh = Ext.Date.format(Ext.getCmp('AvgCalcSelectedstartht').getValue(), 'H');
            var eymd = Ext.Date.format(Ext.getCmp('AvgCalcSelectedenddt').getValue(), 'Ymd');
            var eh = Ext.Date.format(Ext.getCmp('AvgCalcSelectedendht').getValue(), 'H');

            var startdt = symd + sh + "00"; ;
            var enddt = eymd + eh + "00"; ;

            var params = {
                damcd: damCd,
                startdt: startdt,
                enddt: enddt,
                targetcolumn: target
            };

            GetAvg(params);
        }
    };

    var AvgCalcSelectedPanel = {
        xtype: 'container',
        layout: {
            type: 'table'
            , columns: 6
        },
        items: [{
            xtype: 'displayfield'
            , labelAlign: 'right'
            , fieldLabel: '지정시간'
            , labelWidth: 60
        }, {
            xtype: 'datefield'
                , id: 'AvgCalcSelectedstartdt'
                , name: 'AvgCalcSelectedstartdt'
            //, labelAlign: 'right'
            //, fieldLabel: '산출기간'
                , width: 90
                , vtype: 'daterange'
                , endDateField: 'enddt'
                , format: 'Y-m-d'
                , value: prevDate
        }, {
            xtype: 'timefield'
                , name: 'AvgCalcSelectedstartht'
                , id: 'AvgCalcSelectedstartht'
                , width: 50
                , increment: 60
                , format: 'H'
                , emptyText: '선택'
                , value: prevHour
        }, {
            xtype: 'displayfield'
                , name: 'dispfld01'
            //                , margin: '0 0 0 5'
                , value: '~'
                , width: 10
        }, {
            xtype: 'datefield'
                , name: 'AvgCalcSelectedenddt'
                , id: 'AvgCalcSelectedenddt'
                , width: 90
                , vtype: 'daterange'
                , startDateField: 'startdt'
                , format: 'Y-m-d'
                , align: 'right'
                , value: currDate
        }, {
            xtype: 'timefield'
                , name: 'AvgCalcSelectedendht'
                , id: 'AvgCalcSelectedendht'
                , width: 50
                , increment: 60
                , format: 'H'
                , emptyText: '선택'
                , value: currHour
        }, { xtype: 'displayfield' }, AvgCalcSelectedButton, { xtype: 'displayfield' }, AvgCalcYesterdayButton]
    };


    //////////////////////// 지정시간평균값 끝 ///////////////////////////////

    //////////////////////// 일괄수정 시작 ///////////////////////////////
    function showUpdateAllWin() {
        //수정값입력과 품질등급, 기타범례, 사유, 적용버튼

        var win = Ext.create("Ext.Window", {
            title: '댐운영자료 일괄수정',
            layout: {
                type: 'vbox',
                align: 'stretch'
            },
            width: 500,
            height: 213,
            modal: true,
            closeAction: 'hide',
            bbar: [{
                xtype: 'textfield',
                region: 'center',
                id: 'ua_exvl',
                name: 'ua_exvl',
                labelAlign: 'right',
                fieldLabel: '산출값',
                labelWidth: 60,
                width: 230
            }, {
                region: 'south',
                xtype: 'button',
                text: '<span style="font-weight: bold;vertical-align:middle">산출값 적용</span>',
                icon: '/Images/icons/disk.png',
                width: 120,
                valign: 'top',
                height: 24,
                handler: function () {
                    //현재 산출한 값을 그리드에 적용한다.
                    var target = Ext.getCmp("TargetColumn").getValue();
                    var recs = damDataGrid.getSelectionModel().getSelection();

                    if (recs.length < 1) {
                        Ext.Msg.alert('Message', "먼저 적용할 자료를 선택하셔야 합니다.");
                        return false;
                    }
                    var edExWay = Ext.getCmp('edExWayCombo').getValue();
                    if (edExWay == null || edExWay == "") {
                        Ext.Msg.alert('Message', "적용할 보정방법을 선택하셔야 합니다.");
                        return;
                    }
                    var edExLvl = Ext.getCmp('edExLvlCombo').getValue();
                    if (edExLvl == null || edExLvl == "") {
                        Ext.Msg.alert('Message', "적용할 보정등급을 선택하셔야 합니다.");
                        return;
                    }
                    var exvl = Ext.getCmp("ua_exvl").getValue();
                    if (exvl == null || exvl == "") {
                        Ext.Msg.alert('Message', "산출값을 입력하셔야 합니다.");
                        return false;
                    }
                    var cnrsn = Ext.getCmp('cnrsnText').getValue();
                    var cnds = Ext.getCmp('cndsText').getValue();
                    for (var i = 0; i < recs.length; i++) {
                        var rec = recs[i];
                        rec.beginEdit();
                        rec.set(target, exvl);
                        rec.set("EDEXWAY", edExWay);
                        rec.set("EDEXLVL", edExLvl);
                        rec.set("CNRSN", cnrsn);
                        rec.set("CNDS", cnds);
                        rec.endEdit();
                    }
                    win.hide();
                }
            }],
            items: [
                TargetColumn,
                edExWayCombo,
                edExLvlCombo,
                AvgCalcSelectedPanel]
        });
        win.show();
    };
    //////////////////////// 일괄수정 끝 ///////////////////////////////

    //선형보간 적용 시작////////////////////////////////////////////////////////////////////////////////
    var WLDataSearchModel = Ext.define('LinearInterpolationResultModel', {
        extend: 'Ext.data.Model',
        fields: [
                { name: 'OBSDT', type: 'string' },
                { name: 'OBSCD', type: 'string' },
                { name: 'ORIGINAL', type: 'string' },
                { name: 'BOGAN', type: 'string' }
            ]
    });
    var LinearInterpolationResultColumn = [
            { text: '<div style="font-weight: bold; height: 45px;">측정일시</div>', width: 100, sortable: true, align: 'center', dataIndex: 'OBSDT', renderer: formatDate },
            { text: '변경전<BR>수치', width: 70, sortable: false, align: 'center', dataIndex: 'ORIGINAL' },
            { text: '변경후<BR>수치', width: 70, sortable: false, align: 'center', dataIndex: 'BOGAN' }
        ];
    var LinearInterpolationResultProxy = Ext.create('Ext.data.proxy.Ajax', {
        type: 'ajax',
        url: '/Verify/GetDAMLinearInterpolationResult',
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
    });
    var LinearInterpolationResultStore = Ext.create('Ext.data.Store', {
        autoDestroy: true,
        model: 'LinearInterpolationResultModel',
        proxy: LinearInterpolationResultProxy,
        autoLoad: false
    });
    var LinearInterpolationResultGrid = Ext.create('Ext.grid.Panel', {
        region: 'center',
        store: LinearInterpolationResultStore,
        columns: LinearInterpolationResultColumn
    });

    var selectedcolumn = null;
    var LIRShowButton = {
        xtype: 'button',
        name: 'LIRShowButton',
        text: '<span style="font-weight: bold;vertical-align:middle">계 산</span>',
        icon: '/Images/icons/calculator--arrow.png',
        width: 80,
        height: 24,
        handler: function () {
            var recs = damDataGrid.getSelectionModel().getSelection();
            if (recs.length == 0) {
                Ext.Msg.alert('Message', "먼저 적용할 자료를 선택하셔야 합니다.");
                return false;
            }
            var rec = recs[0];
            var damCd = rec.get("DAMCD");
            var dataTp = Ext.getCmp("dataTp").getValue();
            selectedcolumn = Ext.getCmp("TargetColumn").getValue();
            var startDt = rec.get("OBSDT");
            var endDt = startDt;

            //최소 최대 일시를 구함.
            for (var i = 0; i < recs.length; i++) {
                if (startDt > recs[i].get("OBSDT")) {
                    startDt = recs[i].get("OBSDT");
                } else if (endDt < recs[i].get("OBSDT")) {
                    endDt = recs[i].get("OBSDT");
                }
            }

            //alert(damCd + " : " + dataTp + " : " + targetColumn + " : " + startDt + " : " + endDt);
            LinearInterpolationResultGrid.getStore().load({
                params: {
                    damcd: damCd,
                    startdt: startDt,
                    enddt: endDt,
                    dataTp: dataTp,
                    targetcolumn: selectedcolumn
                }
            });

        } // handler
    };

    //    var LIRExcd = {
    //        xtype: 'combo'
    //        , id: 'LIRExcd'
    //        , name: 'LIRExcd'
    //    	, fieldLabel: '품질등급'
    //        , labelWidth: 60
    //        , labelAlign: 'right'
    //        , width: 340
    //        , emptyText: '선택하세요'
    //    	, displayField: 'EXCONT'
    //        , valueField: 'EXCD'
    //    	, store: exCodeStore
    //        , colspan: 2
    //    };

    //    var LIREtccd = {
    //        xtype: 'combo'
    //        , id: 'LIREtccd'
    //        , name: 'LIREtccd'
    //    	, fieldLabel: '기타범례'
    //        , labelWidth: 60
    //        , labelAlign: 'right'
    //        , width: 340
    //        , emptyText: '선택하세요'
    //    	, displayField: 'ETCCONT'
    //        , valueField: 'ETCCD'
    //    	, store: etcCodeStore
    //        , colspan: 2
    //    };
    var edExWayCombo = {
        xtype: 'combo'
            , id: 'edExWayCombo'
            , name: 'edExWayCombo'
			, fieldLabel: '보정방법'
            , labelWidth: 60
            , labelAlign: 'right'
            , width: 340
            , emptyText: '선택하세요'
			, displayField: 'VALUE'
            , valueField: 'KEY'
			, store: edExWayStore
                    , colspan: 2
    };

    var edExLvlCombo = {
        xtype: 'combo'
            , id: 'edExLvlCombo'
            , name: 'edExLvlCombo'
			, fieldLabel: '보정등급'
            , labelWidth: 60
            , width: 340
            , labelAlign: 'right'
            , emptyText: '선택하세요'
			, displayField: 'VALUE'
            , valueField: 'KEY'
            , queryMode: 'local'
			, store: edExLvlStore
                    , colspan: 2
    };

    var LIRButton = {
        xtype: 'button',
        name: 'LIRButton',
        text: '<span style="font-weight: bold;vertical-align:middle">적 용</span>',
        icon: '/Images/icons/disk.png',
        width: 80,
        valign: 'top',
        height: 24,
        handler: function () {
            var edExWay = Ext.getCmp('edExWayCombo').getValue();
            if (edExWay == null || edExWay == "") {
                Ext.Msg.alert('Message', "적용할 보정방법을 선택하셔야 합니다.");
                return;
            }
            var edExLvl = Ext.getCmp('edExLvlCombo').getValue();
            if (edExLvl == null || edExLvl == "") {
                Ext.Msg.alert('Message', "적용할 보정등급을 선택하셔야 합니다.");
                return;
            }
            var LIRRecs = LinearInterpolationResultStore.data.items;
            if (LIRRecs.length == 0) {
                Ext.Msg.alert('Message', "계산을 하셔야 합니다.");
                return;
            }
            var cnrsn = Ext.getCmp('cnrsnText').getValue();
            var cnds = Ext.getCmp('cndsText').getValue();

            var targetRecs = damDataGrid.getSelectionModel().getSelection(); // damDataGrid.getStore().data.items;
            
            for (var i = 0; i < targetRecs.length; i++) {
                for (var j = 0; j < LIRRecs.length; j++) {
                    //alert("targetRecs[i].get('OBSDT'):" + targetRecs[i].get("OBSDT") + "\n" + "nbObsRecs[j].get('OBSDT'):" + nbObsRecs[j].get("OBSDT"));
                    if (targetRecs[i].get("OBSDT") == LIRRecs[j].get("OBSDT")) {
                        targetRecs[i].beginEdit();
                        targetRecs[i].set(selectedcolumn, LIRRecs[j].get("BOGAN"));
                        targetRecs[i].set("EDEXWAY", edExWay);
                        targetRecs[i].set("EDEXLVL", edExLvl);
                        targetRecs[i].set("CNRSN", cnrsn);
                        targetRecs[i].set("CNDS", cnds);
                        targetRecs[i].endEdit();
                    }
                }
            }
            ToggleLinearInterpolationResultWindow();
        }
    };

    var tbarPanel = Ext.create('Ext.panel.Panel', {
        region: 'center',
        frame: true,
        height: 180,
        bodyPadding: 5,
        //forceFit:true,
        autoWidth: true,
        width: 370,
        layout: {
            type: 'table'
            , columns: 2
        },
        items: [TargetColumn, LIRShowButton, edExWayCombo, edExLvlCombo, cnrsnText, cndsText, LIRButton]

    });

    var LinearInterpolationResultWin;
    function ToggleLinearInterpolationResultWindow() {
        if (!LinearInterpolationResultWin) {
            LinearInterpolationResultWin = Ext.create('widget.window', {
                title: '선형보간적용',
                closable: true,
                closeAction: 'hide',
                //animateTarget: this,
                width: 380,
                height: 350,
                layout: 'border',
                bodyStyle: 'padding: 5px;',
                modal: true,
                tbar: [tbarPanel],
                //tbar: [LIRExcd,LIREtccd,LIRButton],
                items: [LinearInterpolationResultGrid]
            });
        } // if

        if (LinearInterpolationResultWin.isVisible()) {
            LinearInterpolationResultWin.hide();
        } else {
            LinearInterpolationResultWin.show();
            LinearInterpolationResultGrid.getStore().removeAll();
        }
    }

    //선형보간 적용 끝  ////////////////////////////////////////////////////////////////////////////////

    var damDataGrid = Ext.create('Verify.DamDataGrid', {
        id: 'DamDataVerifyGrid',
        autoWidth: true,
        autoScroll: true,
        autoRender: false,
        flex: 1,
        style: 'padding:0;',
        store: damDataStore,
        //stateful: false,
        region: 'center',
        //selType: 'cellmodel',
        multiSelect: true,
        verticalScroller: {
            xtype: 'paginggridscroller',
            activePrefetch: false
        },
        loadMask: true,
        invalidateScrollerOnRefresh: false,
        viewConfig: {
            trackOver: false
        },
        columns: [
                { text: '댐명', width: 100, sortable: true, align: 'center', dataIndex: 'DAMNM', locked: true },
                { text: '측정일시', width: 120, sortable: false, align: 'center', dataIndex: 'OBSDT', renderer: formatDate, locked: true },
                { text: '저수위', flex: 1, minWidth: 80, sortable: false, align: 'right', dataIndex: 'RWL', field: { type: 'numberfield'} },
                { text: '저수량', flex: 1, minWidth: 80, sortable: false, align: 'right', dataIndex: 'RSQTY' },
                { text: '저수율', flex: 1, minWidth: 80, sortable: false, align: 'right', dataIndex: 'RSRT' },
                { text: '방수로수위', flex: 1.4, minWidth: 80, sortable: false, align: 'right', dataIndex: 'OSPILWL', field: { type: 'numberfield'} },
                { text: '유입량', flex: 1, minWidth: 80, sortable: false, align: 'right', dataIndex: 'IQTY' },
                { text: '기타유입량1', flex: 1.4, minWidth: 80, sortable: false, align: 'right', dataIndex: 'ETCIQTY1', field: { type: 'numberfield'} },
                { text: '기타유입량2', flex: 1.4, minWidth: 80, sortable: false, align: 'right', dataIndex: 'ETCIQTY2', field: { type: 'numberfield'} },
                { text: '공용량', flex: 1, minWidth: 80, sortable: false, align: 'right', dataIndex: 'ETQTY' },
                { text: '총방류량', flex: 1.3, minWidth: 80, sortable: false, align: 'right', dataIndex: 'TDQTY' },
                { text: '발전방류량', flex: 1.4, minWidth: 80, sortable: false, align: 'right', dataIndex: 'EDQTY', field: { type: 'numberfield'} },
                { text: '기타발전방류량', flex: 1.4, minWidth: 80, sortable: false, align: 'right', dataIndex: 'ETCEDQTY', field: { type: 'numberfield'} },
                { text: '수문방류량', flex: 1.4, minWidth: 80, sortable: false, align: 'right', dataIndex: 'SPDQTY', field: { type: 'numberfield'} },
                { text: '기타방류량1', flex: 1.4, minWidth: 80, sortable: false, align: 'right', dataIndex: 'ETCDQTY1', field: { type: 'numberfield'} },
                { text: '기타방류량2', flex: 1.5, minWidth: 80, sortable: false, align: 'right', dataIndex: 'ETCDQTY2', field: { type: 'numberfield'} },
                { text: '기타방류량3', flex: 1.4, minWidth: 80, sortable: false, align: 'right', dataIndex: 'ETCDQTY3', field: { type: 'numberfield'} },
                { text: '아울렛방류량', flex: 1.4, minWidth: 80, sortable: false, align: 'right', dataIndex: 'OTLTDQTY', field: { type: 'numberfield'} },
                { text: '취수1', flex: 1.5, minWidth: 80, sortable: false, align: 'right', dataIndex: 'ITQTY1', field: { type: 'numberfield'} },
                { text: '취수2', flex: 1.2, minWidth: 80, sortable: false, align: 'right', dataIndex: 'ITQTY2', field: { type: 'numberfield'} },
                { text: '취수3', flex: 1.3, minWidth: 80, sortable: false, align: 'right', dataIndex: 'ITQTY3', field: { type: 'numberfield'} },
                { text: '평균우량', flex: 1, minWidth: 80, sortable: false, align: 'right', dataIndex: 'DAMBSARF' },
                { text: '보정자', width: 60, sortable: false, align: 'center', dataIndex: 'EXEMPNM' },
                { text: '보정일시', width: 120, sortable: false, align: 'center', dataIndex: 'EXDT' },
                { text: '확인자', width: 60, sortable: false, align: 'center', dataIndex: 'CHKEMPNM' },
                { text: '확인일시', width: 120, sortable: false, align: 'center', dataIndex: 'CHKDT' }
            ],
        columnLines: true,
        bbar: [
            {
                xtype: 'button'
                , name: 'btnUpdateAll'
                , text: '<span style="font-weight: bold;vertical-align:middle">일괄수정</span>'
                , icon: '/Images/icons/disk--arrow.png'
                , handler: function () {
                    var recs = damDataGrid.getSelectionModel().getSelection();
                    if (recs.length < 1) {
                        Ext.Msg.alert('Message', "수정할 자료를 선택하셔야 합니다.");
                        return;
                    } else {
                        //일괄수정 모달 팝업 띄우기
                        showUpdateAllWin();
                    }
                }
            },
            {
                xtype: 'button'
                , name: 'btnPrevAvg'
                , text: '<span style="font-weight: bold;vertical-align:middle">전일평균</span>'
                , icon: '/Images/icons/disk--arrow.png'
                , handler: function () {
                    var recs = damDataGrid.getSelectionModel().getSelection();
                    if (recs.length < 1) {
                        Ext.Msg.alert('Message', "수정할 자료를 선택하셔야 합니다.");
                        return;
                    } else {
                        //전일 평균 구하는 store 호출
                        showUpdateAllWin();
                    }
                }
            },
            {
                xtype: 'button'
                , name: 'btnLinearMth'
                , text: '<span style="font-weight: bold;vertical-align:middle">선형보간</span>'
                , icon: '/Images/icons/disk--arrow.png'
                , handler: function () {
                    var recs = damDataGrid.getSelectionModel().getSelection();
                    if (recs.length < 1) {
                        Ext.Msg.alert('Message', "수정할 자료를 선택하셔야 합니다.");
                        return;
                    } else {
                        //선형보간법으로 데이터 구하는 store 호출
                        ToggleLinearInterpolationResultWindow();
                    }
                }
            },
            {
                xtype: 'button'
                , name: 'btnSelectTime'
                , text: '<span style="font-weight: bold;vertical-align:middle">지정시간평균</span>'
                , icon: '/Images/icons/disk--arrow.png'
                , handler: function () {
                    var recs = damDataGrid.getSelectionModel().getSelection();
                    if (recs.length < 1) {
                        Ext.Msg.alert('Message', "수정할 자료를 선택하셔야 합니다.");
                        return;
                    } else {
                        //지정시간평균을 구하는 팝업을 띄운다.
                        showUpdateAllWin();
                    }
                }
            }, '->', {
                xtype: 'button'
                    , name: 'submit2'
                    , margin: '0 0 0 20'
                    , text: '<span style="font-weight: bold;vertical-align:middle">일괄저장</span>'
                    , icon: '/Images/icons/disks.png'
                    , width: 80
                    , handler: function () {
                        var ur = damDataStore.getUpdatedRecords();
                        if (ur.length < 1) {
                            Ext.Msg.show({
                                title: '저장',
                                msg: '저장할 자료가 없습니다.<br/>먼저 자료를 수정하세요.',
                                width: 150,
                                buttons: Ext.Msg.OK,
                                icon: Ext.Msg.INFO
                            });
                        } else {
                            Ext.Msg.show({
                                title: '저장',
                                msg: '수정한 자료를 저장하시겠습니까?',
                                width: 150,
                                buttons: Ext.Msg.OKCANCEL,
                                icon: Ext.Msg.INFO,
                                fn: function (btn) {
                                    if (btn == "ok") {
                                        //update된 항목들에 보정자와 보정일시 입력
                                        var recs = damDataStore.getUpdatedRecords();
                                        for (var i = 0; i < recs.length; i++) {
                                            var rec = recs[i];
                                            rec.beginEdit();
                                            rec.set("EXEMPNO", loginEmpNo);
                                            rec.set("EXEMPNM", loginEmpNm);
                                            rec.set("EXDT", Ext.Date.format(new Date(), 'YmdHis'));
                                            rec.endEdit();
                                        }
                                        damDataStore.sync();
                                        loadChartPanel();
                                    }
                                }
                            });
                        }
                    }
            }]
    });


    var searchForm = Ext.create('Ext.form.Panel', {
        bodyPadding: 0,
        frame: true,
        region: 'center',
        autoWidth: true,
        height: 100,
        bodyPadding: 5,
        layout: {
            type: 'table'
                , columns: 10
        },
        items: [{
            xtype: 'combobox',
            id: 'damType',
            name: 'damType',
            fieldLabel: '댐구분',
            labelWidth: 60,
            labelAlign: 'right',
            width: 160,
            height: 26,
            typeAhead: true,
            triggerAction: 'all',
            selectOnTab: true,
            valueField: 'DAMTYPE',
            displayField: 'DAMTPNM',
            store: damTypeStore,
            queryMode: 'local',
            listeners: {
                change: function (field, newValue, oldValue, options) {
                    damCodeStore.load({
                        params: {
                            DamType: newValue
                        }
                    });
                }
            }
        }, {
            xtype: 'combobox'
                , id: 'damCd'
				, fieldLabel: '댐명'
                , labelWidth: 60
                , labelAlign: 'right'
                , width: 160
                , displayField: 'VALUE'
                , valueField: 'KEY'
				, store: damCodeStore
                , queryMode: 'local'
                , minChars: 1
                , emptyText: '선택'
        }, {
            xtype: 'combo'
                , id: 'srchTp'
				, fieldLabel: '일시'
                , labelWidth: 40
                , labelAlign: 'right'
                , width: 120
				, store: dateTypes
                , value: ''
        }, {
            xtype: 'displayfield'
                , width: 10
        }, {
            xtype: 'datefield'
                , name: 'startdt'
                , labelAlign: 'right'
                , id: 'startdt'
                , width: 90
                , vtype: 'daterange'
                , endDateField: 'enddt'
                , format: 'Y-m-d'
        }, {
            xtype: 'timefield'
                , name: 'startht'
                , id: 'startht'
                , width: 50
                , increment: 60
                , format: 'H'
                , emptyText: '선택'
        }, {
            xtype: 'displayfield'
                , name: 'dispfld01'
                , margin: '0 0 0 5'
                , value: '~'
                , width: 10
        }, {
            xtype: 'datefield'
                , name: 'enddt'
                , id: 'enddt'
                , width: 90
                , vtype: 'daterange'
                , startDateField: 'startdt'
                , format: 'Y-m-d'
                , align: 'right'
        }, {
            xtype: 'timefield'
                , name: 'endht'
                , id: 'endht'
                , width: 50
                , increment: 60
                , format: 'H'
                , emptyText: '선택'
        }, {
            xtype: 'displayfield'
        }, {
            xtype: 'displayfield'
        }, {
            xtype: 'combo'
                , id: 'exempno'
                , name: 'exempno'
                , flex: 1
				, fieldLabel: '사원'
                , labelWidth: 60
                , labelAlign: 'right'
                , width: 160
				, displayField: 'EMPNM'
                , valueField: 'EMPNO'
				, store: userInfoStore
                , queryMode: 'local'
                , minChars: 1
                , emptyText: '사원검색'
        }, {
            xtype: 'combo'
                , id: 'dataTp'
                , name: 'dataTp'
                , flex: 1
				, fieldLabel: '구분 '
                , labelWidth: 40
                , labelAlign: 'right'
                , width: 120
				, store: dataTypes
                , queryMode: 'local'
                , value: '10'
        }, {
            xtype: 'displayfield'
                , width: 10
        }, {
            xtype: 'button'
                , name: 'submit2'
                , text: '<span style="font-weight: bold">엑셀</span>'
                , icon: '/Images/icons/document-excel-table.png'
            //, margin: '0 0 0 10'
                , width: 80
                , height: 24
                , formBind: true
                , handler: function () {
                    run(2);
                }
        }, {
            xtype: 'button'
                , name: 'submit1'
                , text: '<span style="font-weight: bold">조 회</span>'
                , icon: '/Images/icons/magnifier.png'
                , width: 80
                , colspan: 3
                , formBind: true
                , handler: function () {
                    run(1);
                }
        }]
    });

    var mainForm = Ext.create('Ext.Viewport', {
        renderTo: bd,
        layout: {
            type: 'border',
            padding: 0
        },
        border: 0,
        items: [{
            region: 'north',
            height: 120,
            border: 3,
            items: [{
                height: 45,
                border: 0,
                region: 'north',
                padding: '10 20 5 5',
                contentEl: 'menu-title'
            }, searchForm
                ]
        }, damDataGrid,
            {
                id: 'chart-panel',
                region: 'south',
                flex: 0.9,
                bodyStyle: {
                    background: '#fff',
                    padding: '0px'
                },
                frame: true,
                split: true,
                collapsible: false,
                contentEl: 'ChartViewPanel',
                tbar: [{
                    xtype: 'displayfield',
                    value: '<img src="/Images/icons/chart--arrow.png" align="top">&nbsp;<b>댐운영자료 그래프</b>',
                    width: 200
                }, '->', {
                    xtype: 'button',
                    text: '<b>이미지저장</b>',
                    icon: '/Images/icons/image--pencil.png',
                    handler: function () {
                        if (damDataStore.getTotalCount() > 0) {
                            flashMovie.exportImage("/Verify/ExportDamdataChartImage");
                        } else {
                            Ext.Msg.alert('Message', "그래프데이터가 존재하지 않습니다.");
                            return;
                        }
                    }
                }]
            }]

    });

    var gp = Ext.getCmp('DamDataVerifyGrid');
    Ext.getCmp('enddt').setValue(currDate);
    Ext.getCmp('startdt').setValue(prevDate);
    Ext.getCmp('startht').setValue(prevHour);
    Ext.getCmp('endht').setValue(currHour);
    initChartPanel();
});