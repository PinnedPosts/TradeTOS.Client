using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeTOS.Abstractions;
using TradeTOS.Data;
using TradeTOS.Extensions;
using TradeTOS.Models;

namespace TradeTOS.ViewModels
{

    public class PaymentPageViewModel : BaseViewModel
    {
        protected ITOSService _iTOSService;
        public PaymentPageViewModel(INavigationService navigationService, ITOSService iTOSService, IPageDialogService dialogService)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;
            _iTOSService = iTOSService;
            Model = new ReceiptModel();
            Customers = new List<EnumModel>();
            SaveCommand = new DelegateCommand(Save);
            Initialize();
        }

        public ReceiptModel Model { get; set; }

        public List<EnumModel> Customers { get; set; }

        protected async override void Initialize()
        {
            base.Initialize();
            var customers = AppStorage.GetListOfValues("Customers");
            if (customers != null)
            {
                Customers = customers;
            }
        }

        public DelegateCommand SaveCommand { get; set; }

        async void Save()
        {
            var validate = Model.Validate();
            if (!validate.IsValid)
            {
                await _dialogService.DisplayError(validate.ErrorMessage);
                return;
            }
            IsBusy = true;
            var response = await _iTOSService.SaveReceipt(Model);
            if (response.IsSuccess)
            {
                Model.Rec_DocNo = response.Content.Rec_DocNo;
                IsEnabled = false;
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
