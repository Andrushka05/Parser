using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TCPSocket
{
    public class Socks4:Socks
    {
        public Socks4(IPEndPoint ip) : base(ip)
        {
        }

        public Socks4(IPEndPoint ip, string username) : base(ip, username)
        {
        }

        protected override byte[] RequestBytes(string domainName, int port = 80)
        {
            throw new NotImplementedException();
        }

        protected override byte[] RequestBytes(IPEndPoint ipEnd)
        {
            throw new NotImplementedException();
        }

        protected override byte[] UsernameToBytes(string str)
        {
            throw new NotImplementedException();
        }

        public override bool Connect(string domainName)
        {
            throw new NotImplementedException();
        }

        public override bool Connect(IPEndPoint ipEnd)
        {
            throw new NotImplementedException();
        }

        public override string GetResponse(string domainName, TypeRequest tRequest = TypeRequest.GET, string refererLink = "", string cookie = "")
        {
            throw new NotImplementedException();
        }

        public override byte[] UPD(string domainName)
        {
            throw new NotImplementedException();
        }

        public override byte[] UPD(IPAddress ip, int port)
        {
            throw new NotImplementedException();
        }
    }
}
