using System.IO;
using System.Xml.Serialization;

namespace Porpy.Deserializers
{
    public class XmlDeserializer<TResponse> : IDeserializer<TResponse>
    {
        public virtual TResponse Deserialize(StreamReader reader)
        {
            return (TResponse)Serializer().Deserialize(reader);
        }

        protected virtual XmlSerializer Serializer()
        {
            return new XmlSerializer(typeof(TResponse));
        }
    }
}
