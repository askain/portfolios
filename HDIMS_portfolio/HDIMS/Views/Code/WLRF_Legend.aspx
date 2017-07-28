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

        /////////////////검정코드 범례 현황 시작//////////////////
        function getUseTP(value, metaData, record, rowIndex, colIndex, store) {
            var val;
            switch (value) {
                case "W":
                    val = '<div style="color:blue">수위</div>';
                    break;
                case "R":
                    val = '<div style="color:green">우량</div>';
                    break;
                case "C":
                    val = "공통";
                    break;
            }
            return val;
        }

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
                    { name: 'EXTP', type: 'string' },
                    { name: 'EXORD', type: 'string' },
                    { name: 'EXCONT', type: 'string' },
                    { name: 'EXNOTE', type: 'string' },
                    { name: 'EXYN', type: 'string' },
                    { name: 'EXCOLOR', type: 'string' },
                    { name: 'EXCOLUMN', type: 'string' }
           ]
        });

        var examDataStore = Ext.create('Ext.data.Store', {
            autoDestroy: true,
            model: 'examManagementModel',
            proxy: {
                type: 'ajax',
                url: '<%=Page.ResolveUrl("~/Code")%>/GetExamManagementList/?exTp=<%=ViewData["TYPE"]%>',
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

        var examMngColumns = [
            { header: '검정코드', flex: 0.4, minWidth: 70, sortable: false, align: 'center', dataIndex: 'EXCD',
                field: {
                    xtype: 'textfield'
                }
            }, { text: '구분', flex: 0.6, minWidth: 60, sortable: false, align: 'center', dataIndex: 'EXTP', renderer: getUseTP,
                field: {
                    xtype: 'panel'
                }
            }, { text: '우선순위', flex: 0.5, minWidth: 60, sortable: false, align: 'center', dataIndex: 'EXORD',
                field: {
                    xtype: 'panel'
                }
            }, { header: '<div style="text-align:center;">내용</div>', flex: 3.2, minWidth: 150, sortable: false, align: 'left', dataIndex: 'EXCONT',
                field: {
                    xtype: 'panel'
                }
            }, { header: '<div style="text-align:center;">비고</div>', flex: 1.5, minWidth: 80, sortable: false, align: 'left', dataIndex: 'EXNOTE',
                field: {
                    xtype: 'panel'
                }
            }, { header: '사용여부', flex: 0.7, minWidth: 60, sortable: false, align: 'center', dataIndex: 'EXYN', renderer: getUseYN,
                field: {
                    xtype: 'panel'
                }
            }, { header: '색상', flex: 0.5, minWidth: 60, sortable: false, align: 'center', dataIndex: 'EXCOLOR', renderer: getColor,
                field: {
                    xtype: 'panel'
                }
            }];

        var examGrid = Ext.create('Ext.grid.Panel', {
            id: 'examManagement-form',
            title: '검정코드 범례',
            region: 'center',
            flex: 0.6,
            store: examDataStore,
            columns: examMngColumns
        });
        /////////////////검정코드 범례 현황 종료//////////////////

        /////////// examGrid title 변경 시작///////////
        if ('<%=ViewData["TYPE"] %>' == 'W') {
            examGrid.setTitle('수위 검정코드 범례');
        }
        else if ('<%=ViewData["TYPE"] %>' == 'R') {
            examGrid.setTitle('우량 검정코드 범례');
        }
        else {
            examGrid.setTitle('수위/우량 검정코드 범례');
        }
        /////////// examGrid title 변경 종료///////////

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