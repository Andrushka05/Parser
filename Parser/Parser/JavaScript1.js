function showPhone(event) {
    var e = event || window.event, btn = e.target || e.srcElement;
    avito.request.send('GET', btn.href + '?async', function (text) {
        var a = document.createElement('a'), gaActionImages = document.getElementsByClassName('gaPhoneAction'), i;
        a.className = 'm_item_call m_item_call_link';
        a.innerHTML = text;
        a.href = 'tel:' + text.replace(/[^\d]/g, '');
        btn.parentNode.replaceChild(a, btn);
        for (i = 0; i < gaActionImages.length; ++i) {
             gaActionImages[i].src = gaActionImages[i].getAttribute('deferredSrc');
        }
        if ('undefined' !== typeof item_id) {
             avito.tracking.trackCampanjaEvent({ 'Event_Type': 'Item_Contact', 'Contact_Type': 'phone' }, { 'Item_ID': item_id, 'Category_ID': item_category_id, 'Root_Category_ID': item_root_category_id, 'Location_ID': item_location_id }, {});
        }
    }); return false;
};

avito.request = (function () {
    "use strict";
    var getXHR,
        activeXObjectNames = ['Msxml2.XMLHTTP', 'Microsoft.XMLHTTP'],
        i,
        controller = { available: false, send: function (method, url, callback) { } };
    function testActiveXObject(name) {
        try { new ActiveXObject(name); return true; } catch (e) { }
        return false;
    }
    if (!!window.XMLHttpRequest) {
        getXHR = function() { return new window.XMLHttpRequest(); };
    } else {
         for (i = 0; i < activeXObjectNames.length; ++i) {
              if (testActiveXObject(activeXObjectNames[i])) {
                   (function (name) {
                        getXHR = function () {
                             return new ActiveXObject(name);
                        };
                   })(activeXObjectNames[i]); break;
              }
         }
    }
    if (getXHR) {
        controller.available = true;
        controller.send = function (method, url, callback) {
            var xhr = getXHR();
            xhr.open(method, url, true);
            xhr.onerror = function () { callback(); };
            xhr.onreadystatechange = function () {
                 if (4 === xhr.readyState) {
                      if (200 === xhr.status || 304 === xhr.status) {
                           callback(xhr.responseText);
                      } else {
                           callback();
                      }
                 }
            }; xhr.send();
        };
    }
    return controller;
}());





var avito = avito || {};
avito.tracking = (function (win, doc) {
    "use strict";
    var headElement = doc.getElementsByTagName('head')[0], controller = {};
    win._gaq = win._gaq || [];
    win._campanja_config = { 'preload': [] };
    win._campanja_track = function (props, values, ids) { win._campanja_config.preload.push([props, values, ids]); };
    function insertScript(src) {
        var scriptElement = doc.createElement('script');
        scriptElement.type = 'text/javascript';
        scriptElement.async = true;
        scriptElement.src = src;
        headElement.appendChild(scriptElement);
    }
    controller.disabled = { ga: false, campanja: false };
    controller.trackCampanja = function (tg, domain) {
        if (controller.disabled.campanja) { return false; }
        win._campanja_config.tg = tg;
        win._campanja_config.internal = new RegExp('^http://[^/]*' + domain.replace('.', '\\.'), 'i');
        insertScript('//rtt.campanja.com/script');
    };
    controller.trackCampanjaEvent = function (props, values, ids) { controller.disabled.campanja || win._campanja_track(props, values, ids); };
    controller.trackGAUrl = function (customUrl) {
        var pageViewTrackProperty = '_trackPageview';
        if (controller.disabled.ga) { return false; }
        if (customUrl) { win._gaq.push([pageViewTrackProperty, customUrl]); }
        else { win._gaq.push([pageViewTrackProperty]); }
    };
    controller.trackGA = function (account, settings) {
        if (controller.disabled.ga) { return false; }
        win._gaq.push(['_setAccount', account]); if ('undefined' !== typeof settings && '[object Array]' === Object.prototype.toString.call(settings)) { for (var i = 0, slen = settings.length; i < slen; ++i) { win._gaq.push(settings[i]); } }
        insertScript('//stats.g.doubleclick.net/dc.js');
    };
    controller.trackGAEvent = function (category, action, label, value) { controller.disabled.ga || win._gaq.push(['_trackEvent', category, action, label, value]); };
    return controller;
}(window, document));


if (navigator.userAgent.match(/iPhone/i) || navigator.userAgent.match(/iPad/i)) {
                          var viewportmeta = document.querySelector('meta[name="viewport"]');
                          if (viewportmeta) {
                              viewportmeta.content = 'width=device-width, minimum-scale=1.0, maximum-scale=1.0';
                              document.body.addEventListener('gesturestart', function() { viewportmeta.content = 'width=device-width, minimum-scale=0.25, maximum-scale=1.6'; }, false);
                          }
                      }
    

(function () {
    function l(a) {
        var c = [], b; for (b in a) h(a, b) && c.push(b + "=" + encodeURIComponent(a[b]));
        return c.join("&")
    }
    function m(a, c) {
        var b = {}, d; for (d in a) h(a, d) && (b[c(d)] = a[d]);
        return b
    }
    function h(a, c) {
        var b;
        Object.prototype.hasOwnProperty ? b = a.hasOwnProperty(c) : (b = a.__proto__ || a.constructor.prototype, b = c in a && (!(c in b) || b[c] !== a[c]));
        return b
    }
    if (!_campanja_config.tg) throw "invalid _campanja_config (missing tg)";
    var n = function (a, c, b) {
        var d = new Date; d.setDate(d.getDate() + b);
        c = escape(c) + (null == b ? "" : "; expires=" + d.toUTCString()) + "; path=/"; document.cookie = a + "=" + c
    }, 
    j = function (a) {
        var c = a + "=", b;
        0 < document.cookie.length && (a = document.cookie.indexOf(c), -1 !== a && (a += c.length, b = document.cookie.indexOf(";", a), -1 === b && (b = document.cookie.length), b = unescape(document.cookie.substring(a, b)))); return b
    },
    d = _campanja_config.tracker_host ? _campanja_config.tracker_host : "rtt.campanja.com",
    f = document.location.protocol,
    k = f + "//" + d + "/pv",
    r = f + "//" + d + "/visit",
    s = f + "//" + d + "/action",
    d = navigator.cookieEnabled || "cookie" in document && (0 < document.cookie.length || -1 < (document.cookie = "test").indexOf.call(document.cookie, "test")),
    p = !1,
    f = !1,
        t = _campanja_config.tg,
        u = _campanja_config.override_href || document.location.href,
        v = document.referrer,
        w = function () {
            var a = j("campanja_id");
            if (a) return a;
            var a = _campanja_config.cookie_days,
                c;
            c = [document.location.href, document.referrer, navigator.userAgent, screen.width, screen.height].join();
            var b = 5381;
            for (e = 0; e < c.length; e++) character = c.charCodeAt(e), b = (b << 5) + b + character;
            c = Math.abs(b);
            var b = Math.floor(4294967296 * Math.random()), d = (new Date).getTime();
            c = ["0", b, c, d].join("-");
            n("campanja_id", c, a ? a : 10680);
            return (a = j("campanja_id")) ? (p = !0, a) : "anonymous"
        }(),
        g = j("campanja_session_id");
    if (!g || 0 == g.length) f = !0,
    g = "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(/[xy]/g, function (a) { var c = 16 * Math.random() | 0; return ("x" === a ? c : c & 3 | 8).toString(16) }),
    n("campanja_session_id", g);
    var q = { tg: t, href: u, referrer: v, "vids[]": w, session_id: g, c: d, new_id: p, new_session_id: f };
    if (d = "" != document.referrer)
        d = self.location.hostname,
        /^www\./i.exec(d) || (d = "(www.|)" + d),
        d = d.replace(".", "\\."),
        d = RegExp("https?://" + d + "/", "i"),
        d = (_campanja_config.internal ? _campanja_config.internal : d).exec(document.referrer);
    k = (d ? k : r) + ("?" + l(q));
    (new Image).src = k;
    _campanja_track = function (a, c, b) {
        var d = s;
        a = m(a, function (a) { return "p[" + a + "]" });
        var e = m(c, function (a) { return "v[" + a + "]" });
        c = [];
        if (b)
            for (var f = 0; f < b.length; f++)
                1 < b[f].length && c.push("evid-" + b[f]);
        b = { rnd: Math.random() };
        a = [q, a, e, b];
        b = {};
        for (e = 0; e < a.length; e++)
            for (var g in a[e]) h(a[e], g) && (b[g] = a[e][g]);
        g = "?" + l(b); b = [""];
        for (a = 0; a < c.length; a++) b.push("vids[]=" + encodeURIComponent(c[a]));
        c = b.join("&");
        (new Image).src = d + (g + c)
    };
    if (_campanja_config.preload)
        for (var e = 0; e < _campanja_config.preload.length; e++)
            _campanja_track.apply(this, _campanja_config.preload[e])
})();


