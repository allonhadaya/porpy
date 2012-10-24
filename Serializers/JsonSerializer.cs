using System.Text;
using Newtonsoft.Json;

namespace Porpy.Serializers
{
    public class JsonSerializer<TRequest> : ISerializer<TRequest>
    {
        public virtual byte[] Serialize(TRequest entity)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(entity));
        }
    }
}
