using System;
using System.Collections.Generic;
using System.Net;

namespace Porpy
{
    public abstract class PaginatedRoute<TRequest, TResponse> : Route<TRequest, TResponse>
    {
        protected IEnumerable<Tuple<HttpStatusCode, WebHeaderCollection, TResponse>> GetAllPages()
        {
            while (MoveNext()) {
                yield return GetResponse();
            }
        }

        protected abstract bool MoveNext();
    }
}
