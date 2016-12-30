using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeTOS.Models
{
    public class SyncModel
    {
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; }
        public string Key { get; set; }
        public string ValueJson { get; set; }
    }
}
