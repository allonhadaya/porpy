using System.IO;

namespace Porpy.Deserializers
{
    public interface IDeserializer<TResponse>
    {
        TResponse Deserialize(StreamReader reader);
    }
}
