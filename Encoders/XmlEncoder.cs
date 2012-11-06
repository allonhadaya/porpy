using System;
using System.IO;
using System.Xml.Serialization;

namespace Porpy.Encoders
{
    public class XmlWriter<TRequest> : EntityEncoder<TRequest>
    {
        public XmlWriter(String contentType = "text/xml")
            : base(contentType)
        {
            // nothing
        }

        internal override void Encode(StreamWriter writer, TRequest entity)
        {
            Serializer().Serialize(writer, entity);
        }

        protected virtual XmlSerializer Serializer()
        {
            return new XmlSerializer(typeof(TRequest));
        }
    }
}
