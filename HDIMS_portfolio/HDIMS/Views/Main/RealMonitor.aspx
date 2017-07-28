<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="HDIMS.Utils.Login"%>
<%
    EmpData empdata = new EmpData();
    string MGTDAM = empdata.GetEmpData(6);
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
<title>실시간 모니터링</title>
<script type="text/javascript" src="/Common/UserInfoJs"></script>
<script type="text/javascript" src="/Silverlight.js"></script>
<script type="text/javascript" src="/Scripts/jquery-1.6.2.min.js"></script>
<script type="text/javascript" src="/Scripts/jquery.popupWindow.js"></script>
<script type="text/javascript" src="/Scripts/common/PopupWindows.js"></script>
<script type="text/javascript" src="/Scripts/swfobject.js"></script>
<script type="text/javascript">
    /**
    -- hbox
    1. GoogleEarth    (제외)
    2. 이상자료 모니터링,
    3. 알람현황 모니터링,
    4. 설비상태 모니터링
    */

    var MGTDAM = ("<%=MGTDAM %>" || {}).split(',')[0];
    var MGTOBS = "";

    var silverlightCtl = null;

    var getEmpData = function () {
        return glUserInfo;
    };

    function pluginLoaded(sender, args) {
        silverlightCtl = sender.getHost();
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

    function LoadFromMap(DamCd) {
        // 좌측 플래쉬에서 호출
        MGTDAM = "";
        if (DamCd != null && DamCd != undefined) MGTDAM = DamCd;
        //alert(DamCd); //GetAbnormDamList,GetAbnormOperList,GetAbnormDataList,GetAbnormEquipList
        silverlightCtl.Content.MainPage.LoadFromMap(MGTDAM);
    }
    function LoadFromObs(DamCd, WlObsCd, RfObsCd) {
        MGTDAM = DamCd;
        ObsCd = WlObsCd;
        if (RfObsCd != undefined && RfObsCd != "") ObsCd += "," + RfObsCd;
        MGTOBS = ObsCd;
        //alert(DamCd + ObsCd);
        silverlightCtl.Content.MainPage.LoadFromObs(MGTDAM, MGTOBS);
    }
    function ToggleSwf() {
        if ($("#LeftDiv").width() == 480) {
			$("#LeftDiv").css("width",0);
			//var curWidth = parseFloat(document.getElementById("silverlightControlHost").style.width);
			$("#silverlightControlHost").css("width",1600);
			document.getElementById("btnMapVisible").value="지도 표시";
		}
		else {
		    $("#LeftDiv").css("width", 480);
			$("#silverlightControlHost").css("width",1150);
			document.getElementById("btnMapVisible").value="지도 비표시";
		}
    }
</script>
<style>
#LeftDiv{width:480px; height:656px;}
#silverlightControlHost{width:1150px; height:715px;}
</style>
<script type="text/javascript">
    var flashvars = { MGTDAM: MGTDAM };
    var params = {wmode:"transparent", allowScriptAccess: "always"};
    var attributes = {};
    swfobject.embedSWF("/flash/m_map.swf", "mapContent", "100%", "100%", "9.0.0", "expressInstall.swf", flashvars, params, attributes);
</script>
</head>
<body style="margin:0 0 0 0;">
<table>
<tr>
<td  valign='top'>
<!--<input id='btnMapVisible' type="button" value='지도 비표시' onclick='javascript:ToggleSwf()' />-->
</td>
<td rowspan="2" valign="top" >
<div id="silverlightControlHost">
    <object data="data:application/x-silverlight-2," type="application/x-silverlight-2" width="100%" height="100%">
		<param name="source" value="/ClientBin/HDIMSMAIN.xap"/>
		<param name="onError" value="onSilverlightError" />
		<param name="background" value="white" />
        <param name="onLoad" value="pluginLoaded" />
		<param name="minRuntimeVersion" value="4.0.60310.0" />
		<param name="autoUpgrade" value="true" />
        <param name="enableHtmlAccess" value="true" />
		<a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=4.0.60310.0" style="text-decoration:none">
 			<img src="http://go.microsoft.com/fwlink/?LinkId=161376" alt="Microsoft Silverlight 얻기" style="border-style:none"/>
		</a>
	</object><iframe id="_sl_historyFrame" style="visibility:hidden;height:0px;width:0px;border:0px"></iframe>
    </div>
	</td>
</tr>
<tr><td>
<div id='LeftDiv'>
    <div id="mapContent"></div>
</div>
</td></tr></table>
</body>
</html>
