<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%
    string empno = (string)ViewData["empno"];
    string empnm = (string)ViewData["empnm"];
    string authcode = (string)ViewData["authcode"];
    string longitude = (string)ViewData["longitude"];
    string latitude = (string)ViewData["latitude"];
    string range = (string)ViewData["range"];
%>
<html xmlns="http://www.w3.org/1999/xhtml" >

<head>
    <title>HDIMSAPP</title>
    <style type="text/css">
    html, body {
	    height: 100%;
	    overflow: auto;
    }
    body {
	    padding: 0;
	    margin: 0;
    }
    #silverlightControlHost {
	    height: 100%;
	    text-align:center;
    }
    </style>
    <script type="text/javascript" src="/Scripts/extjs/ext-all.js"></script>
    <script type="text/javascript" src="/Scripts/extjs/locale/ext-lang-ko.js"></script>
    <script type="text/javascript" src="/Silverlight.js"></script>
    <script type="text/javascript" src="/Common/UserInfoJs"></script>
    <script type="text/javascript" src="/Scripts/jquery-1.6.2.min.js"></script>
    <script type="text/javascript" src="/Scripts/jquery-ui-1.8.16.custom.min.js"></script>
    <script type="text/javascript" src="/Scripts/jquery.multiselect.js"></script>
    <script type="text/javascript" src="/Scripts/jquery.loadmask.js"></script>
    <script type="text/javascript" src="/Scripts/jquery.popupWindow.js"></script>
    <script type="text/javascript" src="/Scripts/common/PopupWindows.js"></script>
    <script type="text/javascript" src="/Scripts/common/WSFileObjectFunctions.js"></script>
    <script type="text/javascript">
        function logOut() {
            top.window.document.location.href = "/Login/Logout";
        }

        var getEmpData = function () {
            return glUserInfo;
        };
    </script>
    <script type="text/javascript">
        var silverlightCtl = null;

        function pluginLoaded(sender,args){
            silverlightCtl  = sender.getHost();  
        }

        function onSilverlightError(sender, args) {
            var appSource = "";
            if (sender != null && sender != 0) {
                appSource = sender.getHost().Source;
            }

            var errorType = args.ErrorType;
            var iErrorCode = args.ErrorCode;

            if (errorType == "ImageError" || errorType == "MediaError") {
                return;
            }

            var errMsg = "Silverlight 응용 프로그램에서 처리되지 않은 오류 " + appSource + "\n";

            errMsg += "코드: " + iErrorCode + "    \n";
            errMsg += "범주: " + errorType + "       \n";
            errMsg += "메시지: " + args.ErrorMessage + "     \n";

            if (errorType == "ParserError") {
                errMsg += "파일: " + args.xamlFile + "     \n";
                errMsg += "줄: " + args.lineNumber + "     \n";
                errMsg += "위치: " + args.charPosition + "     \n";
            }
            else if (errorType == "RuntimeError") {
                if (args.lineNumber != 0) {
                    errMsg += "줄: " + args.lineNumber + "     \n";
                    errMsg += "위치: " + args.charPosition + "     \n";
                }
                errMsg += "메서드 이름: " + args.methodName + "     \n";
            }

            throw new Error(errMsg);
        }

        function getParam(name) {
            // separating the GET parameters from the current URL
            var getParams = document.URL.split("?");
            // transforming the GET parameters into a dictionnary
            var params = Ext.urlDecode(getParams[getParams.length - 1]);
            return params[name];
        }

        function DownloadFile(Uri) {
//            document.getElementById("DownloadCtl").RemoveAll();

//            document.getElementById("DownloadCtl").CanSelectFolder = true;

//            document.getElementById("DownloadCtl").AddFile(Uri);
//            document.getElementById("DownloadCtl").Download();
            document.location = Uri;
        }
        function DownloadFiles(UriList, LocalPathList) {
            document.getElementById("DownloadCtl").RemoveAll();

            //document.getElementById("DownloadCtl").CanSelectFolder = false;

            if(UriList.length == 0) 
            {
                OpenOasisPopup();//다운로드 할 파일이 없으면 그냥 실행
                return;
            }


            for(i=0; i < UriList.length ; i++) 
            {
                //BoardCd=+ +string ContentCd, string Guid)
                //spath = <%=Page.ResolveUrl("/") %> + "FileUploader/DownloadFile?" + UriList[i];
                //alert(spath);
                //LocalPathList[i] = TEMPORARY_DOWNLOAD_DIRECTORY + multiFileStore.getAt(i).data.name;
                //alert(SERVER_URL + UriList[i]);
                //alert(LocalPathList[i]);
                //alert(document.getElementById("DownloadCtl"));
                //alert(location.host);
                document.getElementById("DownloadCtl").AddFile(UriList[i], LocalPathList[i]);
            }
            document.getElementById("DownloadCtl").Download();
        }
        function OpenOasisPopup() {
             RunShell("C:/HandySoft/BizFlowGroupware6/Bin/HDSUBD32.exe", " M 42 3 C:/HandySoft/BizFlowGroupware6/Bin/Down/HDIMSData.ini /Attach:C:/HandySoft/BizFlowGroupware6/Bin/Down/HDIMSAttach.ini");
        }

        Ext.onReady(function () {
            //세션 업데이트
            <% if(!empno.Trim().Equals("")) { %>
            var updateSession = function() {
                    Ext.Ajax.request({
                        url: '/Login/UpdateSession',
                        method: 'POST',
                        scope: this,
                        params: {
                            userId: "<%=empno %>"
                        },
                        success: function (result, request) {
                            var jsonData = result.responseText;
                        },
                        failure: function (response, opts) {
                            //alert("fail");
                        }
                    });
            }
            Ext.TaskManager.start({run: updateSession,interval: 60000});
            <% } %>

        });
    </script>
</head>
<STYLE TYPE="text/css"> 
body {overflow: hidden}; 
</STYLE>  
<body style="margin:0 0 0 0;">
<SCRIPT LANGUAGE="VBScript">
Sub DownloadCtl_Done
    '별도의 첨부파일이 있을 경우 다운로드가 끝나고 이 sub가 실행됨
    'MsgBox "Download Completed"
    OpenOasisPopup() 
End Sub
</SCRIPT>
    <form id="form1" runat="server" style="height:100%">
    <div id="silverlightControlHost">
        <object data="data:application/x-silverlight-2," type="application/x-silverlight-2" width="100%" height="100%">
		  <param name="source" value="/ClientBin/HDIMSAPP.xap"/>
		  <param name="onError" value="onSilverlightError" />
		  <param name="background" value="white" />
		  <param name="minRuntimeVersion" value="4.0.60310.0" />
		  <param name="autoUpgrade" value="true" />
          <param name="enableHtmlAccess" value="true" />
          <%--<param name="onLoad" value="pluginLoaded" />
          <param name="maxFrameRate" value="200" />
          <param name="EnableGPUAcceleration" value="true" />
          <param name="EnableFrameRateCounter" value="true" />--%>
		  <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=4.0.60310.0" style="text-decoration:none">
 			  <img src="http://go.microsoft.com/fwlink/?LinkId=161376" alt="Microsoft Silverlight 얻기" style="border-style:none"/>
		  </a>
	    </object><iframe id="_sl_historyFrame" style="visibility:hidden;height:0px;width:0px;border:0px"></iframe></div>
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
    </form>
</body>
</html>

