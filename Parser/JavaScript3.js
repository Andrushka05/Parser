(function () {
    function g(a) {
        throw a;
    }
    var h = true,
        i = null,
        n = false,
        o, p, q, Event, t, v, w, x, y, z, A, B = {}.hasOwnProperty;

    function D(a, f) {
        function d() {
            this.constructor = a
        }
        for (var b in f) B.call(f, b) && (a[b] = f[b]);
        d.prototype = f.prototype;
        a.prototype = new d;
        a.wa = f.prototype
    }
    o = function (a, f, d, b, c) {
        c == i && (c = A);
        c.n(a);
        c.l(a);
        c.N(a);
        c.o(f);
        c.z(f);
        c.p(f.length, 255);
        c.n(b);
        c.l(b);
        this.ce = a;
        this.bp = f;
        this.bq = b;
        d ? (c.Ua(d), this.bs = d) : this.bs = {}
    };
    //перевёл в c#
    p = function () {
        function a() { }
        //Шифрует  json
        a.ia = function (a)//a="{"ba":1382731282855,"bc":-1,"eventCounter":0,"purchaseCounter":0,"errorCounter":0,"timedEvents":[]}"
        {
            var d, b, c, e, j, k, l, m, r, a = this.Ob(a);//"{"ba":1382731282855,"bc":-1,"eventCounter":0,"purchaseCounter":0,"errorCounter":0,"timedEvents":[]}"
            l = "";
            for (c = m = 0, r = a.length; m < r; c = m += 3) d = a.charCodeAt(c), b = a.charCodeAt(c + 1), c = a.charCodeAt(c + 2), e = d >> 2, d = (d & 3) << 4 | b >> 4, j = (b & 15) << 2 | c >> 6, k = c & 63, isNaN(b) ? j = k = 64 : isNaN(c) && (k = 64), l += "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".charAt(e) + "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".charAt(d) + "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".charAt(j) + "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".charAt(k);
            return l //eyJiYSI6MTM4MjczMTI4Mjg1NSwiYmMiOi0xLCJldmVudENvdW50ZXIiOjAsInB1cmNoYXNlQ291bnRlciI6MCwiZXJyb3JDb3VudGVyIjowLCJ0aW1lZEV2ZW50cyI6W119
        };
        //Заменяет значение кук _fs
        a.lb = function (a) {
            var d, b, c, e, j, k, l, m, a = a.replace(/[^A-Za-z0-9\+\/\=]/g, "");
            k = "";
            for (j = l = 0, m = a.length; l < m; j = l += 4) d = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".indexOf(a.charAt(j)), b = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".indexOf(a.charAt(j + 1)), e = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".indexOf(a.charAt(j + 2)), j = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".indexOf(a.charAt(j + 3)), d = d << 2 | b >>
                4, b = (b & 15) << 4 | e >> 2, c = (e & 3) << 6 | j, k += String.fromCharCode(d), e !== 64 && (k += String.fromCharCode(b)), j !== 64 && (k += String.fromCharCode(c));
            //k="{"ba":1382719047026,"bc":-1,"eventCounter":0,"purchaseCounter":0,"errorCounter":0,"timedEvents":[]}"
            return k = this.Nb(k)
        };
        a.Ob = function (a) {
            var d, b, c, e, a = a.replace(/\r\n/g, "\n");
            b = "";
            for (d = c = 0, e = a.length; 0 <= e ? c < e : c > e; d = 0 <= e ? ++c : --c)
                d = a.charCodeAt(d), d < 128 ? b += String.fromCharCode(d) : (d > 127 && d < 2048 ? b += String.fromCharCode(d >> 6 | 192) : (b += String.fromCharCode(d >> 12 | 224), b += String.fromCharCode(d >> 6 & 63 | 128)), b += String.fromCharCode(d & 63 | 128));
            return b
        };
        //j="{"ba":1382719047026,"bc":-1,"eventCounter":0,"purchaseCounter":0,"errorCounter":0,"timedEvents":[]}"
        a.Nb = function (a) {
            var d, b, c,
                e, j;
            j = "";
            for (e = 0; e < a.length;)
                d = a.charCodeAt(e),
                d < 128 ? (j += String.fromCharCode(d), e++) : d > 191 && d < 224 ? (b = a.charCodeAt(e + 1), j += String.fromCharCode((d & 31) << 6 | b & 63), e += 2) : (b = a.charCodeAt(e + 1),
                c = a.charCodeAt(e + 2), j += String.fromCharCode((d & 15) << 12 | (b & 63) << 6 | c & 63), e += 3);
            return j
        };
        return a
    }();
    q = function () {
        function a() { }
        //проверочная сумма предаётся как параметры c= к ссылке флюри
        a.bb = function (a) {
            var d, b, c, e, j;
            d = 1;
            b = 0;
            for (c = e = 0, j = a.length; 0 <= j ? e < j : e > j; c = 0 <= j ? ++e : --e) c = a.charCodeAt(c), d += c, b += d;
            d %= 65521;
            b %= 65521;
            return b * 65536 + d
        };
        return a
    }();
    Event = function (a) {
        function f(d, a, c, e, j, k) {
            k == i && (k = A);
            k.M(e);
            f.wa.constructor.call(this, d, a, c, j);
            this.timed = e;
            this.br = 0
        }
        D(f, a);
        return f
    }(o);
    t = function () {
        function a() { }
        var f;
        //Конвертируем "eyJiYSI6MTM4MjcxOTA0NzAyNiwiYmMiOi0xLCJldmVudENvdW50ZXIiOjAsInB1cmNoYXNlQ291bnRlciI6MCwiZXJyb3JDb3VudGVyIjowLCJ0aW1lZEV2ZW50cyI6W119" 
        //в Json "{"ba":1382719047026,"bc":-1,"eventCounter":0,"purchaseCounter":0,"errorCounter":0,"timedEvents":[]}"
        f = function (a) {
            var b, c;
            c = {};
            try {
                b = p.lb(a), c = JSON.parse(b)
            } catch (e) {
                console.log(e)
            }
            return c
        };
        a.ga = function (a, b) {
            return (b ? this.Da : "") + {
                install: "fit",
                session: "fs",
                lastPoll: "flp",
                firstPartyCookie: "fid"
            }[a]
        };
        a.i = h;
        a.appVersion = ""; //"mobile" устанавливается в a.da
        a.O = "";
        a.Ha = 0;
        a.ka = Date.now();
        a.Da = i;
        a.F = i;
        a.Ka = h;
        a.Bb = h;
        a.Qa = n;
        a.Ea = n;
        a.localStorage = n;
        a.fa = i;
        a.Ja = "";
        a.referrer = "";
        a.J = 0;
        a.e = i;
        a.$ = [];
        a.w = i;
        a.Y = 5E3;
        a.ea = i;
        a.I = n;
        a.Z = i;
        a.h = function (a) {
            if (this.Qa) return typeof a === "string" ? this.g("" + a) : this.g("" +
                a.name + "=>" + a.message)
        };
        a.g = function (a) {
            if (this.Ea) return console.log("Flurry " + new Date + ": " + a)
        };
        a.Ub = function () { };
        a.ub = function () {
            this.g("getFlurryAgentVersion() called");
            return 9
        };
        //вызывается из страницы строчкой FlurryAgent.setAppVersion("mobile");  a=mobile
        a.da = function (a) {
            this.g("setAppVersion(" + a + ") called");
            try {
                return A.o(a), A.z(a), A.p(a.length, 255), this.appVersion = a, this.e && this.e.da(a), this.i = h //this.e.da(a)-> bd="mobile"
            } catch (b) {
                return this.h(b)
            }
        };
        a.ua = function (a) {
            this.g("setUserId(" + a + ") called");
            try {
                return A.j(this.e), A.o(a), A.z(a), A.p(a.length, "userId"), this.e.ua(a), this.i = h
            } catch (b) {
                return this.h(b)
            }
        };
        a.ra = function (a) {
            this.g("setGender(" + a + ") called");
            try {
                return A.j(this.e), A.o(a), A.Ta(a, {
                    m: 1,
                    f: 0
                }), this.e.ra(a), this.i = h
            } catch (b) {
                return this.h(b)
            }
        };
        a.qa = function (a) {
            this.g("setAge(" + a + ") called");
            try {
                return A.j(this.e), A.n(a), A.l(a), A.N(a), A.va(a), this.e.qa(a), this.i = h
            } catch (b) {
                return this.h(b)
            }
        };
        a.sa = function (a, b, c) {
            c == i && (c = 0);
            this.g("setLocation(" + a + "," + b + "," + c + ") called");
            try {
                return A.j(this.e), A.n(a), A.l(a), A.n(b), A.l(b), A.n(c), A.l(c), this.e.sa(a, b, c), this.i = h
            } catch (e) {
                return this.h(e)
            }
        };
        a.mb =
            function () {
                this.Ja = navigator.platform
            };
        a.nb = function () {
            this.referrer = document.referrer
        };
        a.ib = function () {
            this.mb();
            this.nb()
        };
        a.L = function (a) {
            this.g("setSessionContinueSeconds(" + a + ") called");
            try {
                if (A.n(a), A.l(a), A.N(a), A.va(a), this.J = a, this.e) return this.e.L(a)
            } catch (b) {
                return this.h(b)
            }
        };
        a.Jb = function (a) {
            this.g("setShowErrorInLogEnabled(" + a + ") called");
            try {
                return A.M(a), this.Qa = a
            } catch (b) {
                return this.h(b)
            }
        };
        a.Gb = function (a) {
            this.g("setDebugLogEnabled(" + a + ") called");
            try {
                return A.M(a), this.Ea = a
            } catch (b) {
                return this.h(b)
            }
        };
        a.Ib = function (a) {
            this.g("setLocalStorageEnabled(" + a + ") called");
            try {
                A.M(a), this.localStorage = a
            } catch (b) {
                this.h(b)
            }
        };
        a.kb = function () {
            var a, b, c = this;
            b = window.onfocus;
            window.onfocus = function () {
                c.g("ACTIVE");
                b != i && b();
                if (c.e) return c.e.Ma() ? c.ta() : (c.A(), c.Ra(c.O))
            };
            a = window.onblur;
            window.onblur = function () {
                var b;
                c.g("PAUSE");
                a != i && a();
                if (c.e && (b = Date.now(), c.e.Pa(b), c.Q(), c.Bb)) return c.q()
            }
        };
        a.jb = function () {
            var a;
            (a = parseInt(this.G("install"))) ? this.ka = a : this.K("install", this.ka)
        };
        a.Ca = function () {
            var a;
            (a = this.G("firstPartyCookie", n)) ? this.F = a : this.F !== i && this.K("firstPartyCookie", this.F, n)
        };
        a.Fb = function () {
            this.Da = p.ia(this.O).replace("=", "") + "_"
        };
        //Вытаскивает из кук _fit=i, _fid
        a.G = function (a, b) {
            var c, e, f, k, l, m, r;
            b == i && (b = h);
            l = this.ga(a, b);
            e = document.cookie.split(";");
            for (m = 0, r = e.length; m < r; m++)
                if (c = e[m], k = c.indexOf("="), f = c.slice(0, k).trim(), f === l) return c.slice(k + 1);
            return i
        };
        a.eb = function () {
            return this.G("install") != i
        };
        //Удаление кук
        a.ha = function (a) {
            var b;
            b == i && (b = h);
            return document.cookie = "" + this.ga(a, b) + "=; expires='Thu, 01 Jan 1970 00:00:00 GMT'; path=/"
        };
        //установка кук 
        a.K = function (a, b, c) {
            var e;
            c == i && (c = h);
            e = new Date;
            e.setTime(Date.now() + 31536E7); //+51721959(10 лет)
            document.cookie = this.ga(a, c) + "=" + b + "; expires=" + e.toGMTString() + "; path=/"
        };
        a.ta = function (a) {
            var b = this;
            try {
                a && A.va(a * 1E3 - 5E3), A.j(this.e)
            } catch (c) {
                this.h(c);
                return
            }
            this.Q();
            if (a) this.Y = a * 1E3;
            this.fa = window.setInterval(function () {
                b.ea = Date.now();
                b.K("lastPoll", b.ea);
                return b.q()
            }, this.Y);
            this.ea && Date.now() - this.ea > this.Y && this.q();
            return this.Y / 1E3
        };
        a.Q = function () {
            this.fa && window.clearInterval(this.fa);
            this.fa = i
        };
        //Возвращает а с значениями по умолчанию, кроме пары полей таймзоны, ba,bj, ch
        a.$a =function () {
                var a, b, c;
                b = this.G("session");   //_fs=b
                a = this.G("lastPoll");  //_flp=a
                this.ha("session");
                this.ha("lastPoll");
                //i=null
                b != i && a != i && (this.g("found paused session"), b = f(b), b[z.c.r] = a, a = new z(b), this.J && a.L(this.J), a.Ma() && (this.g("paused session resumed"), c = a));
                c || (this.g("creating new session"), c = new z, this.J && c.L(this.J));
                this.appVersion && c.da(this.appVersion);
                return c  
        };
        //a=BYCR5JHJJDRQZK2VPDDQ       FlurryAgent.startSession("BYCR5JHJJDRQZK2VPDDQ");
        a.Ra = function (a) {
            this.g("startSession(" + a + ") called");
            try {
                if (!this.e) {
                    A.o(a);
                    A.z(a);
                    A.p(a, 255);
                    this.O = a;
                    this.Fb();
                    this.i = h;//true
                    this.e = this.$a();//Возвращает а с значениями по умолчанию, кроме пары полей таймзоны, ba,bj, ch
                    this.jb();//this.ka=приравнивается значение кук _fit
                    this.Ca();//this.F =_fid.value
                    if (!this.eb())//_fit.value!=null
                        this.Ka = n, this.A(), g("CookieError=>session ending since cookies not enabled!");
                    this.Ha || (this.ib(), this.kb()); //this.ib(this.Ja=navigator.platform "Win32", this.referrer = document.referrer)
                    this.ta();
                    this.q();//создаёт тег скрипт ссылающий на флюре
                    return this.Ha++
                }
            } catch (b) {
                return this.h(b)
            }
        };
        a.A = function () {
            this.g("endSession() called");
            try {
                if (this.Q(), this.e) return this.e.A(), this.i = h, this.Ka ? this.q() : this.$.push(this.e), this.e = i, this.ha("session")
            } catch (a) {
                return this.h(a)
            }
        };
        a.yb = function () {
            this.g("pauseSession() called");
            try {
                return this.Q(), this.Na(), this.e ? (this.q(), this.e = i) : this.g("no session to pause!")
            } catch (a) {
                return this.h(a)
            }
        };
        //Создаёт и устанавливает куки _fs(json зашифрованный), _flp(время)
        a.Na = function () {
            var a;
            try {
                A.j(this.e), a = Date.now(), this.e.Pa(a), this.Tb = a, this.K("session", this.e.Ya()), this.K("lastPoll", a)
            } catch (b) {
                this.h(b)
            }
        };
        //FlurryAgent.logEvent("environment", env)
        a.t = function (a, b, c) {
            b == i && (b = {});
            c == i && (c = n);
            this.g("logEvent(" + a + "," + b + "," + c + ") called");
            try {
                //проверка объекта е что он существует->
                return A.j(this.e), this.e.Wa(a, b, c), this.i = h
            } catch (e) {
                return this.h(e)
            }
        };
        a.qb = function (a, b) {
            b == i && (b = i);
            this.g("endTimedEvent(" + a + "," + b + ") called");
            try {
                return A.j(this.e), this.e.pb(a, b), this.i = h
            } catch (c) {
                return this.h(c)
            }
        };
        a.k = function () {
            var a, b, c, e, f;
            b = [];
            try {
                A.j(this.e);
                f = this.e[z.c.k];
                for (c = 0, e = f.length; c < e; c++) a = f[c], b.push(a.bp)
            } catch (k) {
                this.h(k)
            }
            return b
        };
        a.Hb = function (a) {
            this.g("setEventLogging(" + a + ") called");
            try {
                return A.j(this.e), A.M(a), this.e.ja = a
            } catch (b) {
                return this.h(b)
            }
        };
        a.U = function (a, b, c, e) {
            b == i && (b = 0);
            c == i && (c = "USD");
            e == i && (e = {});
            this.g("logPurchase(" + a + "," + b + "," + c + "," + e + ") called");
            try {
                return A.j(this.e), this.e.Xa(a, b, c, e), this.i = h
            } catch (f) {
                return this.h(f)
            }
        };
        a.wb = function (a, b, c) {
            c == i && (c = 0);
            this.g("logError(" + a + "," + b + "," + c + ") called");
            try {
                return A.j(this.e),
                this.e.Va(a, b, c), this.i = h
            } catch (e) {
                return this.h(e)
            }
        };
        //Создаёт тег скрипт ссылающий на флюре дата
        a.q = function () {
            var a;
            this.g("REQUEST INITIATED");
            try {
                if (!this.I) {
                    if (this.w) this.w.Kb();
                    else if (this.i) a = new y, a.Za(this.appVersion, this.O, this.ka, this.Ja, this.Rb, this.referrer, this.e, this.$, this.F), this.w = a.request, this.fb(a);
                    if (this.w) return this.Na(), this.rb(JSON.stringify(this.w))
                }
            } catch (b) {
                return this.h(b)
            }
        };
        a.fb = function (a) {
            this.e.gb(a.sessionIncluded.events, a.sessionIncluded.purchases, a.sessionIncluded.errors);
            this.$ = this.$.slice(a.sessionsIncluded);
            if (a.level === 0) this.i = n
        };
        //создаёт тэг скрипт с src на флюре дата
        a.rb = function (a) {
            var b, c = this;
            this.g("REQUEST EXECUTED");
            this.I = h;
            this.Z = window.setInterval(function () {
                return c.P(n)
            }, 1E4);
            b = document.createElement("script");
            b.type = "text/javascript";
            b.async = h;
            b.src = encodeURI(x.Fa + "?d=" + p.ia(a) + "&c=" + q.bb(a));
            a = document.getElementsByTagName("script");
            return a[0].parentNode.insertBefore(b, a[0])
        };
        a.Cb = function (a) {
            this.g("RESPONSE RECEIVED");
            try {
                this.I || g("ResponseError=>request considered timed out!");
                typeof a !== "object" && (this.P(n), g("ResponseError=>input is not a valid object!"));
                a.a != i && a.a > 0 ? this.P(h) : this.P(n);
                a.b != i && this.Ib(a.b);
                if (a.d != i) this.F = a.d, this.Ca(), this.i = h;
                if (this.i) return this.q()
            } catch (b) {
                return this.h(b)
            }
        };
        //очистка запроса
        a.P = function (a) {
            this.g("CLEAR REQUEST with " + a);
            if (this.I) {
                this.Z && window.clearInterval(this.Z);
                if (a) this.w = i;
                this.I = n;
                return this.Z = i
            }
        };
        return a
    }();
    window.FlurryAgent = t;
    t.setAppVersion = t.da;
    t.getFlurryAgentVersion = t.ub;
    t.setShowErrorInLogEnabled = t.Jb;
    t.setDebugLogEnabled = t.Gb;
    t.setSessionContinueSeconds = t.L;
    t.setRequestInterval = t.ta;
    t.startSession = t.Ra;
    t.endSession = t.A;
    t.pauseSession = t.yb;
    t.logEvent = t.t;
    t.endTimedEvent = t.qb;
    t.timedEvents = t.k;
    t.logError = t.wb;
    t.logPurchase = t.U;
    t.setUserId = t.ua;
    t.setAge = t.qa;
    t.setGender = t.ra;
    t.setLocation = t.sa;
    t.setEventLogging = t.Hb;
    t.requestCallback = t.Cb;
    v = function (a, f, d, b, c) {
        c == i && (c = A);
        c.n(a);
        c.l(a);
        c.N(a);
        c.o(f);
        c.z(f);
        c.p(f.length, 255);
        c.o(d);
        c.z(d);
        c.p(d.length, 255);
        c.n(b);
        c.l(b);
        c.N(b);
        this.cf = a;
        this.bz = f;
        this.ca = d;
        this.cb = b.toString();
        this.cc = Date.now()
    };
    //запрос a.request
    w = function (a) {
        function f(a, c, e, j, k, l, m) {
            m == i && (m = A);
            m.n(e);
            m.l(e);
            m.Qb(e);
            m.o(j);
            m.Ta(j, d);
            f.wa.constructor.call(this, a, c, k, l);
            this.bw = e * 100;
            this.bx = d[j]
        }
        var d;
        D(f, a);
        d = {
            USD: 0,
            EUR: 1,
            GBP: 2,
            CHF: 3
        };
        return f
    }(o);
    x = function () {
        //вызывается из y
        function a(b, c, e, j, k, l, m, r, s, C, u) {
            this.a = {};
            this.a.af = Date.now();
            this.a.aa = f;//1
            this.a.ab = b; //10
            this.a.ac = c;
            this.a.ae = e;
            this.a.ad = j;
            this.a.ag = k;
            this.a.ah = s[z.c.timestamp];
            a.g("about to check if firstPartyCookieUID " + u + " should be assigned to global map in the report request");
            if (u !== i)
                this.a.cg = u, a.g("firstPartyCookieUID " + u + " was assigned to global map"); //u=_fid.value
            if (s.requestsMade === 0) this.a.ai = l, this.a.aj = r, s.Db();
            this.a.ak = d;
            this.b = [];
            s && (s.Oa(), e = a.za(s), this.b.push(e));
            for (b = 0, c = C.length; b < c; b++) e =
                C[b], e = a.za(e), this.b.push(e)
        }
        var f, d;
        d = f = 1;
        a.Ab = {
            timedEvents: "timedEvents",
            eventLogging: "eventLogging",
            sessionContinue: "sessionContinue",
            pauseTimestamp: "pauseTimestamp",
            age: "age",
            numEventNames: "numEventNames",
            numPurchaseNames: "numPurchaseNames",
            requestsMade: "requestsMade",
            totalEventNames: "totalEventNames",
            totalPurchaseNames: "totalPurchaseNames",
            numEventsLogged: "numEventsLogged",
            numPurchasesLogged: "numPurchasesLogged",
            numErrorsLogged: "numErrorsLogged",
            eventCounter: "eventCounter",
            purchaseCounter: "purchaseCounter",
            errorCounter: "errorCounter"
        };
        a.zb = {
            timed: "timed"
        };
        a.Fa = "https://data.flurry.com/aah.do";
        a.g = function () { };
        a.Sb = function () {
            return this.Fa
        };
        a.hb = function (a) {
            var c, d, f;
            d = {};
            for (c in a) f = a[c], this.zb.hasOwnProperty(c) || (d[c] = f);
            return d
        };
        a.za = function (a) {
            var c, d, f, k, l, m;
            f = {};
            for (d in a)
                if (k = a[d], d === z.c.location)//bo
                    k.bg != i && (f[d] = k);
                else if (d === z.c.D) {//bf
                    f[d] = [];
                    for (l = 0, m = k.length; l < m; l++) c = k[l], f[d].push(this.hb(c))
                } else//если d совпадает с один ключом из списка ab то его не записываем в массив f
                    this.Ab.hasOwnProperty(d) || (f[d] = k);
            return f
        };
        a.prototype.Kb = function () {
            this.a.af = Date.now()
        };
        return a
    }();
    y = function () {
        function a() {
            this.sessionsIncluded = 0;
            this.sessionIncluded = {};
            this.request = {};
            this.level = 0
        }
        a.g = function () { };
        //получаем строку запроса
        a.prototype.Za = function (a, d, b, c, e, j, k, l, m) {
            this.request = new x(10, 9, a, d, b, c, e, j, k, l, m);//m=_fid.value
            for (d = []; ;) {
                this.level === 4 && g("RequestError=>request length is set too short!");
                a = JSON.stringify(this.request);//a="{"a":{"af":1382775648853,"aa":1,"ab":10,"ac":9,"ae":"","ad":"BYCR5JHJJDRQZK2VPDDQ","ag":1382371709662,"ah":1382731282855,"cg":"SG0978FE8DF36DE98AC942EC221DCD35404566F861","ai":"Win32","aj":"http://m.avito.ru/pskov/kvartiry","ak":1},"b":[{"bd":"","be":"","bk":-1,"bl":0,"bj":"ru","bo":[],"bm":false,"bn":{},"bv":[],"bt":false,"bu":{},"by":[],"cd":0,"ba":1382731282855,"bb":48714375,"bc":-1,"ch":"Etc/GMT-4"}]}"
                if (a.length * 4 / 3 <= 3E3) {//3E3=995
                    this.sessionsIncluded = this.request.b.length - 1;
                    this.sessionIncluded.events = this.request.b[0].bo.length;
                    this.sessionIncluded.purchases = this.request.b[0].bv.length;
                    this.sessionIncluded.errors =
                        this.request.b[0].by.length;
                    break;
                }
                d.push(this.request = this.xb(this.request))
            }
        };
        a.prototype.xb = function (a) {
            switch (this.level) {
                case 0:
                    a.b.length === 1 ? this.level = 1 : a.b = this.R(a.b);
                    break;
                case 1:
                    a.b[0].by.length === 0 ? this.level = 2 : a.b[0].by = this.R(a.b[0].by);
                    break;
                case 2:
                    a.b[0].bv.length === 0 ? this.level = 3 : a.b[0].bv = this.R(a.b[0].bv);
                    break;
                case 3:
                    a.b[0].bo.length === 0 ? this.level = 4 : a.b[0].bo = this.R(a.b[0].bo);
            }
            return a;
        };
        a.prototype.R = function (a) {
            var d;
            a.length >= 1 && (d = parseInt(a.length / 2), a = a.slice(0, d));
            return a
        };
        return a
    }();
    z = function () {
        function a(d) {
            d == i && (d = {});
            this[a.c.appVersion] = "";
            this[a.c.Sa] = "";  //be
            this[a.c.Ga] = -1; //bk
            this[a.c.xa] = 0;  //age
            this[a.c.ya] = 0;  //bl
            this[a.c.location] = {}; //bf
            this[a.c.vb] = a.sb();  //bj="ru"
            this[a.c.k] = d[a.c.k] || []; //timedEvents=timedEvents
            this[a.c.C] = d[a.c.C] || 0; //eventCounter=eventCounter
            this[a.c.D] = [];//bo
            this[a.c.V] = 0;//numEventsLogged
            this[a.c.Aa] = n;//bm=false
            this[a.c.T] = {};//totalEventNames
            this[a.c.s] = {};//bn
            this[a.c.ma] = 0;//numEventNames
            this[a.c.H] = d[a.c.H] || 0;//purchaseCounter=purchaseCounter
            this[a.c.X] = [];//bv
            this[a.c.W] = 0;//numPurchasesLogged
            this[a.c.Ba] = n;//bt=false
            this[a.c.oa] = {};//totalPurchaseNames
            this[a.c.v] = {};//bu
            this[a.c.na] = 0;//numPurchaseNames
            this[a.c.B] = d[a.c.B] || 0;//errorCounter=errorCounter
            this[a.c.S] = [];//by
            this[a.c.la] = 0;//numErrorsLogged
            this[a.c.Ia] = 0;//cd
            this.ba = d[a.c.timestamp] ||Date.now(); // =ba 
            this[a.c.duration] = 0;//bb
            this[a.c.ja] = h;//eventLogging=false
            this[a.c.pa] = f;//sessionContinue=300000
            this[a.c.r] = d[a.c.r] || 0;//pauseTimestamp=pauseTimestamp
            this[a.c.u] = d[a.c.u] || -1;//bc=bc
            this[a.c.La] = 0;//requestsMade
            this[a.c.Lb] = a.tb()//ch=(timeZone) -4
        }
        var f;
        a.c = {
            appVersion: "bd",
            Sa: "be",
            Ga: "bk",
            xa: "age",
            ya: "bl",
            location: "bf",
            vb: "bj",
            k: "timedEvents",
            C: "eventCounter",
            D: "bo",
            V: "numEventsLogged",
            Aa: "bm",
            T: "totalEventNames",
            s: "bn",
            ma: "numEventNames",
            H: "purchaseCounter",
            X: "bv",
            W: "numPurchasesLogged",
            Ba: "bt",
            oa: "totalPurchaseNames",
            v: "bu",
            na: "numPurchaseNames",
            B: "errorCounter",
            S: "by",
            la: "numErrorsLogged",
            Ia: "cd",
            timestamp: "ba",
            duration: "bb",
            ja: "eventLogging",
            pa: "sessionContinue",
            r: "pauseTimestamp",
            u: "bc",
            La: "requestsMade",
            Lb: "ch"
        };
        f = 3E5;
        a.sb = function () {
            var a;
            a = "";
            if (navigator.language != i) a = navigator.language;
            else if (navigator.Mb != i) a = navigator.Mb;
            return a
        };
        //определение таймзоны
        a.tb = function () {
            var a;
            a = (new Date).getTimezoneOffset() / 60;
            return "Etc/GMT" + (a > 0 ? "+" : "-") + Math.abs(a)
        };
        a.prototype.Db = function () {
            this[a.c.La]++
        };
        a.prototype.da = function (d) {
            return this[a.c.appVersion] = d
        };
        a.prototype.ua = function (d) {
            return this[a.c.Sa] = d
        };
        a.prototype.ra =
            function (d) {
                return this[a.c.Ga] = d === "f" ? 0 : 1
            };
        a.prototype.qa = function (d) {
            this.Eb(d);
            return this[a.c.xa] = d
        };
        a.prototype.Eb = function (d) {
            var b;
            b = new Date;
            this[a.c.ya] = (new Date(b.getFullYear() - d, b.getMonth(), 1)).getTime()
        };
        a.prototype.sa = function (d, b, c) {
            return this[a.c.location] = {
                bg: d,
                bh: b,
                bi: c
            }
        };
        a.prototype.L = function (d) {
            return this[a.c.pa] = d * 1E3
        };
        //bb=date.now-ba
        a.prototype.Oa = function () {
            this[a.c.duration] = Date.now() - this[a.c.timestamp] 
        };
        a.prototype.gb = function (d, b, c) {
            this[a.c.D] = this[a.c.D].slice(d);
            this[a.c.s] = {};
            this[a.c.X] = this[a.c.X].slice(b);
            this[a.c.v] = {};
            this[a.c.S] = this[a.c.S].slice(c)
        };
        a.prototype.A = function () {
            this.ob();
            this.Oa();
            this[a.c.V] < 1E3 && (this[a.c.Aa] = n);
            if (this[a.c.W] < 100) return this[a.c.Ba] = n
        };
        //pauseTimestamp=long time
        a.prototype.Pa = function (d) {
            this[a.c.r] = d
        };
        a.prototype.Ma = function () {
            var d;
            //d=pauseTimestamp>0?
            d = this[a.c.r] > 0 ? Date.now() - this[a.c.r] : 0;
            //          bc===-1
            d > 0 && (this[a.c.u] === -1 && (this[a.c.u] = 0), this[a.c.u] += d, this[a.c.r] = 0);
            return d < this[a.c.pa] //d<sessionContinue d<300000
        };
        //шифрует Json [ba,bc,eventCounter,purchaseCounter,errorCounter,timedEvents]
        a.prototype.Ya = function () {
            var d, b, c, e, f;
            d = {};
            c = [a.c.timestamp, a.c.u, a.c.C, a.c.H, a.c.B, a.c.k];//[ba,bc,eventCounter,purchaseCounter,errorCounter,timedEvents]
            for (e = 0, f = c.length; e < f; e++)
                b = c[e], d[b] = this[b];
            return p.ia(JSON.stringify(d))  
        };
        a.prototype.t = function (d) {
            if (this[a.c.ja] && this[a.c.V] < 1E3) return d.timed && !d.br ? this[a.c.k].push(d) : (this[a.c.D].push(d), this[a.c.V]++)
        };
        a.prototype.Wa = function (d, b, c) {
            this[a.c.C]++;//eventCounter++
            b = new Event(this[a.c.C], d, b, c, Date.now() - this[a.c.timestamp]);
            d in this[a.c.T] ? (this[a.c.T][d]++, d in this[a.c.s] ? this[a.c.s][d]++ : this[a.c.s][d] = 1, this.t(b)) : this[a.c.ma] < 100 ? (this[a.c.T][d] = this[a.c.s][d] = 1, this[a.c.ma]++, this.t(b)) : g("LogError=>unique name limit reached!")
        };
        a.prototype.pb = function (d, b) {
            var c, f, j, k, l;
            f = 0;
            l = this[a.c.k];
            for (j = 0, k = l.length; j < k; j++) {
                c = l[j];
                if (c.bp === d) {
                    c.br = Date.now() - (this[a.c.timestamp] + c.bq);
                    b && A.Ua(b) && (c.bs = b);
                    this.t(c);
                    break
                }
                f++
            }
            this[a.c.k].splice(f, 1)
        };
        a.prototype.ob = function () {
            var d, b, c, f;
            f = this[a.c.k];
            for (b = 0, c = f.length; b < c; b++) d = f[b], d.br = Date.now() - (this[a.c.timestamp] + d.bq), this.t(d);
            this[a.c.k] = []
        };
        a.prototype.U = function (d) {
            if (this[a.c.W] < 100) return this[a.c.X].push(d), this[a.c.W]++
        };
        a.prototype.Xa = function (d, b, c, f) {
            this[a.c.H]++;
            b = new w(this[a.c.H], d, b, c, f, Date.now() - this[a.c.timestamp]);
            d in this[a.c.oa] ? (this.totalPurchaseNames[d]++, d in this[a.c.v] ? this[a.c.v][d]++ : this[a.c.v][d] = 1, this.U(b)) : this[a.c.na] < 10 ? (this[a.c.oa][d] = this[a.c.v][d] = 1, this[a.c.na]++, this.U(b)) : g("PurchaseError=>unique name limit reached!")
        };
        a.prototype.Va = function (d, b, c) {
            this[a.c.B]++;
            d = new v(this[a.c.B], d, b, c);
            this[a.c.la] < 10 && (this[a.c.S].push(d), this[a.c.la]++);
            this[a.c.Ia]++
        };
        return a
    }();
    String.prototype.trim || (String.prototype.trim = function () {
        return this.replace(/^\s+|\s+$/g, "")
    });
    //проверки
    A = function () {
        function a() { }
        a.M = function (a) {
            typeof a !== "boolean" && g("ValidationError=>input is not valid!")
        };
        a.n = function (a) {
            typeof a !== "number" && g("ValidationError=>input is not valid!")
        };
        //Проверка что значение строка
        a.o = function (a) {
            typeof a !== "string" && g("ValidationError=>input is not valid!")
        };
        a.Pb = function (a) {
            typeof a !== "object" && g("ValidationError=>input is not valid!")
        };
        a.N = function (a) {
            a !== parseInt(a) && g("ValidationError=>input is not valid!")
        };
        //.trim()
        a.z = function (a) {
            a.trim() || g("ValidationError=>input is not valid!")
        };
        //1>2
        a.p = function (a,
            d) {
            a > d && g("ValidationError=>input is not valid!")
        };
        a.l = function (a) {
            isNaN(a) && g("ValidationError=>input is not valid!")
        };
        a.Qb = function (a) {
            a === parseInt(a) || a.toFixed(2) === a.toString() || a.toFixed(1) === a.toString() || g("ValidationError=>input is not valid!")
        };
        a.Ta = function (a, d) {
            d.hasOwnProperty(a) || g("ValidationError=>input is not valid!")
        };
        a.va = function (a) {
            a < 0 && g("ValidationError=>input is not valid!")
        };
        a.j = function (a) {
            a || g("ValidationError=>input does not exist!")
        };
        a.Ua = function (a) {
            var d, b, c;
            this.Pb(a);
            b = 1;
            for (d in a) c = a[d], this.o(d), this.p(d.length, 255), this.o(c), this.p(c.length, 255), this.p(b, 10), b++;
            return h
        };
        return a
    }();
})();