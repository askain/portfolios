<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	수위자료 보고서
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript" src="<%=Page.ResolveUrl("~/Scripts") %>/Report/ReportCommon.js"></script>
<%
    string mrdpath = "";
    string param = "/rfn [http://rdserver.kowaco.or.kr:8080/RDServer/rdagent.jsp] /rdn [HDIMS]";
 %>
<style type="text/css">
    .x-panel-framed {
        padding: 0;
    }
</style>
<script type="text/javascript">
    var currDate = new Date();
    //var prevDate = new Date();
    Ext.onReady(function () {
        Ext.BLANK_IMAGE_URL = '/Scripts/extjs/s.gif';
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

        /** DUBMMRF(TRMDV=10,30), DUBHRRF (10분자료,30분자료,시간자료)
        1. 댐코드,댐명,관측국코드, 관측국명,측정일시,원시자료[누가우량,시간우량,등급],추정치(누가우량,우량],기타범례
        확인자, 확인일시, 보정자, 보정일시
        2. Damcd, Damnm, Obscd, Obsnm,Obsdt,OrigAcurf,OrigRf,OrigQlvl,UdtAcurf,UdtRf,EtcExam
        ,ChkUserId,ChkUserNm,ChkDt, EdUserId,EdUserNm,EdDt
        3. DUBMMRF 의 EXEDLVL(검보정레벨) 을 어떻게 처리할까? 댐운영시스템(DDIS)와 호환성 고려할 것.
        */


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
                url: '<%=Page.ResolveUrl("~/Common")%>/DamCodeList',
                reader: { type: 'json', root: 'Data' }
            },
            autoLoad: true,
            listeners: {
                load: function (store, records, successful, operation) {
                    if (records && records.length > 0) {
                        var damcd = records[0].data.KEY;
                        Ext.getCmp('damcd').setValue(damcd);
                    }
                }
            }
        });
        /* 관측국 저장소 정의 */
        var obsCodeStore = Ext.create('Ext.data.Store', {
            autoDestroy: true, model: 'CodeMode',
            proxy: {
                type: 'ajax',
                url: '<%=Page.ResolveUrl("~/Common")%>/ObsCodeList/?type=wlSearch',
                reader: { type: 'json', root: 'Data' }
            },
            autoLoad: true,
            listeners: {
                load: function (store, records, successful, operation) {
                    if (records && records.length > 0) {
                        var obscd = records[0].data.KEY;
                        Ext.getCmp('obscd').setValue(obscd);
                    }
                }
            }
        });
        //품질등급 저장소 정의 */
        var exCodeStore = Ext.create('Ext.data.Store', {
            autoDestroy: true, model: 'Common.ExCodeModel',
            proxy: {
                type: 'ajax',
                url: encodeURI('<%=Page.ResolveUrl("~/Common")%>/GetExCodeList/?ExTp=W&firstvalue=전체'),
                reader: { type: 'json', root: 'Data' }
            },
            sorters: [{ property: 'EXORD', direction: 'ASC'}],
            autoLoad: true,
            listeners: {
                load: function (store, records, successful, operation) {
                    if (records && records.length > 0) {
                        var excd = records[0].data.EXCD;
                        Ext.getCmp('excd').setValue(excd);
                    }
                }
            }
        });
        var dateTypes = [
            ['D', '일별'],
            ['M', '월별'],
        /* ['Q', '분기'],*/
            ['Y', '년별']
        ];

        var searchForm = Ext.create('Ext.form.Panel', {
            url: '<%=Page.ResolveUrl("") %>/Statistics/WaterLevelVerify',
            bodyPadding: 0,
            frame: true,
            height: 100,
            bodyPadding: 5,
            layout: {
                type: 'table'
                , columns: 7
            },
            items: [{
                xtype: 'combo'
                    , id: 'damcd'
                    , name: 'damcd'
				    , fieldLabel: '댐명'
                    , labelWidth: 60
                    , labelAlign: 'right'
                    , width: 190
                    , displayField: 'VALUE'
                    , valueField: 'KEY'
				    , store: damCodeStore
                    , queryMode: 'local'
                    , multiSelect: true
                    , forceSelection: true
                    , multiSelect: true
                    , listeners: {
                        change: function (field, newValue, oldValue, options) { //[object Object], 1003110 - 현재선택 코드, 1012110 - 이전선택 코드, [object Object]
                            obsCodeStore.proxy.url = '<%=Page.ResolveUrl("~/Common")%>/ObsCodeList/?DamCode=' + newValue + '&type=wlSearch';
                            obsCodeStore.load();
                        },
                        expand: function (field, eOpts) {
                            ExpandOverActiveX(field, eOpts);
                        },
                        collapse: function () {
                            Ext.get("myframe").hide();
                        }
                    }
            }, {
                xtype: 'combo'
                    , id: 'obscd'
                    , name: 'obscd'
				    , fieldLabel: '관측국'
                    , labelWidth: 60
                    , labelAlign: 'right'
                    , width: 190
                    , displayField: 'VALUE'
                    , valueField: 'KEY'
				    , store: obsCodeStore
                    , queryMode: 'local'
                    , multiSelect: true
                    , forceSelection: true
                    , listeners: {
                        expand: function (field, eOpts) {
                            ExpandOverActiveX(field, eOpts);
                        },
                        collapse: function () {
                            Ext.get("myframe").hide();
                        }
                    }
            }, {
                xtype: 'datefield'
                    , name: 'startdt'
                    , fieldLabel: '검색기간'
                    , id: 'startdt'
                    , labelWidth: 60
                    , labelAlign: 'right'
                    , width: 190
                    , vtype: 'daterange'
                    , endDateField: 'enddt'
                    , format: 'Y-m-d'
                    , listeners: {
                        expand: function (field, eOpts) {
                            ExpandOverActiveX(field, eOpts);
                        },
                        collapse: function () {
                            Ext.get("myframe").hide();
                        }
                    }
            }, {
                xtype: 'displayfield'
                    , name: 'dispfld01'
                    , value: '~'
                    , width: 10
            }, {
                xtype: 'datefield'
                    , name: 'enddt'
                    , id: 'enddt'
                    , width: 120
                    , vtype: 'daterange'
                    , startDateField: 'startdt'
                    , format: 'Y-m-d'
                    , align: 'right'
                    , listeners: {
                        expand: function (field, eOpts) {
                            ExpandOverActiveX(field, eOpts);
                        },
                        collapse: function () {
                            Ext.get("myframe").hide();
                        }
                    }
            }, {
                xtype: 'displayfield'
                    , name: 'dispfld22'
                    , value: ''
                    , width: 10
            }, {                              //360 검색구분 조회전까지
                xtype: 'button'
                , name: 'submit1'
                , text: '<span style="font-weight: bold">조 회</span>'
                , icon: '<%=Page.ResolveUrl("~/Images") %>/icons/magnifier.png'
                , width: 70
                , height: 24
                , formBind: true
                , handler: function () {
                    rdOpen();
                }
            }, {
                xtype: 'combo'
                    , id: 'excd'
                    , name: 'excd'
				    , fieldLabel: '품질등급'
                    , labelWidth: 60
                    , labelAlign: 'right'
                    , width: 190
                    , queryMode: 'local'
                    , displayField: 'EXCONT'
                    , valueField: 'EXCD'
				    , store: exCodeStore
                    , multiSelect: true
                    , listeners: {
                        expand: function (field, eOpts) {
                            ExpandOverActiveX(field, eOpts);
                        },
                        collapse: function () {
                            Ext.get("myframe").hide();
                        }
                    }
            }, { xtype: 'combo'
                    , id: 'dtype'
                    , name: 'dtype'
				    , fieldLabel: '구 분'
                    , labelWidth: 60
                    , labelAlign: 'right'
                    , width: 190
                    , queryMode: 'local'
                    , forceSelection: true
				    , store: dateTypes
                    , value: 'D'
                    , listeners: {
                        expand: function (field, eOpts) {
                            ExpandOverActiveX(field, eOpts);
                        },
                        collapse: function () {
                            Ext.get("myframe").hide();
                        }
                    }
            }
            ]
        });

        /**
        제목창 : north,
        내용 : center
        - 검색창 : north
        - 목록 : center
        - 그래프 : south
        modal window : 범례
        */

        function getServerUrl() {
            var protocol = document.location.protocol;
            var domain = document.domain;
            var port = document.location.port;
            var url = "http://" + domain;
            if (port != "") url += ":" + port;
            return url;
        }
        function getParamsForReport() {
            var dLen = Ext.getCmp("dtype").getValue() == "Y" ? 4 : (Ext.getCmp("dtype").getValue() == "M" ? 6 : 8);
            var startDt = Ext.util.Format.date(Ext.getCmp("startdt").getValue(), 'Ymd');
            var endDt = Ext.util.Format.date(Ext.getCmp("enddt").getValue(), 'Ymd');
            var damCd = Ext.getCmp("damcd").getValue();
            var obsCd = Ext.getCmp("obscd").getValue();
            return "/rv " + "damcd[" + damCd + "] " + "obscd[" + obsCd + "] " + "dlen[" + dLen + "] " + "sdate[" + startDt + "] " + "edate[" + endDt + "] ";

        }
        var mrdPath = "/mrd/waterlevel.mrd";
        var mrdUrl = getServerUrl() + mrdPath;

        function rdOpen() {
            var params = "<%=param %>" + " " + getParamsForReport();
            //alert(params);
            Rdviewer.AutoAdjust = true;
            Rdviewer.ZoomDefault();
            Rdviewer.FileOpen(mrdUrl, params);
        }

        var mainForm = Ext.create('Ext.Viewport', {
            layout: {
                type: 'border'
                , padding: 0
            },
            border: 0,
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
                }, searchForm]
            }, {
                id: 'reportPanel',
                region: 'center',
                contentEl: 'report-panel'
            }
            ],
            renderTo: bd
        });

        //Chart Loading
        //loadChartPanel();
        //시작날짜, 끝날짜 조정
        Ext.getCmp('enddt').setValue(currDate);
        Ext.getCmp('startdt').setValue(currDate);

    });


</script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div id="menu-title" class="title_box x-hide-display" align="center" style="text-align:left;">
	<span style="font-weight:bold; margin-left:10px;">
    <img src="<%=Page.ResolveUrl("/Images") %>/icons/application-blog.png" align="absmiddle"/>&nbsp;&nbsp;
    수위자료 보고서
    </span>
</div>
<div id="report-panel" class="x-hide-display" >
<object id="Rdviewer" name="Rdviewer" style="z-index:1000;width:100%" classid="clsid:8068959B-E424-45AD-B62B-A3FA45B1FBAF" codebase="<%=Page.ResolveUrl("~/Cab") %>/rdviewer40.cab#version=4,0,0,156"></object>
<script type="text/javascript">
    document.getElementById("Rdviewer").height = document.documentElement.clientHeight - 125;
    document.getElementById("Rdviewer").width = document.documentElement.clientWidth - 200;
</script>
</div>
<iframe id="myframe" frameborder=0 style="z-index:1000;display:none;border:none;margin:0;padding:0;position:absolute;width:100%;height:100%;top:0;left:0;filter:Alpha(Opacity=85);" src="about:blank"></iframe>

</asp:Content>
