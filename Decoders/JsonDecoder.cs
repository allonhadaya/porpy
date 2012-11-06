using System.IO;
using Newtonsoft.Json;

namespace Porpy.Decoders
{
    public class JsonDecoder<TResponse> : EntityDecoder<TResponse>
    {
        internal override TResponse Read(StreamReader reader)
        {
            return JsonConvert.DeserializeObject<TResponse>(reader.ReadToEnd());
        }
    }
}
