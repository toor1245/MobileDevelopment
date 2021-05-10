using System.Collections.Generic;
using System.Threading.Tasks;
using MobileDevelopment.Models;

namespace MobileDevelopment.Interfaces
{
    public interface IGalleryStore
    {
        Task<ICollection<Hit>> GetImagesAsync();
    }
}