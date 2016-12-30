using TradeTOS.Helpers;
using TradeTOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TradeTOS.Data
{
    public class AppStorage
    {
        public static void SetSyncTable(string key, string value)
        {
            using (SQLiteHelper helper = new SQLiteHelper())
            {
                helper.CreateTableIfNotExists<SyncModel>("SyncModel");
                var query = helper.Connection.Table<SyncModel>().FirstOrDefault(a => a.Key.Equals(key));
                if (query == null)
                {
                    query = new SyncModel();
                }
                query.Key = key;
                query.ValueJson = value;               
                helper.SetTable<SyncModel>(query);
            }
        }

        public static void SetAppTable(string userName,string language ="en_US")
        {
            using (SQLiteHelper helper = new SQLiteHelper())
            {
                helper.CreateTableIfNotExists<AppModel>("AppModel");
                var query = helper.Connection.Table<AppModel>().FirstOrDefault();
                if (query == null)
                {
                    query = new AppModel();
                }
                query.LastSync = DateTime.Now.ToString();
                query.UserName = userName;
                query.Language = language;
                helper.SetTable<AppModel>(query);
            }
        }

        public static SyncModel GetSyncTable(string key)
        {
            using (SQLiteHelper helper = new SQLiteHelper())
            {
                helper.CreateTableIfNotExists<SyncModel>("SyncModel");
                return helper.Connection.Table<SyncModel>().FirstOrDefault(a => a.Key.Equals(key));
            }
        }

        public static AppModel GetAppTable()
        {
            using (SQLiteHelper helper = new SQLiteHelper())
            {
                helper.CreateTableIfNotExists<AppModel>("AppModel");
                return helper.Connection.Table<AppModel>().FirstOrDefault();
            }
        }

        public static List<EnumModel> GetListOfValues(string key)
        {
            var syncModel = GetSyncTable(key);
            if (syncModel == null)
            {
                return new List<EnumModel>();
            }
            return JsonConvert.DeserializeObject<List<EnumModel>>(syncModel.ValueJson);
        }

        public static bool IsSyncTableExists()
        {
            using (SQLiteHelper helper = new SQLiteHelper())
            {
                return helper.IsDbExists();
            }
        }
    }
}
