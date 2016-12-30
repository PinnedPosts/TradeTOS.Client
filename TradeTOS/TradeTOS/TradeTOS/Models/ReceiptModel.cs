using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeTOS.Utilities;

namespace TradeTOS.Models
{
    public class ReceiptModel : BaseModel
    {
        public ReceiptModel()
        {
            SelectedCustomer = new EnumModel();
            Rec_Date = DateTime.Now;
        }
        private long? _rec_DocNo;

        [JsonIgnore]
        public long? Rec_DocNo
        {
            get
            {
                return _rec_DocNo;
            }
            set
            {
                SetProperty(ref _rec_DocNo, value);
            }
        }
        public string Rec_CustID { get; set; }
        public string Rec_CustName { get; set; }
        public System.DateTime Rec_Date { get; set; }
        public decimal Rec_Amount { get; set; }
        public string Rec_Comments { get; set; }

        private EnumModel _selectedCustomer;
        [JsonIgnore]
        public EnumModel SelectedCustomer
        {
            get
            {
                return _selectedCustomer;
            }
            set
            {
                SetProperty(ref _selectedCustomer, value);
                Rec_CustID = value.Key;
                Rec_CustName = value.Value;
            }
        }

        public override ValidationModel Validate()
        {
            ValidationModel validation = new ValidationModel();
            Mandatory(Rec_CustName, ResourceManager.SharedInstance.GetLabel("retailer"), validation);
            Mandatory(Rec_Amount, ResourceManager.SharedInstance.GetLabel("amount"), validation);
            Mandatory(Rec_Date, ResourceManager.SharedInstance.GetLabel("date"), validation);

            if (validation.IsValid)
            {
                IsNumeric(Rec_Amount, ResourceManager.SharedInstance.GetLabel("amount"), validation);
            }
            return validation;
        }
    }

    public class ReceiptResponseModel : BaseModel
    {
        public ReceiptResponseModel()
        {
            Rec_Date = DateTime.Now;
        }
        private long? _rec_DocNo;

        public long? Rec_DocNo
        {
            get
            {
                return _rec_DocNo;
            }
            set
            {
                SetProperty(ref _rec_DocNo, value);
            }
        }
        public string Rec_CustID { get; set; }
        public string Rec_CustName { get; set; }
        public System.DateTime Rec_Date { get; set; }
        public decimal Rec_Amount { get; set; }
        public string Rec_Comments { get; set; }

       
    }
}
