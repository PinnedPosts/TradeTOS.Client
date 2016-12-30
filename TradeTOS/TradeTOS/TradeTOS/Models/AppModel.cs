using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeTOS.Models
{
    public class AppModel : BaseModel
    {
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; }
        public string LastSync { get; set; }
        public string UserName { get; set; }
        public string Language { get; set; }
    }
}
