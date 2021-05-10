using System;
using System.IO;
using MobileDevelopment.EFContext;

namespace MobileDevelopment.Droid.Configuration
{
    public class AndroidDatabasePath : IDatabasePath
    {
        public string GetDatabasePath(string filename)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", filename);
        }
    }
}