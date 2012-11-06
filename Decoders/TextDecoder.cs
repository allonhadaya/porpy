using System;
using System.IO;

namespace Porpy.Decoders
{
    public class TextDecoder : EntityDecoder<String>
    {
        public string Read(StreamReader reader)
        {
            return reader.ReadToEnd();
        }
    }
}
