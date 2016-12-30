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
using TradeTOS.Models;

namespace TradeTOS.ViewModels
{

    public class SettingsPageViewModel : BaseViewModel
    {
        protected ITOSService _iTOSService;
        public SettingsPageViewModel(INavigationService navigationService, ITOSService iTOSService, IPageDialogService dialogService)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;
            _iTOSService = iTOSService;
            Model = new LoginModel();
            SyncCommand = new DelegateCommand(Sync);
            Initialize();
        }

        protected async override void Initialize()
        {
            base.Initialize();
            Model = AppConstants.CurrentUser;
            Model.LastSync = AppStorage.GetAppTable()?.LastSync;
        }

        public LoginModel Model { get; set; }

        public List<EnumModel> Customers { get; set; }

        public DelegateCommand SyncCommand { get; set; }

        async void Sync()
        {
            bool isSuccess = true;
            IsBusy = true;
            var responseString = await _iTOSService.GetProducts();
            if (responseString.IsSuccess)
            {
                AppStorage.SetSyncTable("Products", responseString.Content);
            }
            else
            {
                isSuccess = false;
            }
            responseString = await _iTOSService.GetLocations();
            if (responseString.IsSuccess)
            {
                AppStorage.SetSyncTable("Locations", responseString.Content);
            }
            else
            {
                isSuccess = false;
            }
            responseString = await _iTOSService.GetCustomers();
            if (responseString.IsSuccess)
            {
                AppStorage.SetSyncTable("Customers", responseString.Content);
            }
            else
            {
                isSuccess = false;
            }
            AppStorage.SetAppTable(AppConstants.CurrentUser.UserName);
            Model.LastSync = DateTime.Now.ToString();
            IsBusy = false;
            if(isSuccess)
            {
                await _dialogService.DisplayAlertAsync("Success", "Synced successfully!", "Ok");
            }
            else
            {
                await _dialogService.DisplayAlertAsync("Success", "Sync done with errors!", "Ok");
            }
        }
    }
}
