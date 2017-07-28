Ext.websolus = function () {
    var msgCt;

    function createBox(t, s) {
        // return ['<div class="msg">',
        //         '<div class="x-box-tl"><div class="x-box-tr"><div class="x-box-tc"></div></div></div>',
        //         '<div class="x-box-ml"><div class="x-box-mr"><div class="x-box-mc"><h3>', t, '</h3>', s, '</div></div></div>',
        //         '<div class="x-box-bl"><div class="x-box-br"><div class="x-box-bc"></div></div></div>',
        //         '</div>'].join('');
        return '<div class="msg"><h3>' + t + '</h3><p>' + s + '</p></div>';
    }
    return {
        msg: function (title, format) {
            if (!msgCt) {
                msgCt = Ext.core.DomHelper.insertFirst(document.body, { id: 'msg-div' }, true);
            }
            var s = Ext.String.format.apply(String, Array.prototype.slice.call(arguments, 1));
            var m = Ext.core.DomHelper.append(msgCt, createBox(title, s), true);
            m.hide();
            m.slideIn('t').ghost("t", { delay: 1000, remove: true });
        },

        init: function () {
            //            var t = Ext.get('exttheme');
            //            if(!t){ // run locally?
            //                return;
            //            }
            //            var theme = Cookies.get('exttheme') || 'aero';
            //            if(theme){
            //                t.dom.value = theme;
            //                Ext.getBody().addClass('x-'+theme);
            //            }
            //            t.on('change', function(){
            //                Cookies.set('exttheme', t.getValue());
            //                setTimeout(function(){
            //                    window.location.reload();
            //                }, 250);
            //            });
            //
            //            var lb = Ext.get('lib-bar');
            //            if(lb){
            //                lb.show();
            //            }
        }
    };
} ();

var Cookies = {};
Cookies.set = function (name, value) {
    var argv = arguments;
    var argc = arguments.length;
    var expires = (argc > 2) ? argv[2] : null;
    var path = (argc > 3) ? argv[3] : '/';
    var domain = (argc > 4) ? argv[4] : null;
    var secure = (argc > 5) ? argv[5] : false;
    document.cookie = name + "=" + escape(value) +
       ((expires == null) ? "" : ("; expires=" + expires.toGMTString())) +
       ((path == null) ? "" : ("; path=" + path)) +
       ((domain == null) ? "" : ("; domain=" + domain)) +
       ((secure == true) ? "; secure" : "");
};

Cookies.get = function (name) {
    var arg = name + "=";
    var alen = arg.length;
    var clen = document.cookie.length;
    var i = 0;
    var j = 0;
    while (i < clen) {
        j = i + alen;
        if (document.cookie.substring(i, j) == arg)
            return Cookies.getCookieVal(j);
        i = document.cookie.indexOf(" ", i) + 1;
        if (i == 0)
            break;
    }
    return null;
};

Cookies.clear = function (name) {
    if (Cookies.get(name)) {
        document.cookie = name + "=" +
    "; expires=Thu, 01-Jan-70 00:00:01 GMT";
    }
};

Cookies.getCookieVal = function (offset) {
    var endstr = document.cookie.indexOf(";", offset);
    if (endstr == -1) {
        endstr = document.cookie.length;
    }
    return unescape(document.cookie.substring(offset, endstr));
};

//Active X에 가려진 콤보박스의 확장을 Active X 의 위로 표시되게 하기 위한 펑션
// 추가할 코드 : <iframe id="myframe" frameborder=0 style="z-index:1000;display:none;border:none;margin:0;padding:0;position:absolute;width:100%;height:100%;top:0;left:0;filter:Alpha(Opacity=85);" src="about:blank"></iframe>
function ExpandOverActiveX(field, eOpts) {
    var pck = field.getPicker();
    var currentWidth = pck.el.getWidth();
    var currentHeight = pck.el.getHeight();

    Ext.get("myframe").setTop(pck.pageY);
    Ext.get("myframe").setLeft(pck.pageX);
    Ext.get("myframe").setWidth(currentWidth);
    Ext.get("myframe").setHeight(currentHeight);
    Ext.get("myframe").show();
}

function recontrolGrid(grid, top) {
    try {
        if (top != null && Ext.isNumber(top))
            grid.setScrollTop(top);
        var ow = grid.getWidth();
        grid.setWidth(ow - 1);
        grid.setWidth(ow);
    } catch (e) {
    }
}

function getChartRange(dobMax, dobMin) {
    var iRet = {};
    if (dobMax == 0 && dobMin == 0) {
        iRet.max = 0.5;
        iRet.min = -0.2;
    }
    else if (dobMax == dobMin) {
        if (Math.abs(dobMax) < 0.1) {
            iRet.max = dobMax + 0.01;
            iRet.min = dobMin - 0.04;
        }
        else if (Math.abs(dobMax) < 1) {
            iRet.max = dobMax + 0.07;
            iRet.min = dobMin - 0.09;
        }
        else if (Math.abs(dobMax) < 10) {
            iRet.max = dobMax + 0.9;
            iRet.min = dobMin - 1.5;
        } 
        else if (Math.abs(dobMax) < 50) {
            iRet.max = dobMax + 1.5;
            iRet.min = dobMin - 2.5;
        }
        else if (Math.abs(dobMax) < 100) {
            iRet.max = dobMax + 2.5;
            iRet.min = dobMin - 3.5;
        }
        else if (Math.abs(dobMax) < 500) {
            iRet.max = dobMax + 20.5;
            iRet.min = dobMin - 30.5;
        }
        else if (Math.abs(dobMax) < 1000) {
            iRet.max = dobMax + 50.5;
            iRet.min = dobMin - 80.5;
        }
        else {
            iRet.max = dobMax + 100.5;
            iRet.min = dobMin - 90.5;
        }
    }
    else {
        if (Math.abs(dobMax - dobMin) < 0.1) {
            iRet.max = dobMax + 0.01;
            iRet.min = dobMin - 0.04;
        }
        else if (Math.abs(dobMax - dobMin) < 1) {
            iRet.max = dobMax + 0.07;
            iRet.min = dobMin - 0.09;
        }
        else if (Math.abs(dobMax - dobMin) < 10) {
            iRet.max = dobMax + 0.9;
            iRet.min = dobMin - 1.5;
        }
        else if (Math.abs(dobMax - dobMin) < 50) {
            iRet.max = dobMax + 12.5;
            iRet.min = dobMin - 16.5;
        }
        else if (Math.abs(dobMax - dobMin) < 100) {
            iRet.max = dobMax + 20.5;
            iRet.min = dobMin - 30.5;
        }
        else if (Math.abs(dobMax - dobMin) < 500) {
            iRet.max = dobMax + 80.5;
            iRet.min = dobMin - 90.5;
        }
        else if (Math.abs(dobMax - dobMin) < 1000) {
            iRet.max = dobMax + 200.5;
            iRet.min = dobMin - 300.5;
        }
        else {
            iRet.max = dobMax + 1000.5;
            iRet.min = dobMin - 1200.5;
        }

    }

    return iRet;
}



function getXmlDomFromString(xmlStr) {
    if (window.ActiveXObject && window.GetObject) {
        // for Internet Explorer
        var dom = new ActiveXObject('Msxml.DOMDocument');
        dom.loadXML(xmlStr);
        return dom;
    }
    if (window.DOMParser) { // for other browsers
        return new DOMParser().parseFromString(xmlStr, 'text/xml');
    }
    throw new Error('No XML parser available');

}

function isNumber(n) { return !isNaN(parseFloat(n)) && isFinite(n); }


function commify(n) {
    var reg = /(^[+-]?\d+)(\d{3})/;   // 정규식
    n += '';                          // 숫자를 문자열로 변환

    while (reg.test(n))
        n = n.replace(reg, '$1' + ',' + '$2');

    return n;
}

