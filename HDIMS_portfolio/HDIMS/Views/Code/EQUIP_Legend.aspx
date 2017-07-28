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

        ///////////////// 범례 현황 시작//////////////////
        function setGroupNm(value, metaData, record, rowIndex, colIndex, store, view) {
            var crec = equipGroupComboStore.findRecord("GROUPCD", value);
            if (crec) {
                return '<div style="cursor:pointer;width:100%;">' + crec.get("GROUPNM") + '</div>';
            }
            return '미선택';
        }

        function getUseYN(value, metaData, record, rowIndex, colIndex, store) {
            return value == "Y" ? "사용함" : "사용안함";
        }

        function getColor(value) {
            var retVal = '<div id="test" name="test" style="background-color:#' + value + '">' + value + '</div>';
            return retVal;
        }

        /* 그룹명 콤보 모델 정의 */
        var equipGroupComboModel = Ext.define('equipGroupComboModel', {
            extend: 'Ext.data.Model',
            idProperty: 'GROUPCD',
            fields: [
                { name: 'GROUPCD', type: 'string' },
                { name: 'GROUPNM', type: 'string' },
                { name: 'GROUPEXPLAIN', type: 'string' }
            ]
        });

        Ext.define('equipManagementModel', {
            extend: 'Ext.data.Model',
            idProperty: 'ABCOLUMN',
            fields: [
                    { name: 'ABCOLUMN', type: 'string' },
                    { name: 'ABCONT', type: 'string' },
                    { name: 'ABYN', type: 'string' },
                    { name: 'ABCOLOR', type: 'string' },
                    { name: 'GROUPCD', type: 'string' },
                    { name: 'GROUPNM', type: 'string' },
                    { name: 'ABCOMMENT', type: 'string' }
           ]
        });

        /* 리스트내 그룹명 저장소 정의 */
        var equipGroupComboStore = Ext.create('Ext.data.Store', {
            autoDestroy: true,
            model: 'equipGroupComboModel',
            proxy: {
                type: 'ajax',
                url: '<%=Page.ResolveUrl("~/Code")%>/GetEquipGroupCombo',
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
            autoLoad: true
        });

        //설비상태코드관리 Store
        var equipManagementStore = Ext.create('Ext.data.Store', {
            autoDestroy: true,
            model: 'equipManagementModel',
            proxy: {
                type: 'ajax',
                url: '<%=Page.ResolveUrl("~/Code")%>/GetEquipManagementList',
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
            autoLoad: true
        });
        //equipManagementStore.load();

        var equipManagementColumns = [
        { id: 'ABCOLUMN', header: '<div style="text-align:center;">코드<span style="color:red;"> *</span></div>', flex: 1.0, minWidth: 100, sortable: false, align: 'left', dataIndex: 'ABCOLUMN', renderer: Ext.util.Format.uppercase,
            field: {
                type: 'textfield',
                allowBlank: false
            }
        }, { header: '<div style="text-align:center;">세부사항</div>', flex: 3.4, minWidth: 200, sortable: false, align: 'left', dataIndex: 'ABCONT',
            field: {
                xtype: 'textfield'
            }
        }, { header: '사용여부', flex: 0.6, minWidth: 70, sortable: false, align: 'center', dataIndex: 'ABYN', renderer: getUseYN,
            field: {
                xtype: 'textfield'
            }
        }, { header: '색상', flex: 0.5, minWidth: 60, sortable: false, align: 'center', dataIndex: 'ABCOLOR', id: 'ABCOLOR', renderer: getColor,
            field: {
                xtype: 'colorfield',
                id: 'cf',
                name: 'cf',
                value: 'ffffff'
            }
        }, { header: '그룹명', flex: 0.7, minWidth: 90, align: 'center', dataIndex: 'GROUPCD', renderer: setGroupNm,
            field: {
                xtype: 'combobox',
                typeAhead: true,
                triggerAction: 'all',
                selectOnTab: true,
                valueField: 'GROUPCD',
                displayField: 'GROUPNM',
                store: equipGroupComboStore
            }
        }, { header: '<div style="text-align:center;">줄임말</div>', flex: 1.0, minWidth: 100, sortable: false, align: 'left', dataIndex: 'ABCOMMENT',
            field: {
                type: 'textfield'
            }
        }];


        var equipManagementGrid = Ext.create('Ext.grid.Panel', {
            id: 'grid',
            name: 'grid',
            flex: 1,
            region: 'center',
            frame: true,
            columns: equipManagementColumns,
            store: equipManagementStore
        });

        /////////////////검정코드 범례 현황 종료//////////////////
        var mainViewport = Ext.create('Ext.Viewport', {
            layout: {
                type: 'border'
            , padding: 0
            },
            border: 0,
            renderTo: Ext.getBody(),
            items: [{
                region: 'north',
                height: 45,
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
                }]
            }, equipManagementGrid]
        });


    });

</script>
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <div id="menu-title" class="title_box x-hide-display" align="center" style="text-align:left;">
	    <span style="font-weight:bold; margin-left:10px;">
        <img src="<%=Page.ResolveUrl("/Images") %>/icons/gear.png" align="absmiddle"/>&nbsp;&nbsp;
        설비상태코드 범례
        </span>
    </div>
</asp:Content>