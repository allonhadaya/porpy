using System.IO;
using Newtonsoft.Json;
using System;

namespace Porpy.Encoders
{
    public class JsonEncoder<TRequest> : EntityEncoder<TRequest>
    {
        public JsonEncoder(String contentType = "application/json")
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
