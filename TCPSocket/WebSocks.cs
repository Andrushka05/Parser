using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPSocket
{
    public class WebSocks:IWebProxy
    {
        private ICredentials m_credentials;

        public Uri GetProxy(Uri destination)
        {
            throw new NotImplementedException();
        }

        public bool IsBypassed(Uri host)
        {
            throw new NotImplementedException();
        }

        public ICredentials Credentials
        {
            get { return m_credentials; }
            set { m_credentials = value; }
        }

    }
}
