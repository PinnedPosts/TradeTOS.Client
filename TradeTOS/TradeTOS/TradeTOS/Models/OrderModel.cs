using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeTOS.Models
{
    public class OrderModel : BaseModel
    {
        public OrderModel()
        {
            Soh_Deleted = 0;
        }
        public long? Soh_Id { get; set; }
        public long? Soh_DocNo { get; set; }
        public Nullable<int> Soh_LiciD { get; set; }
        public Nullable<int> Soh_OrgId { get; set; }
        public string Soh_CustOrdNo { get; set; }
        public string Soh_CustId { get; set; }
        public string Soh_CustLocationID { get; set; }
        public short Soh_Deleted { get; set; }
        public Nullable<int> Crt_User { get; set; }
        public Nullable<System.DateTime> Crt_Date { get; set; }
        public Nullable<int> Upd_User { get; set; }
        public Nullable<System.DateTime> Upd_Date { get; set; }
        public Nullable<int> Del_User { get; set; }
        public Nullable<System.DateTime> Del_Date { get; set; }
    }

    public class OrderProductModel : BaseModel
    {
        public OrderProductModel()
        {
            Sop_Deleted = 0;
        }
        public long? Sop_Id { get; set; }
        public long Sop_sohID { get; set; }
        public string Sop_ProdId { get; set; }
        public string Sop_ProdName { get; set; }
        public decimal Sop_Qty { get; set; }
        public decimal Sop_Rate { get; set; }
        public short Sop_Deleted { get; set; }
        public Nullable<int> Crt_User { get; set; }
        public Nullable<System.DateTime> Crt_Date { get; set; }
        public Nullable<int> Upd_User { get; set; }
        public Nullable<System.DateTime> Upd_Date { get; set; }
        public Nullable<int> Del_User { get; set; }
        public Nullable<System.DateTime> Del_Date { get; set; }
    }
}
