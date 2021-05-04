using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileDevelopment.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileDevelopment.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GridGalleryPage : ContentPage
    {
        public GridGalleryPage()
        {
            InitializeComponent();
            BindingContext = new GridGalleryViewModel();
        }
    }
}