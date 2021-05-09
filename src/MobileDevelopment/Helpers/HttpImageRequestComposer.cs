using MobileDevelopment.Extensions;

namespace MobileDevelopment.Helpers
{
    public class HttpImageRequestComposer : HttpRequestComposer
    {
        private readonly string _url;
        private readonly string _apiKey;
        private readonly int _count;

        public HttpImageRequestComposer(string request) : 
            this(Constants.URL_IMAGES, Constants.API_KEY, request, Constants.COUNT)
        {
            
        }

        public HttpImageRequestComposer() : 
            this(Constants.URL_IMAGES, Constants.API_KEY, Constants.REQUEST, Constants.COUNT)
        {
            
        }

        private HttpImageRequestComposer(string url, string apiKey, string request, int count) : base(request)
        {
            _url = url;
            _apiKey = apiKey;
            _count = count;
        }

        protected override string BuildUrlRequest()
        {
            return $"{_url}?key={_apiKey}&q={_request}&image_type=photo&per_page={_count}";
        }
    }
}