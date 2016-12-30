using Prism.Services;
using System.Threading.Tasks;
using TradeTOS.Utilities;

namespace TradeTOS.Extensions
{
    public static class DialogServiceExtension
    {
        public async static Task DisplayError(this IPageDialogService dialogService, string message)
        {
            await dialogService.DisplayAlertAsync(ResourceManager.SharedInstance.GetLabel("error"), message, ResourceManager.SharedInstance.GetLabel("ok"));
        }
    }
}
