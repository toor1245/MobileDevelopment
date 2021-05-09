using System.Threading.Tasks;
using MobileDevelopment.Models;

namespace MobileDevelopment.Interfaces
{
    public interface IGalleryStore
    {
        Task<GalleryResponse> GetImagesAsync();
        Task<GalleryResponse> GetImagesAsync(string request, int count);
    }
}