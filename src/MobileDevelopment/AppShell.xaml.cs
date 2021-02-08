using System;
using Xamarin.Forms;

namespace MobileDevelopment
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent(); 
            // Routing example
            // Routing.RegisterRoute(nameof(FromPage), typeof(ToPage));
        }
    }
}
