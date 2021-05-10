using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;


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

        public static async Task DisplayAlertAsync()
        {
            await Shell.Current.DisplayAlert(
                "Incorrect search", 
                "restriction characters: ';', '/', '?', ':', '@', '&', '=', '$'",
                "OK");
        }
    }
}