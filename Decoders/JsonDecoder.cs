using System.IO;
using Newtonsoft.Json;

namespace Porpy.Readers
{
    public class JsonDecoder<TResponse> : EntityDecoder<TResponse>
    {
        public virtual TResponse Read(StreamReader reader)
        {
            return JsonConvert.DeserializeObject<TResponse>(reader.ReadToEnd());
        }
    }
}
