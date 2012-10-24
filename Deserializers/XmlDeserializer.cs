using System.IO;
using System.Xml.Serialization;

namespace Porpy.Deserializers
{
    public class XmlDeserializer<TResponse> : IDeserializer<TResponse>
    {
        public virtual TResponse Deserialize(byte[] reader)
        {
            return (TResponse)Serializer().Deserialize(new MemoryStream(reader));
        }

        protected virtual XmlSerializer Serializer()
        {
            return new XmlSerializer(typeof(TResponse));
        }
    }
}
