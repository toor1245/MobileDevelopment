using MobileDevelopment.Views;
using System;
using MobileDevelopment.Interfaces;
using MobileDevelopment.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileDevelopment
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            DependencyService.Register<IChartStore, ChartStore>();
            DependencyService.Register<IBookStore, BookStore>();
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
