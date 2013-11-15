using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCPSocket;

namespace ParseLib.Avito
{
    public static class CheckJs
    {
        ///// <summary>
        ///// a is bool?
        ///// </summary>
        ///// <param name="a"></param>
        ///// <returns></returns>
        //public static bool M(object a)
        //{
        //    return (a is bool); //&& g("ValidationError=>input is not valid!")
        //}
        //public static bool n(string a){
        //    return (a is int); // && g("ValidationError=>input is not valid!")
        //}
        ////Проверка что значение строка
        //public static bool o(string a){
        //    return (a is string); // && g("ValidationError=>input is not valid!")
        //}
        //a.Pb = function (a) {
        //    typeof a !== "object";// && g("ValidationError=>input is not valid!")
        //};
        //a.N = function (a) {
        //    a !== parseInt(a);// && g("ValidationError=>input is not valid!")
        //};
        ////.trim()
        //a.z = function (a) {
        //    a.trim() || g("ValidationError=>input is not valid!")
        //};
        ////1>2
        //a.p = function (a,d) {
        //    a > d && g("ValidationError=>input is not valid!")
        //};
        //a.l = function (a) {
        //    isNaN(a) && g("ValidationError=>input is not valid!")
        //};
        //a.Qb = function (a) {
        //    a === parseInt(a) || a.toFixed(2) === a.toString() || a.toFixed(1) === a.toString() || g("ValidationError=>input is not valid!")
        //};
        //a.Ta = function (a, d) {
        //    d.hasOwnProperty(a) || g("ValidationError=>input is not valid!")
        //};
        //a.va = function (a) {
        //    a < 0 && g("ValidationError=>input is not valid!")
        //};
        //a.j = function (a) {
        //    a || g("ValidationError=>input does not exist!")
        //};
        //a.Ua = function (a) {
        //    var d, b, c;
        //    this.Pb(a);
        //    b = 1;
        //    for (d in a) c = a[d], this.o(d), this.p(d.length, 255), this.o(c), this.p(c.length, 255), this.p(b, 10), b++;
        //    return h
        //};
        //return a
    
    }
    /// <summary>
    /// {a: 1 b: false c: Array[1] {0: Object {1383499767854: 1} } }
    /// </summary>
    public class ACb
    {
        public int a { get; set; }
        public bool b { get; set; }
        public int c1383499767854 { get; set; }
        public object d { get; set; }
    }

    public class EventO
    {
        public string bp { get; set; }
        public int ce { get; set; }
        public long bq { get; set; }
        public object bs { get; set; }
        public bool timed { get; set; }
        public long br { get; set; }
    }
    /// <summary>
    /// {level: 0 request: a{} sessionIncluded: Object{ errors: 0 events: 2 purchases: 0 } sessionsIncluded: 0}
    /// </summary>
    public class AFb
    {
        public int level { get; set; }
        public int errors { get; set; }
        public int events { get; set; }
        public int purchases { get; set; }
        public int sessionsIncluded { get; set; }
    }
    //{"ba":1383569616354,"bc":-1,"eventCounter":0,"purchaseCounter":0,"errorCounter":0,"timedEvents":[]}
    public class Fb
    {
        public int ba { get; set; }
        public int bc { get; set; }
        public int eventCounter { get; set; }
        public int purchaseCounter { get; set; }
        public int errorCounter { get; set; }
        public List<EventO> timedEvents { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class A
    {
        //age: 0
        //ba: 1383499767854
        //bb: 3678158
        //bc: 2441459
        //bd: "mobile"
        //be: ""
        //bf: Object
        //bj: "ru"
        //bk: -1
        //bl: 0
        //bm: false
        //bn: Object
        //bo: Array[0]
        //bt: false
        //bu: Object
        //bv: Array[0]
        //by: Array[0]
        //cd: 0
        //ch: "Etc/GMT-4"
        //errorCounter: 0
        //eventCounter: 2
        //eventLogging: true
        //numErrorsLogged: 0
        //numEventNames: 2
        //numEventsLogged: 2
        //numPurchaseNames: 0
        //numPurchasesLogged: 0
        //pauseTimestamp: 1383504144221
        //purchaseCounter: 0
        //requestsMade: 1
        //sessionContinue: 300000
        
        ///age: 0
        public int age { get; set; }
        //ba: 1382867611139
        public long ba { get; set; }
        ///bb: 0
        public long bb { get; set; }
        ///bc: 192696
        public long bc { get; set; }
        ///bd: ""
        public string bd { get; set; }
        ///be: ""
        public string be { get; set; }
        ///bf: Object
        public Dictionary<string, int> bf { get; set; }
        ///bj: "ru"
        public string bj { get; set; }
        ///bk: -1
        public int bk { get; set; }
        ///bl: 0
        public long bl { get; set; }
        ///bm: false
        public bool bm { get; set; }
        ///bn: Object
        public List<TotalEvent> bn { get; set; }
        ///bo: Array[0]
        public List<EventO> bo { get; set; }
        ///bt: false
        public bool bt { get; set; }
        ///bu: Object
        public object bu { get; set; }
        ///bv: Array[0]
        public List<object> bv { get; set; }
        ///by: Array[0]
        public List<object> by { get; set; }
        ///cd: 0
        public int cd { get; set; }
        ///ch: "Etc/GMT-4"
        public string ch { get; set; }
        ///errorCounter: 0
        public int errorCounter { get; set; }
        ///eventCounter: 8
        public int eventCounter { get; set; }
        ///eventLogging: true
        public bool eventLogging { get; set; }
        ///numErrorsLogged: 0
        public int numErrorsLogged { get; set; }
        ///numEventNames: 0
        public int numEventNames { get; set; }
        ///numEventsLogged: 0
        public int numEventsLogged { get; set; }
        ///numPurchaseNames: 0
        public int numPurchaseNames { get; set; }
        ///numPurchasesLogged: 0
        public int numPurchasesLogged { get; set; }
        ///pauseTimestamp: "1382867984840"
        public long pauseTimestamp { get; set; }
        ///purchaseCounter: 0
        public int purchaseCounter { get; set; }
        ///requestsMade: 0
        public int requestsMade { get; set; }
        ///sessionContinue: 300000
        public int sessionContinue { get; set; }
        ///timedEvents: Array[0]
        public List<EventO> timedEvents { get; set; }
        ///totalEventNames: Object
        public List<TotalEvent> totalEventNames { get; set; }
        ///totalPurchaseNames: Object
        public object totalPurchaseNames { get; set; }
    }

    public class TotalEvent
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

    public class Y
    {
        public Y()
        {
            sessionsIncluded = 0;
            sessionIncluded = null;
            request = null;
            level = 0;
        }
        public int level { get; set; }
        public int sessionsIncluded { get; set; }
        public X request { get; set; }
        public AFb sessionIncluded { get; set; }

        public void a_prototype_Za(string a,string d,string b,string c,string e,string j,A k,List<A> l,object m) {
            request = new X(10, 9, a, d, b, c, e, j, k, l, m);
            var res = new List<X>();
            for (; ; ) {
                //this.level === 4 && g("RequestError=>request length is set too short!");
                a = JSON.stringify(request);
                if (a.Length * 4 / 3 <= 3E3) {
                    sessionsIncluded = request.bX.Count - 1;
                    sessionIncluded.events = request.bX[0].bo.length;
                    sessionIncluded.purchases = request.bX[0].bv.length;
                    sessionIncluded.errors = request.bX[0].by.length;
                    break;
                }
                res.Add(request = xb(request));
            }
        }

        public X xb(X a) {
            switch (level) {
                case 0:
                    a.bX.Length == 1 ? level = 1 : a.bX = this.R(a.bX);
                    break;
                case 1:
                    a.bX[0].by.length == 0 ? level = 2 : a.bX[0].by = R(a.bX[0].by);
                    break;
                case 2:
                    a.bX[0].bv.length == 0 ? level = 3 : a.bX[0].bv = R(a.bX[0].bv);
                    break;
                case 3:
                    a.bX[0].bo.length == 0 ? level = 4 : a.bX[0].bo = R(a.bX[0].bo)
            }
            return a;
        }

        /// <summary>
        /// Возвращает первую половину элементов из массива
        /// </summary>
        /// <param name="a">Массив</param>
        /// <returns></returns>
        public object R(Dictionary<string,object> a) {
            if(a.Count >= 1)
            {
                var d = a.Count/2;
                var temp = new Dictionary<string, object>();
                int i = 0;
                foreach(var tr in a)
                {
                    temp.Add(tr.Key,tr.Value);
                    i++;
                    if(i==d)
                        break;
                }
                a = temp;
            }
            return a;
        }
    }

    public class Xa
    {
        public long af { get; set; }
        public int aa { get; set; }
        public int ab { get; set; }
        public int ac { get; set; }
        public string ae { get; set; }
        public string ad { get; set; }
        public string ag { get; set; }
        public int ah { get; set; }
        public object cg { get; set; }
        public string ai { get; set; }
        public string aj { get; set; }
        public int ak { get; set; }
        public Dictionary<string,object> za(A a) {
            var f = new Dictionary<string,object>();
            var tempA= Helpers.ConvertToDictionary<string,object[]>(a);
            foreach (var d in tempA)
            {
                var k = d;
                if (d.Key == "bf"&&d.Value[0] != null)
                    f.Add(d.Key,k);
                else if (d.Key == "bo")
                {
                    //f[d] = [];
                    var temp = new List<object>();
                    for (int l = 0, m = k.Value.Count(); l < m; l++)
                    {
                        var c = k.Value[l];
                        temp.Add(c);
                    }
                    f[d.Key]=temp;
                }
                else
                    if(!Ab.Contains(d.Key+":")) f.Add(d.Key,k.Value);
            }
            return f;
        }
        public string Ab="{timedEvents: 'timedEvents',eventLogging: 'eventLogging',sessionContinue: 'sessionContinue'," +
                         "pauseTimestamp: 'pauseTimestamp',age: 'age',numEventNames: 'numEventNames'," +
                         "numPurchaseNames: 'numPurchaseNames',requestsMade: 'requestsMade',totalEventNames: 'totalEventNames'," +
                         "totalPurchaseNames: 'totalPurchaseNames',numEventsLogged: 'numEventsLogged'," +
                         "numPurchasesLogged: 'numPurchasesLogged',numErrorsLogged: 'numErrorsLogged',eventCounter: 'eventCounter'," +
                         "purchaseCounter: 'purchaseCounter',errorCounter: 'errorCounter'}";
        
    }

    public class X
    {
        public int f = 1;
        public int d = 1;
        public List<string> bX ;
        public X(int b, int c, string e, string j, string k, string l, string m, string r, A s, List<A> C, object u)
        {
            var a = new Xa {af = (long)((DateTime.Now-d1970).TotalMilliseconds), aa = f, ab = b, ac = c, ae = e, ad = j, ag = k, ah = s.ba};
            //a.g("about to check if firstPartyCookieUID " + u + " should be assigned to global map in the report request");
            if (u != null)
                a.cg = u;
            if (s.requestsMade == 0)
            { 
                a.ai = l;
                a.aj = r;
                //s.Db();
                s.requestsMade++;
            }
            a.ak = d;
            bX = new List<string>();
            if(s!=null)
            {
                //s.Oa();   this[a.c.duration] = Date.now() - this[a.c.timestamp]
                s.bb = (long)((DateTime.Now-d1970).TotalMilliseconds) - s.ba;
                var ee = a.za(s);
                bX.AddRange(ee.Values.ToString());
            };
            for (b = 0, c = C.Count; b < c; b++)
            {
                var ee = a.za(C[b]);
                bX.AddRange(ee);
            }
        }
        public Dictionary<string,string> zb() {timed: "timed"}
        public string Fa = "https://data.flurry.com/aah.do";
        //public string Sb() {
        //    return this.Fa
        //}
        public string hb(string a) {
            var c, d, f;
            d = {};
            for (c in a)
                f = a[c], this.zb.hasOwnProperty(c) || (d[c] = f);
            return d
        }
        
        public string a_prototype_Kb() {
            this.a.af = (long)((DateTime.Now-d1970).TotalMilliseconds)
        }
    }
}
