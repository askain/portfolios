<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="HDIMS.Utils.Login"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	내 정보
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PreHeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<%
    EmpData empdata = new EmpData();
%>
<script type="text/javascript">
//0: 사번 , 1: 이름 , 2: 비번, 3: 권한코드, 4: 권한명, 5: 로그인일시, 6: 관리댐 코드, 7: LONGITUDE, 8:LATITUDE, 9:RANGE, 10:관리단코드
var empNo = "<%=empdata.GetEmpData(0) %>";
var empNm = "<%=empdata.GetEmpData(1) %>";
var authCd = "<%=empdata.GetEmpData(3) %>";
var authNm = "<%=empdata.GetEmpData(4) %>";
var mgtDam = "<%=empdata.GetEmpData(6) %>";
var mgtCd = "<%=empdata.GetEmpData(10) %>";
var mgtNm = '<%=ViewData["mgtNm"] %>';
var dept = '<%=ViewData["dept"] %>';


Ext.onReady(function () {
    var confirm = function (ret, oldPwd) {
        //alert(ret);
        if (ret == undefined || ret == "cancel") return;
        if (oldPwd == undefined || oldPwd.length <= 0) return;

        Ext.getCmp("oldPwd").setValue(oldPwd);

        UserInfoPanel.getForm().submit({
            success: function (form, action) {
                Ext.Msg.show({
                    title: 'Success',
                    msg: '정보 갱신완료',
                    buttons: Ext.Msg.OK,
                    animEl: 'elId'
                });

                Ext.getCmp('newPwd1').setValue('');
                Ext.getCmp('newPwd2').setValue('');

            },
            failure: function (form, action) {
                Ext.Msg.alert('Failed', '정보 갱신 에러 : ' + action.result.msg);
            }
        });

    };

    /* 모델 정의 */
    Ext.define('UserModel', {
        extend: 'Ext.data.Model',
        fields: [
                { name: 'EMPNO', type: 'string' }
                , { name: 'EMPNM', type: 'string' }
                , { name: 'PASSWD', type: 'string' }
                , { name: 'DEPT', type: 'string' }
                , { name: 'EMAIL', type: 'string' }
                , { name: 'HPTEL', type: 'string' }
                ]
    });

    var UserInfoStore = Ext.create('Ext.data.Store', {
        autoDestroy: true,
        model: 'UserModel',
        proxy: {
            type: 'ajax',
            url: '/ManAuthMng/GetManList',
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
            load: function (store, records, successful) {
                if (records != '') {
                    Ext.getCmp('email').setValue(records[0].data.EMAIL);
                    Ext.getCmp('hptel').setValue(records[0].data.HPTEL);
                }
            }
        },
        autoLoad: false
    });

    /* 메뉴 관리 오른쪽 하단 bbar 아이템 정의 */
    var damMgtRightDownTbarItems = [
            '->',
            {
                text: '<span style="vertical-align:middle">저장</span>',
                icon: '<%=Page.ResolveUrl("~/Images") %>/icons/disk.png',
                itemId: 'save2',
                tooltip: '데이터 저장',
                handler: function () {

                    var newPwd1 = Ext.getCmp("newPwd1").getValue();
                    var newPwd2 = Ext.getCmp("newPwd2").getValue();
                    if (newPwd1 && !newPwd2) {
                        Ext.Msg.alert('새 비밀번호 입력', ' 새 비밀번호를 입력해 주세요.');
                        return false;
                    } else if (newPwd2 && !newPwd1) {
                        Ext.Msg.alert('새 비밀번호 재입력', ' 새 비밀번호를 입력해 주세요.');
                        return false;
                    } else if (newPwd1 != newPwd2) {
                        Ext.Msg.alert('확인', ' 새 비밀번호가 일치하지 않습니다.');
                        return false;
                    }

                    Ext.MessageBox.prompt('확인', '   현재 비밀번호를 입력해 주세요.', confirm);

                }
            }
        ];

    var UserInfoPanel = Ext.create('Ext.form.Panel', {
        id: 'UserInfoPanel',
        name: 'UserInfoPanel',
        url: '/ManAuthMng/SubmitUserInfo',
        renderTo: Ext.fly("MainContent"),
        frame: true,
        title: '내 정보',
        region: 'center',
        width: 400,
        height: 350,
        fbar: damMgtRightDownTbarItems,
        layout: {
            type: 'vbox',
            padding: 5
        },
        defaults: {
            anchor: '0',
            padding: '5px'
        },
        items: [{
            xtype: 'fieldset',
            title: empNm,
            width: 370,
            items: [{
                xtype: 'textfield',
                fieldLabel: '사번',
                labelWidth: 170,
                width: 350,
                id: 'empNo',
                name: 'empNo',
                readOnly: true,
                value: empNo
            }, {
                xtype: 'textfield',
                fieldLabel: '새 비밀번호',
                labelWidth: 250,
                width: 350,
                id: 'newPwd1',
                name: 'newPwd1'
            }, {
                xtype: 'textfield',
                fieldLabel: '새 비밀번호 재입력',
                labelWidth: 250,
                width: 350,
                id: 'newPwd2',
                name: 'newPwd2'
            }, {
                xtype: 'textfield',
                fieldLabel: '이름',
                labelWidth: 130,
                width: 350,
                readOnly: true,
                value: empNm
            }, {
                xtype: 'textfield',
                fieldLabel: '부서명',
                labelWidth: 130,
                width: 350,
                readOnly: true,
                value: dept
            }, {
                xtype: 'textfield',
                fieldLabel: '관리단',
                labelWidth: 130,
                width: 350,
                readOnly: true,
                value: mgtNm
            }, {
                xtype: 'textfield',
                width: 350,
                id: 'mgtCd',
                name: 'mgtCd',
                hidden: true,
                value: mgtCd
            }, {
                xtype: 'textfield',
                fieldLabel: '권한',
                labelWidth: 130,
                width: 350,
                readOnly: true,
                value: authNm
            }, {
                xtype: 'textfield',
                width: 350,
                id: 'authCode',
                name: 'authCode',
                hidden: true,
                value: authCd
            }, {
                xtype: 'textfield',
                width: 350,
                id: 'oldPwd',
                name: 'oldPwd',
                hidden: true
            }, {
                xtype: 'textfield',
                fieldLabel: '이메일',
                labelWidth: 130,
                width: 350,
                id: 'email',
                name: 'email'
            }, {
                xtype: 'textfield',
                fieldLabel: '연락처',
                labelWidth: 130,
                width: 350,
                id: 'hptel',
                name: 'hptel'
            }
            ]
        }]
    });

    UserInfoStore.load({
        params: {
            authCode: authCd
            , term: 'EMPNO'
            , txt: empNo
            , start: 1
            , limit: 10
            , sort: '[{"property":"PASSWD","direction":"ASC"}]'
        }
    });
});

</script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
<div id="menu-title" class="title_box x-hide-display" align="center" style="text-align:left;">
	<span style="font-weight:bold; margin-left:10px;">
    <img src="<%=Page.ResolveUrl("/Images") %>/icons/gear.png" align="absmiddle"/>&nbsp;&nbsp;
    내 정보
    </span>
</div>
<div id="MainContent"></div>
</asp:Content>
