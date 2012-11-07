using System;
using System.IO;
using System.Xml.Serialization;

namespace Porpy.Encoders
{
    public class XmlEncoder<TRequest> : EntityEncoder<TRequest>
    {
        public XmlEncoder(String contentType = "application/xml")
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
