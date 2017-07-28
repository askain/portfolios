using System;

namespace HDIMSAPP.Utils
{
    public class NetworkUtil
    {
        public static string GetFullUrl(Uri _uri)
        {
            string _retUri = "";
            if (_uri != null && _uri.Scheme != null && _uri.Host != null)
            {
                string _scheme = _uri.Scheme;
                string _host = _uri.Host;
                int _port = _uri.Port;
                _retUri = _scheme + "://" + _host;
                if (_port != 80) _retUri += ":" + _port;
            }
            return _retUri;
        }
    }
}
