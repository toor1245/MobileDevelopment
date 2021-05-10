using System;
using MobileDevelopment.EFContext;
using MobileDevelopment.Interfaces;
using MobileDevelopment.Repositories;
using MobileDevelopment.Services;
using Xamarin.Forms;

namespace MobileDevelopment
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            DependencyService.Register<IChartStore, ChartStore>();
            DependencyService.Register<IBookStore, BookStore>();
            DependencyService.Register<IGalleryStore, GalleryStore>();
            MainPage = new AppShell();
        }
        
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
