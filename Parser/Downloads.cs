using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;

namespace Parser
{
    public class Downloads
    {
        public static string DownloadString(string link)
        {
            return DownloadString(link, null);
        }
        public static string DownloadString(string link,WebProxy webProxy,bool json=false)
        {
            string result="";
            WebRequest request = WebRequest.Create(link);
            if (webProxy != null)
                request.Proxy = webProxy;
            if (json)
                request.ContentType = "text/json";
            
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (var dataStream = response.GetResponseStream())
                {
                    if (dataStream != null)
                    {
                        using (var reader = new StreamReader(dataStream))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                }
                response.Close();
            }
            catch (Exception ex)
            {
                try
                {
                    Console.WriteLine("Error DownloadString");
                    HttpWebResponse response = (HttpWebResponse) request.GetResponse();
                    using (var dataStream = response.GetResponseStream())
                    {
                        if (dataStream != null)
                        {
                            using (var reader = new StreamReader(dataStream))
                            {
                                result = reader.ReadToEnd();
                            }
                        }
                    }
                    response.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Repeat Error DownloadString");
                }
            }
            finally
            {
                
            }
            return result;
        }
        


        
        
    }
}
