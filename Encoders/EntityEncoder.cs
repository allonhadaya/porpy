using System;
using System.IO;

namespace Porpy.Encoders
{
    public class EntityEncoder<TRequest>
    {
        private readonly Action<StreamWriter, TRequest> Encoder;
        internal readonly String ContentType;

        internal EntityEncoder(String contentType, Action<StreamWriter, TRequest> encoder = null)
        {
            Encoder = encoder;
            ContentType = contentType;
        }

        internal virtual void Encode(StreamWriter writer, TRequest entity)
        {
            if (Encoder == null) {
                throw new ArgumentNullException("Encoder");
            }

            Encoder(writer, entity);
        }
    }
}
