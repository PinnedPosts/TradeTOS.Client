using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeTOS.Utilities;

namespace TradeTOS.Models
{
    public class LoginModel : BaseModel
    {
        [JsonProperty("UserName")]
        public string UserName { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("Token")]
        public string Token { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        private string _lastSync;
        [JsonIgnore]
        public string LastSync
        {
            get
            {
                return _lastSync;
            }
            set
            {
                SetProperty(ref _lastSync, value);
            }
        }

        public override ValidationModel Validate()
        {
            ValidationModel validate = new ValidationModel();
            Mandatory(UserName, ResourceManager.SharedInstance.GetLabel("username"), validate);
            Mandatory(Password, ResourceManager.SharedInstance.GetLabel("password"), validate);
            return validate;
        }
    }
}
