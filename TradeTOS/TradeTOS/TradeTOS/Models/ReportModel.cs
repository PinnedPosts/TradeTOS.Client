using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeTOS.Models
{
    public class ReportModel : BaseModel
    {
        public ReportModel()
        {
            Url = "http://www.who.int/responsiveness/KIS%20Report.pdf";
        }
        public string Name { get; set; }
        public string Url { get; set; }

        public string DisplayName
        {
            get
            {
                if(string.IsNullOrEmpty(Name))
                {
                    return "";
                }
                return Name[0].ToString();
            }
        }
    }
}
