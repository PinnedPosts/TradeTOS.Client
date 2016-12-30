using TradeTOS.Utilities;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeTOS.Extensions
{
    public static class NavigationServiceExtension
    {
        public static void Navigate(this INavigationService navigationservice, string uri, NavigationParameters parameters = null)
        {
            string[] uriList = uri.Split('/');
            string screen = uriList[uriList.Length - 1];
            ResourceManager.Instance.Initialize(screen);
            navigationservice.NavigateAsync(uri, parameters);
        }

        public static void Navigate(this INavigationService navigationservice, Uri uri , NavigationParameters parameters = null)
        {
            string[] uriList = uri.ToString().Split('/');
            string screen = uriList[uriList.Length - 1];
            ResourceManager.Instance.Initialize(screen);
            navigationservice.NavigateAsync(uri, parameters);
        }
    }
}
