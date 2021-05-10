using System;
using System.IO;
using MobileDevelopment.Droid;
using MobileDevelopment.EFContext;
using Xamarin.Forms;

[assembly:Dependency(typeof(AndroidDatabasePath))]
namespace MobileDevelopment.Droid
{
    public class AndroidDatabasePath : IDatabasePath
    {
        public string GetDatabasePath(string filename)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), filename);
        }
    }
}