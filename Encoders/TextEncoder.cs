using System;
using System.IO;

namespace Porpy.Encoders
{
    public class TextEncoder : EntityEncoder<String>
    {
        public TextEncoder(String contentType = "text/plain")
            : base(contentType)
        {
            // nothing
        }

        internal override void Encode(StreamWriter writer, string entity)
        {
            writer.Write(entity);
        }
    }
}
