using System.IO;
using System.Xml.Serialization;

namespace Porpy.Serializers
{
    public class XmlSerializer<TRequest> : ISerializer<TRequest>
    {
        public virtual byte[] Serialize(TRequest entity)
        {
            using (var stream = new MemoryStream()) {
                Serializer().Serialize(stream, entity);
                return stream.GetBuffer();
            }
        }

        protected virtual XmlSerializer Serializer()
        {
            return new XmlSerializer(typeof(TRequest));
        }
    }
}
