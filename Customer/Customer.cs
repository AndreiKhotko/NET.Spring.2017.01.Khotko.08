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
    public class Customer
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
        /// Constructor with 3 parameters
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="contactPhone">Contacct phone number</param>
        /// <param name="revenue">Revenue</param>
        public Customer(string name, string contactPhone, decimal revenue)
        {
            Name = name;
            ContactPhone = contactPhone;
            Revenue = revenue;
        }

        /// <summary>
        /// Override method ToString() which returns standart string representation of class Customers
        /// </summary>
        /// <returns>Customer string representation</returns>
        public override string ToString()
        {
            return ToString("S");
        }

        /// <summary>
        /// ToString with format
        /// </summary>
        /// <param name="format">Format</param>
        /// <returns>Customer string representation</returns>
        public string ToString(string format)
        {
            if (format == null)
                throw new ArgumentNullException();

            string result = "Customer record: ";
            //string x = string.Format("fasafa", 1);
            string revenueString = string.Format(CultureInfo.GetCultureInfo("en-US"), "{0:C}", Revenue);

            if (Revenue == 0)
                revenueString = "Zero";    
            
            switch (format.ToUpperInvariant())
            {
                case "S":
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
