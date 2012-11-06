using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using Porpy.Readers;
using Porpy.Writers;

namespace Porpy.Generic
{
    public class Route<TRequest, TResponse>
    {
        protected readonly String BaseUri;
        protected readonly IWriter<TRequest> Writer;
        protected readonly IReader<TResponse> Reader;

        public Route(String baseUri, IWriter<TRequest> writer, IReader<TResponse> reader)
        {
            BaseUri = baseUri;
            Writer = writer;
            Reader = reader;
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
            if (headers != null) {
                request.Headers.Add(headers);
            }

            ModifyRequest(request);

            if (MethodHasRequestEntity(method)) {
                using (var requestWriter = new StreamWriter(request.GetRequestStream())) {
                    Writer.Write(requestWriter, entity);
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
                        responseEntity = Reader.Read(requestReader);
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
