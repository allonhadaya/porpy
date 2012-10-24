using System.IO;
using System.Xml.Serialization;

namespace Porpy.Serializers
{
    public class XmlSerializer<TRequest> : ISerializer<TRequest>
    {
        public virtual void Serialize(StreamWriter writer, TRequest entity)
        {
            Serializer().Serialize(writer, entity);
        }

        protected virtual XmlSerializer Serializer()
        {
            return new XmlSerializer(typeof(TRequest));
        }
    }
}
