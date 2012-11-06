using System;
using System.IO;

namespace Porpy.Writers
{
    public class PlainTextWriter : IWriter<String>
    {
        public void Write(StreamWriter writer, string entity)
        {
            writer.Write(entity);
        }
    }
}
