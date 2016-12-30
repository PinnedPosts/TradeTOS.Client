using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeTOS.Models;

namespace TradeTOS.Common
{
    public class AppConstants
    {
        public static string ApiUrl { get { return "http://tradetos.apphb.com/api/"; } }
        public static LoginModel CurrentUser { get; set; }
    }

}
