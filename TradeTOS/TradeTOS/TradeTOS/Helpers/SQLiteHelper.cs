using TradeTOS.Abstractions;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace TradeTOS.Helpers
{
    public class SQLiteHelper : IDisposable
    {

        static object locker = new object();
        public SQLiteHelper()
        {
            Connection= DependencyService.Get<ISQLite>().GetConnection();
        }
        public SQLiteConnection Connection { get; private set; }
        public bool IsDbExists()
        {
            try
            {
                lock (locker)
                {

                    var table = Connection.GetTableInfo("SyncModel");
                    if (table == null || table.Count == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public void CreateTableIfNotExists<T>(string tableName) where T : new()
        {
            lock (locker)
            {
                var tableinfo = Connection.GetTableInfo(tableName);
                if (tableinfo == null || tableinfo.Count == 0)
                {
                    Connection.CreateTable<T>();
                }
            }
        }

        public List<T> GetTable<T>() where T : class, new()
        {
            lock (locker)
            {
                return Connection.Table<T>().ToList();
            }
        }

        public void SetTable<T>(T itemRow) where T : class
        {
            lock (locker)
            {
                Connection.InsertOrReplace(itemRow);
            }
        }

        public void InsertTable<T>(T itemRow) where T : class
        {
            lock (locker)
            {
                Connection.Insert(itemRow);
            }
        }

        public void UpdateTable<T>(T itemRow) where T : class
        {
            lock (locker)
            {
                Connection.Update(itemRow);
            }
        }

        public void DeleteItem<T>(string key) where T : class
        {
            lock (locker)
            {
                Connection.Delete<T>(key);
            }
        }

        public void Dispose()
        {
            Connection.Dispose();
            Connection = null;
        }
    }
}
