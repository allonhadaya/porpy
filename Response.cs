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

        public readonly Boolean Success;

        public Response(WebExceptionStatus exception, HttpStatusCode statusCode, WebHeaderCollection headers, TResponse entity)
        {
            Exception = exception;
            StatusCode = statusCode;
            Headers = headers;
            Entity = entity;

            Success = exception == WebExceptionStatus.Success && (int)statusCode < 400;
        }
    }
}
