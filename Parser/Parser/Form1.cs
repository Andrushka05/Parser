using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using HtmlAgilityPack;

using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Diagnostics;
using ParseLib.Avito;
using TCPSocket;
using WatiN.Core;
using Form = System.Windows.Forms.Form;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;


namespace Parser
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static List<Company> _companies = new List<Company>();


        private async void StartButton_Click(object sender, EventArgs e)
        {
            //var socksList = Helpers.GetIpEndOfFile(@"E:\ProxyList\socks.txt");
            //var ipWorks = new List<IPEndPoint>();//Helpers.GetIpEndOfFile(@"E:\ProxyList\socksWork.txt");
            //int s5 = 0, s4 = 0;
            //Parallel.ForEach(socksList, s =>
            //{
            //    var zxc = Socks.GetTypeSocks(s);
            //    if (zxc != TypeSocks.Error)
            //    {
            //        ipWorks.Add(s);
            //        if (zxc == TypeSocks.Socks5)
            //            s5++;
            //        if (zxc == TypeSocks.Socks4)
            //            s4++;
            //    }

            //});
            var a = new AvJs("123123123");
            var tre =
                a.q_a_bb("{\"a\":{\"af\":1382981724491,\"aa\":1,\"ab\":10,\"ac\":9,\"ae\":\"\",\"ad\":\"BYCR5JHJJDRQZK2VPDDQ\",\"ag\":1382371709662,\"ah\":1382981634346,\"cg\":\"SG0978FE8DF36DE98AC942EC221DCD35404566F861\",\"ai\":\"Win32\",\"aj\":\"http://m.avito.ru/pskov/kvartiry\",\"ak\":1},\"b\":[{\"bd\":\"\",\"be\":\"\",\"bk\":-1,\"bl\":0,\"bj\":\"ru\",\"bo\":[],\"bm\":false,\"bn\":{},\"bv\":[],\"bt\":false,\"bu\":{},\"by\":[],\"cd\":0,\"ba\":1382981634346,\"bb\":90145,\"bc\":36612,\"ch\":\"Etc/GMT-4\"}]}");
            //File.WriteAllLines(@"E:\ProxyList\socksWork.txt", ipWorks.Select(x => x.Address + ":" + x.Port));
            AvitoHandler aH = new AvitoHandler();
            var sa = aH.GetFieldAvito("http://m.avito.ru/sankt-peterburg/avtomobili_s_probegom/toyota_will_2002_227393083");
            Helpers.SaveInOnTypeProxy(@"E:\ProxyList\ipMyFind.txt");
            var ipRus = File.ReadAllLines(@"E:\ProxyList\GeoipRus1.txt");
            List<long> time=new List<long>();
            foreach (var str in ipRus)
            {
                var rangeIp = Helpers.GetRangeIpOfStr(str);
                var temp = rangeIp[0].ToString();
                var temp1 = rangeIp[1].ToString();
                //Перебор {111.111}.111.111 где {p0}.i.j
                var ip0 = temp.Substring(0, temp.IndexOf(".", temp.IndexOf(".") + 1));
                var ip1 = temp1.Substring(0, temp1.IndexOf(".", temp1.IndexOf(".") + 1));
                int iEnd, iBegin,zBegin=0,zEnd=1;
                string one = rangeIp[0].ToString().Substring(0, temp.IndexOf("."));
                if (ip0.Equals(ip1))
                {
                    temp = temp.Replace(ip0 + ".", "");
                    var iStr = temp.Substring(0, temp.IndexOf("."));
                    iBegin = Convert.ToInt32(iStr);
                    temp1 = temp1.Replace(ip0 + ".", "");
                    iStr = temp1.Substring(0, temp1.IndexOf("."));
                    iEnd = Convert.ToInt32(iStr);
                }
                else
                { 
                    //111.{p0}.111.111
                    ip0 = temp.Substring(temp.IndexOf(".")+1, temp.IndexOf(".",temp.IndexOf(".")));
                    ip1 = temp1.Substring(temp1.IndexOf(".")+1, temp1.IndexOf(".", temp1.IndexOf(".")));
                    zBegin = Convert.ToInt32(ip0);
                    zEnd= Convert.ToInt32(ip1);
                    temp = temp.Replace(temp.Substring(0, temp.IndexOf(".", temp.IndexOf(".") + 1))+".","");
                    var iStr = temp.Substring(0, temp.IndexOf("."));
                    iBegin = Convert.ToInt32(iStr);
                    temp1 = temp1.Replace(temp1.Substring(0, temp1.IndexOf(".", temp1.IndexOf(".") + 1)) + ".", "");
                    iStr = temp1.Substring(0, temp1.IndexOf("."));
                    iEnd = Convert.ToInt32(iStr);
                }
                
                var st = new Stopwatch();
                var sad = new List<string>();
                sad.Add("--------------------------------------------------------------------------------------------------");
                sad.Add("-----------------" + str + " - " + rangeIp[0].ToString() + ":" + rangeIp[1].ToString() + "----------");
                sad.Add("--------------------------------------------------------------------------------------------------");
                st.Start();
                int zx = 0;
                for (int z = zBegin; z <= zEnd; z++)
                {
                    List<string> p1080=new List<string>();
                    Parallel.For(iBegin, iEnd + 1,/* new ParallelOptions() {MaxDegreeOfParallelism = 10},*/ i =>
                    {
                        string ipStr = string.Empty;
                        if(zEnd==1)
                            ipStr = string.Format("{0}.{1}.0-255", ip0, i);
                        else
                            ipStr = string.Format("{0}.{1}.{2}.0-255",one, z, i);
                        var res = SearchProxy.GetOpenProxyPorts(ipStr);
                        zx++;
                        if (res.Contains("open"))
                        {
                            sad.Add(res);
                        }
                        if (res.Contains("1080"))
                        {
                            p1080.Add(res);
                        }
                    });
                    File.AppendAllLines(@"E:\ProxyList\ScanSocks.txt", p1080);
                    if(zEnd==1)
                        break;
                }
                st.Stop();
                sad.Add("------------------------------------------------------------------------------------------");
               File.AppendAllLines(@"E:\ProxyList\Scan.txt", sad);
               time.Add(st.ElapsedMilliseconds);
            }


            
            var pathProxyList = @"E:\ProxyList\proxy.txt";
            Proxy.FormattingProxyFile(@"E:\ProxyList\ProxyListWhiteSpace.txt", pathProxyList);
            var proxyList = Proxy.ReadIpList(pathProxyList);
            var error400s = new List<string>();

            string[] headerProxies ={ "x-via", "cdn-src-ip", "x-bluecoat-via",
                "mt-proxy-id","x-real-ip","x-proxy-id","x-forwarded-for", "via"
                , "max-forwards", "x-via", "x-forwarded-server", "x-forwarded-host"};
            if (proxyList != null && proxyList.Any())
            {
                var myIp = Proxy.GetIp(null);
                var ipWork = new List<IpAdr>();
                Parallel.ForEach(proxyList, s =>
                {
                    var adr = s.Trim();
                    var endIp = adr.IndexOf(":");
                    if (endIp < 1)
                        endIp = adr.IndexOf(" ");

                    string ip = adr.Substring(0, endIp);
                    int port = Int32.Parse(adr.Substring(endIp + 1));
                    var proxy = new Proxy(new WebProxy(ip, port));
                    if (proxy.IsPing(ip))
                    {
                        var send = proxy.SocketSendHttp();
                        if (!string.IsNullOrEmpty(send))
                        {
                            var status = send.Substring(9, 3);
                            if (status.Substring(0, 1) == "2")
                            {
                                var ipProxy = proxy.GetIp();

                                if (!ipProxy.Equals(""))
                                {
                                    var countryName = proxy.GetGeolocation();
                                    bool visible = ipProxy.Equals(myIp);
                                    var ipSecurity = IpSecurity.Transparent;
                                    if (!visible)
                                    {
                                        if (!ipProxy.Equals(ip))
                                        {
                                            ipProxy = ip;
                                        }
                                        foreach (var header in headerProxies)
                                        {
                                            if (send.IndexOf(header) > 0)
                                            {
                                                ipSecurity = IpSecurity.Anonymous;
                                                break;
                                            }
                                        }
                                        if (ipSecurity == IpSecurity.Transparent)
                                            ipSecurity = IpSecurity.Elite;
                                    }
                                    ipWork.Add(new IpAdr()
                                    {
                                        Ip = ip,
                                        Port = port,
                                        Security = ipSecurity,
                                        TimePing = proxy.TimePing,
                                        Country = countryName
                                    });
                                }
                            }
                            else if (status.Equals("400"))
                            {
                                error400s.Add(ip + ":" + port.ToString());
                            }
                        }
                    }
                });
                File.WriteAllLines(@"E:\ProxyList\proxyElite.txt", ipWork.Where(x => x.Security == IpSecurity.Elite).Select(x => x.Ip + ":" + x.Port));
                File.WriteAllLines(@"E:\ProxyList\proxy.txt", ipWork.Select(x => x.Ip + ":" + x.Port));
                File.WriteAllLines(@"E:\ProxyList\proxyError400.txt", error400s);
                WriteCSV(ipWork, @"E:\ProxyList\proxy.csv");
            }
            var ret = 0;

        }

        static IEnumerable<CategoryCompany> GetContentLink(HtmlDocument html)
        {
            //*[@id="catalog"]/div[1]/div[3]/div[4]/div[3]/div[8]="g_91 catalog-default
            var divNodes = html.DocumentNode.SelectNodes("//div[@class='g_91 catalog-default']/div[@class='t_i']");
            //<h3 class="t_i_h3"> <a class="second-link" name="79279201" href="/pskov/doma_dachi_kottedzhi/kottedzh_830_m_na_uchastke_23_sotok_79279201" title="Коттедж 830 м² на участке 23 соток в Пскове">
            //Коттедж 830 м² на участке 23 соток</a> </h3>
            var category = (from item in divNodes
                            select item.ChildNodes.Where(x => x.Name == "a").ToArray()
                                into aNodes
                                where aNodes.Any()
                                from aLink in aNodes
                                select new CategoryCompany()
                                {
                                    Link = aLink.Attributes["href"].Value,
                                    Name = aLink.ChildNodes.FindFirst("strong").InnerHtml
                                });
            return category;
        }
        private void SearchButton_Click(object sender, EventArgs e)
        {
            int countFirm;
            string linkSite = "http://spravka.pskovlive.ru/companies";

            //Парсим информацию по верхним каталогам
            //div(id=content)>table>td>a(href - адрес каталога)>strong  - Название каталога 
            var CompanyCategories = GetCategoryCompanyLinks(OpenLink(linkSite));
            var t = CompanyCategories.ToList();

            List<string> stopwatches = new List<string>();

            List<string> companiesLink = new List<string>();
            var st = new Stopwatch();
            st.Start();
            //Заходим в каждый каталог. 
            Parallel.ForEach(CompanyCategories, (company, state, i) =>
                {
                    var CompanyCategories2 = from cat in GetCategoryCompanyLinks2(OpenLink(company.Link))
                                             select cat;
                    foreach (var categoryCompany2 in CompanyCategories2)
                    {
                        //Если у ссылок нет Id=razdel_name_... Значит перед нами список компаний,в противном случае собираем ссылки каталогов
                        if (IsCategory(categoryCompany2.Link))
                        {
                            //Заходим в под под каталог
                            var CompanyCategories3 = from cat in GetCategoryCompanyLinks2(OpenLink(categoryCompany2.Link))
                                                     select cat;
                            //Далее вытаскиваем ссылку на компанию a(href) и название из a>
                            companiesLink.AddRange(from categoryCompany3 in CompanyCategories3
                                                   where !IsCategory(categoryCompany3.Link)
                                                   from companyLink in
                                                       GetCompanyLinks(OpenLink(categoryCompany3.Link))
                                                   where
                                                       company.Link != companyLink &&
                                                       categoryCompany2.Link != companyLink
                                                   select companyLink);
                        }
                        else
                        {
                            //Вытаскиваем ссылки на компании
                            var companies = GetCompanyLinks(OpenLink(categoryCompany2.Link));
                            //Далее вытаскиваем ссылку на компанию a(href) и название из a>
                            foreach (
                                var companyLink in companies.Where(companyLink => company.Link != companyLink))
                            {
                                companiesLink.Add(companyLink);
                            }

                        }
                    }
                });
            //Далее заходим в каждую компанию и собираем информацию. div(id=company_contacts). 
            //Ищем слова из массива propertyCompany после каждого слова ищем <em>
            //Если почта или веб сайт то em>a

            //Удаляем повторения
            var companiesHashSet = new HashSet<string>(companiesLink);
            st.Stop();
            stopwatches.Add(st.Elapsed.ToString());
            st = new Stopwatch();
            st.Start();
            //вытаскиваем информацию по каждой организации
            Parallel.ForEach(companiesHashSet, (companyLink, state, i) =>
                {
                    var company = GetCompanyInfo(OpenLink(companyLink), companyLink);
                    if (company != null)
                    {
                        _companies.Add(company);
                    }
                    if (i == 200)
                    {
                        state.Break();
                    }
                });

            st.Stop();
            stopwatches.Add(st.Elapsed.ToString());
            st = new Stopwatch();
            st.Start();
            //Parallel.ForEach(companiesHashSet, (companyLink, state, i) =>
            //{
            //    _companies.AddRange(from com in GetCompanyInfo(OpenLink(companyLink), companyLink) where com!=null
            //                            select com);
            //    if (i == 200)
            //    {
            //        state.Break();
            //    }
            //});

            st.Stop();
            stopwatches.Add(st.Elapsed.ToString());
            st = new Stopwatch();
            st.Start();
            WriteCSV(_companies, @"C:\people.csv");
            //var xvc = _companies.Where(x => x.Email != null);
            WriteXLS(_companies, @"C:\peoplke.xls");

            //Workbook workbook = new Workbook();
            //workbook.LoadFromFile(@"D:\michelle\my file\csv2xls.csv", ",", 1, 1);
            //Worksheet sheet = workbook.Worksheets[0];
            //workbook.SaveToFile("result.xls", ExcelVersion.Version97to2003);


            st.Stop();
            stopwatches.Add(st.Elapsed.ToString());

        }




        private static HtmlDocument OpenLink(string link)
        {
            var web = new HtmlWeb();
            return web.Load(link);
        }

        public static void WriteCSV<T>(IEnumerable<T> items, string path)
        {
            Type itemType = typeof(T);
            var props = itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .OrderBy(p => p.Name);

            using (var writer = new StreamWriter(path))
            {
                writer.WriteLine(string.Join(", ", props.Select(p => p.Name)));

                foreach (var item in items)
                {
                    writer.WriteLine(string.Join(", ", props.Select(p => p.GetValue(item, null))));
                }
            }
        }

        public static void WriteXLS<T>(IEnumerable<T> items, string filePath)
        {
            Type itemType = typeof(T);
            var props = itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .OrderBy(p => p.Name);
            var xlApp = new Microsoft.Office.Interop.Excel.Application();
            Workbook xlWorkBook;
            Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            //Если не существует файла то создать его
            bool isFileExist;
            var fInfo = new FileInfo(filePath);
            if (!fInfo.Exists)
            {
                xlWorkBook = xlApp.Workbooks.Add(misValue); //Добавить новый book в файл
                isFileExist = false;
            }
            else
            {
                //Открыть существующий файл
                xlWorkBook = xlApp.Workbooks.Open(filePath, 0, false, 5, "", "", true,
                                                  XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                isFileExist = true;
            }
            //Открытие первой вкладки - таблица 1
            xlWorkSheet = (Worksheet)xlWorkBook.Sheets[1];
            //Получить количество используемых строк
            int usedRowsCount = xlWorkSheet.UsedRange.Rows.Count;

            //Запись значения в самую первую ячейку  [y - строка,x - столбец]
            int i = 1;
            //Хедер таблицы
            foreach (var propertyInfo in props)
            {
                xlWorkSheet.Cells[1, i] = propertyInfo.Name;
                i++;
            }
            //Так как в первой строке название столбцов
            int y = 2;
            var propsList = props.ToList();
            //Тело таблицы
            foreach (var item in items)
            {
                for (int j = 1; j <= props.Count(); j++)
                {
                    xlWorkSheet.Cells[y, j] = propsList[j - 1].GetValue(item);
                }
                y++;
            }

            //Если файл существовал, просто сохранить его по умолчанию. Иначе сохранить в указанную директорию
            if (isFileExist)
            {
                xlWorkBook.Save();
            }
            else
            {
                xlWorkBook.SaveAs(filePath, XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue,
                                  misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue,
                                  misValue);
            }
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();
            //Освобождение ресурсов
            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
        }
        /// <summary>
        /// Serializes an object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="fileName"></param>
        public static void WriteXML<T>(T item, string fileName)
        {
            if (item == null) { return; }

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(item.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, item);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(fileName);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                //Log exception here
            }
        }


        /// <summary>
        /// Deserializes an xml file into an object list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T ReadXml<T>(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) { return default(T); }

            T objectOut = default(T);

            try
            {
                string attributeXml = string.Empty;

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(T);

                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        objectOut = (T)serializer.Deserialize(reader);
                        reader.Close();
                    }

                    read.Close();
                }
            }
            catch (Exception ex)
            {
                //Log exception here
            }

            return objectOut;
        }
        public static void WriteXML<T>(IEnumerable<T> items, string filePath)
        {

        }

        static bool IsCategory(string LinkCategory)
        {
            var doc = OpenLink(LinkCategory);
            if (doc != null)
            {
                var aNodes = doc.DocumentNode.SelectNodes("//a[contains(@id,'razdel_name')]");
                if (aNodes != null)
                    return true;
            }
            return false;
        }


        static Company GetCompanyInfo(HtmlDocument html, string link, string catalog = "", string catalog2 = "", string catalog3 = "")
        {
            var divNode = html.GetElementbyId("company_contacts");
            if (divNode != null)
            {
                var emNodes = divNode.SelectNodes("//em");
                if (emNodes != null)
                {
                    var aNameNode = html.DocumentNode.SelectSingleNode("//h3 /a");
                    var company = new Company() { Title = aNameNode.InnerText, Catalog = catalog, Url = link, Catalog2 = catalog2, Catalog3 = catalog3 };
                    var propertyCompany = new List<string>() { "Адрес", "Телефон", "Факс", "Веб-сайт" };
                    var innerText = divNode.InnerText;
                    foreach (var emNode in emNodes)
                    {
                        if (emNode.InnerText.IndexOf("eval(unescape('") > 0)
                        {
                            var str = emNode.InnerText.Substring(emNode.InnerText.IndexOf("eval(unescape('") + 15);
                            str = str.Remove(str.IndexOf("')"));
                            str = HttpUtility.UrlDecode(str);
                            str = str.Substring(str.IndexOf("('") + 2);
                            str = str.Remove(str.IndexOf("')"));
                            HtmlNode node = HtmlNode.CreateNode(str);
                            company.Email = node.InnerText;
                        }
                        else
                        {
                            for (var i = 0; i < propertyCompany.Count(); i++)
                            {
                                if (innerText.IndexOf(propertyCompany[i]) > 0)
                                {
                                    switch (propertyCompany[i])
                                    {
                                        case "Адрес":
                                            {
                                                company.Address = emNode.InnerText;
                                                break;
                                            }
                                        case "Телефон":
                                            {
                                                company.Phone = emNode.InnerText;
                                                break;
                                            }
                                        case "Факс":
                                            {
                                                company.Fax = emNode.InnerText;
                                                break;
                                            }
                                        case "Веб-сайт":
                                            {
                                                company.Site = emNode.InnerText;
                                                break;
                                            }
                                    }
                                    propertyCompany.Remove(propertyCompany[i]);
                                    break;
                                }
                            }
                        }
                    }
                    return company;
                }
            }
            return null;
        }

        static IEnumerable<string> GetCompanyLinks(HtmlDocument html)
        {
            var aNodes = html.DocumentNode.SelectNodes("//div[@id='content'] //div //div //div /a");
            if (aNodes != null)
                return aNodes.Select(aNode => aNode.Attributes["href"].Value);
            return null;
        }

        static IEnumerable<CategoryCompany> GetCategoryCompanyLinks(HtmlDocument html)
        {
            var tdNodes = html.DocumentNode.SelectNodes("//div[@id='content'] //table //tr //td");

            var category = (from item in tdNodes
                            select item.ChildNodes.Where(x => x.Name == "a").ToArray()
                                into aNodes
                                where aNodes.Any()
                                from aLink in aNodes
                                select new CategoryCompany()
                                {
                                    Link = aLink.Attributes["href"].Value,
                                    Name = aLink.ChildNodes.FindFirst("strong").InnerHtml
                                });
            return category;
        }

        static IEnumerable<CategoryCompany> GetCategoryCompanyLinks2(HtmlDocument html)
        {
            var aNodes = html.DocumentNode.SelectNodes("//a[contains(@id,'razdel_name')]");
            var categories = new List<CategoryCompany>();
            if (aNodes.Any())
            {
                foreach (var aNode in aNodes)
                {
                    categories.Add(new CategoryCompany()
                        {
                            Link = aNode.Attributes["href"].Value,
                            Name = aNode.InnerText
                        });
                }
            }

            return categories;
        }
        public void WorkWithExcel()
        {
            var xlApp = new Microsoft.Office.Interop.Excel.Application();
            Workbook xlWorkBook;
            Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            string filePath = "c:\\InputValuesExcel.xls";
            //Если не существует файла то создать его
            bool isFileExist;
            var fInfo = new FileInfo(filePath);
            if (!fInfo.Exists)
            {
                xlWorkBook = xlApp.Workbooks.Add(misValue); //Добавить новый book в файл
                isFileExist = false;
            }
            else
            {
                //Открыть существующий файл
                xlWorkBook = xlApp.Workbooks.Open(filePath, 0, false, 5, "", "", true,
                                                  XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                isFileExist = true;
            }
            //Открытие первой вкладки - таблица 1
            xlWorkSheet = (Worksheet)xlWorkBook.Sheets[1];
            //Запись значения в самую первую ячейку  [y - строка,x - столбец]
            xlWorkSheet.Cells[1, 1] = "Client Name";

            //Получить количество используемых столбцов
            int columnsCount = xlWorkSheet.UsedRange.Columns.Count;
            //Получить количество используемых строк
            int usedRowsCount = xlWorkSheet.UsedRange.Rows.Count;
            //Проверить значение последней используемой ячейки в последней исползуеймой строке и 
            //последнем используемом столбце. Если значение этой ячейки равно "Привет", изменить его на "Пока".
            if ((xlWorkSheet.UsedRange.Cells[usedRowsCount, columnsCount] as Range).Value2 != null)
            {
                if ((xlWorkSheet.UsedRange.Cells[usedRowsCount, columnsCount] as Range).Value2.ToString() == "Привет")
                    (xlWorkSheet.UsedRange.Cells[usedRowsCount, columnsCount] as Range).Value2 = "Пока";
            }
            //Если файл существовал, просто сохранить его по умолчанию. Иначе сохранить в указанную директорию
            if (isFileExist)
            {
                xlWorkBook.Save();
            }
            else
            {
                xlWorkBook.SaveAs(filePath, XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue,
                                  misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue,
                                  misValue);
            }
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();
            //Освобождение ресурсов
            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
        }

        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                Console.Write("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
        #region YandexMetrika
        private void SearchYandex_Click(object sender, EventArgs e)
        {
            setProxy("109.194.65.175", 3128);
            var modelsSet = new HashSet<string>(OpenFileTxtToLine(@"E:\models.txt")).ToArray();
            var modelsYM = new List<Mod>();
            Stopwatch fd = new Stopwatch();
            fd.Start();
            foreach (var model in modelsSet)
            {
                var idProduct = FindIdFromQueryYM(model);
                if (idProduct != null)
                {
                    var value = GetPropertyProductYM(idProduct, "Тип электростанции").Values;
                    modelsYM.Add(new Mod() { Title = model, Value = value.First() });
                }
            }

            fd.Stop();
            using (StreamWriter sw = new StreamWriter(@"E:\end.txt", true))
            {
                foreach (var mod in modelsYM)
                {
                    sw.WriteLine(mod.Title + "   -   " + mod.Value);
                }
                sw.Close();
            }
        }
        /// <summary>
        /// Выбирает первый по списку товар из поиска
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static string FindIdFromQueryYM(string query)
        {
            //https://api.partner.market.yandex.ru/v2/models.xml?query=iPhone+4s
            string link = "http://market.yandex.ru/search.xml?text=";
            link += HttpUtility.UrlEncode(query);
            var page = OpenLink(link);
            if (page == null) return null;
            var models = page.DocumentNode.SelectNodes("//div[contains(concat(' ', @class, ' '), ' b-offers_type_guru_mix ')]");
            if (models == null) return null;
            var idProduct = models[0].Attributes["id"].Value;
            if (idProduct == null) return null;
            return idProduct;
        }
        /// <summary>
        /// Получение всех свойств товара по id - по умолчанию, или можно найти только определённые свойства
        /// </summary>
        /// <param name="modelId">id товара</param>
        /// <param name="findProp">Название нужного свойства</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetPropertyProductYM(string modelId, string findProp = "")
        {
            //http://market.yandex.ru/model-spec.xml?modelid=
            string link = "http://market.yandex.ru/model-spec.xml?modelid=" + modelId;
            var page = OpenLink(link);
            var propertyList = page.DocumentNode.SelectNodes("//table[contains(concat(' ', @class, ' '), ' b-properties ')]//tr");
            propertyList.Remove(0);
            var propDictionary = new Dictionary<string, string>();
            foreach (var prop in propertyList)
            {
                var name = prop.ChildNodes[0].ChildNodes[0].InnerText; //SelectNodes("th/span");//[0].InnerText;

                if (findProp != "")
                {
                    if (findProp.Trim() == name.Trim())
                    {
                        var value = prop.ChildNodes[1].InnerText;
                        propDictionary.Add(name, value);
                        break;
                    }
                }
                else
                {
                    var value = prop.ChildNodes[0].InnerText;
                    propDictionary.Add(name, value);
                }
            }
            return propDictionary;
        }

        /// <summary>
        /// Считвания из текстовго файла построчно в массив в кодировке 1251
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] OpenFileTxtToLine(string path)
        {
            if (File.Exists(path))
            {
                string[] list = File.ReadAllLines(path, Encoding.GetEncoding(1251)); //чтение всех строк файла в массив строк
                return list;
            }
            return null;
        }

        public static XmlDocument OpenLinkXML(string link)
        {
            var xdoc = new XmlDocument();
            xdoc.Load(link);
            return xdoc;
        }
        private void setProxy(string ip, int port)
        {
            WebProxy proxyObject = new WebProxy(ip, port);
            GlobalProxySelection.Select = proxyObject;
        }
        #endregion


    }

    class Mod
    {
        public string Title { get; set; }
        public string Value { get; set; }
    }
    class Company
    {
        public string Title { get; set; }
        public string Catalog { get; set; }
        public string Catalog2 { get; set; }
        public string Catalog3 { get; set; }
        public string Url { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }

    }

    class CategoryCompany
    {
        public string Name { get; set; }
        public string Link { get; set; }
    }

    class PreviewLink
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }

    class AdAvito
    {
        public string Title { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
    }


    class IpAdr
    {
        public string Ip { get; set; }
        public int Port { get; set; }
        public IpSecurity Security { get; set; }
        public long TimePing { get; set; }
        public string Country { get; set; }
    }
}
