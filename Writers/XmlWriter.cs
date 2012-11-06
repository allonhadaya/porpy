﻿using System.IO;
using System.Xml.Serialization;

namespace Porpy.Writers
{
    public class XmlWriter<TRequest> : IWriter<TRequest>
    {
        public virtual void Write(StreamWriter writer, TRequest entity)
        {
            Serializer().Serialize(writer, entity);
        }

        protected virtual XmlSerializer Serializer()
        {
            return new XmlSerializer(typeof(TRequest));
        }
    }
}