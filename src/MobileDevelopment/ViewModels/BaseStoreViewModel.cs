using MobileDevelopment.Interfaces;
using Xamarin.Forms;

namespace MobileDevelopment.ViewModels
{
    public static class BaseStoreViewModel
    {
        private static IBookStore _bookStore;
        public static IBookStore BookStore => _bookStore ??= DependencyService.Get<IBookStore>();
    }
}