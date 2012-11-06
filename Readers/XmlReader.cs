using System.IO;
using System.Xml.Serialization;

namespace Porpy.Readers
{
    public class XmlReader<TResponse> : IReader<TResponse>
    {
        public virtual TResponse Read(StreamReader reader)
        {
            return (TResponse)Serializer().Deserialize(reader);
        }

        protected virtual XmlSerializer Serializer()
        {
            return new XmlSerializer(typeof(TResponse));
        }
    }
}
