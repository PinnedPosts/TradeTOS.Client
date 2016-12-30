using TradeTOS.Abstractions;
using TradeTOS.Common;
using System.Threading.Tasks;
using TradeTOS.Models;
using TradeTOS.Helpers;
using System;
using System.Collections.Generic;

namespace TradeTOS.Services
{
    public class TOSService : ITOSService
    {
        private RestHelper restHelper;
        public TOSService()
        {
            restHelper = new RestHelper(AppConstants.ApiUrl);
        }

        public async Task<RestResponse<LoginModel>> Authenticate(LoginModel login)
        {
            var response = await restHelper.PostAsync<LoginModel, LoginModel>(AuthMethods.Authenticate, login);
            return response;
        }

        public async Task<RestResponse<string>> GetCustomers()
        {
            var response = await restHelper.GetAsync<string>(AuthMethods.Customers);
            return response;
        }

        public async Task<RestResponse<string>> GetLocations()
        {
            var response = await restHelper.GetAsync<string>(AuthMethods.Locations);
            return response;
        }

        public async Task<RestResponse<string>> GetProducts()
        {
            var response = await restHelper.GetAsync<string>(AuthMethods.Products);
            return response;
        }

        public async Task<RestResponse<List<ReportModel>>> GetReports()
        {
            var list = new List<ReportModel>();
            list.Add(new ReportModel()
            {
                Name = "ABC Report"
            });
            list.Add(new ReportModel()
            {
                Name = "DA Report"
            });
            list.Add(new ReportModel()
            {
                Name = "RE Report"

            });
            list.Add(new ReportModel()
            {
                Name = "GGG Report"
            });
            list.Add(new ReportModel()
            {
                Name = "RRRR Report"
            });
            list.Add(new ReportModel()
            {
                Name = "EEEE Report"
            });
            list.Add(new ReportModel()
            {
                Name = "ABC Report"
            });
            list.Add(new ReportModel()
            {
                Name = "DA Report"
            });
            list.Add(new ReportModel()
            {
                Name = "RE Report"

            });
            list.Add(new ReportModel()
            {
                Name = "GGG Report"
            });
            list.Add(new ReportModel()
            {
                Name = "RRRR Report"
            });
            list.Add(new ReportModel()
            {
                Name = "EEEE Report"
            });
            return new RestResponse<List<ReportModel>>()
            {
                IsSuccess = true,
                Content = list
            };
        }

        public async Task<RestResponse<List<InventoryModel>>> GetStock(string itemId)
        {
            var response = await restHelper.GetAsync<List<InventoryModel>>(AuthMethods.Stock(itemId));
            return response;
        }

        public async Task<RestResponse<ReceiptResponseModel>> SaveReceipt(ReceiptModel receipt)
        {
            var response = await restHelper.PostAsync<ReceiptModel, ReceiptResponseModel>(AuthMethods.Receipt, receipt);
            return response;
        }
    }

    public class AuthMethods
    {
        public static string Authenticate { get { return "Account/Login"; } }
        public static string Products { get { return "Enum/Products"; } }
        public static string Locations { get { return "Enum/Locations"; } }
        public static string Customers { get { return "Enum/Customers"; } }
        public static string Receipt { get { return "Receipt/PostReceipt"; } }
        public static string Stock(string itemId) { return string.Format("Inventory/Stock?itemId={0}", itemId); }
    }

}
