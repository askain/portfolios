google.load("earth", "1");
google.load("maps", "2");  // For JS geocoder  
var googleEarth = null;
var geocoder;

//최근 시간대로 변경하고, 마지막 시간대로 모두 로딩한다.
//로딩할 경우 체크박스를 확인하여 해당사항만 로딩한다.
//로딩시 이전에 로딩되었던 것들은 제거한다.
var cTimeLines;
var cKmlTypes = ['tissen', 'damarea', 'waterarea', 'radar', 'wlob', 'rfob', 'legend'];
var cKmlDatas = {
	'main':null,
	'radar':null,
	'damarea': null,
	'waterarea': null,
	'tissen': null,
	'wlob': null,
	'rfob': null,
    'legend' : null
}
var firstRecalMin = true;
//현재 로딩하는 시간
var cKmlLoadTime = null;
//현재 선택된 레이더 색상 종류
var cKmlRadarType = "E30S"; //강조색1:E30N,강조색2:E30R,강조색3:E30S,연속색:S30N

//현재 영상컨트롤러에서 표현되는 시간.
var fulltime = [];
var ControllerTime = '';
var ModifyTime = 0;
var NowBarNo = 5;

function CM_ModifyTime(no) {
    CloseBalloon();
    ModifyTime += no;
    if (ModifyTime > 0) {
        ModifyTime = 0;
        alert('아직 데이터가 생성되지 않았습니다. \n가장 최신 데이터로 조회 합니다.');
    } else if (ModifyTime < -24 * 30) {
        ModifyTime = -24*30;
        alert('30일을 초과하는 데이터는 존재 하지 않습니다. \n30일 이전 데이터로 조회 합니다.');
    }
    //alert(ModifyTime);
    recalMin();
//    if (no < 0) {
//        setControllBarPos(5)
//    } else {
//        setControllBarPos(0)
//    }
}

function CH_ControllerTime(no) {
    ControllerTime = fulltime[no];
    NowBarNo = no;
    CloseBalloon();
}
		
function gearthInit(panelId) {
    geocoder = new GClientGeocoder();
    google.earth.createInstance(panelId, gearthInitCB, gearthFailureCB);
}

function gearthInitCB(instance) {
    googleEarth = instance;
    googleEarth.getWindow().setVisibility(true);
    // add a navigation control
    //googleEarth.getNavigationControl().setVisibility(googleEarth.VISIBILITY_AUTO); // 컨트롤 뷰
    // add some layers
    //googleEarth.getLayerRoot().enableLayerById(googleEarth.LAYER_BORDERS, true);
    googleEarth.getLayerRoot().enableLayerById(googleEarth.LAYER_TERRAIN, true);
    //googleEarth.getOptions().setTerrainExaggeration(2.2);
    var la = googleEarth.createLookAt('');
    la.set(36.52, 127.50,
      0, // altitude
      googleEarth.ALTITUDE_RELATIVE_TO_GROUND,
      0, // heading
      0, // straight-down tilt
      533930 // range (inverse of zoom)
    );
    googleEarth.getView().setAbstractView(la);
    google.earth.addEventListener(googleEarth.getWindow(), 'mouseover', function (event) {
        if (event.getTarget().getType() == 'KmlPlacemark' && event.getTarget().getGeometry().getType() == 'KmlPoint') {
            event.preventDefault();
            var placemark = event.getTarget();
            var balloon = googleEarth.createHtmlStringBalloon('');
            var id = placemark.getId();
            var name = placemark.getName();
            var type = placemark.getSnippet();
            if (id != '') {
                //ballon.setMaxHeight(100);
                balloon.setFeature(placemark);
                balloon.setCloseButtonEnabled(true);
                if (type == 'RF') {
                    balloon.setContentString("<iframe src='/Main/PopBalloon/?id=" + id + "&name=" + name + "&type=" + type + "&ControllerTime=" + ControllerTime + "' width=230 height=150 frameborder=0 scrolling='no'></iframe>");
                } else {
                    balloon.setContentString("<iframe src='/Main/PopBalloon/?id=" + id + "&name=" + name + "&type=" + type + "&ControllerTime=" + ControllerTime + "' width=100 height=20 frameborder=0 scrolling='no'></iframe>");
                }
                googleEarth.setBalloon(balloon);
            }
        }
    });
    google.earth.addEventListener(googleEarth.getWindow(), 'click', function (event) {
        if (event.getTarget().getType() == 'KmlPlacemark' && event.getTarget().getGeometry().getType() == 'KmlPoint') {
            event.preventDefault();
            var placemark = event.getTarget();
            var balloon = googleEarth.createHtmlStringBalloon('');
            var id = placemark.getId();
            var name = placemark.getName();
            var type = placemark.getSnippet();
            if (id != '') {
                //ballon.setMaxHeight(100);
                balloon.setFeature(placemark);
                balloon.setCloseButtonEnabled(true);
                if (type == 'RF') {
                    balloon.setContentString("<iframe src='/Main/PopBalloon/?id=" + id + "&name=" + name + "&type=" + type + "&ControllerTime=" + ControllerTime + "' width=230 height=150 frameborder=0 scrolling='no'></iframe>");
                } else {
                    balloon.setContentString("<iframe src='/Main/PopBalloon/?id=" + id + "&name=" + name + "&type=" + type + "&ControllerTime=" + ControllerTime + "' width=100 height=20 frameborder=0 scrolling='no'></iframe>");
                }
                googleEarth.setBalloon(balloon);
            }
        }
    });

    loadKml('HDIMS.kml', 'main');
    recalMin();

}

function CloseBalloon() {
    googleEarth.setBalloon(null);
}

var recalMin = function () {
    //alert(ModifyTime);
    CloseBalloon();
    $.ajax({
        url: '/Main/GetRadarTimes?ModifyTime=' + ModifyTime,
        success: function (data) {
            var recs = data.Data;
            cTimeLines = recs;
            for (var i = 0; i < recs.length; i++) {
                var obsdt = recs[i].OBSDT; //201104012200
                var tm = obsdt.substring(8, 10) + ":" + obsdt.substring(10, 12);
                $("#bar_text" + i).text(tm);
                fulltime[i] = obsdt;
            }
            ControllerTime = recs[NowBarNo].OBSDT;
            //$("#conyear").text(obsdt.substring(0, 4));
            $("#conday").text(obsdt.substring(0, 4) + "." + obsdt.substring(4, 6) + "." + obsdt.substring(6, 8));
            //alert(tm);
            loadKmlByTime(recs[NowBarNo].OBSDT);
            if (firstRecalMin) {
                firstRecalMin = false;
                lookAtDam();
            }
        }
    });
}

function lookAtDam() {
    var f_longitude = parseFloat(g_longitude);
    var f_latitude = parseFloat(g_latitude);
    var f_range = parseFloat(g_range);
    if (f_range < 80000) f_range = 80000;
    //alert(f_longitude + "," + f_latitude + "," + f_range);
    if (f_longitude > 0 && f_latitude > 0 && f_range > 0) {
        var la = googleEarth.createLookAt('');
        la.set(f_latitude, f_longitude,
          0, // altitude
          googleEarth.ALTITUDE_RELATIVE_TO_GROUND,
          0, // heading
          0, // straight-down tilt
          f_range // range (inverse of zoom)
        );
        googleEarth.getView().setAbstractView(la);
    }
}

function loadKmlByTime(time) {
	cKmlLoadTime = time;
	var file,type;
	for(var i=0; i < cKmlTypes.length; i++) {
	    type = cKmlTypes[i];
		file = getPathFromTime(type);
		toggleKml(file,type);
	}
}

function loadKmlByType(type) {
    var file = getPathFromTime(type);
    toggleKml(file, type);
}

function loadKmlPerTimeLine(type, No) {
    cKmlLoadTime = cTimeLines[No].OBSDT;
    //alert(type + ":" + No + ":" + cKmlLoadTime);
    var file = getPathFromTime(type);
    toggleKml(file, type);
}

function getPathFromTime(type) {
	var path = cKmlLoadTime.substring(0,4)+"/"+cKmlLoadTime.substring(4,6)+"/"+cKmlLoadTime.substring(6,8);
	if(type=='radar') {
	    return path + "/" + cKmlRadarType + "_" + cKmlLoadTime + ".kml";
	} else if(type=='tissen') {
	    return path + "/tissen_" + cKmlLoadTime + ".kml";
	} else if (type == "legend") {
	    return "legend_" + cKmlRadarType + ".kml";
	} else {
		return type+".kml";
    }
}

//function loadKmlByRadarType(type) {
//    cKmlRadarType = type;
//    var chk = $("#CH_radar").attr("checked");
//    if (chk == "checked") {
//        var file = getPathFromTime('radar');
//        toggleKml(file, 'radar');
//    }
//    toggleKml('legend_'+cKmlRadarType+'.kml', 'legend');
//}

function getServerUrl() {
    var protocol = document.location.protocol;
    var domain = document.domain;
    var port = document.location.port;
    var url = "http://" + domain;
    if (port != "") url += ":" + port;
    return url;
}

function MoveLookAt(LONGITUDE, LATITUDE, CONTENT, TYPE) {
    var la = googleEarth.createLookAt('');
    la.set(LATITUDE, LONGITUDE,
      0, // altitude
      googleEarth.ALTITUDE_RELATIVE_TO_GROUND,
      0, // heading
      0, // straight-down tilt
      40000 // range (inverse of zoom)
    );
    googleEarth.getView().setAbstractView(la);
    if (CONTENT != "") {
        var div = document.createElement('DIV');
        div.innerHTML = CONTENT;

        var balloon = googleEarth.createHtmlDivBalloon('');
        balloon.setFeature(window.placemark);
        balloon.setContentDiv(div);
        balloon.setMaxWidth(220);
        googleEarth.setBalloon(balloon);
    }

}

function loadRadarOverlay(imgUrl) {
    var url = getServerUrl();
    url += "/kml/" + imgUrl;
    try {
        if (cKmlDatas["radar"]) {
            if(cKmlDatas["radara"].kobj)
                googleEarth.getFeatures().removeChild(cKmlDatas["radara"].kobj);
            cKmlDatas["radar"] = null;
        }
        var go = googleEarth.createGroundOverlay('');
        var icon = googleEarth.createIcon('');
        icon.setHref(url);
        go.setIcon(icon);
        var latLonBox = googleEarth.createLatLonBox('');
        latLonBox.setBox(40.32, 33.03, 131.00, 121.60, 0);
        go.setLatLonBox(latLonBox);
        googleEarth.getFeatures().appendChild(go);
        cKmlDatas['radar'] = { 'time': cKmlLoadTime, 'kobj': go };
    } catch (e) {
       // alert(e);
        cKmlDatas['radar'] = null;
    }
}

function toggleKml(file, type) {
    removePrevNode(type);
   // alert(type + ": " + file);
    if (type != "legend") {
        var chk = $("#CH_" + type).attr("checked");
        if (chk == "checked")
            loadKml(file, type);
    } else {
        loadKml(file, type);
    }
}



function loadKml(file, type) {
    var url = getServerUrl();
    url += "/kml/" + file;
    google.earth.fetchKml(googleEarth, url, function (kobj) {
        removePrevNode(type);
        if (kobj) {
            googleEarth.getFeatures().appendChild(kobj);
            cKmlDatas[type] = { 'time': cKmlLoadTime, 'kobj': kobj };
            //console.log("loadtime : " + cKmlLoadTime + ",type : " + type);
        } else {
            cKmlDatas[type] = null;
            setTimeout(function () {
                //alert('잘못된 KML URL입니다.');
            }, 0);
        }
    });
}

function removePrevNode(type) {
    if (cKmlDatas[type] && cKmlDatas[type].kobj) {
        googleEarth.getFeatures().removeChild(cKmlDatas[type].kobj);
        cKmlDatas[type] = null;
    }
}
function gearthFailureCB(object) {
    //alert('load failed');
}

function gearthSubmitLocation() {
    var address = document.getElementById('address').value;
    geocoder.getLatLng(address,
		function (point) {
			if (point && googleEarth != null) {
			    var la = googleEarth.createLookAt('');
			    la.set(point.y, point.x, 100, googleEarth.ALTITUDE_RELATIVE_TO_GROUND, 0, 0, 4000);
			    googleEarth.getView().setAbstractView(la);
			}
		}
	);
}
