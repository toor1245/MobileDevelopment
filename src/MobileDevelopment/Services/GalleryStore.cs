using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MobileDevelopment.Helpers;
using MobileDevelopment.Interfaces;
using MobileDevelopment.Models;
using Xamarin.Forms;

namespace MobileDevelopment.Services
{
    public class GalleryStore : IGalleryStore
    {
        private static HttpClient _httpClient;

        public GalleryStore()
        {
            _httpClient = new HttpClient();
        }
        
        public async Task<GalleryResponse> GetImagesAsync()
        {
            var composer = new HttpImageRequestComposer();
            
            if (!composer.IsCorrectRequest)
            {
                await Shell.Current.DisplayAlert(
                    title: "Incorrect search", 
                    message: "restriction characters: ';', '/', '?', ':', '@', '&', '=', '$'",
                    cancel: "OK");
                return null;
            }
            
            var response = await _httpClient.GetAsync(composer.UrlRequest);
            
            if (response.StatusCode != HttpStatusCode.OK)
            {
                await Shell.Current.DisplayAlert("Alert", "failed to get images", "OK");
                return null;
            }
            
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GalleryResponse>(content);
        }

        public async Task<GalleryResponse> GetImagesAsync(string request, int count)
        {
            var composer = new HttpImageRequestComposer(request);
            var response = await _httpClient.GetAsync(composer.UrlRequest);
            
            if (response.StatusCode != HttpStatusCode.OK)
            {
                await Shell.Current.DisplayAlert("Alert", "failed to get images", "OK");
                return null;
            }
            
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GalleryResponse>(content);
        }
    }
}