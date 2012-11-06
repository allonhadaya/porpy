using System;
using Porpy.Readers;
using Porpy.Writers;

namespace Porpy
{
    public class Route : Porpy.Generic.Route<String, String>
    {
        public Route(String baseUri)
            : base(baseUri, PlainTextWriter.Instance, PlainTextReader.Instance)
        {
            // nothing
        }
    }
}
