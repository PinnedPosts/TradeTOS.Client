using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeTOS.Abstractions;
using TradeTOS.Common;
using TradeTOS.Data;
using TradeTOS.Extensions;
using TradeTOS.Helpers;
using TradeTOS.Models;

namespace TradeTOS.ViewModels
{
    public class LoginPageViewModel : BaseViewModel, INavigationAware
    {
        protected ITOSService _iTOSService;
        public LoginPageViewModel(INavigationService navigationService, ITOSService iTOSService, IPageDialogService dialogService)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;
            _iTOSService = iTOSService;
            Model = new LoginModel();
            LoginCommand = new DelegateCommand(Authenticate);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {

        }

        public LoginModel Model { get; set; }
        public DelegateCommand LoginCommand { get; set; }

        async void Authenticate()
        {
            var validate = Model.Validate();
            if (!validate.IsValid)
            {
                await _dialogService.DisplayError(validate.ErrorMessage);
                return;
            }
            IsBusy = true;
            var response = await _iTOSService.Authenticate(Model);
            if (response.IsSuccess)
            {
                #region Sync
                AppConstants.CurrentUser = response.Content;
                if (!AppStorage.IsSyncTableExists())
                {
                    var responseString = await _iTOSService.GetProducts();
                    if (responseString.IsSuccess)
                    {
                        AppStorage.SetSyncTable("Products", responseString.Content);
                    }
                    responseString = await _iTOSService.GetLocations();
                    if (responseString.IsSuccess)
                    {
                        AppStorage.SetSyncTable("Locations", responseString.Content);
                    }
                    responseString = await _iTOSService.GetCustomers();
                    if (responseString.IsSuccess)
                    {
                        AppStorage.SetSyncTable("Customers", responseString.Content);
                    }
                    AppStorage.SetAppTable(response.Content.UserName);
                }
                #endregion Sync
                _navigationService.Navigate(NavigationHelper.GetAbsoluteUri("HomePage"));
                IsBusy = false;
            }
            else
            {
                IsBusy = false;
                await _dialogService.DisplayError(response.ErrorMessage);
            }
        }

    }
}

