using System;
using System.IO;

namespace Porpy.Decoders
{
    public class TextDecoder : EntityDecoder<String>
    {
        internal override string Read(StreamReader reader)
        {
            return reader.ReadToEnd();
        }
    }
}
