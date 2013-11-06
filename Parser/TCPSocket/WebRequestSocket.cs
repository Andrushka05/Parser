using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TCPSocket
{
    public class WebRequestSocket:WebRequest
    {
        public override Stream GetRequestStream()
        {
            return base.GetRequestStream();
        }
    }
}
