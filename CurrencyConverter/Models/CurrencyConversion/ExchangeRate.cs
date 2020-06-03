namespace CurrencyConverter.Models.CurrencyConversion
{
    public class ExchangeRate
    {
        public Currency CurrencyTo { get; set; }
        public decimal Rate { get; set; }

        public ExchangeRate(Currency currencyTo, decimal rate)
        {
            CurrencyTo = currencyTo;
            Rate = rate;
        }
    }
}
