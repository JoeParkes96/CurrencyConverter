using System;
using System.Collections.Generic;

namespace CurrencyConverter.Models
{
    public class Currency
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public List<ExchangeRate> ExchangeRates { get; set; }

        public Currency(string name, string symbol)
        {
            Name = name;
            Symbol = symbol;
            ExchangeRates = new List<ExchangeRate>();
        }

        public decimal Convert(Currency currencyTo, decimal amount)
        {
            decimal exactConversion = FindExchangeRate(currencyTo).Rate * amount;
            return Math.Round(exactConversion, 2);
        }

        public ExchangeRate FindExchangeRate(Currency currencyTo)
        {
            foreach (ExchangeRate rate in ExchangeRates)
            {
                if (rate.CurrencyTo.Equals(currencyTo))
                    return rate;
            }

            throw new Exception("Invalid currency " + currencyTo);
        }

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !(GetType().Equals(obj.GetType())))
                return false;
            else
            {
                Currency c = (Currency)obj;
                return (Name == c.Name) && (Symbol == c.Symbol);
            }
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Symbol.GetHashCode();
        }
    }
}
