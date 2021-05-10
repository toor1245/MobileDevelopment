using System;
using MobileDevelopment.Interfaces;
using MobileDevelopment.Services;
using MobileDevelopment.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileDevelopment.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BooksPage : ContentPage
    {
        public readonly BooksViewModel _viewModel;
        public BooksPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new BooksViewModel();
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}