<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
<title>모니터링</title>
<link rel="stylesheet" type="text/css" href="../../Scripts/extjs/resources/css/ext-all.css" />
<script type="text/javascript" src="<%=Page.ResolveUrl("~/Scripts")%>/extjs/ext-all.js"></script>
<script type="text/javascript" src="<%=Page.ResolveUrl("~/Scripts")%>/jquery-1.6.1.min.js"></script>
<script type="text/javascript" src="<%=Page.ResolveUrl("~/Scripts")%>/extjs/locale/ext-lang-ko.js"></script>
<script type="text/javascript" src="<%=Page.ResolveUrl("~/Scripts")%>/swfobject.js"></script>
<script type="text/javascript">
    Ext.Loader.setConfig({ enabled: true });
    Ext.Loader.setPath('Ext.ux', '<%=Page.ResolveUrl("~/Scripts")%>/extjs/ux/');
    Ext.require(['*']);
</script>

<%
    string empno = (string)ViewData["empno"];
    string empnm = (string)ViewData["empnm"];
    string authcode = (string)ViewData["authcode"];
    string longitude = (string)ViewData["longitude"];
    string latitude = (string)ViewData["latitude"];
    string range = (string)ViewData["range"];

    IList<Hashtable> data = (List<Hashtable>)ViewData["data"];
    if (data.Count > 0 && data[0] != null)
    {
        longitude = data[0]["LONGITUDE"].ToString();
        latitude = data[0]["LATITUDE"].ToString();
        range = data[0]["RANGE"].ToString();
    }
%>
<script type="text/javascript">
    var g_empno = "<%=empno %>";
    var g_empnm = "<%=empnm %>";
    var g_authcode = "<%=authcode %>";
    var g_longitude = "<%=longitude %>";
    var g_latitude = "<%=latitude %>";
    var g_range = "<%=range %>";
    //alert(g_longitude);
</script>
<script type="text/javascript" src="http://www.google.com/jsapi?key=ABQIAAAAUM-s_OICaCpU5GwjuaPx-RSQE_YrE0r97jk5HP9_JYKrwtEQEBQ9I2VbfIJCYuEdMLZcbj_ns_PplQ"></script>
<script type="text/javascript" src="<%=Page.ResolveUrl("~/Scripts")%>/GoogleEarth.js"></script>
<link rel="stylesheet" href="/css/style.css" />
<script type="text/javascript">
    var bar_cur_pos = 5;
    var bar_action = "N";
    var spaceWidth = 43;
    var exTop = 4; exLeft = 0;
    var gBarPosTop = 33; gBarPosLeft = 165;
    var gBarTextTop = 45; gBarTextLeft = 85;
    
    Ext.onReady(function () {
        var mainView = Ext.create('Ext.Viewport', {
            border: 0,
            layout: {
                type: 'border',
                border: 0
            },
            split: true,
            items: [{
                region: 'north',
                autoHeight: true,
                contentEl: 'control-panel'
            }, {
                region: 'center',
                contentEl: 'gearth-panel'
            }]
        });

        mainView.show();

        gearthInit('gearth-panel');
        var spareMin = 10 - parseInt(parseInt(new Date().getMinutes()) % 10);
        var initRecalMinFlag = false;
        var initRecalMinTask = {
            run: function () {
                if (initRecalMinFlag) {
                    //alert(Ext.Date.format(new Date(), 'Y-m-d g:i:s A'));
                    Ext.TaskManager.start({ run: recalMin, interval: 60000 * 10 }); //정확히 10분단위로 호출되어야 한다.
                    Ext.TaskManager.stop(initRecalMinTask);
                } else {
                    initRecalMinFlag = true;
                }
            },
            interval: spareMin * 60000
        }
        // 오재우 차장 요청으로 Reload 제거
        //Ext.TaskManager.start(initRecalMinTask);

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        var controllerPos = $("#controllerImg").offset();
        var gControllerPlayTop, gControllerPlayLeft, gControllerStopTop, gControllerStopLeft;
        gBarPosTop = controllerPos.top + 4;
        gBarPosLeft = controllerPos.left + 92;
        gBarTextTop = controllerPos.top + 15;
        gControllerPlayTop = controllerPos.top + 3;
        gControllerPlayLeft = controllerPos.left + 463;
        gControllerStopTop = controllerPos.top + 3;
        gControllerStopLeft = controllerPos.left + 491;
        gControllerM1DTop = controllerPos.top + 5;
        gControllerM1DLeft = controllerPos.left + 334;
        gControllerM1HTop = controllerPos.top + 5;
        gControllerM1HLeft = controllerPos.left + 365;
        gControllerP1DTop = controllerPos.top + 5;
        gControllerP1DLeft = controllerPos.left + 427;
        gControllerP1HTop = controllerPos.top + 5;
        gControllerP1HLeft = controllerPos.left + 396;
        //gControllerconyearTop = controllerPos.top + 2;
        //gControllerconyearLeft = controllerPos.left + 460;
        gControllercondayTop = controllerPos.top + 7;
        gControllercondayLeft = controllerPos.left + 10;
        var initBarPosLeft = gBarPosLeft + (spaceWidth * 5);
        $("#bar").offset({ top: gBarPosTop, left: initBarPosLeft });
        $("#bar_text0").offset({ top: gBarTextTop, left: gBarTextLeft }); gBarTextLeft += spaceWidth;
        $("#bar_text1").offset({ top: gBarTextTop, left: gBarTextLeft }); gBarTextLeft += spaceWidth;
        $("#bar_text2").offset({ top: gBarTextTop, left: gBarTextLeft }); gBarTextLeft += spaceWidth;
        $("#bar_text3").offset({ top: gBarTextTop, left: gBarTextLeft }); gBarTextLeft += spaceWidth;
        $("#bar_text4").offset({ top: gBarTextTop, left: gBarTextLeft }); gBarTextLeft += spaceWidth;
        $("#bar_text5").offset({ top: gBarTextTop, left: gBarTextLeft });
        $("#controllerPlay").offset({ top: gControllerPlayTop, left: gControllerPlayLeft });
        $("#controllerStop").offset({ top: gControllerStopTop, left: gControllerStopLeft });
        $("#btn_M1D").offset({ top: gControllerM1DTop, left: gControllerM1DLeft });
        $("#btn_P1D").offset({ top: gControllerP1DTop, left: gControllerP1DLeft });
        $("#btn_M1H").offset({ top: gControllerM1HTop, left: gControllerM1HLeft });
        $("#btn_P1H").offset({ top: gControllerP1HTop, left: gControllerP1HLeft });
        //$("#conyear").offset({ top: gControllerconyearTop, left: gControllerconyearLeft });
        $("#conday").offset({ top: gControllercondayTop, left: gControllercondayLeft });
    });

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////


    function setControllBarPos(no) {
        bar_cur_pos = no;
        bar_action = "N";
        var leftPos = gBarPosLeft + spaceWidth * no;
        $("#bar").offset({ top: gBarPosTop, left: leftPos });
        if (document.getElementById('CH_radar').checked) {
            loadKmlPerTimeLine('radar', no);
        }
        if (document.getElementById('CH_tissen').checked) {
            loadKmlPerTimeLine('tissen', no);
        }
        CH_ControllerTime(no); //벌룬 강우량 정보와 연동하기 위한 호출.
    }

    function bar_Action(BA) {
        bar_cur_pos = 5;
        bar_action = BA;
        CloseBalloon();
        AC_width();
    }

    function CK_ModifyTime(no) {
        CM_ModifyTime(no);

        var controllerPos = $("#controllerImg").offset();
        gBarPosTop = controllerPos.top + 4;
        var initBarPosLeft = gBarPosLeft + (spaceWidth * 5)
        $("#bar").offset({ top: gBarPosTop, left: initBarPosLeft });
    }

    function selectRadarType() {
        var rt = $("#radarType").val();
        loadKmlByRadarType(rt);
    }

    function AC_width() {
        var tid;
        if (bar_action == "Y") {
            bar_cur_pos = bar_cur_pos + 1;
            if (bar_cur_pos > 5) { bar_cur_pos = 0 }
            var leftPos = gBarPosLeft + spaceWidth * bar_cur_pos;
            $("#bar").offset({ top: gBarPosTop, left: leftPos });

            if (document.getElementById('CH_radar').checked) {
                loadKmlPerTimeLine('radar', bar_cur_pos);
            }
            if (document.getElementById('CH_tissen').checked) {
                loadKmlPerTimeLine('tissen', bar_cur_pos);
            }

            tid = setTimeout(AC_width, 1000 * 3);
        } else {
            var leftPos = gBarPosLeft + spaceWidth * bar_cur_pos;
            $("#bar").offset({ top: gBarPosTop, left: leftPos });
            if (document.getElementById('CH_radar').checked) {
                loadKmlPerTimeLine('radar', bar_cur_pos);
            }
            if (document.getElementById('CH_tissen').checked) {
                loadKmlPerTimeLine('tissen', bar_cur_pos);
            }

            clearTimeout(tid);
        }
    }
</script>
</head>
<body style="z-index:100000000">
    <div id='menu-title' class="x-hide-display">실시간 모니터링</div>
    <div id='control-panel' class="x-hide-display">
        <table style="width:515px;height:50px;border:0px solid red;padding:0px;margin:0px">
            <tr>
                <td>
                    <table style="width:500px;height:20px;border:0px solid red;padding:0px;margin:0px">
                        <tr>
                            <td width="10px">&nbsp;</td>
                            <td style="width:10px;height:10px;"><input type="checkbox" id="CH_radar" onclick="loadKmlByType('radar')" checked="checked"/></td>
                            <td style="width:75px;"><b>레이더영상</b></td>
                            <td><input type="checkbox" id="CH_tissen" onclick="loadKmlByType('tissen')" checked="checked"/></td>
                            <td><b>티센망</b></td>
                            <td><input type="checkbox" id="CH_rfob" onclick="loadKmlByType('rfob')" checked="checked"/></td>
                            <td><b>우량국</b></td>
                            <td><input type="checkbox" id="CH_wlob" onclick="loadKmlByType('wlob')"/></td>
                            <td><b>수위국</b></td>
                            <td><input type="checkbox" id="CH_damarea" onclick="loadKmlByType('damarea')" checked="checked"/></td>
                            <td><b>댐유역</b></td>
                            <td><input type="checkbox" id="CH_waterarea" onclick="loadKmlByType('waterarea')"/></td>
                            <td><b>수계</b></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td><img id="controllerImg" src="/Images/GoogleEarth/imgController.png" /></td>
            </tr>
        </table>
        <img id="bar" src="/Images/GoogleEarth/imgControllba.png" style="position:absolute;z-index:1000;">
        <div id="bar_text0" style="position:absolute;z-index:1000;cursor:hand;font-weight:bold;font-size:10px;color:White;" onclick="javascript: setControllBarPos(0);"></div>
        <div id="bar_text1" style="position:absolute;z-index:1000;cursor:hand;font-weight:bold;font-size:10px;color:White;" onclick="javascript: setControllBarPos(1);"></div>
        <div id="bar_text2" style="position:absolute;z-index:1000;cursor:hand;font-weight:bold;font-size:10px;color:White;" onclick="javascript: setControllBarPos(2);"></div>
        <div id="bar_text3" style="position:absolute;z-index:1000;cursor:hand;font-weight:bold;font-size:10px;color:White;" onclick="javascript: setControllBarPos(3);"></div>
        <div id="bar_text4" style="position:absolute;z-index:1000;cursor:hand;font-weight:bold;font-size:10px;color:White;" onclick="javascript: setControllBarPos(4);"></div>
        <div id="bar_text5" style="position:absolute;z-index:1000;cursor:hand;font-weight:bold;font-size:10px;color:Yellow;" onclick="javascript: setControllBarPos(5);"></div>
        <img id="controllerPlay" src="/Images/GoogleEarth/btn_controller_play.png" style="position:absolute;cursor:hand;" onclick="bar_Action('Y');" alt="영상플레이버튼"/>
        <img id="controllerStop" src="/Images/GoogleEarth/btn_controller_stop.png" onclick="bar_Action('N');" style="position:absolute;cursor:hand;" alt="영상스톱버튼"/>
        <img id="btn_M1D" src="/Images/GoogleEarth/btn_M1D.png" style="position:absolute;cursor:hand;" onclick="CK_ModifyTime(-24);" alt="1일 빼기"/>
        <img id="btn_P1D" src="/Images/GoogleEarth/btn_P1D.png" style="position:absolute;cursor:hand;" onclick="CK_ModifyTime(24);" alt="1일 더하기"/>
        <img id="btn_M1H" src="/Images/GoogleEarth/btn_M1H.png" style="position:absolute;cursor:hand;" onclick="CK_ModifyTime(-1);" alt="1시간 빼기"/>
        <img id="btn_P1H" src="/Images/GoogleEarth/btn_P1H.png" style="position:absolute;cursor:hand;" onclick="CK_ModifyTime(1);" alt="1시간 더하기"/>
        <%--<div id="conyear" style="position:absolute;z-index:1000;font-weight:bold;font-size:13px;color:Yellow;"></div>--%>
        <div id="conday" style="position:absolute;z-index:1000;font-weight:bold;font-size:12px;color:Yellow;"></div>
    </div>
    <div id='gearth-panel' class="x-hide-display" style="height:100%;z-index:5"></div>

</body>
</html>
