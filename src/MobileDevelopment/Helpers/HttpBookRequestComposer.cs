using MobileDevelopment.Extensions;

namespace MobileDevelopment.Helpers
{
    public class HttpBookRequestComposer : HttpRequestComposer
    {
        public HttpBookRequestComposer(string request) : base(request)
        {
        }

        protected override string BuildUrlRequest()
        {
            return string.Concat(Constants.URL_SEARCH_BOOKS, _request);
        }
    }
}