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
        [TestCase("Andrei", "+375 (44) 777 88 99", 200000, ExpectedResult = "Customer record: Andrei, $200,000.00, +375 (44) 777 88 99")]
        public string ToString_ZeroParameters_ReturnsStandartFullInfoString(string name, string contactPhone, decimal revenue)
        {
            return new Customer(name, contactPhone, revenue).ToString();
        }

        [TestCase("Dmitrii", "+375 (29) 788 77 69", 200.00, "S", ExpectedResult = "Customer record: Dmitrii, $200.00, +375 (29) 788 77 69")]
        [TestCase("Dmitrii", "+375 (29) 788 77 69", 200, "N", ExpectedResult = "Customer record: Dmitrii")]
        [TestCase("Dmitrii", "+375 (29) 788 77 69", 200, "P", ExpectedResult = "Customer record: +375 (29) 788 77 69")]
        [TestCase("Dmitrii", "+375 (29) 788 77 69", 200, "R", ExpectedResult = "Customer record: $200.00")]
        [TestCase("Dmitrii", "+375 (29) 788 77 69", 200, "NP", ExpectedResult = "Customer record: Dmitrii, +375 (29) 788 77 69")]
        [TestCase("Dmitrii", "+375 (29) 788 77 69", 200, "NR", ExpectedResult = "Customer record: Dmitrii, $200.00")]
        [TestCase("Dmitrii", "+375 (29) 788 77 69", 200, "RP", ExpectedResult = "Customer record: $200.00, +375 (29) 788 77 69")]
        public string ToString_WithDifferentCorrectFormatParams_ReturnsStringAccordingToFormat(string name, string contactPhone, decimal revenue, string format)
        {
            return new Customer(name, contactPhone, revenue).ToString(format);
        }

        [TestCase("Customer record: Empty, Zero, Empty")]
        public void ToString_UsingZeroParamsCtor_ReturnsStringAccordingToFormat(string expected)
        {
            string actual = new Customer().ToString();

            Assert.AreEqual(expected, actual);
        }
    }

    [TestFixture]
    public class CustomerFormatProviderTests
    {
        private readonly CustomerFormatProvider provider = new CustomerFormatProvider();
        private Customer customer;

        [TestCase("asfsa", null)]
        public void Format_TakesNullArg_ReturnsEmptyString(string format, object arg)
        {
            string expected = string.Empty;
            string actual = provider.Format(format, arg, provider);

            Assert.AreEqual(expected, actual);
        }

        [TestCase()]
        public void Format_ArgTypeIsNotCustomerType_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => provider.Format("blablabla", new StringBuilder(), provider));
        }

        [TestCase("BadFormat", "Alex", "8 (029) 321 23 43", 10005.25, ExpectedResult = "Customer record: Alex, $10,005.25, 8 (029) 321 23 43")]
        public string Format_TakesBadFormat_returnsStandartCustomerStringRepresentation(string format, string name, string phoneNumber,
            decimal revenue)
        {
            customer = new Customer(name, phoneNumber, revenue);

            return provider.Format(format, customer, provider);
        }

        [TestCase("FIA", "Alex", "8 (029) 321 23 43", 1500.25,
            ExpectedResult = "Customer record: name: Alex, revenue: $1,500.25, contact phone number: 8 (029) 321 23 43")]
        public string Format_TakesFullInfoAttributesFormat_PositiveTest(string format, string name, string phoneNumber,
            decimal revenue)
        {
            customer = new Customer(name, phoneNumber, revenue);

            return provider.Format(format, customer, provider);
        }

        [TestCase("FIUC", "Alex", "velcom 8 (029) 321 23 43", 1500.25,
            ExpectedResult = "Customer record: name: ALEX, revenue: $1,500.25, contact phone number: VELCOM 8 (029) 321 23 43")]
        public string Format_TakesFullInfoUpperCaseFormat_PositiveTest(string format, string name, string phoneNumber,
            decimal revenue)
        {
            customer = new Customer(name, phoneNumber, revenue);

            return provider.Format(format, customer, provider);
        }

        [TestCase("FILC", "Alex", "vElCoM 8 (029) 321 23 43", 1500.25,
            ExpectedResult = "Customer record: name: alex, revenue: $1,500.25, contact phone number: velcom 8 (029) 321 23 43")]
        public string Format_TakesFullInfoLowerCaseFormat_PositiveTest(string format, string name, string phoneNumber,
            decimal revenue)
        {
            customer = new Customer(name, phoneNumber, revenue);

            return provider.Format(format, customer, provider);
        }

        [TestCase("FT", "Alex", "Velcom 8 (029) 321 23 43", 1500.25,
            ExpectedResult = "Hello! This is customer. His name is \"Alex\". His revenue is $1,500.25. You can contact him using this number: Velcom 8 (029) 321 23 43")]
        public string Format_TakesFairyTaleFormat_PositiveTest(string format, string name, string phoneNumber,
            decimal revenue)
        {
            customer = new Customer(name, phoneNumber, revenue);

            return provider.Format(format, customer, provider);
        }
    }
}
