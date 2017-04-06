using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Customer
{
    public class CustomerFormatProvider : IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            return formatType == typeof(ICustomFormatter) ? this : null;
        }

        public string Format(string format, object arg, IFormatProvider provider)
        {
            if (arg == null)
                return string.Empty;

            string result = "Customer record: ";
            
            var customer = arg as Customer;

            if (customer == null)
                throw new ArgumentException($"The type of arg is not {typeof(Customer)}");

            string name = customer.Name;
            string revenue = string.Format(CultureInfo.GetCultureInfo("en-US"), "{0:C}", customer.Revenue);
            string contactPhone = customer.ContactPhone;

            switch (format)
            {
                case "FT": // Fairy Tale format
                    return $"Hello! This is customer. His name is \"{name}\". His revenue is {revenue}. You can contact him using this number: {contactPhone}";
                case "FIA":  // Full info with attribute names
                    break;
                case "FIUC": // Full info with upper case
                    name = name.ToUpperInvariant();
                    contactPhone = contactPhone.ToUpperInvariant();
                    break;
                case "FILC": // Full info with lower case
                    name = name.ToLowerInvariant();
                    contactPhone = contactPhone.ToLowerInvariant();
                    break;
                default: // Other formats
                    return customer.ToString();
            }

            return result + $"name: {name}, revenue: {revenue}, contact phone number: {contactPhone}";
        }
    }
}
