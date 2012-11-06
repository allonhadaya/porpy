using System.IO;
using Newtonsoft.Json;

namespace Porpy.Writers
{
    public class JsonWriter<TRequest> : IWriter<TRequest>
    {
        public static readonly IWriter<TRequest> Instance = new JsonWriter<TRequest>();

        private JsonWriter()
        {
            // nothing
        }

        public virtual void Write(StreamWriter writer, TRequest entity)
        {
            writer.Write(JsonConvert.SerializeObject(entity));
        }
    }
}
