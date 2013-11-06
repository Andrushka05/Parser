using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPSocket
{
    public enum IpSecurity
    {
        Elite,
        Anonymous,
        Transparent
    }

    public enum IpType
    {
        Http,
        Https,
        Socks4,
        Socks5
    }

    public enum TypeSocks : byte
    {
        Socks4 = 0x04,
        Socks5 = 0x05,
        Error=0x00
    }

    public enum OperationSocks : byte
    {
        Connect = 0x01,
        Bind = 0x02,
        UPD = 0x03
    }

    public enum TypeAddressSocks : byte
    {
        IPv4 = 0x01,
        HostName = 0x03,
        IPv6 = 0x04
    }

    public enum TypeRequest
    {
        HEAD,
        GET,
        POST
    }
    public enum StatusResponseSocks4 : byte
    {
        Success = 0x5a,
        ErrorRequest = 0x5b,
        ErrorServer = 0x5c,
        NotConfirmUser = 0x5d
    }
    public enum StatusResponseSocks5 : byte
    {
        Success = 0x00,
        ErrorServer = 0x01,
        ErrorRuleConnect = 0x02,
        ErrorNetwork = 0x03,
        ErrorHost = 0x04,
        ErrorConnect = 0x05,
        ErrorTTL = 0x06,
        ErrorCommand = 0x07,
        ErrorTypeAddress = 0x08
    }
    public enum AuthSocks : byte
    {
        NonAuth = 0x00,
        GSSAPI = 0x01,
        Auth = 0x02,
        IANA = 0x03,
        Private = 0x80,
        Error = 0xFF
    }
}
