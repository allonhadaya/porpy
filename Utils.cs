using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Net;

namespace Porpy
{
    internal static class Utils
    {
        public static String UrlEncode(NameValueCollection querystring)
        {
            return String.Join("&", querystring.AllKeys.Select(key => WebUtility.HtmlEncode(key) + "=" + WebUtility.HtmlEncode(querystring[key])));
        }
    }
}
