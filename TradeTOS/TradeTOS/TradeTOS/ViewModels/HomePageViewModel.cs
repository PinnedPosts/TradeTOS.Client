using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeTOS.Abstractions;
using TradeTOS.Common;
using TradeTOS.Data;
using TradeTOS.Extensions;
using TradeTOS.Helpers;
using TradeTOS.Utilities;
using Xamarin.Forms;

namespace TradeTOS.ViewModels
{

    public class HomePageViewModel : BaseViewModel
    {
        protected ITOSService _authService;
        public HomePageViewModel(INavigationService navigationService, ITOSService authService, IPageDialogService dialogService)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;
            _authService = authService;
            NavigateCommand = new DelegateCommand<string>(Navigate);
            LogoutCommand = new DelegateCommand(Logout);
            ChangeLanguageCommand = new DelegateCommand(changeLanguage);
            DeviceUriCommand = new DelegateCommand<string>(DeviceUri);
        }

        public DelegateCommand<string> NavigateCommand { get; set; }

        public DelegateCommand LogoutCommand { get; set; }
        public DelegateCommand ChangeLanguageCommand { get; set; }

        public DelegateCommand<string> DeviceUriCommand { get; set; }

        async void Navigate(string pageName)
        {
            IsBusy = true;
            _navigationService.Navigate(pageName);
            IsBusy = false;
        }

        async void Logout()
        {
            IsBusy = true;
            _navigationService.Navigate(NavigationHelper.GetAbsoluteUriAsBase("LoginPage"));
            IsBusy = false;
        }

        async void DeviceUri(string action)
        {
            IsBusy = true;
            if (action == "PHONE")
            {
                Device.OpenUri(new Uri("tel:+919322101243"));
            }
            else
            {
                Device.OpenUri(new Uri(action));
            }
            IsBusy = false;
        }

        async void changeLanguage()
        {
            IsBusy = true;
            ResourceManager.Instance.Initialize("homepage");
            var value = await  _dialogService.DisplayActionSheetAsync(ResourceManager.SharedInstance.GetLabel("language"), null, ResourceManager.SharedInstance.GetLabel("cancel"), "English", "العربية");
            switch (value)
            {
                case "English":
                    AppStorage.SetAppTable(AppConstants.CurrentUser.UserName, "en_US");
                    ResourceManager.Instance.CurrentCulture = "en_US";
                    IsBusy = false;
                    break;
                case "العربية":
                    AppStorage.SetAppTable(AppConstants.CurrentUser.UserName, "ar_SA");
                    ResourceManager.Instance.CurrentCulture = "ar_SA";
                    IsBusy = false;
                    break;
                default:
                    IsBusy = false;
                    break;
            }
        }


    }
}
