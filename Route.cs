using System;
using System.IO;
using Porpy.Encoders;
using Porpy.Generic;
using Porpy.Readers;

namespace Porpy
{
    public static class Route
    {
        public static Route<String, String> Create(String baseUri)
        {
            return new Route<string,string>(baseUri, new TextEncoder(), new TextDecoder());
        }

        public static Route<String, TResponse> Create<TResponse>(String baseUri, EntityDecoder<TResponse> decoder)
        {
            return new Route<String, TResponse>(baseUri, new TextEncoder(), decoder);
        }

        public static Route<TRequest, String> Create<TRequest>(String baseUri, EntityEncoder<TRequest> encoder)
        {
            return new Route<TRequest, String>(baseUri, encoder, new TextDecoder());
        }

        public static Route<TRequest, TResponse> Create<TRequest, TResponse>(String baseUri, EntityEncoder<TRequest> encoder, EntityDecoder<TResponse> decoder)
        {
            return new Route<TRequest, TResponse>(baseUri, encoder, decoder);
        }

        public static Route<TRequest, TResponse> Create<TRequest, TResponse>(String baseUri, Action<StreamWriter, TRequest> encoder, String contentType, Func<StreamReader, TResponse> decoder)
        {
            return new Route<TRequest, TResponse>(baseUri, new EntityEncoder<TRequest>(contentType, encoder), new EntityDecoder<TResponse>(decoder));
        }
    }
}
