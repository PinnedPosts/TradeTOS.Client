using TradeTOS.Abstractions;
using TradeTOS.Extensions;
using TradeTOS.Helpers;
using TradeTOS.Services;
using TradeTOS.Views;
using Prism.Unity;
using Microsoft.Practices.Unity;
using Prism.Navigation;
using TradeTOS.Data;
using TradeTOS.Utilities;

namespace TradeTOS
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();
            var settings = AppStorage.GetAppTable();
            if (settings != null && !string.IsNullOrEmpty(settings.Language))
            {
                ResourceManager.Instance.CurrentCulture = settings.Language;
            }
            NavigationService.Navigate("LoginPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<RootPage>();
            Container.RegisterTypeForNavigation<LoginPage>();
            Container.RegisterTypeForNavigation<HomePage>();
            Container.RegisterType<ITOSService, TOSService>();
            Container.RegisterTypeForNavigation<ReportsPage>();
            Container.RegisterTypeForNavigation<OrderPage>();
            Container.RegisterTypeForNavigation<PaymentPage>();
            Container.RegisterTypeForNavigation<SettingsPage>();
            Container.RegisterTypeForNavigation<OrderDetailsPage>();
            Container.RegisterTypeForNavigation<InventoryPage>();
        }
    }
}
