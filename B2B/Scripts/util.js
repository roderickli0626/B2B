function showWait(preloader) {
    var div = $('<div class="div-wait"/>').css({
        'cursor': 'wait',
        'z-index': 100000,
        'height': '100%',
        'width': '100%',
        'left': 0,
        'position': 'fixed',
        'top': 0,
        'text-align': 'center',
    })
    $('body').css("overflow", "hidden").append(div);

    if (preloader) {
        div.append($('<div style="background: black;width: 80px;padding: 15px 0;text-align: center;border-radius: 6px;opacity: 0.8;display: inline-block;position: absolute;top: 50%;transform: translate(0, -50%);"><img src="assets/vendor/jquery-loading/img/ajax-loading.gif">'))
    }
}

function hideWait() {
    $('body').css("overflow", "");
    $('.div-wait').remove();
}

function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');
    for (var i = 0; i < sURLVariables.length; i++) {
        var sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] == sParam) {
            return decodeURIComponent(sParameterName[1]);
        }
    }
}

function escapeHtml(string) {
    var entityMap = {
        '&': '&amp;',
        '<': '&lt;',
        '>': '&gt;',
        '"': '&quot;',
        "'": '&#39;',
        '/': '&#x2F;',
        '`': '&#x60;',
        '=': '&#x3D;',
        '\n': '<br />'
    };
    return String(string).replace(/[&<>"'`=\/\n]/g, function (s) {
        return entityMap[s];
    });
}

function findIndex(array, key, value) {
    var found = -1;
    $.each(array || [], function (i, item) {
        if (key && item[key] == value) {
            found = i;
            return false;
        } else if (!key && item == value) {
            found = i;
            return false;
        }
    });
    return found;
}
function findObject(array, key, value) {
    var found = null;
    $.each(array || [], function (i, item) {
        if (key && item[key] == value) {
            found = item;
            return false;
        } else if (!key && item == value) {
            found = item;
            return false;
        }
    });
    return found;
}

var defopt = {
    times: 1,
    point: 2,
    float: false,
    emptyZero: false,
};

function zeroString(count) {
    var str = '', i;
    for (i = 0; i < count; i++) {
        str += '0';
    }
    return str;
}

function formatDecimal(val, options) {
    var opt = $.extend({}, defopt, options);
    val = val || 0;
    if (val == 0 && opt.emptyZero) return '';

    var fmt = '0,0';
    if (opt.float) {
        fmt = '0,0.[0]';
    } else {
        if (opt.point > 0) {
            //fmt += '.[' + zeroString(opt.point) + ']';
            fmt += '.' + zeroString(opt.point);
        }
    }
    
    return numeral(val).divide(opt.times || 1).format(fmt);
}

function formatCurrency(val, options) {
    var opt = $.extend({}, defopt, options);
    val = val || 0;
    if (val == 0 && opt.emptyZero) return '';

    var fmt = '$0,0';
    if (opt.point > 0) {
        fmt += '.' + zeroString(opt.point);
    }
    return numeral(val / (opt.times || 1)).format(fmt)
}

function calculateContentSize(content) {
    var windowHeight = $(window).height();
    var contentTop = content.offset().top;
    var height = windowHeight - contentTop;
    return height;
}

function isVideoFile(ext) {
    ext = (ext || '').toLowerCase();
    return ext == '.mp4' || ext == '.avi' || ext == '.mov' || ext == '.3gp' ||
        ext == '.m4v' || ext == '.ogv' || ext == '.webm';
}

function isWordFile(ext) {
    ext = (ext || '').toLowerCase();
    return ext == '.doc' || ext == '.docx';
}
function isPdfFile(ext) {
    ext = (ext || '').toLowerCase();
    return ext == '.pdf';
}

function parseJson(obj, def) {
    try {
        return JSON.parse(obj);
    } catch (e) {
    }
    return def;
}

function checkDateRangeType(filter) {
    filter = (filter || '').toLowerCase();
    if (filter == 'cm' || filter == 'lm' || filter == 'cq' || filter == 'lq' ||
        filter == 'cfy' || filter == 'lfy' || filter == '12m') return filter;
    return 'all';
}

function getDateRangeByType(filter)
{
    var mcur = moment();
    var mstart, mend;

    if (filter == 'cm')
    {
        mstart = mcur.clone().startOf('month');
        mend = mcur.clone().endOf('month');
    }
    else if (filter == 'lm')
    {
        mstart = mcur.clone().add(-1, 'month').startOf('month');
        mend = mcur.clone().add(-1, 'month').endOf('month');
    }
    else if (filter == 'cq')
    {
        mstart = mcur.clone().startOf('quarter');
        mend = mcur.clone().endOf('quarter');
    }
    else if (filter == 'lq')
    {
        mstart = mcur.clone().add(-1, 'quarter').startOf('quarter');
        mend = mcur.clone().add(-1, 'quarter').endOf('quarter');
    }
    else if (filter == 'cfy')
    {
        if (mcur.month() <= 5)                  // Months are from 0 to 11
        {
            mstart = moment([mcur.year()-1, 6, 1]);
            mend = moment([mcur.year(), 5, 30]);
        }
        else
        {
            mstart = moment([mcur.year(), 6, 1]);
            mend = moment([mcur.year() + 1, 5, 30]);
        }
    }
    else if (filter == 'lfy')
    {
        if (mcur.month() <= 5)                  // Months are from 0 to 11
        {
            mstart = moment([mcur.year()-2, 6, 1]);
            mend = moment([mcur.year()-1, 5, 30]);
        }
        else
        {
            mstart = moment([mcur.year()-1, 6, 1]);
            mend = moment([mcur.year(), 5, 30]);
        }
    }
    else
    {
        mstart = mcur.clone().add(-1, 'year');
        mend = mcur.clone();
    }
    return [mstart.format('DD/MM/YYYY'), mend.format('DD/MM/YYYY')];
}

function isFileDragging(e) {
    if (e.dataTransfer && e.dataTransfer.types) {
        for (var i = 0; i < e.dataTransfer.types.length; i++) {
            if (e.dataTransfer.types[i] == "Files") {
                return true;
            }
        }
    }

    return false;
}

function validateEmail(email) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email.toLowerCase());
}


function getBrowserInfo() {

    var ua = window.navigator.userAgent.toLowerCase();

    var match = /(edge)\/([\w.]+)/.exec(ua) ||
        /(opr)[\/]([\w.]+)/.exec(ua) ||
        /(chrome)[ \/]([\w.]+)/.exec(ua) ||
        /(version)(applewebkit)[ \/]([\w.]+).*(safari)[ \/]([\w.]+)/.exec(ua) ||
        /(webkit)[ \/]([\w.]+).*(version)[ \/]([\w.]+).*(safari)[ \/]([\w.]+)/.exec(ua) ||
        /(webkit)[ \/]([\w.]+)/.exec(ua) ||
        /(opera)(?:.*version|)[ \/]([\w.]+)/.exec(ua) ||
        /(msie) ([\w.]+)/.exec(ua) ||
        ua.indexOf("trident") >= 0 && /(rv)(?::| )([\w.]+)/.exec(ua) ||
        ua.indexOf("compatible") < 0 && /(mozilla)(?:.*? rv:([\w.]+)|)/.exec(ua) ||
        [];

    var platform_match = /(ipad)/.exec(ua) ||
        /(ipod)/.exec(ua) ||
        /(iphone)/.exec(ua) ||
        /(kindle)/.exec(ua) ||
        /(silk)/.exec(ua) ||
        /(android)/.exec(ua) ||
        /(windows phone)/.exec(ua) ||
        /(win)/.exec(ua) ||
        /(mac)/.exec(ua) ||
        /(linux)/.exec(ua) ||
        /(cros)/.exec(ua) ||
        /(playbook)/.exec(ua) ||
        /(bb)/.exec(ua) ||
        /(blackberry)/.exec(ua) ||
        [];

    var browser = {},
        matched = {
            browser: match[5] || match[3] || match[1] || "",
            version: match[2] || match[4] || "0",
            versionNumber: match[4] || match[2] || "0",
            platform: platform_match[0] || ""
        };

    if (matched.browser) {
        browser[matched.browser] = true;
        browser.version = matched.version;
        browser.versionNumber = parseInt(matched.versionNumber, 10);
    }

    if (matched.platform) {
        browser[matched.platform] = true;
    }

    // These are all considered mobile platforms, meaning they run a mobile browser
    if (browser.android || browser.bb || browser.blackberry || browser.ipad || browser.iphone ||
        browser.ipod || browser.kindle || browser.playbook || browser.silk || browser["windows phone"]) {
        browser.mobile = true;
    }

    // These are all considered desktop platforms, meaning they run a desktop browser
    if (browser.cros || browser.mac || browser.linux || browser.win) {
        browser.desktop = true;
    }

    // Chrome, Opera 15+ and Safari are webkit based browsers
    if (browser.chrome || browser.opr || browser.safari) {
        browser.webkit = true;
    }

    // IE11 has a new token so we will assign it msie to avoid breaking changes
    // IE12 disguises itself as Chrome, but adds a new Edge token.
    if (browser.rv || browser.edge) {
        var ie = "msie";

        matched.browser = ie;
        browser[ie] = true;
    }

    // Blackberry browsers are marked as Safari on BlackBerry
    if (browser.safari && browser.blackberry) {
        var blackberry = "blackberry";

        matched.browser = blackberry;
        browser[blackberry] = true;
    }

    // Playbook browsers are marked as Safari on Playbook
    if (browser.safari && browser.playbook) {
        var playbook = "playbook";

        matched.browser = playbook;
        browser[playbook] = true;
    }

    // BB10 is a newer OS version of BlackBerry
    if (browser.bb) {
        var bb = "blackberry";

        matched.browser = bb;
        browser[bb] = true;
    }

    // Opera 15+ are identified as opr
    if (browser.opr) {
        var opera = "opera";

        matched.browser = opera;
        browser[opera] = true;
    }

    // Stock Android browsers are marked as Safari on Android.
    if (browser.safari && browser.android) {
        var android = "android";

        matched.browser = android;
        browser[android] = true;
    }

    // Kindle browsers are marked as Safari on Kindle
    if (browser.safari && browser.kindle) {
        var kindle = "kindle";

        matched.browser = kindle;
        browser[kindle] = true;
    }

    // Kindle Silk browsers are marked as Safari on Kindle
    if (browser.safari && browser.silk) {
        var silk = "silk";

        matched.browser = silk;
        browser[silk] = true;
    }

    // Assign the name and platform variable
    browser.name = matched.browser;
    browser.platform = matched.platform;

    return browser;
}

function isBootsrapSupported(browser) {
    if (browser.safari) {
        if (browser.desktop && browser.win)
            return false;
        if (browser.mobile && browser.android)
            return false;

    }

    return true;
}

function IsInternetExplorer() {
    var version = detectIE();
    if (version === false) {
        return false;
    } else if (version >= 12) {
        return false;
    } else {
        return true;
    }
}

function detectIE() {
    var ua = window.navigator.userAgent;

    // Test values; Uncomment to check result …

    // IE 10
    // ua = 'Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; Trident/6.0)';

    // IE 11
    // ua = 'Mozilla/5.0 (Windows NT 6.3; Trident/7.0; rv:11.0) like Gecko';

    // Edge 12 (Spartan)
    // ua = 'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.71 Safari/537.36 Edge/12.0';

    // Edge 13
    // ua = 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2486.0 Safari/537.36 Edge/13.10586';

    var msie = ua.indexOf('MSIE ');
    if (msie > 0) {
        // IE 10 or older => return version number
        return parseInt(ua.substring(msie + 5, ua.indexOf('.', msie)), 10);
    }

    var trident = ua.indexOf('Trident/');
    if (trident > 0) {
        // IE 11 => return version number
        var rv = ua.indexOf('rv:');
        return parseInt(ua.substring(rv + 3, ua.indexOf('.', rv)), 10);
    }

    var edge = ua.indexOf('Edge/');
    if (edge > 0) {
        // Edge (IE 12+) => return version number
        return parseInt(ua.substring(edge + 5, ua.indexOf('.', edge)), 10);
    }

    // other browser
    return false;
}

function roundHalf(num) {
    if (isNaN(num)) num = 0;
    return Math.ceil((num || 0) * 2) / 2;
}