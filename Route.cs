using System;
using Porpy.Generic;
using Porpy.Readers;
using Porpy.Writers;

namespace Porpy
{
    public static class Route
    {
        public static Route<String, String> Create(String baseUri)
        {
            return new Route<string,string>(baseUri, PlainTextWriter.Instance, PlainTextReader.Instance);
        }

        public static Route<String, TResponse> Create<TResponse>(String baseUri, IReader<TResponse> reader)
        {
            return new Route<String, TResponse>(baseUri, PlainTextWriter.Instance, reader);
        }

        public static Route<TRequest, String> Create<TRequest>(String baseUri, IWriter<TRequest> writer)
        {
            return new Route<TRequest, String>(baseUri, writer, PlainTextReader.Instance);
        }

        public static Route<TRequest, TResponse> Create<TRequest, TResponse>(String baseUri, IWriter<TRequest> writer, IReader<TResponse> reader)
        {
            return new Route<TRequest, TResponse>(baseUri, writer, reader);
        }
    }
}
