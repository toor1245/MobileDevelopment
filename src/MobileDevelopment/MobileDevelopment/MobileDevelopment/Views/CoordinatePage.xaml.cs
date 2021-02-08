using MobileDevelopment.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileDevelopment.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoordinatePage : ContentPage
    {
        public CoordinatePage()
        {
            InitializeComponent();
            BindingContext = new CoordinatePageViewModel();
        }
    }
}