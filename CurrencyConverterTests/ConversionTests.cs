using System;
using CurrencyConverter.Models.CurrencyConversion;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CurrencyConverterTests
{
    [TestClass]
    public class ConversionTests
    {
        private readonly Currency gbp;
        private readonly Currency aud;
        private readonly Currency usd;
        private readonly Currency eur;

        public ConversionTests()
        {
            gbp = new Currency(1, "GBP", "\u00A3");
            usd = new Currency(2, "USD", "\u0024");
            aud = new Currency(3, "AUD", "A\u0024");
            eur = new Currency(4, "EUR", "\u20AC");
            ExchangeRate gbpToUsd = new ExchangeRate(usd, 1.24697m);
            ExchangeRate gbpToAud = new ExchangeRate(aud, 1.83744m);
            ExchangeRate gbpToEur = new ExchangeRate(eur, 1.12344m);
            gbp.ExchangeRates.Add(gbpToUsd);
            gbp.ExchangeRates.Add(gbpToAud);
            gbp.ExchangeRates.Add(gbpToEur);
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

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void NegativeEdgeCaseThrowsArgumentOutOfRangeException()
        {
            gbp.Convert(usd, -10);

            // Assert - Expect ArgumentOutOfRange Exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MaxDecimalValueCaseThrowsArgumentOutOfRangeException()
        {
            gbp.Convert(usd, decimal.MaxValue);

            // Assert - Expect ArgumentOutOfRange Exception
        }
    }
}
