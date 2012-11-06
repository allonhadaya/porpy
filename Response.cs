using System;
using System.Net;

namespace Porpy
{
    public class Response<TResponse>
    {
        public readonly HttpStatusCode StatusCode;
        public readonly WebExceptionStatus Exception;
        public readonly WebHeaderCollection Headers;
        public readonly TResponse Entity;

        public virtual Boolean Success { get { return Exception == WebExceptionStatus.Success; } }

        public Response(HttpStatusCode statusCode, WebExceptionStatus exception, WebHeaderCollection headers, TResponse entity)
        {
            StatusCode = statusCode;
            Exception = exception;
            Headers = headers;
            Entity = entity;
        }
    }
}
