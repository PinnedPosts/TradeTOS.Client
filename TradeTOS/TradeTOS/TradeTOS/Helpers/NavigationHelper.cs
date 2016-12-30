using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeTOS.Helpers
{
    public class NavigationHelper
    {
        public static Uri GetAbsoluteUri(string pageName)
        {
            return new Uri("http://www.TradeTOS.com/RootPage/" + pageName, UriKind.Absolute);
        }

        public static Uri GetAbsoluteUriAsBase(string pageName)
        {
            return new Uri("http://www.TradeTOS.com/" + pageName, UriKind.Absolute);
        }
        public static string GetUri(string pageName)
        {
            return "RootPage/" + pageName;
        }
    }
}
