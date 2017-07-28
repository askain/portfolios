<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	범례
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PreHeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">

    Ext.onReady(function () {
        Ext.BLANK_IMAGE_URL = '/Scripts/extjs/s.gif';
        Ext.tip.QuickTipManager.init();

        /////////////////댐검정코드 범례 현황 시작//////////////////
        var ExColumnRenderer = function (value, metaData, record, rowIndex, colIndex, store) {
            var ret = '항목없음';
            switch (value) {
                case 'RWL': ret = '저수위'; break;
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

        function getUseYN(value, metaData, record, rowIndex, colIndex, store) {
            return value == "Y" ? "사용함" : "사용안함";
        }

        function getColor(value) {
            var retVal = '<div id="test" name="test" style="background-color:#' + value + '">' + value + '</div>';
            return retVal;
        }

        Ext.define('examManagementModel', {
            extend: 'Ext.data.Model',
            idProperty: 'EXCD',
            fields: [
                    { name: 'EXCD', type: 'string' },
                    { name: 'EXCOLUMN', type: 'string' },
                    { name: 'EXORD', type: 'string' },
                    { name: 'EXCONT', type: 'string' },
                    { name: 'EXNOTE', type: 'string' },
                    { name: 'EXYN', type: 'string' },
                    { name: 'EXCOLOR', type: 'string' },
                    { name: 'EXCOLUMNNM', type: 'string' }
           ]
        });

        var examDataStore = Ext.create('Ext.data.Store', {
            autoDestroy: true,
            model: 'examManagementModel',
            proxy: {
                type: 'ajax',
                url: '<%=Page.ResolveUrl("~/Code")%>/GetDAMExamManagementList',
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
        examDataStore.load();

        var examGrid = Ext.create('Ext.grid.Panel', {
            id: 'examManagement-form',
            title: '댐 검정코드 범례',
            region: 'center',
            flex: 0.8,
            store: examDataStore,
            columns: [
            { header: '검정코드', flex: 0.4, minWidth: 70, sortable: false, align: 'center', dataIndex: 'EXCD',
                field: {
                    xtype: 'textfield'
                }
            }, { text: '항목', flex: 0.6, minWidth: 60, sortable: false, align: 'center', dataIndex: 'EXCOLUMN', renderer: ExColumnRenderer,
                field: {
                    xtype: 'combobox',
                    typeAhead: true,
                    triggerAction: 'all',
                    selectOnTab: true,
                    store: [
                        ['RWL', '저수위'],
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
            }, { text: '우선순위', flex: 0.5, minWidth: 60, sortable: false, align: 'center', dataIndex: 'EXORD',
                field: {
                    xtype: 'textfield'
                }
            }, { header: '<div style="text-align:center;">설명</div>', flex: 3.2, minWidth: 150, sortable: false, align: 'left', dataIndex: 'EXCONT',
                field: {
                    xtype: 'textfield'
                }
            }, { header: '<div style="text-align:center;">요약</div>', flex: 1.5, minWidth: 80, sortable: false, align: 'left', dataIndex: 'EXNOTE',
                field: {
                    xtype: 'textfield'
                }
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
            }, { header: '색상', flex: 0.5, minWidth: 60, sortable: false, align: 'center', dataIndex: 'EXCOLOR', id: 'EXCOLOR', renderer: getColor,
                field: {
                    xtype: 'colorfield',
                    id: 'cf',
                    name: 'cf',
                    value: 'FFFFFF'
                }
            }, { header: '항목명', flex: 0.5, minWidth: 60, sortable: false, align: 'center', dataIndex: 'EXCOLUMNNM', id: 'EXCOLUMNNM', hidden: true,
                field: {
                    xtype: 'textfield'
                }
            }],
            listeners: {
                selectionchange: function (model, records) {
                    var json, name, i, l, items, series, fields;
                    if (records[0]) {
                    }
                }
            }
        });
        /////////////////댐검정코드 범례 현황 종료//////////////////


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
            }, examGrid]
        });

    });

</script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <div id="menu-title" class="title_box x-hide-display" align="center" style="text-align:left;">
	    <span style="font-weight:bold; margin-left:10px;">
        <img src="<%=Page.ResolveUrl("/Images") %>/icons/gear.png" align="absmiddle"/>&nbsp;&nbsp;
       검정코드 범례
        </span>
    </div>
</asp:Content>
