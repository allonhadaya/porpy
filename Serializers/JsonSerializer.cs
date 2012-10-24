using System.IO;
using Newtonsoft.Json;

namespace Porpy.Serializers
{
    public class JsonSerializer<TRequest> : ISerializer<TRequest>
    {
        public virtual void Serialize(StreamWriter writer, TRequest entity)
        {
            writer.Write(JsonConvert.SerializeObject(entity));
        }
    }
}
