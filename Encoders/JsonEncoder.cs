using System.IO;
using Newtonsoft.Json;
using System;

namespace Porpy.Encoders
{
    public class JsonWriter<TRequest> : EntityEncoder<TRequest>
    {
        public JsonWriter(String contentType = "text/json")
            : base(contentType)
        {
            // nothing
        }

        internal override void Encode(StreamWriter writer, TRequest entity)
        {
            writer.Write(JsonConvert.SerializeObject(entity));
        }
    }
}
