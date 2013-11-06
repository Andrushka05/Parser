using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;
using TCPSocket;

namespace Parser
{
    public class Proxy
    {
        private WebProxy m_WebProxy;
        private long m_TimePing;
        public Proxy(WebProxy pr)
        {
            m_WebProxy = pr;
            m_TimePing = 0;
        }
        public Proxy(string ip, int port)
            : this(new WebProxy(ip, port))
        { }

        public WebProxy WebProxy { get { return m_WebProxy; } set { m_WebProxy = value; } }
        public long TimePing { get; set; }

        /// <summary>
        /// http://jsonip.appspot.com/
        /// </summary>
        /// <returns></returns>
        public string GetIp()
        {
            return GetIp(m_WebProxy);
        }

        public static string GetIp(WebProxy pr)
        {
            string link = "http://ip2country.sourceforge.net/ip2c.php?format=JSON";
            var s = Downloads.DownloadString(link, pr, true);
            if (string.IsNullOrEmpty(s) || s.IndexOf("{ip:") < 0)
                return string.Empty;
            var res = JsonConvert.DeserializeObject<Dictionary<string, string>>(s);
            return res != null ? res["ip"] : string.Empty;
        }

        /// <summary>
        /// http://api.hostip.info/get_json.php
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public string GetGeolocation()
        {
            return GetGeolocation(m_WebProxy);
        }
        public static string GetGeolocation(WebProxy pr)
        {
            string link = "http://api.hostip.info/country.php?ip=" + (pr != null ? pr.Address.DnsSafeHost : string.Empty);
            //var res = JsonConvert.DeserializeObject<Dictionary<string, string>>(Downloads.DownloadString(link, null, true));
            var res = Downloads.DownloadString(link, null, true);
            return res ?? string.Empty;
        }

        public static Socket ConnectSocket(IPEndPoint ipEnd)
        {
            IPHostEntry hostEntry = null;
            Socket tempSocket = null;
            tempSocket = new Socket(ipEnd.AddressFamily, SocketType.Stream, ProtocolType.Tcp) { SendTimeout = 7000, ReceiveTimeout = 7000 };
            try
            {
                tempSocket.Connect(ipEnd);
            }
            catch (SocketException ex)
            {
                Console.WriteLine("Connect error" + ipEnd.Address.ToString() + ":" + ipEnd.Port.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ConnectSocket");
                tempSocket = null;
            }

            return tempSocket;
        }

        public string SocketSendSocks()
        {
            //if (m_WebProxy != null)
                //return SocketSendSocks(IPAddress.Parse(m_WebProxy.Address.Host), m_WebProxy.Address.Port);
            return null;
        }
        
        /// <summary>
        /// Create request for Socks4/Socks5
        /// </summary>
        /// <param name="tSocks">Type Socks</param>
        /// <param name="username">For auth Socks4</param>
        /// <returns></returns>
        private byte[] _requestSocks(TypeSocks tSocks, string username="")
        {
            byte[] bRequest = new byte[50];
            var port = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)80));
            bRequest[0] = (byte)tSocks;
            bRequest[1] = (byte)OperationSocks.Connect;
            if (tSocks == TypeSocks.Socks5)
            {
                bRequest[2] = 0x00;
                bRequest[3] = (byte) TypeAddressSocks.HostName;
                var hostName = Encoding.ASCII.GetBytes("google.com");
                bRequest[4] = (byte) hostName.Length;
                Array.Copy(hostName, 0, bRequest, 5, hostName.Length);
                bRequest[5 + hostName.Length] = port[0];
                bRequest[6 + hostName.Length] = port[1];
            }
            else
            {
                bRequest[2] = port[0];
                bRequest[3] = port[1];
                var ip = Encoding.ASCII.GetBytes("173.194.32.191");
                bRequest[4] = ip[0];
                bRequest[5] = ip[1];
                bRequest[6] = ip[2];
                bRequest[7] = ip[3];
                if (string.IsNullOrEmpty(username))
                    bRequest[8] = 0;
                else
                {
                    var bUser = Encoding.ASCII.GetBytes(username);
                    Array.Copy(bUser,0,bRequest,8,bUser.Length);
                    bRequest[8 + bUser.Length] = 0;
                }
            }

            return bRequest;
        }

        private byte[] _requestSocks4(TypeSocks tSocks)
        {
            byte[] bRequest = new byte[300];
            bRequest[0] = (byte)tSocks;
            bRequest[1] = (byte)OperationSocks.Connect;

            return bRequest;
        }

        
        public string SocketSendHttp()
        {
            if (m_WebProxy != null)
                return SocketSendHttp(IPAddress.Parse(m_WebProxy.Address.Host), m_WebProxy.Address.Port);
            return string.Empty;
        }
        /// <summary>
        /// Socket sent, Ip:port
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns>if return "" - error</returns>
        public static string SocketSendHttp(IPAddress ip, int port)
        {
            string request = "HEAD http://www.python.org/index.html HTTP/1.1\r\nHost: python.org\r\nAccept:text/plain\r\n\r\n";
            Byte[] bytesSent = Encoding.ASCII.GetBytes(request);
            var bytesReceived = new Byte[512];

            Socket s = ConnectSocket(new IPEndPoint(ip, port));
            if (s == null)
                return string.Empty;
            string page = string.Empty;
            int bytes = 0;
            try
            {
                s.Send(bytesSent, bytesSent.Length, 0);
                bytes = s.Receive(bytesReceived, bytesReceived.Length, 0);
                page = page + Encoding.ASCII.GetString(bytesReceived, 0, bytes);
                s.Close();
            }
            catch (SocketException ex)
            {
                Console.WriteLine("Ruptured bond ahead of time: " + ip + ":" + port.ToString());
                s = ConnectSocket(new IPEndPoint(ip, port));
                try
                {
                    s.Send(bytesSent, bytesSent.Length, 0);
                    bytes = s.Receive(bytesReceived, bytesReceived.Length, 0);
                    page = page + Encoding.ASCII.GetString(bytesReceived, 0, bytes);
                    s.Close();
                }
                catch (Exception exc)
                {
                    if (s != null) s.Close();
                    page = string.Empty;
                    Console.WriteLine("Repeat error: " + ip + ":" + port.ToString());
                }
            }
            catch (Exception ex)
            {
                if (s != null) s.Close();
                page = string.Empty;
                Console.WriteLine("Error SendRequestSocket");
            }

            return page;
        }

        public bool IsPing(string address)
        {
            Ping ping = new Ping();

            try
            {
                PingReply reply = ping.Send(address, 5000);

                if (reply.Status == IPStatus.TimedOut)
                {
                    reply = ping.Send(address, 5000);
                }

                m_TimePing = reply.RoundtripTime;
                return (reply.Status == IPStatus.Success);
            }
            catch (PingException e)
            {
                return false;
            }
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

        public static bool FormattingProxyFile(string path, string pathSave)
        {
            if (File.Exists(path))
            {
                try
                {
                    var text = File.ReadAllText(path);
                    if (!string.IsNullOrEmpty(text))
                    {
                        var result = text.Split(Convert.ToChar(" ")).ToList();
                        if (File.Exists(pathSave))
                            File.AppendAllLines(pathSave, result);
                        else
                            File.WriteAllLines(pathSave, result);
                        File.Delete(path);
                        File.Create(path);
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error read file(formatting): " + path);
                }
            }
            return false;
        }
    }
}
