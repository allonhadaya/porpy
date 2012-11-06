using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;

namespace Porpy.Encoders
{
    class FormEncoder : EntityEncoder<NameValueCollection>
    {
        public FormEncoder(String contentType = "formWhatever")
            : base(contentType)
        {
            // nothing
        }

        internal override void Encode(StreamWriter writer, NameValueCollection entity)
        {
            writer.Write(String.Join("&", entity.AllKeys.Select(key => WebUtility.HtmlEncode(key) + "=" + WebUtility.HtmlEncode(entity[key]))));
        }
    }
}
