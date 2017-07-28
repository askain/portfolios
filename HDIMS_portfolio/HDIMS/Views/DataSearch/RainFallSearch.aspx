<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	이상상태 이력조회(우량)
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PreHeadContent" runat="server">
<script type="text/javascript" src="<%=Page.ResolveUrl("~/Scripts")%>/common/renderers.js"></script>
<script type="text/javascript" src="<%=Page.ResolveUrl("~/Scripts") %>/common/rainfallModule.js"></script>
<script type="text/javascript" src="<%=Page.ResolveUrl("~/Scripts") %>/swfobject.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<% 
    string DAMCD = (string)ViewData["DAMCD"];
    string DAMTYPE = (string)ViewData["DAMTYPE"];
    string OBSCD = (string)ViewData["OBSCD"];
    string OBSDT = (string)ViewData["OBSDT"];
    string EXCD = (string)ViewData["EXCD"];
%>
<script type="text/javascript">
    var currDate = new Date();
    var currHour = currDate.getHours();
    var prevDate = Ext.Date.add(currDate, Ext.Date.HOUR, -2);
    var prevHour = prevDate.getHours();

    var pageSize = <%=ViewData["PAGE_SIZE"] %>;
    var loadCount = 0;
    var rainFallStore;
    var searchFlag = 0;
    var params = {
        bgcolor:"#FFFFFF"
    };

    var parm_damcd, parm_obscd, parm_searchtype, parm_startdt, parm_enddt, parm_excd, parm_etccd, parm_exempno, parm_datatp;
    var listCount = 0; // 조회를 하지 않았을경우 변수 또는 조회 결과가 없을경우 0 , 아닐때 1
    var obsEmpty = 0; // 관측국 전체 선택 0, 아닐때 1

    var R_DAMCD = '<%=DAMCD %>';
    var R_DAMTYPE = '<%=DAMTYPE %>';
    var R_OBSCD = '<%=OBSCD %>';
    var R_OBSDT = '<%=OBSDT %>';
    var R_EXCD = '<%=EXCD %>';

    if (R_OBSDT.length > 0) {
        currDate = R_OBSDT.substring(0, 10);
        currHour = R_OBSDT.substring(11, 13);
        prevDate = R_OBSDT.substring(0, 10);
        prevHour = R_OBSDT.substring(11, 13);
    }

    
	var flashVars = {
		path: "<%=Page.ResolveUrl("~/Scripts") %>/amcharts/flash/",
		settings_file: "<%=Page.ResolveUrl("~/Config") %>/Chart/VerifyRainFallSettings.xml",
        data_file: "<%=Page.ResolveUrl("~/Config") %>/Chart/VerifyRainFallData.xml",
		chart_id:"ChartViewPanel"
	};
    //챠트 초기화 완료
    function amChartInited(chart_id){
        flashMovie = document.getElementById(chart_id);
    }  
    function amProcessCompleted(chart_id, process_name) {
        //alert(process_name);
        if(process_name=="setData") {
            searchFlag=3;
        }
    }
    //챠트 x축 날짜범위 변경시 이벤트
    function amGetZoom(chart_id, fromDT, toDT, from_xid, to_xid){

        if(searchFlag==3) {
            var from = Ext.Date.format(Ext.Date.parse(fromDT,"Y-m-d H:i"),"YmdHi");
            var to = Ext.Date.format(Ext.Date.parse(toDT,"Y-m-d H:i"),"YmdHi");
            rainFallStore.filterBy(
                function(rec,id) {  
                    return rec.data.OBSDT >= from && rec.data.OBSDT <= to;
                }
            );
        } 
    }

    Ext.onReady(function () {
        Ext.BLANK_IMAGE_URL = '/Scripts/extjs/s.gif';
        
        var getChartData = function() {
            var data = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            var sdata = "<series>";
            var gdata1 = "<graph gid=\"1\">";
            var gdata2 = "<graph gid=\"2\">";
            var gdata3 = "<graph gid=\"3\">";
            data += "<chart>";
            var cnt = rainFallStore.getCount();
            if( cnt > 0) { 
                var j=0;
                for(var i=cnt-1; i>=0; i--) {
                    var cm = rainFallStore.getAt(i);
                    sdata  += "<value xid=\""+i+"\">"+Ext.Date.format(Ext.Date.parse(cm.get("OBSDT"),"YmdHi"),"Y-m-d H:i")+"</value>";
                    gdata1 += "<value xid=\""+j+"\" description=\"(mm)\">"+cm.get("RF")+"</value>";
                    gdata2 += "<value xid=\""+j+"\" description=\"(mm)\">"+cm.get("ACURF")+"</value>";
                    gdata3 += "<value xid=\""+j+"\" description=\"(mm)\">"+cm.get("EXVL")+"</value>";
                    j++;
                } 
                data += sdata + "</series>";
                data += "<graphs>";
                data += gdata1 + "</graph>";
                data += gdata2 + "</graph>";
                data += gdata3 + "</graph>";
                data += "</graphs>";
                searchFlag = 2;
            } else {
                //series,value//현재시작날짜와 끝날짜로 처리한다.
                var CDHM = Ext.Date.format(currDate,"YmdHi");
                //series,value
                data += "<series>";
                data += "<value xid=\"0\">"+CDHM+"</value>";
                data += "</series>";
                //graphs,graph, value
                data += "<graphs>";
                data += "<graph gid=\"1\">";
                data += "<value xid=\"0\" description=\"(mm)\"></value>";
                data += "</graph>";
                data += "<graph gid=\"2\">";
                data += "<value xid=\"0\" description=\"(mm)\"></value>";
                data += "</graph>";
                data += "<graph gid=\"3\">";
                data += "<value xid=\"0\" description=\"(mm)\"></value>";
                data += "</graph>";
                data += "</graphs>";
                searchFlag = 1;
            }
            data += "</chart>";
            return data;
        }

        var initChartPanel = function() {
            var chartWidth = '100%';
            var chartHeight = '100%';
            swfobject.embedSWF("<%=Page.ResolveUrl("~/Scripts") %>/amcharts/flash/amline.swf", "ChartViewPanel", chartWidth, chartHeight, "8.0.0", "<%=Page.ResolveUrl("~/Scripts") %>/amcharts/flash/expressInstall.swf", flashVars, params);
        }
        var loadChartPanel = function () {
            //max, min 다시잡기
            var maxY = rainFallStore.max("RF");
            var minY = rainFallStore.min("RF");
            var maxY2 = rainFallStore.max("ACURF");
            var minY2 = rainFallStore.min("ACURF");
            var maxY3 = rainFallStore.max("EXVL");
            var minY3 = rainFallStore.min("EXVL");

            if(maxY3 > maxY2) maxY2 = maxY3;
            if(minY3 < minY2) minY2 = minY3;

            var minMax1 = getChartRange(maxY, minY);
            var minMax2 = getChartRange(maxY2, minY2);
            

            //alert(minMax1.min + "," + minMax1.max + "," + minMax2.min + "," + minMax2.max);
            flashMovie.setParam("grid.x.approx_count",5);
            flashMovie.setParam("values.y_left.max",minMax1.max);
            flashMovie.setParam("values.y_left.min",minMax1.min);
            flashMovie.setParam("values.y_right.max",minMax2.max);
            flashMovie.setParam("values.y_right.min",minMax2.min);
            var data = getChartData();
            flashMovie.setData(data);
        }

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

        var run = function(flag) {
            
            var DamCd = Ext.getCmp("DamNm").getValue();
            var ObsCd = Ext.getCmp("ObsNm").getValue();
            var SearchType = Ext.getCmp("SearchType").getValue();
            var StartDt = Ext.util.Format.date(Ext.getCmp("StartDt").getValue(), 'Ymd');
            var StartHt = Ext.util.Format.date(Ext.getCmp("StartHt").getValue(), 'H');
            var EndDt = Ext.util.Format.date(Ext.getCmp("EndDt").getValue(), 'Ymd');
            var EndHt = Ext.util.Format.date(Ext.getCmp("EndHt").getValue(), 'H');
            var ExCd = Ext.getCmp("ExCd").getValue();
            var EtcCd = Ext.getCmp("EtcCd").getValue();
            var ExEmpno = Ext.getCmp("EmpNo").getValue();
            var DataTp = Ext.getCmp("DataTp").getValue();

            //년월일 00시 부터 23시59분까지 설정
            if (DataTp == "10" || DataTp == "30") {
                StartDt = StartDt + StartHt + "00";
                EndDt = EndDt + EndHt + "59";
            } else {
                StartDt += StartHt;
                EndDt += EndHt;
            };

            parm_damcd = DamCd ? DamCd : "";
            parm_obscd = ObsCd ? ObsCd : "";
            parm_searchtype = SearchType ? SearchType : "";
            parm_startdt = StartDt ? StartDt : "";
            parm_enddt = EndDt ? EndDt : "";
            parm_excd = ExCd ? ExCd : "";
            parm_etccd = EtcCd ? EtcCd : "";
            parm_exempno = ExEmpno ? ExEmpno : "";
            parm_datatp = DataTp ? DataTp : "";         
               
            if(flag == 1) {
                rainFallStore.proxy.extraParams =  {
                    DamCd: DamCd,
                    ObsCd: ObsCd,
                    SearchType: SearchType,
                    StartDt: StartDt,
                    EndDt: EndDt,
                    ExCd: ExCd,
                    EtcCd: EtcCd,
                    ExEmpno: ExEmpno,
                    DataTp: DataTp
                };
                searchFlag = 0;
                rainFallStore.load();
            } else if(flag == 2) {
                try {
                    if (rfDataGrid.getStore().getCount() == 0) { throw new Exception(); }
                } catch(e) {
                    Ext.Msg.alert('Message', "먼저 조회를 하셔야 합니다.");
                    return false;
                }
                var url = '/DataSearch/GetRainFallSearchExcel?';
                var params = {
                    DamCd: DamCd,
                    ObsCd: ObsCd,
                    SearchType: SearchType,
                    StartDt: StartDt,
                    EndDt: EndDt,
                    ExCd: ExCd,
                    EtcCd: EtcCd,
                    ExEmpno: ExEmpno,
                    DataTp: DataTp
                };
                document.location = url + Ext.urlEncode(params);;
                
            }

        };

        ///////////////////*************************** 상단 조회 Panel 종료  ***************************///////////////////
        
        
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
            url: '<%=Page.ResolveUrl("~/DamBoObsMng")%>/GetDamType',
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

        //댐구분 Store
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
                        if (R_DAMCD.length > 0) {
                            Ext.getCmp('damType').setValue(R_DAMTYPE);
                        } else {
                            Ext.getCmp('damType').setValue(records[0].data.DAMTYPE);
                        }
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


        //품질등급 저장소 정의 */
        var exCodeStore = Ext.create('Ext.data.Store', {
            autoDestroy: true, model: 'Common.ExCodeModel',
            proxy: {
                type: 'ajax',
                url: '/Common/GetExCodeList/?ExTp=W',
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
            sorters: [{ property: 'EXORD', direction: 'ASC'}],
            autoLoad: true
        });

        //기타범례 저장소 정의 */
        var etcCodeStore = Ext.create('Ext.data.Store', {
            autoDestroy: true, model: 'Common.EtcCodeModel',
            proxy: {
                type: 'ajax',
                url: '/Common/GetEtcCodeList/?EtcTp=W',
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
            sorters: [{ property: 'ETCCD', direction: 'ASC'}],
            autoLoad: true
        });

        //사용자정보 저장소 정의 */
        var userInfoStore = Ext.create('Ext.data.Store', {
            autoDestroy: true, model: 'Common.UserInfo',
            proxy: {
                type: 'ajax',
                url: '/Common/GetUserInfoList',
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
            sorters: [{ property: 'EMPNM', direction: 'ASC'}],
            autoLoad: true
        });



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
            autoDestroy: true,
            model: 'CodeMode',
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
                        var damcd = records[0].data.KEY;
                        if (R_DAMCD.length > 0) {
                            damcd = R_DAMCD;
                            Ext.getCmp('DamNm').setValue();
                        } 
                        Ext.getCmp('DamNm').setValue(damcd);
                        obsCodeStore.load({
                            params: {
                                DamCode: damcd,
                                Tp: 'rfSearch'
                            }
                        });
                    }
                }
            },
            sorters: [{ property: 'VALUE', direction: 'ASC'}],
            autoLoad: false
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
            sorters: [{ property: 'OBSNM', direction: 'ASC'}],
            autoLoad: false,
            listeners: {
                load: function (store, records, successful, operation) {
                    if (records && records.length > 0) {
                        if (R_OBSCD.length > 0) {                            
                            Ext.getCmp('ObsNm').setValue(R_OBSCD);
                            run(1);
                        } else {
                            Ext.getCmp('ObsNm').setValue(records[0].data.KEY);
                        }
                    }
                }
            }
        });



        var rfDataProxy = Ext.create('Ext.data.proxy.Ajax', {
            type: 'ajax',
            url: '<%=Page.ResolveUrl("~/DataSearch")%>/GetRainFallSearchList',
            reader: { type: 'json', totalProperty: 'totalCount', root: 'Data' },
            pageParam: 'page',
            simpleSortMode: true,
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

        /* 리스트 목록 저장소 정의 */
        rainFallStore = Ext.create('Ext.data.Store', {
            id: 'rainFallStore',
            autoDestroy: true,
            model: 'RainFallModel',
            proxy: rfDataProxy,
            autoLoad: false,
            pageSize: pageSize,
            purgePageCount: 0,
            buffered: true,
            listeners: {
                load: function (store, records, successful, operation, options) {
                    
                    if(rainFallStore.getTotalCount() == 0)
                    {
                        Ext.Msg.alert('Message', '조회 결과가 없습니다.'); 
                        return;
                    }
                    loadChartPanel(store);
                }
            }
        });



         function popupChart(damcd, obscd) {
            jQuery.popupWindow2({
                width: 900,
                height: 610,
                windowURL: '/chart/DataSearch/RF_Chart.aspx?DamCd=' + damcd + '&ObsCd=' + obscd + '&SearchType=' + parm_searchtype + '&StartDt=' + parm_startdt + '&EndDt=' + parm_enddt + '&ExCd=' + parm_excd + '&EtcCd=' + parm_etccd + '&ExEmpno=' + parm_exempno + '&DataTp=' + parm_datatp,
                centerScreen: 1,
                resizable: 0,
                scrollbars: 2
            });
        }

        var dataTypes = [
                        ['10','10분'],['30','30분'],['60','60분']
                    ];

       var searchPanel = Ext.create('Ext.form.Panel', {
            bodyPadding: 0,
            frame: true,
            height: 100,
            layout: {
                type: 'table',
                columns: 10
            },
            items: [{
                xtype: 'combobox',
                id: 'damType',
                name: 'damType',
                fieldLabel: '댐구분',
                labelWidth: 60,
                labelAlign: 'right',
                width: 190,
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
                xtype: 'combobox',
                id: 'DamNm',
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
                                Tp: 'rfSearch'
                            }
                        });
                    }
                }
            }, {
                xtype: 'combobox',
                id: 'ObsNm',
                fieldLabel: '관측국',
                labelWidth: 60,
                labelAlign: 'right',
                width: 190,
                displayField: 'VALUE',
                valueField: 'KEY',
                store: obsCodeStore,
                queryMode: 'local',
                listeners: {
                    change: function (field, newValue, oldValue, options) {
                        if(newValue == "")
                        {
                            obsEmpty = 0;
                        }
                        else
                        {
                            obsEmpty = 1; // 전체 이외의 다른 관측국 선택시
                        }
                    }
                }
            }, {
                xtype: 'combobox',
                id: 'SearchType',
                fieldLabel: '일시',
                labelWidth: 60,
                labelAlign: 'right',
                width: 140,
                value: 'obdt',
                store: [
                    ['obdt', '측정일시'],
                    ['vrdt', '보정일시']
                ]
            },{
                xtype: 'displayfield',                
                value: '',
                width: 10
            }, {
                xtype: 'datefield',
                name: 'StartDt',
                id: 'StartDt',
                width: 87,
                value: currDate,
                maxValue: currDate,
                format: 'Y-m-d'
            }, {
                xtype: 'timefield',
                name: 'StartHt',
                id: 'StartHt',
                width: 37,
                value: prevHour,
                increment: 60,
                format: 'H'
            }, {
                xtype: 'displayfield',                
                value: '~',
                width: 10
            }, {
                xtype: 'datefield',
                name: 'EndDt',
                id: 'EndDt',
                width: 87,
                value: currDate,
                maxValue: currDate,
                format: 'Y-m-d',
                align: 'right'
            }, {
                xtype: 'timefield',
                name: 'EndHt',
                id: 'EndHt',
                width: 37,
                value: currHour,
                increment: 60,
                format: 'H'
            }, {
                xtype: 'combobox',
                id: 'ExCd',
                name: 'ExCd',
                flex: 1,
                fieldLabel: '품질등급',
                labelWidth: 60,
                labelAlign: 'right',
                width: 190,
                emptyText: '전체',
                displayField: 'EXCD',
                valueField: 'EXCD',
                queryMode: 'local',
                store: exCodeStore
            }, {
                xtype: 'combobox',
                id: 'EtcCd',
                name: 'EtcCd',
                fieldLabel: '기타범례',
                labelWidth: 60,
                labelAlign: 'right',
                width: 190,
                emptyText: '전체',
                displayField: 'ETCTITLE',
                valueField: 'ETCCD',
                queryMode: 'local',
                store: etcCodeStore
            }, {
                xtype: 'combobox',
                id: 'EmpNo',
                name: 'EmpNo',
                flex: 1,
                fieldLabel: '사용자',
                labelWidth: 60,
                labelAlign: 'right',
                width: 190,
                queryMode: 'local',
                emptyText: '전체',
                displayField: 'EMPNM',
                valueField: 'EMPNO',
                store: userInfoStore
            }, {
                xtype: 'combo'
                , id: 'DataTp'
                , name: 'DataTp'
                , flex: 1
				, fieldLabel: '구분'
                , labelWidth: 60
                , labelAlign: 'right'
                , width: 140
				, store: dataTypes
                , queryMode: 'local'
                , value: '10'
            }, {
                xtype: 'displayfield',                
                value: '',
                width: 10
            }, {
                xtype: 'button'
                , name: 'submit1'
                , text: '<span style="font-weight: bold">엑셀</span>'
                , icon: '<%=Page.ResolveUrl("~/Images") %>/icons/document-excel-table.png'
                , width: 70
                , height: 24
                , formBind: true
                , handler: function () {
                    run(2);
                }
            },{
                xtype: 'button',
                id: 'btnSearch',
                name: 'btnSearch',
                text: '<span style="font-weight: bold">조 회</span>',
                colspan: 3,
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/magnifier.png',
                width: 70,
                handler: function() {run(1);}
            }]
        });

        var rfDataGrid = Ext.create('Ext.grid.Panel', {
            id: 'rfDataGrid',
            region: 'center',
            autoWidth: true,
            autoScroll: true,
            stateful: false,
            flex: 1,
            store: rainFallStore,
            verticalScroller: {
                xtype: 'paginggridscroller',
                activePrefetch: false
            },
            loadMask: true,
            disableSelection: false,
            invalidateScrollerOnRefresh: false,
            viewConfig: {
                trackOver: false,
                listeners: {
                    itemdblclick: function(view, record, item, index, e) {
                        //var damcd = record.get("DAMCD");
                        var Obscd = record.get("OBSCD");
                        var Obsdt = record.get("OBSDT").split('-').join('').split(' ').join('').split(':').join('');
                        rfDataHistoryStore.load({
                            params:{
                                obscd: Obscd,
                                obsdt: Obsdt
                            }
                        }); //load
                        ToggleHistoryWindow();
                    }//itemdblclick
                }//listeners
            },
            columns: rfDataColumns,
            columnLines: true
        });


//변경이력보기 기능 시작////////////////////////////////////////////////////////////////////////////////
        var rfDataHistoryProxy = Ext.create('Ext.data.proxy.Ajax', {
            type: 'ajax',
            url: '<%=Page.ResolveUrl("~/DataSearch")%>/GetRainFallHistoryList',
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

        var rfDataHistoryStore = Ext.create('Ext.data.Store', {
            autoDestroy: true,
            model: 'RFDataHistoryModel',
            proxy: rfDataHistoryProxy,
            autoLoad: false,
            listeners: {
                load: function (store, records, successful, operation, options) {
//                    if(records.length == 0)
//                    {
//                        Ext.Msg.alert('Message', '조회 결과가 없습니다.'); 
//                        return;
//                    }
                }
            }
        });

        var rfDataHistoryGrid = Ext.create('Ext.grid.Panel', {
            region: 'center',
            store: rfDataHistoryStore,
            columns: rfDataHistoryColumns
        });

        var win;
        function ToggleHistoryWindow() {
            if(!win)    {
                win = Ext.create('widget.window', {
                    title: '변경이력 조회',
                    closable: true,
                    closeAction: 'hide',
                    //animateTarget: this,
                    width: 1200,
                    height: 250,
                    layout: 'border',
                    bodyStyle: 'padding: 5px;',
                    items: [rfDataHistoryGrid]
                });
            } // if

            if (win.isVisible()) {
                win.hide();
            } else {
                win.show();
            }
        }
//변경이력보기 기능 끝////////////////////////////////////////////////////////////////////////////////


        var mainForm = Ext.create('Ext.Viewport', {
            layout: {
                type: 'border'
                , padding: 0
            },
            border: 0,
            renderTo: Ext.getBody(), // 랜더링이 되는곳 정의,
            items: [{
                region: 'north',
                height: 110, 
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
                }, searchPanel]
            }, 
            rfDataGrid,
            {
                id : 'chart-panel',
                frame: true,
                region: 'south',
                flex:0.7,
                collapsible: false,
                split: true,
                contentEl: 'ChartViewPanel',
                tbar: [{
                    xtype: 'displayfield',
                    value: '<img src="<%=Page.ResolveUrl("~/Images") %>/icons/chart--arrow.png" align="top">&nbsp;<b>우량자료 그래프</b>',
                    width: 200
                },'->',{
                    xtype: 'button',
                    text: '<b>이미지저장</b>',
                    icon: '<%=Page.ResolveUrl("~/Images") %>/icons/image--pencil.png',
                    handler: function() {
                        if(rainFallStore.getCount() > 0) {
                            flashMovie.exportImage("/Verify/ExportRainfallChartImage");
                        } else {
                            alert("그래프데이터가 존재하지 않습니다.");
                            return;
                        }
                    }
                }]
            }]
        });


        initChartPanel();

    });

</script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<div id="menu-title" class="title_box x-hide-display" align="center" style="text-align:left;">
	<span style="font-weight:bold; margin-left:10px;">
    <img src="<%=Page.ResolveUrl("/Images") %>/icons/notebook--exclamation.png" align="absmiddle"/>&nbsp;&nbsp;
    우량자료 이력조회
    </span>
</div>
<div id="ChartViewPanel" class="x-hide-display"></div>
</asp:Content>
