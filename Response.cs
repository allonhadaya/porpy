using System;
using System.Net;

namespace Porpy
{
    public class Response<TResponse>
    {
        public readonly HttpWebResponse RawResponse;
        public readonly WebExceptionStatus WebExceptionStatus;
        public readonly TResponse Entity;
        public readonly Boolean Success;

        public Response(HttpWebResponse rawResponse, WebExceptionStatus webExceptionStatus, TResponse entity)
        {
            RawResponse = rawResponse;
            WebExceptionStatus = webExceptionStatus;
            Entity = entity;
            Success =
                webExceptionStatus == WebExceptionStatus.Success &&
                rawResponse != null &&
                (int)rawResponse.StatusCode < 400;
        }
    }
}
