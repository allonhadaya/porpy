using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using Porpy.Decoders;
using Porpy.Encoders;

namespace Porpy.Generic
{
    public class Route<TRequest, TResponse>
    {
        protected readonly String Path;
        protected readonly EntityEncoder<TRequest> Encoder;
        protected readonly EntityDecoder<TResponse> Decoder;
        protected Action<HttpWebRequest> GlobalRequestCallback;

        public Route(String path, EntityEncoder<TRequest> encoder, EntityDecoder<TResponse> decoder)
        {
            Path = path;
            Encoder = encoder;
            Decoder = decoder;
        }

        public Route<TRequest, TResponse> SetGlobalCallback(Action<HttpWebRequest> globalRequestCallback)
        {
            GlobalRequestCallback = globalRequestCallback;
            return this;
        }

        public Response<TResponse> Get(NameValueCollection querystring = null, NameValueCollection headers = null, Action<HttpWebRequest> requestCallback = null)
        {
            return Call("GET", querystring, headers, requestCallback);
        }

        public Response<TResponse> Post(TRequest entity, NameValueCollection querystring = null, NameValueCollection headers = null, Action<HttpWebRequest> requestCallback = null)
        {
            return Call("POST", querystring, headers, requestCallback, entity);
        }

        public Response<TResponse> Put(TRequest entity, NameValueCollection querystring = null, NameValueCollection headers = null, Action<HttpWebRequest> requestCallback = null)
        {
            return Call("PUT", querystring, headers, requestCallback, entity);
        }

        public Response<TResponse> Delete(NameValueCollection querystring = null, NameValueCollection headers = null, Action<HttpWebRequest> requestCallback = null)
        {
            return Call("DELETE", querystring, headers, requestCallback);
        }

        protected virtual Response<TResponse> Call(String method, NameValueCollection querystring, NameValueCollection headers, Action<HttpWebRequest> requestCallback, TRequest entity = default(TRequest))
        {
            querystring = querystring ?? new NameValueCollection(0);
            headers = headers ?? new NameValueCollection(0);

            Action<HttpWebRequest> requestCallbackChain = r => {
                if (GlobalRequestCallback != null) {
                    GlobalRequestCallback(r);
                }
                if (requestCallback != null) {
                    requestCallback(r);
                }
            };

            var request = CreateRequest(querystring);
            request.Method = method;
            request.ContentType = Encoder.ContentType;
            request.Headers.Add(headers);
            requestCallbackChain(request);
            WriteRequestEntity(request, entity);

            HttpWebResponse rawResponse;
            WebExceptionStatus webExceptionStatus;
            TResponse responseEntity = default(TResponse);

            try {
                rawResponse = request.GetResponse() as HttpWebResponse;
                webExceptionStatus = WebExceptionStatus.Success;
            } catch (WebException e) {
                rawResponse = e.Response as HttpWebResponse;
                webExceptionStatus = e.Status;
            }

            if (rawResponse != null && MethodHasResponseEntity(method)) {
                using (var requestReader = new StreamReader(rawResponse.GetResponseStream())) {
                    responseEntity = Decoder.Read(requestReader);
                }
            }

            return new Response<TResponse>(rawResponse, webExceptionStatus, responseEntity);
        }

        private HttpWebRequest CreateRequest(NameValueCollection querystring)
        {
            var uri = String.Format("{0}?{1}", Path,
                String.Join("&", querystring.AllKeys.Select(key => String.Join("{0}={1}", WebUtility.HtmlEncode(key), WebUtility.HtmlEncode(querystring[key])))))
                .TrimEnd('?');

            return WebRequest.Create(uri) as HttpWebRequest;
        }

        private void WriteRequestEntity(HttpWebRequest request, TRequest entity)
        {
            if (MethodHasRequestEntity(request.Method)) {
                using (var requestWriter = new StreamWriter(request.GetRequestStream())) {
                    Encoder.Encode(requestWriter, entity);
                }
            }
        }

        private Boolean MethodHasRequestEntity(String method)
        {
            return method == "POST" || method == "PUT";
        }

        protected virtual Boolean MethodHasResponseEntity(String method)
        {
            return method != "HEAD";
        }
    }
}
