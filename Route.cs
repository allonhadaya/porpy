using System;
using System.Net;
using Porpy.Deserializers;
using Porpy.Serializers;

namespace Porpy
{
    public abstract class Route<TRequest, TResponse>
    {
        protected readonly Boolean HasEntity;
        protected readonly TRequest RequestEntity;
        protected readonly Func<WebHeaderCollection, WebHeaderCollection> HeaderAdapter;
        protected readonly Func<String, String> UriAdapter;

        protected Route(TRequest requestEntity, Func<WebHeaderCollection, WebHeaderCollection> headerAdapter = null, Func<String, String> uriAdapter = null)
        {
            HasEntity = true;
            RequestEntity = requestEntity;
            HeaderAdapter = headerAdapter ?? (h => h);
            UriAdapter = uriAdapter ?? (u => u);
        }

        protected Route(Func<WebHeaderCollection, WebHeaderCollection> headerAdapter = null, Func<String, String> uriAdapter = null)
        {
            HasEntity = false;
            HeaderAdapter = headerAdapter ?? (h => h);
            UriAdapter = uriAdapter ?? (u => u);
        }

        public virtual Tuple<HttpStatusCode, WebHeaderCollection, TResponse> GetResponse()
        {
            throw new NotImplementedException();
        }

        protected abstract String GetUri();
        protected abstract String GetMethod();
        protected abstract ISerializer<TRequest> GetSerializer();
        protected abstract IDeserializer<TResponse> GetDeserializer();
    }
}
