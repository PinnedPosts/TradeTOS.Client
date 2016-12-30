using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeTOS.Models
{
    public partial class InventoryModel
    {
        public string Ioh_itemID { get; set; }
        public string Ioh_OrgID { get; set; }
        public string Ioh_Warehouse { get; set; }
        public string Ioh_Quality { get; set; }
        public string Ioh_UOM { get; set; }
        public decimal Ioh_Quantity { get; set; }
        public string Ioh_Remark { get; set; }
    }
}
