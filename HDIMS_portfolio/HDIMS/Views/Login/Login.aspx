<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Login.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="WEBSOLTOOL.Util" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	HDIMS 로그인
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript" src="/Silverlight.js"></script>    
<script type="text/javascript">

    Ext.onReady(function () {
        Ext.BLANK_IMAGE_URL = '/Scripts/extjs/s.gif';

        var checkLogin = function () {
            var EmpNo = Ext.getCmp('id').getValue();
            var PassWD = Ext.getCmp('password').getValue();

            Ext.Ajax.request({
                url: '<%=Page.ResolveUrl("~/")%>Login/Login',
                method: 'POST',
                scope: this,
                isUpload: true,
                params: {
                    sEmpNo: EmpNo,
                    sPassWD: PassWD
                },
                success: function (result, request) {
                    var jsonData = result.responseText;
                    var myArr = Ext.JSON.decode(jsonData);
                    var myData = myArr.Data[0];
                    if (myData.RETVAL == 1) {           // 유효한 사번
                        //saveid가 true이면 사번쿠키 저장아니면 쿠키 제거
                        if (Ext.getCmp("saveid").getValue() == true) {
                            Ext.util.Cookies.set("hdims_cookie_userid", Ext.getCmp("id").getValue(), Ext.Date.add(new Date(), Ext.Date.YEAR, 1));
                        } else {
                            Ext.util.Cookies.clear("hdims_cookie_userid");
                        }
                        var redirect = '/R';
                        top.location = redirect;
                    }
                    else if (myData.RETVAL == 0) {      // 암호가 틀림
                        Ext.Msg.alert('Message', '비밀번호가 틀렸습니다.', function () {
                            Ext.getCmp('password').setValue("");
                            Ext.getCmp('password').focus(false);
                        });
                    }
                    else if (myData.RETVAL == -1) {      // 존재하지 않는 사번
                        Ext.Msg.alert('Message', '존재하지 않는 사번입니다.', function () {
                            Ext.getCmp('id').setValue("");
                            Ext.getCmp('password').setValue("");
                            Ext.getCmp('id').focus(false);
                        });
                    }
                    else if (myData.RETVAL == -2) {      // 기타(처음 페이지 로딩)
                        if (Ext.getCmp('id').getValue() == "") {
                            Ext.Msg.alert('Message', '사번을 입력하세요.', function () {
                                Ext.getCmp('id').setValue("");
                                Ext.getCmp('password').setValue("");
                                Ext.getCmp('id').focus(false);
                            });
                        }
                        else if (Ext.getCmp('password').getValue() == "") {
                            Ext.Msg.alert('Message', '비밀번호를 입력하세요.', function () {
                                Ext.getCmp('password').setValue("");
                                Ext.getCmp('password').focus(false);
                            });
                        }
                    }
                },
                failure: function (response, opts) {
                    Ext.Msg.alert('Message', '로그인중 에러가 발생하였습니다.');
                }
            });
        };

        var form = new Ext.FormPanel({
            id: 'main-panel',
            baseCls: 'x-plain',
            autoWidth: true,
            autoHeight: true,
            renderTo: 'LOGIN-FORM',
            labelAlign: 'left',
            layout: {
                type: 'table',
                columns: 5
            },
            items: [
                {
                    xtype: 'displayfield',
                    value: '사원번호 :'
                }, {
                    xtype: 'displayfield',
                    width: 5
                }, {
                    xtype: 'textfield',
                    name: 'id',
                    id: 'id',
                    tabIndex: 1,
                    listeners: {
                        specialkey: function (field, e) {
                            if (e.getKey() == e.ENTER) {
                                checkLogin();
                            }
                        }
                    }
                }, {
                    xtype: 'displayfield',
                    colspan: 2
                }, {
                    xtype: 'displayfield',
                    value: '비밀번호 :'
                }, {
                    xtype: 'displayfield',
                    width: 5
                }, {
                    xtype: 'textfield',
                    inputType: 'password',
                    name: 'password',
                    id: 'password',
                    tabIndex: 2,
                    listeners: {
                        specialkey: function (field, e) {
                            if (e.getKey() == e.ENTER) {
                                checkLogin();
                            }
                        }
                    }
                }, {
                    xtype: 'displayfield',
                    width: 5
                }, {
                    xtype: 'button',
                    name: 'loginBtn',
                    id: 'loginBtn',
                    tabIndex: 3,
                    margin: '0 5 5 5',
                    icon: '<%=Page.ResolveUrl("~/Images") %>/icons/arrow.png',
                    text: '<span style="font-weight:bold;vertical-align:middle;">로그인</span>',
                    handler: function () {
                        checkLogin();
                    }
                }, {
                    xtype: 'checkbox',
                    name: 'saveid',
                    id: 'saveid',
                    boxLabel: '사원번호저장',
                    inputValue: 'save',
                    colspan: 3,
                    checked: true,
                    tabIndex: 4,
                    width: 230
                }, {
                    xtype: 'displayfield',
                    colspan: 2
                }
                ]
        });

        var cookieUserId = Ext.util.Cookies.get("hdims_cookie_userid");
        if (cookieUserId != null) {
            Ext.getCmp("id").setValue(cookieUserId);
            Ext.getCmp('password').focus(false);
        } else {
            Ext.getCmp('id').focus(false);
        }

    });

</script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">  
<div id="loginDiv">
 <table width="100%"  height="90%" border="0" cellspacing="0" cellpadding="0">
 <tr><td width="100%"  height="100%" align="center" valign="middle">
     <table width="462" border="0" cellspacing="0" cellpadding="0">
     <tr><td height="323" align="center" valign="bottom" background="<%=Page.ResolveUrl("~/")%>Images/login/hdims_login.png">
         <table width="379" border="0" cellspacing="0" cellpadding="0">
         <tr><td height="178" align="center">
         <div id="LOGIN-FORM"></div>
         </td></tr>
         </table>
         <table width="379" border="0" cellspacing="0" cellpadding="0">
         <tr><td height="54">&nbsp;</td></tr>
         </table>
     </td>
     </tr>
     </table>
 </td></tr>
</table>
</div>
        <OBJECT classid="CLSID:70E6CD54-8979-4977-9321-48DA55439F6C" 
	codeBase="<%=Page.ResolveUrl("/XUpload") %>/XUpload.ocx#version=3,2,0,0"
	id="DownloadCtl" 
	width="0" height="0">
<PARAM NAME="RegKey" VALUE="zBrxTD6R4ibUhoEIzZduDBu565T3DG5U8S+9t5Nm6WIUgMqIQv+3Q7ueN/SLnF5C2W55n8QIgaSJ">
<PARAM NAME="CanSelectFolder" VALUE="False">
<PARAM NAME="WarnOverwrite" VALUE="False"> 
<PARAM NAME="ShowProgress" VALUE="False"> 
<PARAM NAME="International" VALUE="
aABkADEAPQAM03zHIwAjAGgAZAAyAD0ArMB0x4jJIwAjAGgAZAAzAD0ADNN8xyMAIwBoAGQANAA9
AKzAdMeIySMAIwBtAG4AMQA9ACYADNN8xyAAIMHd0CMAIwBtAG4AMgA9ACYA9NNUs3y5IAAgwd3Q
IwAjAG0AbgAzAD0AJgCtwBzIIwAjAG0AbgA0AD0AJgB8uSAABMiAvSAArcAcyCMAIwBtAG4ANQA9
ACYAxcVcuNy0IwAjAG0AbgA2AD0AJgDksrTGXLjctCMAIwBmAGwAMQA9AMXFXLjctGDVIAAM03zH
RMcgACDB3dBY1TjBlMYjACMAZgBkADEAPQD001SzfLkgACDB3dBY1TjBlMYjACMAZgBkADIAPQAc
wQy+IAD001SzxLMgAOzTaNUjACMAZgBkADMAPQCkwpTOIwAjAHAAYgAxAD0AxcVcuNy0IAARyS4A
LgAuACMAIwBwAGIAMgA9AAzTfMdExyAAxcVcuNy0WNXgrCAAiMe1wsiy5LIjACMAcABiADMAPQAE
yLTMIADEyYnVIADBwGnWIwAjAHAAYgA0AD0AqLBAxyAA3MIErCMAIwBwAGIANQA9ABHJwMkjACMA
cABiADYAPQBRx/WyRMcgADCu5LK9uciy5LIuAC4ALgAjACMAcABiADcAPQDksrTGXLjctCAAEcku
AC4ALgAjACMAcABiADgAPQAM03zHRMcgAOSytMZcuNy0WNXgrCAAiMe1wsiy5LIjACMAcgBzADEA
PQAcwYS8XLiAvTDRIABRx/WyIwAjAHUAbQAxAD0AWABVAHAAbABvAGEAZAAgANDF7LcjACMAdQBt
ADIAPQBV1qXHkMcgACIAJQBzACIAIAB8uSAAAKzEySAADNN8x0DHIADI1QCsGLTAySAASsW1wsiy
5LIuACAAmLA4usDJIAAM03zHQMcgAJjMrLlg1UyulMY/ACMAIwB1AG0AMwA9AFzNALMgAACspbIg
ACDB3dAgABjCfLkgAAjN/KwgAFjVAMa1wsiy5LIuACAAKAAlAGQAKQAuACMAIwB1AG0ANAA9AAzT
fMcgACUAcwAgAACsIABczQCzIADI1QCsIACpxsm3RMcgAAjNAKxY1QDGtcLIsuSyLgAgACgAJQBs
AHUAIABiAHkAdABlAHMALgApACMAIwB1AG0ANQA9AATItMwgAAzTfMcgAKnGybdAxyAAJQBsAHUA
IABiAHkAdABlAHMAfLkgAAjN/Kxg1SAAGMIgAMbFtcLIsuSyLgAjACMAdQBtADYAPQAlAGwAdQAg
AAzTfMd0xyAAIMHd0Bi0yMW1wsiy5LIuACAAdMcHuIysIADOuUDHIAAM03zHRMcgAFzViLzQxSAA
xcVcuNy0IABY1ZSyIACDrEDHIACUzZzMGLTAySAASsW1wsiy5LIuACAAEcnAyVjV3MKgrLXCyLJM
rj8AIwAjAHUAbQA3AD0AJQBzACAADNN8x0THIAD0xSAAGMIgAMbFtcLIsuSyLgAgAJiwOLrAySAA
DNN8x0DHIACYzKy5YNVMrpTGPwAjACMAdQBtADgAPQAlAGQAIAAcrFjHIAAM03zHRMcgAHy5IADk
srTGXLjctCAAWNUkuOCsIABp1ciy5LIuACAAxKyNwWDVTK6Uxj8AIwAjAHUAbQA5AD0AWNWYsCAA
dMfBwFjHIAAM03zHRMcgAG6ztMXwxCS44KwgAGnVyLLksi4AIADErI3BYNVMrpTGPwAjACMAdQBt
ADEAMAA9ACUAZAAgABysWMcgAAzTfMdExyAAxcVcuNy0IABY1SS44KwgAGnVyLLksi4AIADErI3B
YNVMrpTGPwAjACMAdQBtADEAMQA9AHTHIABVAFIATAAgADzHXLggAMXFXLjctCAAYNVMtSAA5LLc
wiAAO7vAySAASsVMxyMAIwA=">
</OBJECT>
<OBJECT ID="WSFileObject"
            CLASSID="CLSID:F7942C0B-3E4B-4652-93EA-C38CEC675212"
            CODEBASE="<%=Page.ResolveUrl("/cab") %>/WSFileObject.cab#version=1,0,0,3" width="0" height="0">
</OBJECT>
</asp:Content>
