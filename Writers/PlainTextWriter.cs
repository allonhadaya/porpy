using System;
using System.IO;

namespace Porpy.Writers
{
    public class PlainTextWriter : IWriter<String>
    {
        public static readonly IWriter<String> Instance = new PlainTextWriter();

        private PlainTextWriter()
        {
            // nothing
        }

        public void Write(StreamWriter writer, string entity)
        {
            writer.Write(entity);
        }
    }
}
