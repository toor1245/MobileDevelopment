using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MobileDevelopment.Helpers;
using MobileDevelopment.Interfaces;
using MobileDevelopment.Models;
using MobileDevelopment.Repositories;

namespace MobileDevelopment.Services
{
    public class GalleryStore : IGalleryStore
    {
        private static HttpClient _httpClient;

        public GalleryStore()
        {
            _httpClient = new HttpClient();
        }
        
        public async Task<ICollection<Hit>> GetImagesAsync()
        {
            var composer = new HttpImageRequestComposer();
            
            if (!composer.IsCorrectRequest)
            {
                await HttpRequestComposer.DisplayAlertAsync();
                return null;
            }
            
            var response = await _httpClient.GetAsync(composer.UrlRequest);

            if (!response.IsSuccessStatusCode)
            {
                var list = UnitOfWork.Hits.GetItems();
                if (list is not null)
                {
                    return list;
                }
                HttpResponseHelper.DisplayNotSuccessfulStatusCodeAlert(response);
                return null;
            }
            
            var content = await response.Content.ReadAsStringAsync();
            var hits = JsonSerializer.Deserialize<GalleryResponse>(content)?.Hits;
            UnitOfWork.Hits.SaveItems(hits);
            return hits;
        }
    }
}