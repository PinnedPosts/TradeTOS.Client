using TradeTOS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TradeTOS.Utilities
{
    public class ResourceManager
    {
        #region Ctor

        public ResourceManager()
        {

        }

        #endregion Ctor

        #region Properties

        private string _screenId;
        public string ScreenId
        {
            get
            {
                return _screenId;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    value = value.ToLower();
                }
                _screenId = value;
            }
        }

        static readonly ResourceManager _instance = new ResourceManager();
        public static ResourceManager Instance
        {
            get
            {
                return _instance;
            }
        }

        static ResourceManager _sharedInstance;
        
        public static ResourceManager SharedInstance
        {
            get
            {
                if(_sharedInstance == null)
                {
                    _sharedInstance  = new ResourceManager()
                    {
                        ScreenId = "shared"
                    };
                    _sharedInstance.changeLanguage();
                }
                return _sharedInstance;
            }
        }

        private static string _currentCulture = "en_US";
        public string CurrentCulture
        {
            get
            {
                return _currentCulture;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _currentCulture = value.Replace("-", "_");
                }
                changeLanguage();
                MessagingCenter.Send<ResourceManager>(this, "CultureChanged");
            }
        }

        private ScreenModel _cultureInfo;
        public ScreenModel CultureInfo
        {
            get
            {
                _cultureInfo = _cultureInfo ?? new ScreenModel();
                return _cultureInfo;
            }
            set
            {
                _cultureInfo = value;
            }
        }

        #endregion Properties

        #region Methods

        public string GetLabel(string key)
        {
            string labelText = "";
            var keyLabel = CultureInfo.Labels.Find(a => a.Id.Equals(key, System.StringComparison.OrdinalIgnoreCase));
            if (keyLabel != null)
            {
                labelText = keyLabel.Title;
            }
            return labelText;
        }

        public string GetPlaceHolder(string key)
        {
            string placeHolderText = "";
            if (CultureInfo.Inputs != null)
            {
                var keyLabel = CultureInfo.Inputs.Find(a => a.Id.Equals(key, System.StringComparison.OrdinalIgnoreCase));
                if (keyLabel != null)
                {
                    placeHolderText = keyLabel.Placeholder;
                }
            }
            return placeHolderText;
        }

        public string GetTitle()
        {
            return CultureInfo.Title;
        }

        public string GetTitle(string key)
        {
            string labelText = "";
            if (CultureInfo.PageTitles != null)
            {
                var keyLabel = CultureInfo.PageTitles.Find(a => a.Id.Equals(key, System.StringComparison.OrdinalIgnoreCase));
                if (keyLabel != null)
                {
                    labelText = keyLabel.Title;
                }
            }
            return labelText;
        }

        public string GetButtonText(string key)
        {
            string buttonText = "";
            var keyButton = CultureInfo.Buttons.Find(a => a.Id.Equals(key, System.StringComparison.OrdinalIgnoreCase));
            if (keyButton != null)
            {
                buttonText = keyButton.Text;
            }
            return buttonText;
        }

        private void changeLanguage()
        {
            try
            {
                var fileManager = new FileManager();
                var filePath = string.Format("TradeTOS.Resources.Culture.{0}.{1}.json", _currentCulture, _screenId);
                string json = fileManager.Load(filePath);
                CultureInfo = JsonConvert.DeserializeObject<ScreenModel>(json);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public void Initialize(Page view)
        {
            ScreenId = view.GetType().Name;
            changeLanguage();
        }

        public void Initialize(Type viewType)
        {
            ScreenId = viewType.Name;
            changeLanguage();
        }

        public void Initialize(string viewName)
        {
            ScreenId = viewName;
            changeLanguage();
        }

        #endregion Methods
    }
}
