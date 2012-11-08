using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;

namespace Porpy.Encoders
{
    public class FormEncoder : EntityEncoder<NameValueCollection>
    {
        public FormEncoder(String contentType = "multipart/form-data")
            : base(contentType)
        {
            // nothing
        }

        internal override void Encode(StreamWriter writer, NameValueCollection entity)
        {
            var body = Utils.UrlEncode(entity);
            writer.Write(body);
        }
    }
}
