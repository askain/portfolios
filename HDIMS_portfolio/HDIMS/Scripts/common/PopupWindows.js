function showDamLegend() {
    jQuery.popupWindow2({
        windowName: "DamMonitorLegend",
        width: 850,
        height: 450,
        windowURL: '/Code/DAM_Legend',
        centerScreen: 1,
        resizable: 0
    });
}

function showLegend(legendType) {
    jQuery.popupWindow2({
        windowName: "wlLegend",
        width: 1000,
        height: 500,
        windowURL: '/Code/WLRF_Legend/?type=' + legendType,
        centerScreen: 1,
        resizable: 1
    });
}

function showAlarmLegend() {
    jQuery.popupWindow2({
        windowName: "AlarmLegend",
        width: 1000,
        height: 650,
        windowURL: '/Code/ALARM_Legend',
        centerScreen: 1,
        resizable: 1
    });
}

function showEquipLegend() {
    jQuery.popupWindow2({
        windowName: "EquipLegend",
        width: 1000,
        height: 650,
        windowURL: '/Code/EQUIP_Legend',
        centerScreen: 1,
        resizable: 1
    });
}

function showTeleLegend() {
    jQuery.popupWindow2({
        windowName: "TeleMonitorLegend",
        width: 250,
        height: 450,
        windowURL: '/Main/TeleMonitorLegend',
        centerScreen: 1,
        resizable: 0
    });
}

function popupRadar() {
    jQuery.popupWindow2({
        windowName: "GoogleEarth",
        width: 900,
        height: 700,
        windowURL: '/Main/GoogleEarth',
        centerScreen: 1,
        resizable: 1,
        scrollbars: 2
    });
}

function popupCurrentUsers() {
    jQuery.popupWindow2({
        windowName: "CurrentUsers",
        width: 450,
        height: 350,
        windowURL: '/Main/CurrentUsers',
        centerScreen: 1,
        resizable: 1,
        scrollbars: 2
    });
}


var linkToCims = function (damcd, selDt) {
    var damCd = damcd;
    var selectDt = selDt;

    jQuery.popupWindow2({
        windowName: "cims",
        width: 1100,
        height: 700,
        windowURL: 'http://cims.kwater.or.kr/Main/DataSearch2/?damcd=' + damCd + '&obsdt=' + selectDt,
        //windowURL: 'http://localhost:59123/Main/DataSearch2/?damcd=' + damCd + '&obsdt=' + selectDt,   //테스트용
        centerScreen: 1,
        resizable: 1
    });
};

var showReport = function (url) {
    jQuery.popupWindow2({
        windowName: "ReportView",
        width: 1024,
        height: 700,
        windowURL: url,
        centerScreen: 1,
        resizable: 1,
        scrollbars: 2
    });
};

var OpenOasisDocPopup = function (docidxid) {
    /*
    //결재완료문서용 파라미터인데 //안됌.
    var uri = 'http://oasis.kwater.or.kr/handydocs/confhtml/hcltex.jsp?';
    uri += 'USS=1&USU=21049804&K=0000C2RYT2';
    uri += '&_NOARG=' + (new Date()).getTime();
    uri += '&APPRIDLIST=' + docidxid;
    uri += '&APPRDEPTID=65537.1738';
    uri += '&APPRIDXID=' + docidxid;
    uri += '&CLTAPP=1';
    uri += '&APPRSTATUSLIST=256';
    uri += '&APPRTYPELIST=7';
    uri += '&MENUMASKLIST=YNNNN';
    uri += '&SIGNERTYPELIST=';
    uri += '&WORDTYPE=3';
    uri += '&WEBIP=oasis.kwater.or.kr';
    uri += '&XSCLTMODE=0';
    uri += '&DOCINFOIDX=';
    uri += '&APPLID=6020';
    */

//    //결재진행중인 문서용 파라미터
//    var uri = 'http://oasis.kwater.or.kr/handydocs/confhtml/hcltex.jsp?';
//    uri += 'USS=1';
//    uri += '&USU=21049804';
//    uri += '&K=0000C2RYT2';
//    uri += '&_NOARG=' + (new Date()).getTime();
//    uri += '&APPRIDLIST=' + docidxid;
//    uri += '&APPRDEPTID=65537.1738';
//    uri += '&APPRIDXID=' + docidxid;
//    uri += '&CLTAPP=1';
//    uri += '&APPRSTATUSLIST=1';
//    uri += '&APPRTYPELIST=7';
//    uri += '&MENUMASKLIST=NNNNN';
//    uri += '&SIGNERTYPELIST=4097';
//    uri += '&WORDTYPE=3';
//    uri += '&WEBIP=oasis.kwater.or.kr';
//    uri += '&XSCLTMODE=0';
//    uri += '&DOCINFOIDX=';
//    uri += '&APPLID=2020';

    var uri = "http://oasis.kwater.or.kr/custom/view_external.jsp?DOCID=" + docidxid

    jQuery.popupWindow2({
        windowName: "Oasis",
        width: 100,
        height: 100,
        windowURL: uri,
        centerScreen: 1,
        resizable: 1
    });
}