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
        protected readonly String BaseUri;
        protected readonly EntityEncoder<TRequest> Encoder;
        protected readonly EntityDecoder<TResponse> Decoder;
        protected readonly Action<HttpWebRequest> Before;
        protected readonly Action<HttpWebResponse> After;

        public Route(String baseUri, EntityEncoder<TRequest> encoder, EntityDecoder<TResponse> decoder, Action<HttpWebRequest> before, Action<HttpWebResponse> after)
        {
            BaseUri = baseUri;
            Encoder = encoder;
            Decoder = decoder;
            Before = before ?? (request => { });
            After = after ?? (response => { });
        }

        public Response<TResponse> Get(NameValueCollection querystring = null, NameValueCollection headers = null)
        {
            return Call("GET", querystring, headers);
        }

        public Response<TResponse> Post(TRequest entity, NameValueCollection querystring = null, NameValueCollection headers = null)
        {
            return Call("POST", querystring, headers, entity);
        }

        public Response<TResponse> Put(TRequest entity, NameValueCollection querystring = null, NameValueCollection headers = null)
        {
            return Call("PUT", querystring, headers, entity);
        }

        public Response<TResponse> Delete(NameValueCollection querystring = null, NameValueCollection headers = null)
        {
            return Call("DELETE", querystring, headers);
        }

        protected virtual Response<TResponse> Call(String method, NameValueCollection querystring, NameValueCollection headers, TRequest entity = default(TRequest))
        {
            querystring = querystring ?? new NameValueCollection(0);
            headers = headers ?? new NameValueCollection(0);

            var request = CreateRequest(querystring);
            request.Method = method;
            request.ContentType = Encoder.ContentType;
            request.Headers.Add(headers);
            Before(request);
            WriteRequestEntity(request, entity);

            var response = GetResponseResult(request);

            After(response.Item2);

            if (response.Item2 != null) {
                if (MethodHasResponseEntity(method)) {
                    using (var requestReader = new StreamReader(response.Item2.GetResponseStream())) {
                        return new Response<TResponse>(response.Item1, response.Item2.StatusCode, response.Item2.Headers, Decoder.Read(requestReader));
                    }
                }
                return new Response<TResponse>(response.Item1, response.Item2.StatusCode, response.Item2.Headers, default(TResponse));
            }

            return new Response<TResponse>(response.Item1, 0, null, default(TResponse));
        }

        private HttpWebRequest CreateRequest(NameValueCollection querystring)
        {
            var uri = String.Format("{0}?{1}", BaseUri,
                String.Join("&", querystring.AllKeys.Select(key => String.Join("{0}={1}", WebUtility.HtmlEncode(key), WebUtility.HtmlEncode(querystring[key])))));

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

        private Tuple<WebExceptionStatus, HttpWebResponse> GetResponseResult(HttpWebRequest request)
        {
            try {
                return Tuple.Create(WebExceptionStatus.Success, request.GetResponse() as HttpWebResponse);
            } catch (WebException e) {
                return Tuple.Create(e.Status, e.Response as HttpWebResponse);
            }
        }

        protected virtual Boolean MethodHasResponseEntity(String method)
        {
            return method != "HEAD";
        }
    }
}
