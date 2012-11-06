using System;
using System.Net;
using Porpy.Decoders;
using Porpy.Encoders;
using Porpy.Generic;

namespace Porpy
{
    public static class Route
    {
        public static Route<String, String> Create(String baseUri, Action<HttpWebRequest> before = null, Action<HttpWebResponse> after = null)
        {
            return new Route<String, String>(baseUri, new TextEncoder(), new TextDecoder(), before, after);
        }

        public static Route<String, TResponse> Create<TResponse>(String baseUri, EntityDecoder<TResponse> decoder, Action<HttpWebRequest> before = null, Action<HttpWebResponse> after = null)
        {
            return new Route<String, TResponse>(baseUri, new TextEncoder(), decoder, before, after);
        }

        public static Route<TRequest, String> Create<TRequest>(String baseUri, EntityEncoder<TRequest> encoder, Action<HttpWebRequest> before = null, Action<HttpWebResponse> after = null)
        {
            return new Route<TRequest, String>(baseUri, encoder, new TextDecoder(), before, after);
        }

        public static Route<TRequest, TResponse> Create<TRequest, TResponse>(String baseUri, EntityEncoder<TRequest> encoder, EntityDecoder<TResponse> decoder, Action<HttpWebRequest> before = null, Action<HttpWebResponse> after = null)
        {
            return new Route<TRequest, TResponse>(baseUri, encoder, decoder, before, after);
        }
    }
}
