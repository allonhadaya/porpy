using System;
using System.IO;

namespace Porpy.Readers
{
    public class PlainTextReader : IReader<String>
    {
        public string Read(StreamReader reader)
        {
            return reader.ReadToEnd();
        }
    }
}
