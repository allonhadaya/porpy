using System;
using System.IO;

namespace Porpy.Readers
{
    public class PlainTextReader : IReader<String>
    {
        public static readonly IReader<String> Instance = new PlainTextReader();

        private PlainTextReader()
        {
            // nothing
        }

        public string Read(StreamReader reader)
        {
            return reader.ReadToEnd();
        }
    }
}
