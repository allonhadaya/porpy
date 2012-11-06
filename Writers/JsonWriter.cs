using System.IO;
using Newtonsoft.Json;

namespace Porpy.Writers
{
    public class JsonWriter<TRequest> : IWriter<TRequest>
    {
        public virtual void Write(StreamWriter writer, TRequest entity)
        {
            writer.Write(JsonConvert.SerializeObject(entity));
        }
    }
}
