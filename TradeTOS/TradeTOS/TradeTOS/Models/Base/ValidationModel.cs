using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeTOS.Models
{
    public class ValidationModel
    {
        public ValidationModel()
        {
            IsValid = true;
            ErrorMessage = "";
        }
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }
    }
}
