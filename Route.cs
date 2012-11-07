using System;
using Porpy.Decoders;
using Porpy.Encoders;
using Porpy.Generic;

namespace Porpy
{
    public static class Route
    {
        public static Route<String, String> Create(String path)
        {
            return new Route<String, String>(path, new TextEncoder(), new TextDecoder());
        }

        public static Route<String, TResponse> Create<TResponse>(String path, EntityDecoder<TResponse> decoder)
        {
            return new Route<String, TResponse>(path, new TextEncoder(), decoder);
        }

        public static Route<TRequest, String> Create<TRequest>(String path, EntityEncoder<TRequest> encoder)
        {
            return new Route<TRequest, String>(path, encoder, new TextDecoder());
        }

        public static Route<TRequest, TResponse> Create<TRequest, TResponse>(String path, EntityEncoder<TRequest> encoder, EntityDecoder<TResponse> decoder)
        {
            return new Route<TRequest, TResponse>(path, encoder, decoder);
        }
    }
}
