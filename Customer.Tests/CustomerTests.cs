using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Customer.Tests
{
    [TestFixture]
    public class CustomerTests
    {
        private Customer customer;

        [TestCase("Andrei", "+375 (44) 777 88 99", 200000, ExpectedResult = "Customer record: Andrei, $200,000.00, +375 (44) 777 88 99")]
        public string ToString_ZeroParameters_ReturnsStandartFullInfoString(string name, string contactPhone, decimal revenue)
        {
            customer = new Customer
            {
                Name = name,
                Revenue = revenue,
                ContactPhone = contactPhone
            };

            return customer.ToString();
        }

        [TestCase("Dmitrii", "+375 (29) 788 77 69", 200.00, "G", ExpectedResult = "Customer record: Dmitrii, $200.00, +375 (29) 788 77 69")]
        [TestCase("Dmitrii", "+375 (29) 788 77 69", 200, "N", ExpectedResult = "Customer record: Dmitrii")]
        [TestCase("Dmitrii", "+375 (29) 788 77 69", 200, "P", ExpectedResult = "Customer record: +375 (29) 788 77 69")]
        [TestCase("Dmitrii", "+375 (29) 788 77 69", 200, "R", ExpectedResult = "Customer record: $200.00")]
        [TestCase("Dmitrii", "+375 (29) 788 77 69", 200, "NP", ExpectedResult = "Customer record: Dmitrii, +375 (29) 788 77 69")]
        [TestCase("Dmitrii", "+375 (29) 788 77 69", 200, "NR", ExpectedResult = "Customer record: Dmitrii, $200.00")]
        [TestCase("Dmitrii", "+375 (29) 788 77 69", 200, "RP", ExpectedResult = "Customer record: $200.00, +375 (29) 788 77 69")]
        public string ToString_WithDifferentCorrectFormatParams_ReturnsStringAccordingToFormat(string name, string contactPhone, decimal revenue, string format)
        {
            customer = new Customer
            {
                Name = name,
                Revenue = revenue,
                ContactPhone = contactPhone
            };

            return customer.ToString(format);
        }

        [TestCase("Customer record: Empty, $0.00, Empty")]
        public void ToString_UsingZeroParamsCtor_ReturnsStringAccordingToFormat(string expected)
        {
            string actual = new Customer().ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
