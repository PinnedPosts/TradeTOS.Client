using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using System;
using System.IO;
using TradeTOS.Abstractions;
using Xamarin.Forms;

[assembly: Dependency(typeof(TradeTOS.Droid.Dependencies.SQLite))]
namespace TradeTOS.Droid.Dependencies
{
    public class SQLite : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var fileName = "TradeTOS.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, fileName);
            var platform = new SQLitePlatformAndroid();
            var connection = new SQLiteConnection(platform, path);
            return connection;
        }

    }
}