using System.IO;
using Newtonsoft.Json;

namespace Porpy.Readers
{
    public class JsonReader<TResponse> : IReader<TResponse>
    {
        public virtual TResponse Read(StreamReader reader)
        {
            return JsonConvert.DeserializeObject<TResponse>(reader.ReadToEnd());
        }
    }
}
