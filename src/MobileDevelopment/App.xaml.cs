using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MobileDevelopment.EFContext;
using MobileDevelopment.Interfaces;
using MobileDevelopment.Repositories;
using MobileDevelopment.Services;
using Xamarin.Forms;

namespace MobileDevelopment
{
    public partial class App : Application
    {
        private const string DB_FILE_NAME = "mobile_development.db";
        
        public App()
        {
            InitializeComponent();
            DependencyService.Register<IChartStore, ChartStore>();
            DependencyService.Register<IBookStore, BookStore>();
            DependencyService.Register<IGalleryStore, GalleryStore>();
            using var host = CreateHostBuilder().Build();
            host.Run();
            MainPage = new AppShell();
        }

        private static IHostBuilder CreateHostBuilder()
        {
            var dbPath = DependencyService.Get<IDatabasePath>().GetDatabasePath(DB_FILE_NAME);
            return Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
                {
                    services.AddScoped<IUnitOfWork, UnitOfWork>();
                    services.AddDbContext<ApplicationContext>(opt => opt.UseSqlite(dbPath));
                });
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
