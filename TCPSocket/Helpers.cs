using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TCPSocket
{
    public static class Helpers
    {
        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
        /// <summary>
        /// Вытаскивает все ip + port с заданной страницы+
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public static List<string> GetIpEndOfLink(string link)
        {

            return null;
        }

        public static IPAddress[] GetRangeIpOfStr(string str)
        {
            var ip=new IPAddress[2];
            var ips = str.Trim().Substring(0, str.IndexOf(",") - 1);
            ip[0] = IPAddress.Parse(ips);
            ips = str.Trim().Substring(str.IndexOf(",") + 1);
            ip[1] = IPAddress.Parse(ips);
            return ip;
        }
        public static List<IPEndPoint> GetIpEndOfFile(string path)
        {
            var ipList=ReadIpList(path);
            if (ipList != null && ipList.Any())
            {
                var ipEnds=new List<IPEndPoint>();
                foreach (var s in ipList)
                {
                    var adr = s.Trim();
                    var endIp = adr.IndexOf(":");
                    if (endIp < 1)
                        endIp = adr.IndexOf(" ");

                    string ip = adr.Substring(0, endIp);
                    int port = Int32.Parse(adr.Substring(endIp + 1));
                    ipEnds.Add(new IPEndPoint(IPAddress.Parse(ip),port));
                }
                return ipEnds;
            }
            return null;
        }

        public static string GetIp(WebProxy pr)
        {
            string link = "http://ip2country.sourceforge.net/ip2c.php?format=JSON";
            var s = DownloadString(link, pr, true);
            if (string.IsNullOrEmpty(s) || s.IndexOf("{ip:") < 0)
                return string.Empty;
            var res = JsonConvert.DeserializeObject<Dictionary<string, string>>(s);
            return res != null ? res["ip"] : string.Empty;
        }

        public static string GetGeolocation(WebProxy pr)
        {
            string link = "http://api.hostip.info/country.php?ip=" + (pr != null ? pr.Address.DnsSafeHost : string.Empty);
            //var res = JsonConvert.DeserializeObject<Dictionary<string, string>>(Downloads.DownloadString(link, null, true));
            var res = DownloadString(link, null, true);
            return res ?? string.Empty;
        }

        public static HashSet<string> ReadIpList(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    var strHS = new HashSet<string>(File.ReadAllLines(path));
                    return strHS;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error read file: " + path);
                    return null;
                }
            }
            return null;
        }

        public static string GetHostOfLink(string link)
        {
            var res = string.Empty;
            var host = Regex.Replace(link, @"(http(s)?://)(www.)|(www.)|(http(s)?://)", String.Empty);
            res = host.IndexOf("/") > 0 ? host.Remove(host.IndexOf("/")) : host;
            return res;
        }

        /// <summary>
        /// Разделяем списки на сокс и http и сохраняем в разных файлах
        /// </summary>
        public static bool SaveInOnTypeProxy(string path, string pathSocks = @"E:\ProxyList\socksMyFind.txt", string pathProxy = @"E:\ProxyList\proxyMyFind.txt")
        {
            if (File.Exists(path))
            {
                var lines = File.ReadAllLines(path);
                List<string> socks=new List<string>();
                List<string> http = new List<string>();
                foreach (var line in lines)
                {
                    if (line.Contains("1080") || line.Contains("1081"))
                    {
                        socks.Add(line);
                    }
                    else
                    {
                        http.Add(line);
                    }
                }
                
                File.AppendAllLines(pathProxy, http);
                File.AppendAllLines(pathSocks, socks);
                File.WriteAllText(path, "");
                return true;
            }
            return false;
        }
        /// <summary>
        /// Выбирает все ip и port из файла, после программы nmap
        /// </summary>
        /// <param name="path"></param>
        /// <returns>List (ip:port)</returns>
        public static List<string> GetIpPortOfFileNmap(string path)
        {
            //Discovered open port 80/tcp on 31.8.128.7
            if (File.Exists(path))
            {
                var lines=File.ReadAllLines(path);
                var discovered = new List<string>();
                Parallel.ForEach(lines, l =>
                {
                    if (l.Contains("Discovered"))
                    {
                        discovered.Add(l);
                    }
                });
                var res=new List<string>();
                foreach (var dis in discovered)
                {
                    if (!string.IsNullOrEmpty(dis))
                    {
                        var ip = dis.Substring(dis.IndexOf("on") + 2).Trim();
                        var port = dis.Substring(dis.IndexOf("port") + 4, dis.IndexOf(@"/") - (dis.IndexOf("port") + 4)).Trim();
                        res.Add(ip + ":" + port);
                    }
                }
                File.AppendAllLines(@"E:\ProxyList\ipMyFind.txt", res);
                return res;
            }
            return null;
        }

        public static string DownloadString(string link)
        {
            return DownloadString(link, null);
        }
        public static string DownloadString(string link, WebProxy webProxy, bool json = false)
        {
            string result = "";
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



        public static KeyValuePair<object, object> Cast<K, V>(this KeyValuePair<K, V> kvp)
        {
            return new KeyValuePair<object, object>(kvp.Key, kvp.Value);
        }

        public static KeyValuePair<T, V> CastFrom<T, V>(Object obj)
        {
            return (KeyValuePair<T, V>)obj;
        }
        /// <summary>
        /// Конвертировать object to Dictionary
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static KeyValuePair<object, object> CastFrom(Object obj)
        {
            var type = obj.GetType();
            if (type.IsGenericType)
            {
                if (type == typeof(KeyValuePair<,>))
                {
                    var key = type.GetProperty("Key");
                    var value = type.GetProperty("Value");
                    var keyObj = key.GetValue(obj, null);
                    var valueObj = value.GetValue(obj, null);
                    return new KeyValuePair<object, object>(keyObj, valueObj);
                }
            }
            throw new ArgumentException(" ### -> public static KeyValuePair<object , object > CastFrom(Object obj) : Error : obj argument must be KeyValuePair<,>");
        }

        public static Dictionary<TKey, TValue> ConvertToDictionary<TKey, TValue>(object obj)
        {
            var stringDictionary = obj as Dictionary<TKey, TValue>;

            if (stringDictionary != null)
            {
                return stringDictionary;
            }
            var baseDictionary = obj as IDictionary;

            if (baseDictionary != null)
            {
                var dictionary = new Dictionary<TKey, TValue>();
                foreach (DictionaryEntry keyValue in baseDictionary)
                {
                    if (!(keyValue.Value is TValue))
                    {
                        // value is not TKey. perhaps throw an exception
                        return null;
                    }
                    if (!(keyValue.Key is TKey))
                    {
                        // value is not TValue. perhaps throw an exception
                        return null;
                    }

                    dictionary.Add((TKey)keyValue.Key, (TValue)keyValue.Value);
                }
                return dictionary;
            }
            // object is not a dictionary. perhaps throw an exception
            return null;

        }
    }
}
