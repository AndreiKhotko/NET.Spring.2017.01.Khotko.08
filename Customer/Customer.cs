using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer
{
    /// <summary>
    /// Class customer
    /// </summary>
    public class Customer : IFormattable
    {
        /// <summary>
        /// Customer Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Customer contact phone number
        /// </summary>
        public string ContactPhone { get; set; }

        /// <summary>
        /// Customer revenue
        /// </summary>
        public decimal Revenue { get; set; }

        /// <summary>
        /// Constructor with 0 parameters
        /// </summary>
        public Customer()
        {
            Name = "Empty";
            ContactPhone = "Empty";
        }

        /// <summary>
        /// Override method ToString() which returns standart string representation of class Customers
        /// </summary>
        /// <returns>Customer string representation</returns>
        public override string ToString()
        {
            return ToString("G");
        }

        /// <summary>
        /// ToString with format
        /// </summary>
        /// <param name="format">Format</param>
        /// <param name="provider">Provider</param>
        /// <returns>Customer string representation</returns>
        public string ToString(string format, IFormatProvider provider = null)
        {
            if (format == null)
                format = "G";

            if (provider == null)
                provider = CultureInfo.GetCultureInfo("en-US");

            string result = "Customer record: ";
            string revenueString = string.Format(provider, "{0:C}", Revenue);
            
            switch (format.ToUpperInvariant())
            {
                case "G":
                    return result + $"{Name}, {revenueString}, {ContactPhone}";

                case "N":
                    return result + Name;

                case "P":
                    return result + ContactPhone;

                case "R":
                    return result + revenueString;

                case "NP":
                    return  result + $"{Name}, {ContactPhone}";

                case "NR":
                    return result + $"{Name}, {revenueString}";

                case "RP":
                    return result + $"{revenueString}, {ContactPhone}";

                default:
                    throw new FormatException("Bad format argument");

            }
        }
    }
}
