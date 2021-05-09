using MobileDevelopment.Interfaces;
using Xamarin.Forms;

namespace MobileDevelopment.ViewModels
{
    public static class BaseStore
    {
        private static IBookStore _bookStore;
        public static IBookStore BookStore => _bookStore ??= DependencyService.Get<IBookStore>();
        
        private static IGalleryStore _galleryStore;
        public static IGalleryStore GalleryStore => _galleryStore ??= DependencyService.Get<IGalleryStore>();
    }
}