using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TCPSocket
{
    public class Socks5 : Socks, IDisposable
    {
        protected TypeAddressSocks m_tAddressSocks;
        protected AuthSocks m_authSocks;
        protected string m_password;

        public Socks5(IPEndPoint ip)
            : this(ip, string.Empty, string.Empty)
        { }

        public Socks5(IPEndPoint ip, string username, string password)
            : base(ip, username)
        {
            m_password = password;

        }
        public string Password{get { return m_password; } set { m_password = value; }}
        public AuthSocks AuthSocks{get { return m_authSocks; } }
        public TypeAddressSocks TypeAddressSocks { get { return m_tAddressSocks; } }


        protected override byte[] RequestBytes(string domainName,int port=80)
        {
            var hostName = Encoding.ASCII.GetBytes(domainName);
            var bRequest = new byte[7+hostName.Length];
            var bPort = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)port));
            bRequest[0] = (byte)TypeSocks.Socks5;
            bRequest[1] = (byte)OperationSocks.Connect;
            bRequest[2] = 0x00;
            bRequest[3] = (byte)TypeAddressSocks.HostName;
            bRequest[4] = (byte)hostName.Length;
            Array.Copy(hostName, 0, bRequest, 5, hostName.Length);
            bRequest[5 + hostName.Length] = bPort[0];
            bRequest[6 + hostName.Length] = bPort[1];
            return bRequest;
        }

        protected override byte[] RequestBytes(IPEndPoint ipEnd)
        {
            var bRequest = new byte[25];
            var bPort = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)ipEnd.Port));
            bRequest[0] = (byte)TypeSocks.Socks5;
            bRequest[1] = (byte)OperationSocks.Connect;
            bRequest[2] = 0x00;
            bRequest[3] = (byte) m_tAddressSocks; 
            var bIp = ipEnd.Address.GetAddressBytes();
            Array.Copy(bIp, 0, bRequest, 4, bIp.Length);
            bRequest[4 + bIp.Length] = bPort[0];
            bRequest[5 + bIp.Length] = bPort[1];
            return bRequest;
        }

        /// <summary>
        /// Конвертирует строку пользователя в массив байтов(в том числе length) для Socks5
        /// </summary>
        /// <param name="str">Имя пользователь</param>
        /// <returns>Массив байт: [0]-length, остальные байты имя пользователя</returns>
        protected override byte[] UsernameToBytes(string str)
        {
            var user = Encoding.ASCII.GetBytes(str);
            var bytes = new byte[user.Length + 1];
            bytes[0] = (byte)user.Length;
            Array.Copy(user, 0, bytes, 1, user.Length);
            return bytes;
        }

        public override bool Connect(string address)
        {
            if (m_socket != null)
            {
                var auth=_GetAuthSocks();
                if (auth == AuthSocks.NonAuth)
                {
                    m_link = address;
                    var host = Helpers.GetHostOfLink(address);
                    var bResponse = _GetResponseConnect(RequestBytes(host));
                    if(bResponse[0]==0x05)
                        return bResponse[1] == 0x00;
                }
            }
            return false;
        }
        public override bool Connect(IPEndPoint ipEnd)
        {
            _GetAuthSocks();
            var bResponse = _GetResponseConnect(RequestBytes(ipEnd));
            if (bResponse[0] == 0x05)
                return bResponse[1] == 0x00;
            return false;
        }
        /// <summary>
        /// Получаем ответ на указанный запрос, так же определяем тип адреса, m_ipBind (BND.PORT и BND.ADDR для Bind подключения)
        /// </summary>
        /// <param name="bRequest"></param>
        /// <returns></returns>
        protected byte[] _GetResponseConnect(byte[] bRequest)
        {
            var bResponce = new byte[100];
            try
            {
                m_socket.Send(bRequest);
                m_socket.Receive(bResponce, 0, bResponce.Length, SocketFlags.None);
            }
            catch
            {
                m_socket.Close();
            }
            if (bResponce[2] == 0x00 && bResponce[0] == 0x05)
            {
                if (bResponce[4] != 0x00)
                {
                    switch (bResponce[3])
                    {
                        case (byte)TypeAddressSocks.IPv4:
                            {
                                m_ipBind.Address = new IPAddress(bResponce.SubArray(4, 4));
                                m_ipBind.Port = BitConverter.ToInt32(bResponce, 8);
                                m_tAddressSocks = TypeAddressSocks.IPv4;
                                break;
                            }
                        case (byte)TypeAddressSocks.IPv6:
                            {
                                m_ipBind.Address = new IPAddress(bResponce.SubArray(4, 16));
                                m_ipBind.Port = BitConverter.ToInt32(bResponce, 20);
                                m_tAddressSocks = TypeAddressSocks.IPv6;
                                break;
                            }
                        case (byte)TypeAddressSocks.HostName:
                            {
                                var length = (int)bResponce[4];
                                m_hostBind = Encoding.ASCII.GetString(bResponce, 5, length);
                                m_ipBind.Port = BitConverter.ToInt32(bResponce, 5 + length);
                                m_tAddressSocks = TypeAddressSocks.HostName;
                                break;
                            }
                    }
                }
            }
            
            return bResponce;
        }
        
        public override string GetResponse(string link,TypeRequest tRequest=TypeRequest.GET,string refererLink="",string cookie="")
        {
            var bResponce = new byte[512];
            string request = string.Empty;
            string result = string.Empty;
            string type = string.Empty;
            string host = Helpers.GetHostOfLink(link);
            if (!string.IsNullOrEmpty(cookie))
                cookie = "Cookie:" + cookie;
            if (string.IsNullOrEmpty(refererLink))
                refererLink = "https://www.google.ru/";
            string reqOption="Connection: keep-alive\r\n" +
                      "User-Agent: Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; WOW64; Trident/6.0)" +
                      "Accept: text/html, application/xhtml+xml, */*\r\n" + // +
                      "Referer: " + refererLink + "\r\n" +
                      "Accept-Encoding: gzip,deflate,sdch\r\n" +
                      "Accept-Language: ru-RU\r\n"+cookie;

            switch (tRequest)
            {
                case TypeRequest.GET:
                {
                    type = "GET " ;
                    break;
                }
                case TypeRequest.HEAD:
                {
                    type = "HEAD ";
                    break;
                }
                case TypeRequest.POST:
                {
                    type = "POST ";
                    break;
                }
            }
            request = type + link + " HTTP/1.1\r\n" +
                      "Host: " + host + "\r\n\r\n" + reqOption;
                      

            var bytesSent = Encoding.ASCII.GetBytes(request);
            try
            {
                
                int offset = 0;
                while (true)
                {
                    m_socket.Send(bytesSent);
                    while (true)
                    {
                        SocketError er;
                        offset += m_socket.Receive(bResponce, 0, bResponce.Length, SocketFlags.None, out er);
                        result += Encoding.UTF8.GetString(bResponce);
                        if (bResponce[0] == 0x00 || result.Contains("302 Found") || result.Contains("</html>"))
                            break;
                    }
                    if (!result.Contains("302 Found"))
                        break;
                    break;
                    link = result.Substring(result.IndexOf("Location:")+9);
                    link = link.Remove(link.IndexOf("\r"));
                    request = type + link + " HTTP/1.1\r\n" +
                      "Host: " + host + "\r\n\r\n" + reqOption;
                    bytesSent = Encoding.ASCII.GetBytes(request);
                }
                if(result.Contains("<html"))
                    result = result.Substring(0,result.IndexOf("</html>")+7);
            }
            catch (SocketException)
            {
                m_socket.Close();
                //throw;
            }
            
            return result;
        }

        public override byte[] UPD(string domainName)
        {
            throw new NotImplementedException();
        }

        public override byte[] UPD(IPAddress ip, int port)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Конвертирует строку пароля в массив байтов(в том числе length) для Socks5
        /// </summary>
        /// <param name="str">Пароль</param>
        /// <returns>Массив байт: [0]-length, остальные байты пароль</returns>
        protected byte[] PasswordToBytes(string str)
        {
            var pass = Encoding.ASCII.GetBytes(str);
            byte[] bytes = new byte[pass.Length + 1];
            bytes[0] = (byte)pass.Length;
            Array.Copy(pass, 0, bytes, 1, pass.Length);
            return bytes;
        }
        /// <summary>
        /// Аутификация Socks5
        /// </summary>
        /// <returns>Тип безопасности Socks5</returns>
        /// <exception cref="SocketException">Ошибка во время подключения или отправки запроса на сервер Socks5</exception>
        private AuthSocks _GetAuthSocks()
        {
            var bRequest = new byte[100];
            var bReceived = new byte[300];
            bRequest[0] = (byte)TypeSocks.Socks5;
            bRequest[1] = 0x01; //NMETHODS
            bRequest[2] = (byte)AuthSocks.NonAuth;
            var auth = AuthSocks.Error;

            try
            {
                m_socket.Send(bRequest, 0, 3, SocketFlags.None);
                m_socket.Receive(bReceived, bReceived.Length, 0);
                if (Enum.IsDefined(typeof(AuthSocks), bReceived[1]))
                    AuthSocks.TryParse(bReceived[1].ToString(), out auth);
                var r = Encoding.ASCII.GetString(bReceived);
            }
            catch (SocketException)
            {
                m_socket.Close();
                //throw;
            }
            //Если для подключения к серверу нужны логин и пароль
            if (auth == AuthSocks.Auth || auth == AuthSocks.GSSAPI)
            {
                if (!string.IsNullOrEmpty(m_username))
                {
                    var bUser = UsernameToBytes(m_username);
                    var bPass = PasswordToBytes(m_password);
                    bRequest = new byte[300];
                    bReceived = new byte[300];
                    bRequest[0] = (byte) AuthSocks.Auth;
                    bRequest[1] = (byte) bUser.Length;
                    Array.Copy(bUser, 0, bRequest, 2, bUser.Length);
                    bRequest[2 + bUser.Length] = (byte) bPass.Length;
                    Array.Copy(bPass, 0, bRequest, 3 + bUser.Length, bPass.Length);
                    try
                    {
                        m_socket.Send(bRequest, 0, bUser.Length + 3 + bPass.Length, SocketFlags.None);
                        m_socket.Receive(bReceived, bReceived.Length, 0);
                        if (bReceived[1] != 0x00)
                        {
                            throw new AuthenticationException("Username or password incorect");
                        }
                    }
                    catch
                    {
                        m_socket.Close();
                        throw;
                    }
                }
                else
                {
                    auth = AuthSocks.Error;
                }
                    //throw new AuthenticationException("Empty username");
            }
            m_authSocks = auth;
            return auth;
        }

        public void Dispose()
        {
            m_socket.Close();
        }
    }
}
