using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using TCPSocket;

namespace ParseLib.Avito
{
    public class AvitoHandler
    {
        private const string PATH_SOCKS_FILE = @"E:\ProxyList\socksWork.txt";

        public AvitoHandler()
        {

        }

        protected string GetCookie(String head)
        {
            //Set-Cookie: sessid=f024d0adc04377b57e9d8bc3ce1e2ec4.1381141897; expires=Tue, 08-Oct-2013 10:31:37 GMT; Max-Age=86400; path=/; domain=.avito.ru; httponly
            //Set-Cookie: u=1o6m9qld.uhprgp.e9cspjcpqy; expires=Wed, 07-Oct-2015 10:31:37 GMT; Max-Age=63072000; path=/; domain=.avito.ru
            //Set-Cookie: _mlocation=621540; expires=Tue, 07-Oct-2014 10:31:37 GMT; Max-Age=31536000; path=/
            if (head.Contains("Set-Cookie"))
            {
                var beginSetCookie = head.Substring(head.IndexOf("Set-Cookie:")).Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                var res = string.Empty;
                foreach (var s in beginSetCookie)
                {
                    if (s.Contains("Set-Cookie:"))
                    {
                        var temp = s.Substring(s.IndexOf("ie:") + 3, s.IndexOf(";") - s.IndexOf("ie:") - 3);
                        res += temp + "; ";
                    }
                }
                return res;
            }
            return string.Empty;
        }

        protected string GetPhone(String adLink, string coockie)
        {
            
            return null;
        }

        protected string GetKeyAd(string link)
        {
            return null;
        }

        public Avito GetFieldAvito(string link)
        {
            if (link == null)
                throw new NullReferenceException();
            var ipWorks = Helpers.GetIpEndOfFile(@"E:\ProxyList\socksWork.txt");
            Socks5 socks = null;
            var response = string.Empty;

            foreach (var sock in ipWorks)
            {
                Socks5 temp = new Socks5(sock);
                if (temp.Connect("m.avito.ru"))
                {
                    response = temp.GetResponse(link);
                    if (response.Contains("200 OK"))
                    {
                        socks = temp;
                        break;
                    }
                }
            }
            if (socks != null)
            {
                var cookie = GetCookie(response);
                var html = new HtmlDocument();
                html.LoadHtml(response);
                var title =
                    html.DocumentNode.SelectNodes("//h2[contains(concat(' ', @class, ' '), ' m_item_title ')]")
                        .First()
                        .InnerText;
                var sdfas = html.DocumentNode.SelectNodes("//p[contains(concat(' ', @class, ' '), ' m_item_date ')]")
                    .First();
                var date =sdfas.InnerText;
                if (date.Contains("Сегодня"))
                {
                    var i = date.IndexOf("Сегодня в ");
                    date = date.Remove(i, i + 10);
                    var now = DateTime.Now.ToString("dd-MMMM");
                    date = now + ", " + date;
                }
                else if (date.Contains("Вчера"))
                {
                    var i = date.IndexOf("Вчера в ");
                    date = date.Remove(i, i + 8);
                    var now = DateTime.Now.AddDays(-1).ToString("dd-MMMM");
                    date = now + ", " + date;
                }
                
                var ul = html.DocumentNode.SelectNodes("//ul[contains(concat(' ', @class, ' '), ' list ')]").First();
                var price = string.Empty;
                var name = string.Empty;
                var name2 = string.Empty;
                var city = string.Empty;
                var email = string.Empty;
                var section = string.Empty;
                foreach (var childNode in ul.ChildNodes)
                {
                    if (childNode.InnerText.Contains("Цена"))
                    {
                        price = childNode.InnerHtml;
                        //Вытаскиваем только цифры из строки
                        price = string.Join("", price.Where(Char.IsDigit).Select(x => x.ToString()));
                    }
                    else if (childNode.InnerText.Contains("Продавец"))
                    {
                        name = childNode.InnerHtml;
                        name = name.Remove(0, name.IndexOf("</strong>") + 9).Trim();
                        if(!string.IsNullOrEmpty(name2))
                            name = name2 + "(" + name + ")";
                    }
                    else if (childNode.InnerText.Contains("Контактное лицо"))
                    {
                        name2 = childNode.InnerHtml;
                        name2 = name2.Remove(0, name2.IndexOf("</strong>") + 9).Trim();
                        if (!string.IsNullOrEmpty(name))
                            name = name2 + "(" + name + ")";
                    }
                    else if (childNode.InnerText.Contains("Город"))
                    {
                        city = childNode.InnerHtml;
                        city = city.Remove(0, city.IndexOf("</strong>") + 9).Trim();
                    }
                    else if (childNode.InnerText.Contains("Email"))
                    {
                        email = childNode.InnerHtml;
                    }
                    else if (childNode.InnerText.Contains("Категория"))
                    {
                        section = childNode.InnerHtml;
                        section = section.Remove(0, section.IndexOf("</strong>") + 9).Trim();
                    }
                }

                var description =
                    ul.SelectNodes("//li[contains(concat(' ', @class, ' '), ' m_item_desc ')]").First().InnerText;
                var sort = string.Empty;
                //<div class="custom_params">дизельный двигатель, полный привод, внедорожник, есть в наличии </div>
                var divs = ul.SelectNodes("//div[contains(concat(' ', @class, ' '), ' custom_params ')]");
                foreach (var div in divs)
                {
                    sort += " " + div.InnerText.Trim();
                }
                sort = sort.Trim();
                
                
                var ulInfo =
                    html.DocumentNode.SelectNodes("//ul[contains(concat(' ', @class, ' '), ' list-info ')]").First();
                var id = string.Empty;
                var countShow = string.Empty;
                foreach (var childNode in ulInfo.ChildNodes)
                {
                    if (childNode.InnerText.Contains("Номер"))
                    {
                        id = childNode.InnerHtml;
                        id = id.Remove(0, id.IndexOf("ия:") + 3).Trim();
                    }else if (childNode.InnerText.Contains("Просмотры"))
                    {
                        countShow = childNode.InnerHtml;
                        countShow = countShow.Remove(countShow.IndexOf(",")).Trim(); 
                        countShow=string.Join("", countShow.Where(Char.IsDigit).Select(x => x.ToString()));
                    }
                }
                
                var phoneUrl =
                    ul.SelectNodes("//a[contains(concat(' ', @id, ' '), ' showPhoneBtn ')]").First().Attributes["href"]
                        .Value;
                phoneUrl = "http://m.avito.ru" + phoneUrl+"?async";

                //<script type="text/javascript" async="" src="https://data.flurry.com/aah.do?d=eyJhIjp7ImFmIjoxMzg0Mjg1MzQxNjU2LCJhYSI6MSwiYWIiOjEwLCJhYyI6OSwiYWUiOiJtb2JpbGUiLCJhZCI6IkJZQ1I1SkhKSkRSUVpLMlZQRERRIiwiYWciOjEzODIzNzE3MDk2NjIsImFoIjoxMzg0Mjg1MTE4MDI0LCJjZyI6IlNHMDk3OEZFOERGMzZERTk4QUM5NDJFQzIyMURDRDM1NDA0NTY2Rjg2MSIsImFpIjoiV2luMzIiLCJhaiI6Imh0dHA6Ly9tLmF2aXRvLnJ1L3Bza292L2t2YXJ0aXJ5IiwiYWsiOjF9LCJiIjpbeyJiZCI6Im1vYmlsZSIsImJlIjoiIiwiYmsiOi0xLCJibCI6MCwiYmoiOiJydSIsImJvIjpbXSwiYm0iOmZhbHNlLCJibiI6e30sImJ2IjpbXSwiYnQiOmZhbHNlLCJidSI6e30sImJ5IjpbXSwiY2QiOjAsImJhIjoxMzg0Mjg1MTE4MDI0LCJiYiI6MjI3MDQyLCJiYyI6LTEsImNoIjoiRXRjL0dNVC00In1dfQ==&amp;c=4035538312"></script>
                //FlurryAgent.startSession("BYCR5JHJJDRQZK2VPDDQ");
                string allHtml = html.DocumentNode.SelectNodes("//body").First().InnerHtml;
                var beginKey = allHtml.IndexOf("startSession")+14;
                var endKey = allHtml.IndexOf(");", beginKey + 3)-1;
                var key = allHtml.Substring(beginKey,endKey-beginKey);
                AvJs avJs=new AvJs(key,cookie,link);
                var valJs = avJs.GetLinkAndCoockie();
                var phoneReq = socks.GetResponse(phoneUrl,refererLink:link, cookie:cookie);
                var phone = GetPhone(phoneUrl, cookie);
                Avito avito = new Avito()
                {
                    Id = id,
                    City = city,
                    CountShow = countShow,
                    Description = description,
                    NameContact = name,
                    Date = date,
                    Title = title,
                    Url = link,
                    Phone = phone,
                    Address = ""
                };
                return avito;
            }
            else
            {
                throw new SocketException();
            }
            return null;
        }

        protected void DownloadImage(List<string> urls)
        {

        }

        protected List<string> GetCity()
        {
            return null;
        }
    }
}
