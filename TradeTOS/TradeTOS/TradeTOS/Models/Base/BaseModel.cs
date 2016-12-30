using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TradeTOS.Utilities;

namespace TradeTOS.Models
{
    public class BaseModel : BindableBase
    {
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }

        [JsonIgnore]
        public DateTime EditedDate { get; set; }

        [JsonIgnore]
        public int CreatedBy { get; set; }

        [JsonIgnore]
        public int EditedBy { get; set; }

        #region Validations
        public virtual ValidationModel Validate()
        {
            return new ValidationModel()
            {
                IsValid = true
            };
        }

        public void Mandatory(object property, string name, ValidationModel validation)
        {
            if (property == null || string.IsNullOrEmpty(property.ToString()))
            {
                validation.IsValid = false;
                validation.ErrorMessage += string.Format(ResourceManager.SharedInstance.GetLabel("required"), name);
                validation.ErrorMessage += Environment.NewLine;
            }
        }

        public void IsInteger(object property, string name, ValidationModel validation)
        {
            int result = 0;
            if (property!=null && !int.TryParse(property.ToString(), out result))
            {
                validation.IsValid = false;
                validation.ErrorMessage += string.Format(ResourceManager.SharedInstance.GetLabel("numeric"), name);
                validation.ErrorMessage += Environment.NewLine;
            }
        }

        public void IsNumeric(object property, string name, ValidationModel validation)
        {
            double result = 0;
            if (property != null && !double.TryParse(property.ToString(), out result))
            {
                validation.IsValid = false;
                validation.ErrorMessage += string.Format(ResourceManager.SharedInstance.GetLabel("numeric"), name);
                validation.ErrorMessage += Environment.NewLine;
            }
        }

        public void MaxLength(object property, string name, int allowedLenth, ValidationModel validation)
        {
            if (property == null || property.ToString().Length > allowedLenth)
            {
                validation.IsValid = false;
                validation.ErrorMessage += string.Format(ResourceManager.SharedInstance.GetLabel("maxlength"), name, allowedLenth);
                validation.ErrorMessage += Environment.NewLine;
            }
        }

        public void MinLength(object property, string name, int allowedLenth, ValidationModel validation)
        {
            if (property != null && property.ToString().Length < allowedLenth)
            {
                validation.IsValid = false;
                validation.ErrorMessage += string.Format(ResourceManager.SharedInstance.GetLabel("minlength"), name, allowedLenth);
                validation.ErrorMessage += Environment.NewLine;
            }
        }

        private const string emailRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        private const string phoneRegex = @"^\d{10}$";

        public void IsEmail(string property, string name, ValidationModel validation)
        {
            bool isValid;
            if(property == null)
            {
                isValid = false;
            }
            else
            {
                Regex regex = new Regex(emailRegex);
                Match match = regex.Match(property);
                isValid = match.Success;
            }        
            if (!isValid)
            {
                validation.IsValid = false;
                validation.ErrorMessage += string.Format(ResourceManager.SharedInstance.GetLabel("invalid"), name);
                validation.ErrorMessage += Environment.NewLine;
            }
        }

        public void IsPhoneNumber(string property, string name, ValidationModel validation)
        {
            bool isValid;
            if (property == null)
            {
                isValid = false;
            }
            else
            {
                Regex regex = new Regex(phoneRegex);
                Match match = regex.Match(property);
                isValid = match.Success;
            }
            if (!isValid)
            {
                validation.IsValid = false;
                validation.ErrorMessage += string.Format(ResourceManager.SharedInstance.GetLabel("invalid"), name);
                validation.ErrorMessage += Environment.NewLine;
            }
        }

        public string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            return (propertyExpression.Body as MemberExpression).Member.Name;
        }

        #endregion
    }
}
