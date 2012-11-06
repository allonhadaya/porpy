using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using Porpy.Readers;
using Porpy.Writers;

namespace Porpy
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

        public Tuple<HttpStatusCode, WebHeaderCollection, TResponse> Get(NameValueCollection querystring = null, NameValueCollection headers = null)
        {
            return Request("GET", querystring, headers);
        }

        public Tuple<HttpStatusCode, WebHeaderCollection, TResponse> Post(TRequest entity, NameValueCollection querystring = null, NameValueCollection headers = null)
        {
            return Request("POST", querystring, headers, entity);
        }

        public Tuple<HttpStatusCode, WebHeaderCollection, TResponse> Put(TRequest entity, NameValueCollection querystring = null, NameValueCollection headers = null)
        {
            return Request("PUT", querystring, headers, entity);
        }

        public Tuple<HttpStatusCode, WebHeaderCollection, TResponse> Delete(NameValueCollection querystring = null, NameValueCollection headers = null)
        {
            return Request("DELETE", querystring, headers);
        }

        protected virtual Tuple<HttpStatusCode, WebHeaderCollection, TResponse> Request(String method, NameValueCollection querystring, NameValueCollection headers, TRequest entity = default(TRequest))
        {
            var webRequest = WebRequest.Create(BuildUri(querystring)) as HttpWebRequest;
            webRequest.Method = method;
            if (headers != null) {
                webRequest.Headers.Add(headers);
            }

            if (MethodHasRequestEntity(method)) {
                using (var requestWriter = new StreamWriter(webRequest.GetRequestStream())) {
                    Writer.Write(requestWriter, entity);
                }
            }

            var webResponse = webRequest.GetResponse() as HttpWebResponse;

            var responseEntity = default(TResponse);

            if (MethodHasResponseEntity(method)) {
                using (var requestReader = new StreamReader(webResponse.GetResponseStream())) {
                    responseEntity = Reader.Read(requestReader);
                }
            }

            return Tuple.Create(webResponse.StatusCode, webResponse.Headers, responseEntity);
        }

        protected virtual String BuildUri(NameValueCollection query)
        {
            if (query == null || query.Count == 0) {
                return BaseUri;
            }
            return BaseUri.TrimEnd('?') + "?" + String.Join("&", query.AllKeys.Select(key => WebUtility.HtmlEncode(key) + "=" + WebUtility.HtmlEncode(query[key])));
        }

        protected virtual Boolean MethodHasRequestEntity(String method)
        {
            throw new NotImplementedException();
        }

        protected virtual Boolean MethodHasResponseEntity(String method)
        {
            throw new NotImplementedException();
        }
    }
}
