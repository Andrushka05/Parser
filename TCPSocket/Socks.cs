using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace TCPSocket
{
    public abstract class Socks
    {
        protected string m_username;
        protected IPEndPoint m_ip;
        protected IPEndPoint m_ipBind;
        protected string m_hostBind;
        protected long m_timePing;
        protected Socket m_socket;
        protected int m_timeSendSocket;
        protected int m_timeReceiveSocket;
        protected string m_link;

        protected Socks(IPEndPoint ip) : this(ip, string.Empty) { }

        protected Socks(IPEndPoint ip, string username)
        {
            m_ipBind = m_ip = ip;
            m_username = username;
            m_socket = ConnectSocket(ip);
            m_timeSendSocket = 7000;
            m_timeReceiveSocket = 15000;
        }

        protected abstract byte[] RequestBytes(string domainName, int port = 80);
        protected abstract byte[] RequestBytes(IPEndPoint ipEnd);
        protected abstract byte[] UsernameToBytes(string str);
        public abstract bool Connect(string domainName);
        public abstract bool Connect(IPEndPoint ipEnd);
        public abstract string GetResponse(string domainName, TypeRequest tRequest = TypeRequest.GET, string refererLink = "", string cookie = "");
        public abstract byte[] UPD(string domainName);
        public abstract byte[] UPD(IPAddress ip, int port);

        public string Username { get { return m_username; } set { m_username = value; } }
        public IPEndPoint Ip { get { return m_ip; } set { m_ip = value; } }
        /// <summary>
        /// IP выданный сервером для доступа
        /// </summary>
        public IPEndPoint IpBind { get { return m_ipBind; } }
        public bool Connected{get { return m_socket.Connected; }}
        /// <summary>
        /// Время Пинга к серверу
        /// </summary>
        public long PingMilisecond { get { return m_timePing; } }
        /// <summary>
        /// TimeOut отправки сообщения серверу
        /// </summary>
        /// <value>По умолчанию <see langword="7000"/> милисекунд</value>
        public int TimeOutSend { get { return m_timeSendSocket; } set { m_timeSendSocket = value; } }
        /// <summary>
        /// TimeOut принятия сообщения от сервера
        /// </summary>
        /// <value>По умолчанию <see langword="7000"/> милисекунд</value>
        public int TimeOutReceive { get { return m_timeReceiveSocket; } set { m_timeReceiveSocket = value; } }
        /// <summary>
        /// Определяет тип Socks по IP и порту
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns>Возвращает тип Socks - Socks4 или Socks5</returns>
        public static TypeSocks GetTypeSocks(IPEndPoint ipEnd)
        {
            var type = TypeSocks.Error;
            if (PingIp(ipEnd.Address))
            {
                Socket socket = new Socket(ipEnd.AddressFamily, SocketType.Stream, ProtocolType.Tcp) { SendTimeout = 7000, ReceiveTimeout = 7000 };
                try
                {
                    socket.Connect(ipEnd);
                }
                catch (SocketException ex)
                {
                    socket.Close();
                }
                
                if (socket.Connected)
                {
                    byte[] bSend = new byte[11];
                    byte[] bReceived = new byte[20];
                    bSend[0] = (byte) TypeSocks.Socks5;
                    bSend[1] = 0x01; // connection
                    bSend[2] = (byte) AuthSocks.NonAuth;
                    try
                    {
                        //Сначала проверяем на Socks5
                        socket.Send(bSend, 0, 3, SocketFlags.None);
                        socket.Receive(bReceived, bReceived.Length, 0);
                        TypeSocks.TryParse(bReceived[0].ToString(), out type);
                        if (TypeSocks.Socks5 != type)
                        {
                            socket.Close();
                            Thread.Sleep(10000);
                            //Проверяем на Socks4
                            bReceived = new byte[10];
                            socket = new Socket(ipEnd.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
                            {
                                SendTimeout = 7000,
                                ReceiveTimeout = 7000
                            };
                            try
                            {
                                socket.Connect(ipEnd);
                            }
                            catch (SocketException ex)
                            {
                                socket.Close();
                            }
                            if (socket.Connected)
                            {
                                var bPort = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short) 80));
                                bSend[0] = (byte) TypeSocks.Socks4;
                                bSend[2] = bPort[0];
                                bSend[3] = bPort[1];
                                var bIP = IPAddress.Parse("173.194.32.191").GetAddressBytes();
                                bSend[4] = bIP[0];
                                bSend[5] = bIP[1];
                                bSend[6] = bIP[2];
                                bSend[7] = bIP[3];
                                bSend[8] = 0x00;
                                socket.Send(bSend, 0, 9, SocketFlags.None);
                                socket.Receive(bReceived, bReceived.Length, 0);
                                if (bReceived[1] == 90)
                                    type = TypeSocks.Socks4;
                                
                            }
                        }

                    }
                    catch
                    {
                        
                    }
                    finally
                    {
                        socket.Close();
                    }
                }
            }
            
            return type;
        }

        #region Convert

        protected byte[] PortToBytes(int port)
        {
            return BitConverter.GetBytes(port);
        }

        protected IPAddress BytesToIPAdress(byte[] bytes, int startIndex = 0, int length = 0)
        {
            string ip = string.Empty;
            if (length == 0)
                ip = BitConverter.ToString(bytes, startIndex);
            else
                ip = BitConverter.ToString(bytes, startIndex, length);
            return IPAddress.Parse(ip);
        }

        protected int BytesToInt(byte[] bytes, int startIndex = 0)
        {
            return BitConverter.ToInt32(bytes, startIndex);
        }
        #endregion

        protected Socket ConnectSocket(IPEndPoint ipEnd)
        {
            Socket tempSocket = null;
            tempSocket = new Socket(ipEnd.AddressFamily, SocketType.Stream, ProtocolType.Tcp) { SendTimeout = m_timeSendSocket, ReceiveTimeout = m_timeReceiveSocket };
            try
            {
                tempSocket.Connect(ipEnd);
            }
            catch (SocketException ex)
            {
                tempSocket.Close();
                return null;
            }
            return tempSocket;
        }

        public long PingIp()
        {
            var ping = new Ping();
            var reply = ping.Send(m_ip.Address, 5000);
            m_timePing = reply.RoundtripTime;
            return m_timePing;
        }

        public static bool PingIp(IPAddress ip)
        {
            var ping = new Ping();
            try
            {
                ping.Send(ip, 5000);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", m_ip.Address, m_ip.Port);
        }

    }
}
