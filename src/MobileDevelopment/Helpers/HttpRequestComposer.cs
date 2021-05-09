using System.Linq;

namespace MobileDevelopment.Helpers
{
    public abstract class HttpRequestComposer
    {
        private static readonly char[] _restrictions;
        protected readonly string _request;

        public bool IsCorrectRequest => _restrictions.All(c => !_request.Contains(c));
        public string UrlRequest => IsCorrectRequest ? BuildUrlRequest() : null;
        
        static HttpRequestComposer()
        {
            _restrictions = new[] {';', '/', '?', ':', '@', '&', '=', '$'};
        }
        
        protected HttpRequestComposer(string request)
        {
            _request = request;
        }

        protected abstract string BuildUrlRequest();
    }
}