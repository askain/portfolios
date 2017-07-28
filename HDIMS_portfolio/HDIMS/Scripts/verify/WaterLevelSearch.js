Ext.onReady(function () {
    Ext.BLANK_IMAGE_URL = '/Scripts/extjs/s.gif';

    var WLVL = false;
    var FLWVL = true;

    var CHColor = [];
    //CHColor = ['0000FF','009900','660000','996600','CC0000','FFCC00','FF00FF','FF9900','000066','006600','660066','990000','CC66FF','CC6600','3366FF','6600FF','666600','996666','CC0066','66CC00','330066','990066','6666FF','999900','CC3366','FF9966','003300','9900FF','669900','9966FF','CC3300','99CC00','006666','9999FF','FF6666','0099FF','00CC00','999966','009966','33CC66','6699FF','666666','669966','000000'];
    CHColor = ['A5BC4E', '0000FF', '00FF00', 'FF0000', 'DAA520', '4169E1', 'FF6347', '00BFFF', '006400', '800000', 'DEB887', '4B0082', '8B4513', '1E90FF', '008B8B', 'FFD700', '800080', 'A52A2A', '87CEEB', '2F4F4F', '8A2BE2', 'FF7F50', '40E0D0', 'BC8F8F', 'B8860B', '20B2AA', '556B2F', 'C0C0C0', 'C71585', 'CD853F', 'DC143C', '000080', '228B22', 'FF00FF', 'E9967A', '6B8E23', 'FF1493', 'F08080', '7CFC00', 'FF69B4', 'F4A460', 'BDB76B', '483D8B', 'FA8072', '9ACD32', '6A5ACD', 'FF4500', '90EE90', '9370D8', 'D2B48C', 'A0522D', '8FBC8F', '808080', 'FFA07A', '808000', 'BA55D3', 'CD5C5C', 'EE82EE', '32CD32', '000000']
  

    var rebuildChartSettings = function () {
        var cnt = waterLevelStore.getCount();
        var cSet, obsNm;
        if (cnt > 0) {
            cSet = "<settings><graphs>";
            for (var i = 0; i < cnt; i++) {
                var cm = waterLevelStore.getAt(i);
                obsNm = cm.get("OBSNM");
                cSet += "<graph gid='" + (i + 1) + "'>";
                cSet += "<title><![CDATA[" + obsNm + "]]></title>";
                cSet += "<axis>left</axis>";
                cSet += "<color>" + CHColor[i] + "</color>"
                cSet += "<line_width>2</line_width>";
                //cSet += "<color_hover>CC0000</color_hover>";
                //cSet += "<balloon_text_color>000000</balloon_text_color>";
                cSet += "<balloon_text><![CDATA[<b>" + obsNm + " : {value}</b>]]></balloon_text>";
                cSet += "</graph>";
            }
            cSet += "</graphs></settings>";
        }

        flashMovie.setSettings(cSet);
    }

    var getChartData = function () {
        var tdata = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
        var sdata = ""; //series
        var gdata = ""; //graph
        tdata += "<chart>";
        var cnt = waterLevelStore.getCount();
        var SearchTP = Ext.getCmp("SearchType").getValue();
        var selDt = Ext.getCmp("selectDay").getValue(); //Ext.Date.parse(Ext.getCmp("selectDay").getValue()+" 00:00:00","Y-m-d H:i:s");

        var cm, cdt, cdd;
        if (cnt > 0) {
            for (var i = 0; i < cnt; i++) {
                cm = waterLevelStore.getAt(i);

                gdata += "<graph gid=\"" + (i + 1) + "\">";
                for (var j = 0; j < 144; j++) {
                    if (cm.get(SearchTP + "_" + j)) {
                        if (i == 0) {
                            if (j == 143) { //24:00일 경우
                                //cdd = Ext.Date.format(selDt, "Y-m-d") + " 24:00";
                                cdd = " 24:00";
                            } else {
                                cdt = Ext.Date.add(selDt, Ext.Date.MINUTE, (j + 1) * 10);
                                //cdd = Ext.Date.format(cdt, "Y-m-d H:i");
                                cdd = Ext.Date.format(cdt, "H:i");
                            }
                            sdata += "<value xid=\"" + j + "\">" + cdd + "</value>";
                        }
                        gdata += "<value xid=\"" + j + "\">" + cm.get(SearchTP + "_" + j) + "</value>";
                    } else {
                        break;
                    }

                }
                gdata += "</graph>";
            }

            searchFlag = 2;
        }
        tdata += "<series>" + sdata + "</series>";
        tdata += "<graphs>" + gdata + "</graphs>";
        tdata += "</chart>";
        //alert(tdata);
        return tdata;
    }

    var initChartPanel = function () {
        var chartWidth = '100%';
        var chartHeight = '100%';
        swfobject.embedSWF("/Scripts/amcharts/flash/amline.swf", "ChartViewPanel", chartWidth, chartHeight, "8.0.0", "/Scripts/amcharts/flash/expressInstall.swf", flashVars, params);
    }
    var loadChartPanel = function () {
        rebuildChartSettings();
        var data = getChartData();
        flashMovie.setData(data);
    }

    var dateTypes = [
        ['10', '10분'], ['30', '30분'], ['60', '60분']
    ];

    var SearchType = [
        ['WL', '수위'], ['FLW', '유량']
    ];

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
    }

    var damTypeStore = Ext.create('Ext.data.Store', {
        id: 'damTypeStore',
        model: 'damTypeModel',
        autoDestroy: true,
        autoLoad: false,
        autoSync: false,
        simpleSortMode: true,
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
    Ext.define('CodeModel', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'Key', type: 'string' },
            { name: 'Value', type: 'string' },
            { name: 'Ordernum', type: 'int' }
        ]
    });

    /* 댐코드 저장소 정의 */
    var damCodeStore = Ext.create('Ext.data.Store', {
        autoDestroy: true, model: 'CodeModel',
        proxy: {
            type: 'ajax',
            url: '/Common/DamCodeList',
            reader: { type: 'json', root: 'Data' }
        },
        sorters: [{ property: 'Ordernum', direction: 'ASC'}],
        autoLoad: false,
        listeners: {
            load: function (store, records, successful, operation) {
                if (records && records.length > 0) {
                    var damCd = records[0].data.Key;
                    Ext.getCmp('damcd').setValue(damCd);
                }
            }
        }
    });

    var searchForm = Ext.create('Ext.form.Panel', {
        bodyPadding: 0,
        frame: true,
        height: 45,
        bodyPadding: 5,
        layout: {
            type: 'table'
            , columns: 13
        },
        items: [{
            xtype: 'combobox',
            id: 'damType',
            name: 'damType',
            fieldLabel: '댐구분',
            labelWidth: 60,
            labelAlign: 'right',
            width: 160,
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
            xtype: 'combo'
            , id: 'damcd'
            , name: 'damcd'
			, fieldLabel: '댐명'
            , labelWidth: 50
            , labelAlign: 'right'
            , width: 180
            , displayField: 'Value'
            , valueField: 'Key'
			, store: damCodeStore
            , queryMode: 'local'
            , emptyText: '전체'
        }, {
            xtype: 'displayfield',
            width: 10
        }, {
            xtype: 'datefield'
                , name: 'selectDay'
                , fieldLabel: '일시'
                , labelWidth: 40
                , labelAlign: 'right'
                , id: 'selectDay'
                , width: 140
                , format: 'Y-m-d'
        }, {
            xtype: 'displayfield',
            width: 10
        }, {
            xtype: 'displayfield'
        }, {
            xtype: 'combo'
            , id: 'dataTp'
            , name: 'dataTp'
			, fieldLabel: '구분'
            , labelWidth: 40
            , labelAlign: 'right'
            , width: 120
			, store: dateTypes
            , queryMode: 'local'
            , value: '10'
            , listeners: {
                change: function (field, newValue, oldValue, options) {
                    var searchTp = Ext.getCmp("SearchType").getValue();
                    reconfigureGrid(newValue, searchTp);
                }
            }
        }, {
            xtype: 'displayfield',
            width: 10
        }, {
            xtype: 'combo'
            , id: 'SearchType'
            , name: 'SearchType'
			, fieldLabel: '검색구분'
            , labelWidth: 70
            , labelAlign: 'right'
            , width: 160
			, store: SearchType
            , queryMode: 'local'
            , value: 'WL'
                , listeners: {
                    change: function (field, newValue, oldValue, options) {
                        if (newValue == 'WL') {
                            WLVL = false;
                            FLWVL = true;
                        } else {
                            WLVL = true;
                            FLWVL = false;
                        }
                        var dateTp = Ext.getCmp("dataTp").getValue();
                        reconfigureGrid(dateTp, newValue);
                    }
                }
        }, {
            xtype: 'displayfield',
            width: 80
        }, {
            xtype: 'button'
            , name: 'submit2'
            , text: '<span style="font-weight: bold">엑셀</span>'
            , icon: '/Images/icons/document-excel-table.png'
            , width: 80
            , height: 24
            , formBind: true
            , handler: function () {
                run(2);
            }
        }, {
            xtype: 'displayfield',
            width: 5
        }, {
            xtype: 'button'
            , name: 'submit1'
            , text: '<span style="font-weight: bold">조 회</span>'
            , icon: '/Images/icons/magnifier.png'
            , width: 80
            , height: 24
            , handler: function () {
                run(1);
            }
        }]
    });

    var run = function (flag) {
        var fp = searchForm.getForm();
        var damType = fp.findField("damType").getValue();
        var damCd = fp.findField("damcd").getValue();
        //var obsCd = fp.findField("obscd").getValue();
        var selectDay = Ext.util.Format.date(fp.findField("selectDay").getValue(), 'Ymd');
        var dataTp = fp.findField("dataTp").getValue();
        var SearchType = fp.findField("SearchType").getValue();

        if (flag == 1) {
            waterLevelStore.load({
                params: {
                    damType: damType,
                    damCd: damCd,
                    selectDay: selectDay,
                    dataTp: dataTp,
                    SearchType: SearchType
                }
            });
        } else if (flag == 2) {
            try {
                if (grid.getStore().getCount() == 0) { throw new Exception(); }
            } catch (e) {
                Ext.Msg.alert('Message', '먼저 조회를 하셔야 합니다.')
                return false;
            }
            var url = '/DataSearch/GetWaterlevelVerifySearchExcel?';
            var params = {
                damType: damType,
                damCd: damCd,
                selectDay: selectDay,
                dataTp: dataTp,
                SearchType: SearchType
            };
            document.location = url + Ext.urlEncode(params);

        }
    }
    //최초 10분 store, 수위컬럼으로 매핑
    var waterLevelColumns = [];
    var waterLevelFields = [];

    function makeFields(dateTp, searchTp) {
        var fields = [
         { "name": "ID", type: 'string' },
         { "name": "DAMCD", type: 'string' },
         { "name": "DAMNM", type: 'string' },
         { "name": "OBSCD", type: 'string' },
         { "name": "OBSNM", type: 'string' }
        ];
        var len = fields.length, timelen;
        if (dateTp == "30") {
            timelen = 48;
        } else if (dateTp == "60") {
            timelen = 24;
        } else {
            timelen = 144;
        }
        for (var i = 0; i < timelen; i++) {
            fields[len + i] = { "name": "WL_" + i, type: 'string', hidden: WLVL };
            fields[len + timelen + i] = { "name": "FLW_" + i, type: 'string', hidden: FLWVL };
        };

        waterLevelFields = fields;
        waterLevelModel = Ext.define('waterLevelModel', {
            extend: 'Ext.data.Model',
            fields: waterLevelFields,
            idProperty: 'ID'
        });
    }
    makeFields('10', 'WL');
    var waterLevelModel = Ext.define('waterLevelModel', {
        extend: 'Ext.data.Model',
        fields: waterLevelFields,
        idProperty: 'ID'
    });

    var waterLevelProxy = {
        type: 'ajax',
        url: '/DataSearch/GetWaterlevelVerifySeachList',
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

    var waterLevelStore = Ext.create("Ext.data.Store", {
        id: 'waterLevelStore',
        autoDestroy: true,
        model: 'waterLevelModel',
        proxy: waterLevelProxy,
        autoLoad: false,
        pageSize: pageSize,
        purgePageCount: 0,
        buffered: true,
        listeners: {
            load: function (store, records, successful, operation, options) {

                if (waterLevelStore.getTotalCount() == 0) {
                    Ext.Msg.alert('Message', '조회 결과가 없습니다.');
                    return;
                }
                loadChartPanel(store);
            }
        }
    });
    //오늘 날짜 00:00:00
    var wlDate = Ext.Date.parse(Ext.Date.format(new Date(), 'Ymd') + "000000", 'YmdHis');

    function makeColumns(dateTp, searchTp) {
        var columns = [
            { text: '관측국명', width: 100, sortable: true, align: 'center', dataIndex: 'OBSNM', locked: true },
            { text: '관측국코드', width: 70, sortable: true, align: 'center', dataIndex: 'OBSCD', locked: true }
        ];
        var len = columns.length, cd;

        var addtime, timelen;
        if (dateTp == "30") {
            addtime = 30, timelen = 48;
        } else if (dateTp == "60") {
            addtime = 60, timelen = 24;
        } else {
            addtime = 10, timelen = 144;
        }
        for (var i = 0; i < timelen; i++) {
            cd = Ext.Date.format(Ext.Date.add(wlDate, Ext.Date.MINUTE, (i + 1) * addtime), 'H:i');
            if (cd == "00:00") cd = "24:00";
            //columns[len + i] = { text: cd, width: 40, sortable: false, align: 'center', dataIndex: searchTp + '_' + i};
            columns[len + i] = { text: cd, width: 40, sortable: false, align: 'center', dataIndex: 'WL_' + i, hidden: WLVL };
            columns[len + timelen + i] = { text: cd, width: 40, sortable: false, align: 'center', dataIndex: 'FLW_' + i, hidden: FLWVL };
        }
        waterLevelColumns = columns;
    }
    //10분+수위 컬럼 생성
    makeColumns('10', 'WL');

    function reconfigureGrid(dateTp, searchTp) {
        makeFields(dateTp, searchTp);
        makeColumns(dateTp, searchTp);
        mainGrid.reconfigure(waterLevelStore, waterLevelColumns);
    }
    var mainGrid = Ext.create("Ext.grid.Panel", {
        id: 'wlDataGrid',
        region: 'center',
        autoScroll: true,
        autoWidth: true,
        autoHeight: true,
        overCls: 'grid-over-cls',
        store: waterLevelStore,
        loadMask: true,
        viewConfig: {
            listeners: {
                celldblclick: function (gg, rowIndex, colIndex, rec) {
                    var fp = searchForm.getForm();
                    var damType = fp.findField("damType").getValue();
                    var damCd = fp.findField("damcd").getValue();
                    var selectDay = Ext.util.Format.date(fp.findField("selectDay").getValue(), 'Ymd');
                    var dataTp = fp.findField("dataTp").getValue();
                    var obsCd = rec.get("OBSCD");
                    var url = "/Verify/WaterLevelVerify";
                    var params = '?DAMCD=' + damCd + '&OBSCD=' + obsCd + '&OBSDT=' + selectDay + '&DAMTYPE=' + damType + "&DATATP=" + dataTp;
                    //alert(url + params);
                    jQuery.popupWindow2({
                        width: 1000,
                        height: 700,
                        windowURL: url + params,
                        centerScreen: 1,
                        resizable: 1
                    });
                }
            }
        },
        columns: waterLevelColumns,
        columnLines: true
    });

    var mainForm = Ext.create('Ext.Viewport', {
        renderTo: Ext.getBody(),
        layout: {
            type: 'border'
            , padding: 0
            , border: 0
        },
        items: [{
            region: 'north',
            height: 90,
            items: [{
                height: 45,
                border: 0,
                padding: '10 20 5 5',
                contentEl: 'menu-title'
            }, searchForm]
        }, mainGrid,
        {
            id: 'chart-panel',
            frame: true,
            region: 'south',
            collapsible: false,
            split: true,
            flex: 2.5,
            bodyStyle: {
                background: '#fff',
                padding: '0px'
            },
            contentEl: 'ChartViewPanel',
            tbar: [{
                xtype: 'displayfield',
                value: '<img src="/Images/icons/chart--arrow.png" align="top">&nbsp;<b>수위자료 그래프</b>',
                width: 200
            }, '->', {
                xtype: 'button',
                text: '<b>이미지저장</b>',
                icon: '/Images/icons/image--pencil.png',
                handler: function () {
                    if (waterLevelStore.getTotalCount() > 0) {
                        flashMovie.exportImage("/Verify/ExportWaterlevelChartImage");
                    } else {
                        alert("그래프데이터가 존재하지 않습니다.");
                        return;
                    }
                }
            }]
        }]
    });
    //mainGrid.render("MainGridPanel");
    //Chart Loading

    Ext.getCmp('selectDay').setValue(currDate);
    initChartPanel();
});