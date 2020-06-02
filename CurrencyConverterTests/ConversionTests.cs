using CurrencyConverter.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CurrencyConverterTests
{
    [TestClass]
    public class ConversionTests
    {
        private readonly Currency gbp;
        private readonly Currency aud;
        private readonly Currency usd;

        public ConversionTests()
        {
            gbp = new Currency("GBP", "£");
            aud = new Currency("AUD", "A$");
            usd = new Currency("USD", "$");
            ExchangeRate gbpToUsd = new ExchangeRate(usd, 1.24697m);
            ExchangeRate gbpToAud = new ExchangeRate(aud, 1.83744m);
            gbp.ExchangeRates.Add(gbpToUsd);
            gbp.ExchangeRates.Add(gbpToAud);
        }

        [TestMethod]
        public void GbpToUsd()
        {
            decimal expected = 187.05m;
            decimal actual = gbp.Convert(usd, 150);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ZeroEdgeCase()
        {
            decimal expected = 0m;
            decimal actual = gbp.Convert(usd, 0);

            Assert.AreEqual(expected, actual);
        }

        //TODO Test case of negative amount AND upper boundry of decimal
    }
}
