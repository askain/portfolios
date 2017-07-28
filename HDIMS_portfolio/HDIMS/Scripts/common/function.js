var glGetDamCdList = function (damTp, firstValue) {
    var list = [],
        len = gl_damcodes.length,
        j = 0;

    if (firstValue != undefined) {
        list[j] = { text: firstValue, value: "" };
        j++;
    }


    for (var i = 0; i < len; i++) {
        if (damTp && damTp != "") {
            if (gl_damcodes[i].DAMTP == damTp) {
                list[j] = { text: gl_damcodes[i].DAMNM, value: gl_damcodes[i].DAMCD };
                j++;
            }
        } else {
            list[j] = { text: gl_damcodes[i].DAMNM, value: gl_damcodes[i].DAMCD };
            j++;
        }
    }
    return list;
};

var glGetMgtCdList = function (damTp, firstValue) {
    var list = [],
        len = glDamMgtInfo.length,
        j = 0;

    if (firstValue != undefined) {
        list[j] = { text: firstValue, value: "" };
        j++;
    }

    for (var i = 0; i < len; i++) {
        if (damTp && damTp != "") {
            if (glDamMgtInfo[i].DAMTYPE == damTp) {
                list[j] = { text: glDamMgtInfo[i].MGTNM, value: glDamMgtInfo[i].MGTCD };
                j++;
            }
        } else {
            list[j] = { text: glDamMgtInfo[i].MGTNM, value: glDamMgtInfo[i].MGTCD };
            j++;
        }
    }
    return list;
};


var glGetDamTpList = function (isBo, firstValue) {
    var list = [],
                len = gl_alldamtypes.length,
        j = 0;

    if (firstValue != undefined) {
        list[j] = { text: firstValue, value: "" };
        j++;
    }

    for (var i = 0; i < len; i++) {
        if (isBo == true) {
            if (gl_alldamtypes[i].DAMTYPE != "B") {
                list[j] = { text: gl_alldamtypes[i].DAMTPNM, value: gl_alldamtypes[i].DAMTYPE };
                j++;
            }
        } else {
            list[j] = { text: gl_alldamtypes[i].DAMTPNM, value: gl_alldamtypes[i].DAMTYPE };
            j++;
        }
    }
    return list;
};

//현재 유저의 기본 댐구분
var glGetDefaultDamTp = function () {
    if (glUserInfo.authCd == '01') return "";

    var defaultDamTp = "D";
    for (var i = 0; i < gl_damcodes.length; i++) {
        if (glUserInfo.MgtDamcd.indexOf(gl_damcodes[i].DAMCD) != -1) {
            defaultDamTp = gl_damcodes[i].DAMTP
            break;
        }
    }
    return defaultDamTp;
};

//현재 유저의 기본 관리 댐코드
var glGetDefaultDamCd = function () {
    if (glUserInfo.MgtDamcd == '') return '';
    //console.log(glUserInfo.MgtDamcd);
    var defaultDamCd = glUserInfo.MgtDamcd.split(',')[0];
    return defaultDamCd;
};

// 현재유저가 데이터를 수정할 권한이 있는가?
var gl_IsThisUserAbleToChangeData = function (damcd) {
    var authenticatedAuthCd = "01,03";
    //console.log(glUserInfo.MgtDamcd);
    if (glUserInfo.authCd == '01' || glUserInfo.MgtDamcd=='MAIN') {    // 관리자권한인 경우, 이거나 본사 직원일 경우
        return true;
    } else if (glUserInfo.authCd == '03' && glUserInfo.MgtDamcd.indexOf(damcd) != -1) {
        return true;
    }
    return false;
}

var glGetDamTp = function (damCd) {
    var damTp = 'D';
    for (var i = 0; i < gl_damcodes.length; i++) {
        if (damCd.indexOf(gl_damcodes[i].DAMCD) != -1) {
            damTp = gl_damcodes[i].DAMTP
            break;
        }
    }
    return damTp;
};

var glGetObsCdList = function (obsTp, damCd, firstValue) {
    var list = [],
        len = gl_obscodes.length,
        j = 0;

    if (obsTp == null) {
        obsTp == "";
    }
    if (damCd == null) {
        damcd = "";
    }
    if (firstValue != undefined) {
        list[j] = { text: firstValue, value: "" };
        j++;
    }

    for (var i = 0; i < len; i++) {
        var compareDamCd = gl_obscodes[i].DAMCD;
        var compareObsTp = gl_obscodes[i].OBSTP;
        if (damCd.indexOf(",") == -1) {
            if (compareDamCd.indexOf(damCd) != -1 && compareObsTp.indexOf(obsTp) != -1) {
                list[j] = { text: gl_obscodes[i].OBSNM, value: gl_obscodes[i].OBSCD };
                j++;
            }
        } else {
            if (damCd.indexOf(compareDamCd) != -1 && compareObsTp.indexOf(obsTp) != -1) {
                list[j] = { text: gl_obscodes[i].OBSNM, value: gl_obscodes[i].OBSCD };
                j++;
            }
        }
    }

    return list;
}

var GetAllDamCodeList = function (damTp, firstValue) {
    var list = [],
        len = gl_alldamcodes.length,
        j = 0;

    if (firstValue != undefined) {
        list[j] = { text: firstValue, value: "" };
        j++;
    }


    for (var i = 0; i < len; i++) {
        if (damTp && damTp != "") {
            if (gl_alldamcodes[i].DAMTP == damTp) {
                list[j] = { text: gl_alldamcodes[i].DAMNM, value: gl_alldamcodes[i].DAMCD };
                j++;
            }
        } else {
            list[j] = { text: gl_alldamcodes[i].DAMNM, value: gl_alldamcodes[i].DAMCD };
            j++;
        }
    }
    return list;
}
//glGetDamTpList
var GetAllDamTpList = function (isBo, firstValue) {
    var list = [],
                len = gl_alldamtypes.length,
        j = 0;

    if (firstValue != undefined) {
        list[j] = { text: firstValue, value: "" };
        j++;
    }

    for (var i = 0; i < len; i++) {
        if (isBo == true) {
            if (gl_alldamtypes[i].DAMTYPE != "B") {
                list[j] = { text: gl_alldamtypes[i].DAMTPNM, value: gl_alldamtypes[i].DAMTYPE };
                j++;
            }
        } else {
            list[j] = { text: gl_alldamtypes[i].DAMTPNM, value: gl_alldamtypes[i].DAMTYPE };
            j++;
        }
    }
    return list;
}

var setOptions = function (id, options) {
    var sel = document.getElementById(id), newOpts;
    var optCnt = sel.options.length;
    for (var i = optCnt - 1; i >= 0; i--) {
        sel.options[i] = null;
    }
    for (var i = 0; i < options.length; i++) {
        newOpt = new Option(options[i].text, options[i].value);
        sel.options[i] = newOpt;
        if (options[i].selected)
            sel.options[i].selected = true;
    }
}
var setSelectOption = function (id, val) {
    var sel = document.getElementById(id), len = sel.options.length;
    for (var i = 0; i < len; i++) {
        if (sel.options[i].value == val) { sel.options[i].selected = true; return true; }
    }
}



var convStrToDate = function (strDate) {
    if (strDate == null || strDate.length == 0)
        return "";

    if (strDate.length == 8) {
        return strDate.substring(0, 4) + "-" + strDate.substring(4, 6) + "-" + strDate.substring(6, 8);
    } else if (strDate.length == 10) {
        return strDate.substring(0, 4) + "-" + strDate.substring(4, 6) + "-" + strDate.substring(6, 8) + " " + strDate.substring(8, 10);
    } else if (strDate.length == 12) {
        return strDate.substring(0, 4) + "-" + strDate.substring(4, 6) + "-" + strDate.substring(6, 8) + " " + strDate.substring(8, 10) + ":" + strDate.substring(10, 12);
    } else if (strDate.length == 14) {
        return strDate.substring(0, 4) + "-" + strDate.substring(4, 6) + "-" + strDate.substring(6, 8) + " " + strDate.substring(8, 10) + ":" + strDate.substring(10, 12) + ":" + strDate.substring(12, 14);
    }

    return "";
}

var convStrToDateMin = function (strDate) {
    if (strDate == null || strDate.length == 0)
        return "";
    if (strDate.length > 10) {
        return strDate.substring(0, 4) + "-" + strDate.substring(4, 6) + "-" + strDate.substring(6, 8) + " " + strDate.substring(8, 10) + ":" + strDate.substring(10, 12);
    } else if (strDate.length == 10) {
        return strDate.substring(0, 4) + "-" + strDate.substring(4, 6) + "-" + strDate.substring(6, 8) + " " + strDate.substring(8, 10);
    } else if (strDate.length == 8) {
        return strDate.substring(0, 4) + "-" + strDate.substring(4, 6) + "-" + strDate.substring(6, 8);
    } else if (strDate.length == 6) {
        return strDate.substring(0, 4) + "-" + strDate.substring(4, 6);
    }
}

var formatTextDate = function (value) {
    return "<div style='mso-number-format:\\@;'>" + formatDate(value) + "</div>";
};

var formatTextDate2 = function (value) {
    return "<div style='mso-number-format:\\@;'>" + convStrToDateMin(value) + "</div>";
};

var setSelectable = function (e, selectable) {
    if (selectable) {
        e.onselectstart = null;
        e.style.MozUserSelect = "text";
        e.style.KhtmlUserSelect = "text";
        e.unselectable = "off"
    } else {
        e.onselectstart = function () { return false; };
        e.style.MozUserSelect = "none";
        e.style.KhtmlUserSelect = "none";
        e.unselectable = "on";
    }
}

var showNewWindow = function (sUrl, arg, params, isModal) {
    var win;
    if (isModal && isModal == true)
        win = window.showModalDialog(sUrl, arg, params);
    else
        win = window.showModelessDialog(sUrl, arg, params);
    return win;
}

var hidePreloader = function () {
    document.getElementById("preloader").style.display = "none";
};

var showLoadPopupMask = function () { //
    document.getElementById("loadPopupMask").style.display = "block";
}

var hideLoadPopupMask = function () { //
    document.getElementById("loadPopupMask").style.display = "none";
}
/* slow script alert 가 나오지 않게 한다.  아래는 사용법 */
//var i = 0;
//var ro = new RepeatingOperation(function () {
//    <코드:반복해서 실행할 코드>
//    if (++i < <숫자:총반복할횟수> ) { ro.step(); }
//    else { <코드:종료후 실행할 코드> }
//}, <숫자:연속적으로 반복할 횟수. 횟수만큼 반복한 다음 0.001초 쉼>);
//ro.step();
var RepeatingOperation = function (op, yieldEveryIteration) {
    var count = 0;
    var instance = this;
    this.step = function (args) {
        if (++count >= yieldEveryIteration) {
            count = 0;
            setTimeout(function () { op(args); }, 10, [])
            return;
        }
        op(args);
    };
};

var trim = function (s) {
    //alert(typeof (s));
    if (s != undefined && typeof (s) == "string")
        return s.replace(/^\s+|\s+$/g, "");
    return "";
};

var isFloat = function (s) {
    var n = trim(s);
    return n.length > 0 && !(/[^0-9.]/).test(n) && (/\.\d/).test(n);
};

var isNumber = function (s) {
    var n = trim(s);
    return n.length > 0 && +n == n;
};

var glNumberPattern = "###,###,###,###.#####";
var glNumberFormat = function (val) {
    return dojo.number.format(val, { pattern: glNumberPattern });
}

var glNumberFormat2 = function (val, digit) {
    if (!val || val == "") return val;
    var pat = "###,###,###,###";
    if (digit > 0) { pat += "."; for (var i = 1; i <= digit; i++) { pat += "0"; } }
    //console.log(val + ":" + pat);
    return dojo.number.format(val, { pattern: pat });
}

var glNumberFormat3 = function (val,pat) {
    if (!val || val == "")  return val;
    if (pat) {
        return dojo.number.format(val, { pattern: pat });
    }
    return dojo.number.format(val, { pattern: glNumberPattern });
}

function getStatDateRange(str) {
    //console.log(str);
    var sDate = str.replace(/\-/, '');
    var eDate = sDate;
    if (sDate.length > 0) {
        if (isNaN(sDate)) { //분기일 경우
            if (sDate.length == 8 && "년" == sDate.substr(4, 1) && "분기" == sDate.substr(6, 2)) {
                var year = sDate.substr(0, 4);
                var period = sDate.substr(5, 1);
                if (period == "1") {
                    sDate = year + "0101";
                    eDate = year + "0331";
                }
                else if (period == "2") {
                    sDate = year + "0401";
                    eDate = year + "0630";
                }
                else if (period == "3") {
                    sDate = year + "0701";
                    eDate = year + "0930";
                }
                else if (period == "4") {
                    sDate = year + "1001";
                    eDate = year + "1231";
                }
            }
            else {
                return; // 그외의 경우
            }
        } else { //숫자만 있을 경우
            if (sDate.length == 6) {
                var mon = parseInt(sDate.substr(4, 2).replace('0', ''));
                eDate = sDate + daysInMonth(mon, sDate.substr(0, 4));
                sDate = sDate + "01";
            }
        }
        var ret = {
            sDate: sDate,
            eDate: eDate
        };
        return ret;
    }
}

//***************************************************************************************************'
// 마지막 날짜를 구함
function daysInMonth(month, year) {
    var dd = new Date(year, month, 0);
    return dd.getDate();
}

//***************************************************************************************************'
// Run Command, run a command in the cmd environment 
// 실행하기 위해서는 IE 옵션을 변경해야 함.
function runCmd(cmdString) {
    var char34 = String.fromCharCode(34);
    var wsh = new ActiveXObject('WScript.Shell');
    if (wsh) {
        var command = 'cmd /c ' + char34 + cmdString + char34;
        wsh.Run(command);
    }
}

//***************************************************************************************************'
// 파일을 작성
function WriteFile(fpath, content) {
    var fso = new ActiveXObject("Scripting.FileSystemObject");
    var fileObject = fso.OpenTextFile(fpath, 2, true, 0); //TristateTrue -1 Opens the file as Unicode /TristateFalse  0 Opens the file as ASCII /TristateUseDefault -2 Use default system setting 
    fileObject.write(content)
    fileObject.close()

}