using System.IO;
using Newtonsoft.Json;

namespace Porpy.Deserializers
{
    public class JsonResponse<TResponse> : IDeserializer<TResponse>
    {
        public virtual TResponse Deserialize(TextReader reader)
        {
            return JsonConvert.DeserializeObject<TResponse>(reader.ReadToEnd());
        }
    }
}
