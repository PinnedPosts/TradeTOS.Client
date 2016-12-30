using TradeTOS.Helpers;
using TradeTOS.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TradeTOS.Abstractions
{
    public interface ITOSService
    {
        Task<RestResponse<LoginModel>> Authenticate(LoginModel login);
        Task<RestResponse<string>> GetProducts();
        Task<RestResponse<string>> GetLocations();
        Task<RestResponse<string>> GetCustomers();
        Task<RestResponse<List<ReportModel>>> GetReports();
        Task<RestResponse<ReceiptResponseModel>> SaveReceipt(ReceiptModel receipt);
        Task<RestResponse<List<InventoryModel>>> GetStock(string itemId);
    }
}
