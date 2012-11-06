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

        internal Route(String baseUri, EntityEncoder<TRequest> encoder, EntityDecoder<TResponse> decoder)
        {
            BaseUri = baseUri;
            Encoder = encoder;
            Decoder = decoder;
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
            var request = WebRequest.Create(BuildUri(querystring)) as HttpWebRequest;
            
            request.Method = method;
            
            request.ContentType = Encoder.ContentType;
            
            if (headers != null) {
                request.Headers.Add(headers);
            }

            ModifyRequest(request);

            if (MethodHasRequestEntity(method)) {
                using (var requestWriter = new StreamWriter(request.GetRequestStream())) {
                    Encoder.Encode(requestWriter, entity);
                }
            }

            HttpWebResponse response = null;

            HttpStatusCode responseStatus = 0;
            WebExceptionStatus exceptionStatus = 0;
            WebHeaderCollection responseHeaders = null;
            TResponse responseEntity = default(TResponse);

            try {
                response = request.GetResponse() as HttpWebResponse;
                exceptionStatus = WebExceptionStatus.Success;
            } catch (WebException e) {
                response = e.Response as HttpWebResponse;
                exceptionStatus = e.Status;
            }

            if (response != null) {
                responseStatus = response.StatusCode;
                responseHeaders = response.Headers;
                if (MethodHasResponseEntity(method)) {
                    using (var requestReader = new StreamReader(response.GetResponseStream())) {
                        responseEntity = Decoder.Read(requestReader);
                    }
                }
            }

            return new Response<TResponse>(responseStatus, exceptionStatus, responseHeaders, responseEntity);
        }

        protected virtual String BuildUri(NameValueCollection query)
        {
            if (query == null || query.Count == 0) {
                return BaseUri;
            }
            return BaseUri.TrimEnd('?') + "?" + String.Join("&", query.AllKeys.Select(key => WebUtility.HtmlEncode(key) + "=" + WebUtility.HtmlEncode(query[key])));
        }

        protected virtual void ModifyRequest(HttpWebRequest request)
        {
            // nothing
        }

        protected virtual Boolean MethodHasRequestEntity(String method)
        {
            return method == "POST" || method == "PUT";
        }

        protected virtual Boolean MethodHasResponseEntity(String method)
        {
            return method == "GET";
        }
    }
}
