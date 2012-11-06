using System.IO;
using Newtonsoft.Json;

namespace Porpy.Readers
{
    public class JsonReader<TResponse> : IReader<TResponse>
    {
        public static readonly IReader<TResponse> Instance = new JsonReader<TResponse>();

        private JsonReader()
        {
            // nothing
        }

        public virtual TResponse Read(StreamReader reader)
        {
            return JsonConvert.DeserializeObject<TResponse>(reader.ReadToEnd());
        }
    }
}
