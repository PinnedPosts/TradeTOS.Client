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
using TradeTOS.Utilities;

namespace TradeTOS.ViewModels
{

    public class InventoryPageViewModel : BaseViewModel
    {
        protected ITOSService _iTOSService;
        public InventoryPageViewModel(INavigationService navigationService, ITOSService iTOSService, IPageDialogService dialogService)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;
            _iTOSService = iTOSService;
            Model = new List<InventoryModel>();
            Products = new List<EnumModel>();
            LoadCommand = new DelegateCommand(Load);
            Initialize();
        }

        private List<InventoryModel> _model;
        public List<InventoryModel> Model
        {
            get
            {
                return _model;
            }
            set
            {
                SetProperty(ref _model, value);
            }
        }

        public List<EnumModel> Products { get; set; }

        public EnumModel SelectedProduct { get; set; }

        protected async override void Initialize()
        {
            base.Initialize();
            var products = AppStorage.GetListOfValues("Products");
            if (products != null)
            {
                Products = products;
            }
        }

        public DelegateCommand LoadCommand { get; set; }

        async void Load()
        {
            if(SelectedProduct == null)
            {
                await _dialogService.DisplayError(ResourceManager.SharedInstance.GetLabel("pleaseselect"));
                return;
            }
            IsBusy = true;
            var response = await _iTOSService.GetStock(SelectedProduct.Key);
            if (response.IsSuccess)
            {
                Model = response.Content;
                if(Model.Count == 0)
                {
                    await _dialogService.DisplayError(ResourceManager.SharedInstance.GetLabel("noitems"));
                }
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
