using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeTOS.Abstractions;
using TradeTOS.Extensions;
using TradeTOS.Models;
using Xamarin.Forms;

namespace TradeTOS.ViewModels
{
  
    public class ReportsPageViewModel : BaseViewModel
    {
        protected ITOSService _iTOSService;
        public ReportsPageViewModel(INavigationService navigationService, ITOSService iTOSService, IPageDialogService dialogService)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;
            _iTOSService = iTOSService;
            DownloadPDFCommand = new DelegateCommand<string>(DownloadPDF);
            Model = new List<ReportModel>();
            Initialize();
        }

        protected async override void Initialize()
        {
            IsBusy = true;
            var response = await _iTOSService.GetReports();
            if (response.IsSuccess)
            {
                Model = response.Content;
                IsBusy = false;
            }
            else
            {
                IsBusy = false;
                await _dialogService.DisplayError(response.ErrorMessage);
            }
        }

        public DelegateCommand<string> DownloadPDFCommand { get; set; }

        private List<ReportModel> _model;
        public List<ReportModel> Model
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


        async void DownloadPDF(string url)
        {
            Device.OpenUri(new Uri(url));
        }

    }
}
