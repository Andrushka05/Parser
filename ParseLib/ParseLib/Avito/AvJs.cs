using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ParseLib.Avito
{
    public class AvJs
    {
        public AvJs(string key,string cookie, string referer)
        {
            //BYCR5JHJJDRQZK2VPDDQ
            t_a_O = key;
            A=new A();
            this.cookie = cookie;
            t_a_referrer = referer;
            //Должно быть так t_a_Da=QllDUjVKSEpKRFJRWksyVlBERFE_
            t_a_Fb();
            
        }
        public string cookie { get; set; }
        public A A;
        ///a.i = h;
        public bool t_a_i = true;
        ///a.appVersion = ""; //"mobile" устанавливается в a.da
        public string t_a_appVersion = "";
        ///FlurryAgent.startSession("BYCR5JHJJDRQZK2VPDDQ");
        public string t_a_O = "";
        ///a.Ha = 0;
        public int t_a_Ha = 0;
        ///a.ka = Date.now();
        public string t_a_ka = DateTime.Now.ToLongTimeString();
        ///a.Da = i;
        public object t_a_Da = null;
        ///a.F = i;
        public object t_a_F = null;
        ///a.Ka = h;
        public bool t_a_Ka = true;
        ///a.Bb = h;
        public bool t_a_Bb = true;
        ///a.Qa = n;
        public bool t_a_Qa = false;
        ///Ошибка, в методе выводить лог t_a_g
        public bool t_a_Ea = false;
        ///a.localStorage = n;
        public bool t_a_localStorage = false;
        ///a.fa = i;
        public object t_a_fa = null;
        ///a.Ja = "";
        public string t_a_Ja = "Win32";
        ///a.referrer = "";
        public string t_a_referrer = "";
        ///a.J = 0;
        public int t_a_J = 0;
        ///a.e = i;
        public A t_a_e = null;
        ///a.$ = [];
        public List<A> t_a_S;
        ///a.w = i;
        public object t_a_w = null;
        ///a.Y = 5E3;
        public int t_a_Y = 300000;
        ///a.ea = i;
        public object t_a_ea = null;
        ///a.I = n;
        public bool t_a_I = false;
        ///a.Z = i;
        public object t_a_Z = null;

        public string referer = "";


        ///x.Fa=https://data.flurry.com/aah.do
        public string x_Fa = "https://data.flurry.com/aah.do";

        public bool n = false;
        public bool h = true;
        /// <summary>
        /// Шифрует  json
        /// </summary>
        /// <param name="json">Пример:json="{"ba":1382731282855,"bc":-1,"eventCounter":0,"purchaseCounter":0,"errorCounter":0,"timedEvents":[]}"</param>
        /// <returns>eyJiYSI6MTM4MjczMTI4Mjg1NSwiYmMiOi0xLCJldmVudENvdW50ZXIiOjAsInB1cmNoYXNlQ291bnRlciI6MCwiZXJyb3JDb3VudGVyIjowLCJ0aW1lZEV2ZW50cyI6W119</returns>
        public string p_a_ia(string json)
        {
            var c=0;
            var l="";
            var m=0;
            int d = 0;
            int b = 0;
            int e,k,j;
            var a = p_a_Ob(json);
            l = "";
            for (var r = a.Length; m < r; c = m += 3)
            {
                d = a.ToCharArray()[c];
                b = a.ToCharArray()[c + 1];
                c = a.ToCharArray()[c + 2];
                e = d >> 2;
                d = (d & 3) << 4 | b >> 4;
                j = (b & 15) << 2 | c >> 6;
                k = c & 63;
                int res = 0;
                var xc = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".Select(x => x.ToString()).ToArray();
                l += xc[e] + xc[d] + xc[j] + xc[k];
            }
            return l;
        }
        /// <summary>
        /// какая та перепроверка json перед шифрованием
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public string p_a_Ob(string json)
        {

            var d = 0;
            var b = "";
            var c = 0;
            var a = json.Replace("\r\n", "\n");
            for (var e = a.Length; 0 <= e ? c < e : c > e; d = 0 <= e ? ++c : --c)
            {
                d = a.ToCharArray()[d];
                if (d < 128)
                    b += ((char) d).ToString();
                else
                {
                    if (d > 127 && d < 2048)
                        b += ((char) (d >> 6 | 192)).ToString();
                    else
                    {
                        b += ((char) (d >> 12 | 224)).ToString();
                        b += ((char) (d >> 6 & 63 | 128)).ToString();
                    }
                    b += ((char) (d & 63 | 128)).ToString();
                }
            }
            return b;
        }
        /// <summary>
        /// Конвертирует зашифрованную строку в json, значение кук _fs
        /// </summary>
        /// <param name="a">eyJiYSI6MTM4MjkwMTI5MDM4MSwiYmMiOjIxNTMxLCJldmVudENvdW50ZXIiOjEsInB1cmNoYXNlQ291bnRlciI6MCwiZXJyb3JDb3VudGVyIjowLCJ0aW1lZEV2ZW50cyI6W119</param>
        /// <returns>{"ba":1382901290381,"bc":21531,"eventCounter":1,"purchaseCounter":0,"errorCounter":0,"timedEvents":[]}</returns>
        public string p_a_lb(string a)
        {
            var d=0;
            var b=0;
            var c=0;
            var e=0;
            var j = 0;
            var k="";
            var l = 0;
            //a = Regex.Replace(a, @"[^A-Za-z0-9\+\/\=]", "");
            var str = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
            var xc =a.Select(x => x.ToString()).ToArray();
            for (var m = a.Length; l < m; j = l += 4)
            {
                d = str.IndexOf(xc[j]);
                b = str.IndexOf(xc[j+1]);
                e = str.IndexOf(xc[j + 2]);
                j = str.IndexOf(xc[j + 3]);
                d = d << 2 | b >> 4;
                b = (b & 15) << 4 | e >> 2;
                c = (e & 3) << 6 | j;
                k += ((char)d).ToString();
                if (e != 64)
                    k += ((char)b).ToString();
                if(j != 64) 
                    k += ((char)c).ToString();
            }

            //k="{"ba":1382901290381,"bc":21531,"eventCounter":1,"purchaseCounter":0,"errorCounter":0,"timedEvents":[]}"
            k = p_a_Nb(k);
            //k="{"ba":1382901290381,"bc":21531,"eventCounter":1,"purchaseCounter":0,"errorCounter":0,"timedEvents":[]}"
            return k;
        }
        /// <summary>
        /// Проверяет и возвращает тоже самое)))
        /// </summary>
        /// <param name="a">{"ba":1382901290381,"bc":21531,"eventCounter":1,"purchaseCounter":0,"errorCounter":0,"timedEvents":[]}</param>
        /// <returns>{"ba":1382901290381,"bc":21531,"eventCounter":1,"purchaseCounter":0,"errorCounter":0,"timedEvents":[]}</returns>
        public string p_a_Nb(string a) {
            var d=0;
            var b=0;
            var c=0;
            var e=0;
            var j = "";
            for (e = 0; e < a.Length;)
            {
                d = a.ToCharArray()[e];
                if (d < 128)
                {
                    j += ((char)d).ToString();
                    e++;
                }
                else
                {
                    if (d > 191 && d < 224)
                    {
                        b = a.ToCharArray()[e + 1];
                        j += ((char)((d & 31) << 6 | b & 63)).ToString();
                        e += 2;
                    }
                    else
                    {
                        b = a.ToCharArray()[e + 1];
                        c = a.ToCharArray()[e + 2];
                        j += ((char)((d & 15) << 12 | (b & 63) << 6 | c & 63)).ToString();
                        e += 3;
                    }
                }
            }
            return j;
        }
        //проверочная сумма предаётся как параметры c= к ссылке флюри
        //а="{"a":{"af":1382981724491,"aa":1,"ab":10,"ac":9,"ae":"","ad":"BYCR5JHJJDRQZK2VPDDQ","ag":1382371709662,"ah":1382981634346,"cg":"SG0978FE8DF36DE98AC942EC221DCD35404566F861","ai":"Win32","aj":"http://m.avito.ru/pskov/kvartiry","ak":1},"b":[{"bd":"","be":"","bk":-1,"bl":0,"bj":"ru","bo":[],"bm":false,"bn":{},"bv":[],"bt":false,"bu":{},"by":[],"cd":0,"ba":1382981634346,"bb":90145,"bc":36612,"ch":"Etc/GMT-4"}]}"
        //return c=4231817511
        public string q_a_bb(string a) {
            var d=1;
            var b=0;
            var c=0;
            var e=0;
            for (var j = a.Length; 0 <= j ? e < j : e > j; c = 0 <= j ? ++e : --e)
            {
                c = a.ToCharArray()[c];
                d += c;
                b += d;
            }
            d %= 65521;
            b %= 65521;

            var z=(b*65536 + d).ToString();
            return z;
        }
        /// <summary>
        /// Расшифровывает строку в Json
        /// </summary>
        /// <param name="a">"eyJiYSI6MTM4MjcxOTA0NzAyNiwiYmMiOi0xLCJldmVudENvdW50ZXIiOjAsInB1cmNoYXNlQ291bnRlciI6MCwiZXJyb3JDb3VudGVyIjowLCJ0aW1lZEV2ZW50cyI6W119"</param>
        /// <returns>"{"ba":1382719047026,"bc":-1,"eventCounter":0,"purchaseCounter":0,"errorCounter":0,"timedEvents":[]}"</returns>
        public Fb t_f(string a)
        {
            var b = "";
            
            try
            {
                b = p_a_lb(a);
               var c = JsonConvert.DeserializeObject<Fb>(b);
                return c;
            } catch (Exception) {
                //console.log(e)
            }
            return null;
        }
        /// <summary>
        /// {"install", "fit"},{"session", "fs"},{"lastPoll", "flp"},{"firstPartyCookie", "fid"}
        /// </summary>
        /// <param name="a">Пример- "install"</param>
        /// <param name="b"></param>
        /// <returns>t_a_Da+_...</returns>
        public string t_a_ga(string a, bool b)
        {
            var str = b ? t_a_Da : "";
            var ar = new Dictionary<string, string>(){{"install", "fit"},{"session", "fs"},{"lastPoll", "flp"},{"firstPartyCookie", "fid"}};
            return str + ar[a];
        }
        /// <summary>
        /// Шмфрует начальный ключ и записывает в t_a_Da
        /// Приравнивает t_a_Fb, вызов перед t_a_ga
        /// </summary>
        public void t_a_Fb()
        {
            t_a_Da = p_a_ia(t_a_O).Replace("=", "") + "_";
        }

        public int t_a_ub() {
            //this.g("getFlurryAgentVersion() called");
            return 9;
        }


        //вызывается из страницы строчкой FlurryAgent.setAppVersion("mobile");  a=mobile
        public void t_a_da(object a) {
            //this.g("setAppVersion(" + a + ") called");
            try {
                // A.o(a)
                if(a is string){
                    //A.z(a)
                    var str = a.ToString().Trim();
                    //A.p(a.length, 255)
                    if (str.Length > 255)
                    {
                        t_a_appVersion = str;
                        //this.e && this.e.da(a)->this[a.c.appVersion] = str->bd="mobile"
                        bd = str;
                        t_a_i = true;
                    }
                }
            } catch (Exception){/*t_a_h(b);*/}
        }
       
        public void t_a_ra(string a) {
              //A.j(this.e), A.o(a), A.Ta(a, {m: 1,f: 0}), this.e.ra(a), this.i = h
                var temp = "{m:1,f:0}";
            if (t_a_e != null && temp.Contains(a))
            {
                A.bk = a == "f" ? 0 : 1;
                t_a_i = true;
            }
        }
        public void t_a_qa(object a) {
            //A.j(this.e), A.n(a), A.l(a), A.N(a), A.va(a), this.e.qa(a), this.i = h
            if (t_a_e != null && a is int && (int) a > 0)
            {
                var temp = (int) a;
                A.age = temp;
                t_a_i = true;
                var b = DateTime.Now;
                A.bl = new DateTime(b.Year - temp-1970, b.Month, 1).Millisecond;
                t_a_i = true;
            }
        }
        public void t_a_sa(object a, int b,int c=0) {
            //A.j(this.e), A.n(a), A.l(a), A.n(b), A.l(b), A.n(c), A.l(c), this.e.sa(a, b, c), this.i = h
            if (t_a_e != null)
            {
                A.bf = new Dictionary<string, int> {{"bg", (int) a}, {"bh", b}, {"bi", c}};
                t_a_i = true;
            }    
            
        }
        
        public void t_a_ib()
        {
            t_a_Ja = "Win32";
        }
        public void t_a_L(int a) {
            //A.n(a), A.l(a), A.N(a), A.va(a), this.J = a, this.e return this.e.L(a)
            if (t_a_e != null&&a>0)
            {
                t_a_J = a;
                A.sessionContinue = Convert.ToInt32(a*1E3);
            } 
        }
        public void t_a_Jb(bool a)
        {
            t_a_Qa = a;
        }
        public void t_a_Gb(bool a)
        {
            t_a_Ea = a;
        } 
        public void t_a_Ib(bool a)
        {
            t_a_localStorage = a;
        }
        public void t_a_kb() {
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
        }
        public void t_a_jb()
        {
            var a = t_a_G("install");
            var num = Regex.Replace(a, @"", "");
            if(num.Length==a.Length)
                t_a_ka = a;
            else
                t_a_K("install", t_a_ka);
        }

        public void t_a_Ca() {
            var a = t_a_G("firstPartyCookie", false);
            if (a != null)
                t_a_F = a;
            else
                if(t_a_F != null) 
                    t_a_K("firstPartyCookie", t_a_F, false);
        }

        //Вытаскивает из кук _fit=i, _fid, по умолчанию b=true
        public string t_a_G(string a,bool b=true) {
            var c="";
            var f="";
            var k=0;
            var l = t_a_ga(a, b);
            var e = cookie.Split(new string[]{";"},StringSplitOptions.None);
            for (int m = 0, r = e.Length; m < r; m++)
            {
                //if (c = e[m], k = c.indexOf("="), f = c.slice(0, k).trim(), f === l) return c.slice(k + 1);
                c = e[m];
                k = c.IndexOf("=");
                f = c.Substring(0, k);
                if (f == l)
                    return c.Substring(k+1);
            }
            return null;
        }
        public bool t_a_eb()
        {
            return t_a_G("install",true) != null;
        }
        /// <summary>
        /// Удаление кук
        /// </summary>
        /// <param name="a"></param>

        public void t_a_ha(string a) {
            var b=true;
            var c = cookie.Split(new string[] {";"}, StringSplitOptions.None);
            var value = t_a_ga(a, b);
            foreach (var s in c)
            {
                if (s.Contains(value))
                {
                    cookie = cookie.Replace(s, "");
                    break;
                }
            }
            //document.cookie = "" + t_a_ga(a, b) + "=; expires='Thu, 01 Jan 1970 00:00:00 GMT'; path=/";
        }
        /// <summary>
        /// Установка кук
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>

        public void t_a_K(string a,string b, bool c=true) {
            //e.setTime(Date.now() + 31536E7); //+51721959(10 лет)
            var e = new DateTime();
            e=e.AddYears(10);
            cookie += t_a_ga(a, c) + "=" + b + "; expires=" + e.ToString("R") + "; path=/";
        }
        //Вызывается из t_a_kb во время движения по странице
        public void t_a_ta(int a) {
            var b = this;
            t_a_Q();
            if (a>0) 
                t_a_Y = Convert.ToInt32(a * 1E3);
            t_a_fa = window.setInterval(function () {
                b.ea = DateTime.Now;//1383498256608
                t_a_K("lastPoll", DateTime.Now.ToString());
                //return b.q()
            }t_a_Y); 
            //t_a_ea && Date.now() - this.ea > this.Y && this.q(); Y=5000
            if (t_a_ea != null &&)
                t_a_q();

            return t_a_Y/1E3;
        }
        public void t_a_Q() {
            if(t_a_fa!=null)
                window.clearInterval(t_a_fa);
            t_a_fa = null;
        }
        
        /// <summary>
        /// Удаляем пару ключей из кук _fs и _flp
        /// </summary>
        /// <returns>Возвращает а с значениями по умолчанию, кроме пары полей таймзоны, ba,bj, ch</returns>
        public A t_a_Sa() {
            string c=null;
            var b = t_a_G("session");   //_fs=b  eyJiYSI6MTM4MzQ5ODI1OTYzNywiYmMiOi0xLCJldmVudENvdW50ZXIiOjAsInB1cmNoYXNlQ291bnRlciI6MCwiZXJyb3JDb3VudGVyIjowLCJ0aW1lZEV2ZW50cyI6W119
            var a = t_a_G("lastPoll");  //_flp=a  1383499085473
            t_a_ha("session");
            t_a_ha("lastPoll");
            Fb xx = null;
            if (b != null && a != null)
            {
                xx = t_f(b);//{"ba":1383569616354,"bc":-1,"eventCounter":0,"purchaseCounter":0,"errorCounter":0,"timedEvents":[]}
                //b[z.c.r] = a;  pauseTimestamp
            }
            var res = new A()
            {
                bd = "mobile",
                be = "",
                bk = -1,
                age = 0,
                bl = 0,
                bf = null,
                bj = "ru",
                timedEvents = xx!=null?xx.timedEvents:null,
                eventCounter = xx!=null?xx.eventCounter:0,
                bo = null,
                numEventsLogged = 0,
                bm = false,
                totalEventNames = null,
                bn=null,
                numEventNames = 0,
                purchaseCounter = xx!=null?xx.purchaseCounter:0,
                bv = null,
                numPurchasesLogged = 0,
                bt = false,
                totalPurchaseNames = null,
                bu = null,
                numPurchaseNames = 0,
                errorCounter = xx!=null?xx.errorCounter:0,
                by = null,
                numErrorsLogged = 0,
                cd=0,
                ba = xx!=null?xx.ba:DateTime.Now.Millisecond,
                bb=0,
                eventLogging = true,
                sessionContinue = 300000,
                pauseTimestamp = 0,
                bc = xx!=null?xx.bc:-1,
                requestsMade = 0,
                ch="Etc/GMT-4"
            };
            //this.J && a.L(this.J);
            if (t_a_J > 0)
                res.sessionContinue = t_a_J;
            return res;
        }
        //a=BYCR5JHJJDRQZK2VPDDQ       FlurryAgent.startSession("BYCR5JHJJDRQZK2VPDDQ");
        public void t_a_Ra (string a) {
            //this.g("startSession(" + a + ") called");
            if (t_a_e==null) {
                a=a.Trim();
                t_a_O = a;//this.O = a;
                t_a_Fb();//this.Fb();
                t_a_i = true;//this.i = h;//true
                //this.e = this.$a();//Возвращает а с значениями по умолчанию, кроме пары полей таймзоны, ba,bj, ch
                t_a_e = t_a_Sa();
                //this.jb();//this.ka=приравнивается значение кук _fit
                t_a_jb();
                t_a_Ca();//this.Ca();//this.F =_fid.value
                if (!t_a_eb())//_fit.value!=null
                {
                    t_a_Ka = n;
                    t_a_A();
                }
                if(!(t_a_Ha>0))
                {
                    t_a_ib();//this.ib(this.Ja=navigator.platform "Win32", this.referrer = document.referrer)
                    t_a_kb();
                } 
                t_a_ta(0);
                t_a_q();//создаёт тег скрипт ссылающий на флюре
                t_a_Ha++;
            }
        }
        public void t_a_A()
        {
            t_a_Q();
            if (t_a_e!=null) {/*return*/
                a_prototype_A();
                t_a_i = h;
                if (t_a_Ka) t_a_q(); else t_a_S.Add(t_a_e);
                t_a_e = null;
                t_a_ha("session");
            }
        }
        public void t_a_yb() {
            this.g("pauseSession() called");
            try {
                return this.Q(), this.Na(), this.e ? (this.q(), this.e = i) : this.g("no session to pause!")
            } catch (a) {
                return this.h(a)
            }
        }
        /// <summary>
        /// Создаёт и устанавливает куки _fs(json зашифрованный), _flp(время)
        /// </summary>
        public void t_a_Na() {
            //A.j(this.e), a = Date.now(), this.e.Pa(a), this.Tb = a, this.K("session", this.e.Ya()), this.K("lastPoll", a)
            if (t_a_e != null)
            {
                var a = DateTime.Now.Millisecond;
                A.pauseTimestamp = a;
                t_a_Tb = a;
                t_a_K("session",a_prototype_Ya());
                t_a_K("lastPoll", a);
            }
            
        }
        //FlurryAgent.logEvent("environment", env)    b={browser: ""device: ""grade: "A"isBot: "0"mobile: ""os: ""}
        public void t_a_t(string a,EventO b=null,bool c=false) {
            //проверка объекта е что он существует->
            //return A.j(this.e); a_prototype_Wa(a, b, c), this.i = h
            if (t_a_e != null)
            {
                a_prototype_Wa(a, b, c);
                t_a_i = true;
            }
        }
        public void t_a_qb(a,object b=null) {
            //return A.j(this.e), this.e.pb(a, b), this.i = h
            if (t_a_e != null)
            {

                t_a_i = h;
            }
        }
        public void t_a_k() {
            var a, b, c, e, f;
            var b = [];
            //A.j(this.e);f = this.e[z.c.k];for (c = 0, e = f.length; c < e; c++) a = f[c], b.push(a.bp)
            if (t_a_e != null)
            {
                var f=
            }
            return b
        }
        public void t_a_Hb(bool a) {
            //return A.j(this.e), A.M(a), this.e.ja = a
            if (t_a_e != null)
            {
                
            }
        }
        public void t_a_U(a, b, c, e) {
            b == i && (b = 0);
            c == i && (c = "USD");
            e == i && (e = {});
            this.g("logPurchase(" + a + "," + b + "," + c + "," + e + ") called");
            try {
                return A.j(this.e), this.e.Xa(a, b, c, e), this.i = h
            } catch (f) {
                return this.h(f)
            }
        }
        public void t_a_wb(a, b, c) {
            c == i && (c = 0);
            this.g("logError(" + a + "," + b + "," + c + ") called");
            try {
                return A.j(this.e),
                this.e.Va(a, b, c), this.i = h
            } catch (e) {
                return this.h(e)
            }
        }
        //Создаёт тег скрипт ссылающий на флюре дата
        public void t_a_q() {
            //this.g("REQUEST INITIATED");
            if (!this.I) {
                    if (this.w) this.w.Kb();
                    else if (this.i)
                    {
                        var a = new Y();
                        a.a_prototype_Za(t_a_appVersion, t_a_O, t_a_ka, t_a_Ja, null/*t_a_Rb*/, t_a_referrer, t_a_e, t_a_S, t_a_F),
                        this.w = a.request,
                        this.fb(a);
                    }
                if (this.w) return this.Na(), this.rb(JSON.stringify(this.w))
                }
        }
        //a={level: 0 request: a{} sessionIncluded: Object{ errors: 0 events: 2 purchases: 0 } sessionsIncluded: 0}
        public void t_a_fb(AFb a) {
            a_prototype_gb(a.events, a.purchases, a.errors);
            //this.$ = this.$.slice(a.sessionsIncluded);
            if(a.sessionsIncluded>0)
                t_a_S = t_a_S.GetRange(a.sessionsIncluded,t_a_S.Count-a.sessionsIncluded);
            if (a.level == 0) t_a_i = false;
        }
        //создаёт тэг скрипт с src на флюре дата и после его выполнения делаеться запрос
        //a="{"a":{"af":1383500358983,"aa":1,"ab":10,"ac":9,"ae":"mobile","ad":"BYCR5JHJJDRQZK2VPDDQ","ag":1382371709662,"ah":1383499767854,"cg":"SG0978FE8DF36DE98AC942EC221DCD35404566F861","ak":1},"b":[{"bd":"mobile","be":"","bk":-1,"bl":0,"bj":"ru","bo":[{"ce":1,"bp":"environment","bq":364163,"bs":{"os":"","grade":"A","mobile":"","browser":"","device":"","isBot":"0"},"br":0},{"ce":2,"bp":"PageView","bq":385294,"bs":{},"br":0}],"bm":false,"bn":{"environment":1,"PageView":1},"bv":[],"bt":false,"bu":{},"by":[],"cd":0,"ba":1383499767854,"bb":591130,"bc":-1,"ch":"Etc/GMT-4"}]}"
        public void t_a_rb(string a) {
            var b, c = this;
            //this.g("REQUEST EXECUTED");
            t_a_I = true;
            //t_a_Z = window.setInterval(function ()
            //{
            //    return c.P(false);
            //}, 1E4);
            b = document.createElement("script");
            b.type = "text/javascript";
            b.async = true;
            b.src = encodeURI(x_Fa + "?d=" + p_a_ia(a) + "&c=" + q_a_bb(a)); //https://data.flurry.com/aah.do
            a = document.getElementsByTagName("script");
            return a[0].parentNode.insertBefore(b, a[0])
        }
        //a={a: 1 b: false c: Array[1] {0: Object {1383499767854: 1} } }
        public void t_a_Cb(ACb a) {
            //this.g("RESPONSE RECEIVED");
            //this.I || g("ResponseError=>request considered timed out!");
            //typeof a != "object" && t_a_P(false);//, g("ResponseError=>input is not a valid object!"));
            a.a > 0 ? t_a_P(h) : t_a_P(n);
            if(a.b != null) t_a_Ib(a.b);
            if (a.d != null)
            {
                t_a_F = a.d;
                t_a_Ca();
                t_a_i = h;
            }
            if (t_a_i)
                t_a_q();
        }
        //очистка запроса
        public void t_a_P(bool a) {
            //this.g("CLEAR REQUEST with " + a);
            if (t_a_I) {
                if(t_a_Z!=null) window.clearInterval(t_a_Z);
                if (a) t_a_w = i;
                t_a_I = n;
                t_a_Z = null;
            }
        }







        public void a_prototype_gb(int d, int b, int c) {
            //this[a.c.D] = this[a.c.D].slice(d);
            A.bo = A.bo.GetRange(d,A.bo.Count-d);
            //this[a.c.s] = {};
            A.bn=new List<TotalEvent>();
            //this[a.c.X] = this[a.c.X].slice(b);
            A.bv = A.bv.GetRange(b);
            //this[a.c.v] = {};
            A.bu=new object();
            //this[a.c.S] = this[a.c.S].slice(c)
            A.by = A.by.GetRange(c,A.by.Count-c);
        }

        /// <summary>
        /// Шифрует ba bc eventCounter purchaseCounter errorCounter timedEvents
        /// </summary>
        /// <returns>"{"ba":1383499767854,"bc":2441459,"eventCounter":2,"purchaseCounter":0,"errorCounter":0,"timedEvents":[]}"</returns>
        public string a_prototype_Ya() {
            //var d = new Dictionary<string,int>();
            var c = new Dictionary<string,object>(){{"ba",A.ba}, {"bc",A.bc}, {"eventCounter",A.eventCounter}, {"purchaseCounter",A.purchaseCounter},
                {"errorCounter",A.errorCounter}, {"timedEvents",A.timedEvents}};
            //for (int e = 0, f = c.Count; e < f; e++)
            //{
            //    var b = c[];
            //    d.Add(c[e].Key,);
            //    d[b] = this[b];
            //}
            var json = JsonConvert.SerializeObject(c);
            var encode=p_a_ia(json);
            return encode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d">PageView</param>
        /// <param name="ev"></param>
        /// <param name="c"></param>
        public void a_prototype_Wa(string d,EventO ev,bool c) {
            A.eventCounter++;
            //b = new Event(this[a.c.C], d, b, c, Date.now() - this[a.c.timestamp]);
            ev = new EventO {bp = d.Trim(), ce = A.eventCounter, bq = DateTime.Now.Millisecond - A.ba, bs = b, timed = c, br = 0};
            //d in this[a.c.T] ? (this[a.c.T][d]++, d in this[a.c.s] ? this[a.c.s][d]++ : this[a.c.s][d] = 1, this.t(b)) : this[a.c.ma] < 100 ? (this[a.c.T][d] = this[a.c.s][d] = 1, this[a.c.ma]++, this.t(b)) : g("LogError=>unique name limit reached!")
            if (A.totalEventNames.Count(x => x.Name == d)>0)
            {
                //this[a.c.T][d]++
                foreach (var totalEventName in A.totalEventNames)
                {
                    if (totalEventName.Name == d)
                    {
                        totalEventName.Value++;
                        break;
                    }
                }
                // d in this[a.c.s] ? this[a.c.s][d]++ : this[a.c.s][d] = 1, this.t(b))
                var tbool = A.bn.Count(x => x.Name == d) > 0;
                foreach (var bn in A.bn)
                {
                    if (bn.Name == d)
                    {
                        if(tbool)
                            bn.Value++;
                        else
                            bn.Value=1;
                        break;
                    }
                }
                if(!tbool)
                    a_prototype_t(ev);
            }
            else
            {
                //this[a.c.ma] < 100 ? (this[a.c.T][d] = this[a.c.s][d] = 1, this[a.c.ma]++, this.t(b)) : g("LogError=>unique name limit reached!")
                if (A.numEventNames < 100)
                {
                    foreach (var totalEventName in A.totalEventNames)
                    {
                        if (totalEventName.Name == d)
                        {
                            totalEventName.Value=1;
                            break;
                        }
                    }
                    foreach (var bn in A.bn)
                    {
                        if (bn.Name == d)
                        {
                            bn.Value=1;
                            break;
                        }
                    }
                    A.numEventNames++;
                    a_prototype_t(ev);
                }
            }
        }
        /// <summary>
        /// Добавление EventO in bo
        /// </summary>
        /// <param name="d"></param>
        public void a_prototype_t(EventO d) {
            if (A.eventLogging && A.numEventsLogged < 1E3)
            {
                //return d.timed && !d.br ? this[a.c.k].push(d) : (this[a.c.D].push(d),this[a.c.V]++)
                if(d.br<0)
                {
                    //A.timedEvents.Add();
                }
                else
                {
                    A.bo.Add(d);
                    A.numEventsLogged++;
                }
            }
        }
        /// <summary>
        /// Используется в focus t_a_kb
        /// </summary>
        /// <returns></returns>
         public bool a_prototype_Ma() {
            //var d = this[a.c.r] > 0 ? Date.now() - this[a.c.r] : 0;
             var d = A.pauseTimestamp > 0 ? DateTime.Now.Millisecond - A.pauseTimestamp : 0;
             //d > 0 && (this[a.c.u] === -1 && (this[a.c.u] = 0), this[a.c.u] += d, this[a.c.r] = 0);return d < this[a.c.pa]
             if (d > 0)
             {
                 if (A.bc == -1)
                     A.bc = 0;
                 A.bc += d;
                 A.pauseTimestamp = 0;
             }
             return d < A.sessionContinue;
         }

        /// <summary>
        /// Используется в focus t_a_kb
        /// A.bb A.bm A.bt сброс timedEvents
        /// </summary>
        public void a_prototype_A() {
            a_prototype_ob();
            //this.Oa(); -> this[a.c.duration] = Date.now() - this[a.c.timestamp]
            A.bb = DateTime.Now.Millisecond - A.ba;
            //this[a.c.V] < 1E3 && (this[a.c.Aa] = n);
            if (A.numEventsLogged < 1E3)
                A.bm = n;
            // if (this[a.c.W] < 100)return this[a.c.Ba] = n
            if (A.numPurchasesLogged < 100)
                A.bt = n;
        }
        /// <summary>
        /// Сброс timedEvents
        /// </summary>
        public void a_prototype_ob() {
            var f = A.timedEvents;
            for (int b = 0, c = f.Count; b < c; b++)
            {
                var d = f[b];
                d.br = DateTime.Now.Millisecond - (A.ba + d.bq);
                a_prototype_t(d);
            }
            A.timedEvents = new List<EventO>();
        }




        public void t_a_ua(object a) {
            //this.g("setUserId(" + a + ") called");
            try {
                //A.j(this.e)
                if (t_a_e != null)
                {
                    //A.o(a)
                    if (a is string)
                    {
                       // A.z(a) - trim
                        var str = a.ToString().Trim();
                        //A.p(a.Length, "userId"),
                        if (str.Length > 6)
                        {
                            //this.e.ua(a),-----------------------------------------------------------------
                            t_a_i = true;
                        }
                    }
            }
            } catch (Exception) {
                //return this.h(b)
            }
        }



        
        /// <summary>
        /// Хрень для логирования
        /// </summary>
        /// <param name="a"></param>
        public void t_a_h(object a) {
            if (t_a_Qa)
            {
                //if(a is string)
                //    t_a_g("" + a.ToString());
                //else
                //    t_a_g("" + a.name + "=>" + a.message);
            }
        }
        /// <summary>
        /// Хрень для логирования
        /// </summary>
        /// <param name="a"></param>
        public void t_a_g(string a)
        {
            if (t_a_Ea)
            {
                //return console.log("Flurry " + new Date + ": " + a);
            }
        }

    }
}
