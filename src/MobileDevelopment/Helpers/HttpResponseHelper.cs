using System.Net.Http;
using Xamarin.Forms;

namespace MobileDevelopment.Helpers
{
    public static class HttpResponseHelper
    {
        public static async void DisplayNotSuccessfulStatusCodeAlert(HttpResponseMessage response)
        {
            await Shell.Current.DisplayAlert(
                "Not Successful response", 
                response.ReasonPhrase,
                "OK");
        }
    }
}