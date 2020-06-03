using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CurrencyConverter.Models.CurrencyConversion;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CurrencyConverter.ViewModels.CurrencyConversion
{
    public class CurrencyConversionViewModel
    {
        public string PageTitle = "Currency Converter";
        public List<Currency> Currencies = CreateCurrencies();
        public IEnumerable<SelectListItem> FromCurrencies { get; set; }
        public int SelectedFromCurrencyId { get; set; }
        public IEnumerable<SelectListItem> ToCurrencies { get; set; }
        public int SelectedToCurrencyId { get; set; }
        public decimal ConvertedResult { get; set; }

        [Required]
        [Display(Name = "Amount To Convert")]
        public decimal AmountToConvert { get; set; }

        public CurrencyConversionViewModel()
        {
            IEnumerable<SelectListItem> selectList = GetSelectListItems(Currencies);
            FromCurrencies = selectList;
            ToCurrencies = selectList;
        }

        public static List<Currency> CreateCurrencies()
        {
            Currency gbp = new Currency(1, "GBP", "\u00A3");
            Currency usd = new Currency(2, "USD", "\u0024");
            Currency aud = new Currency(3, "AUD", "A\u0024");
            Currency eur = new Currency(4, "EUR", "\u20AC");
            ExchangeRate gbpToUsd = new ExchangeRate(usd, 1.24697m);
            ExchangeRate gbpToAud = new ExchangeRate(aud, 1.83744m);
            ExchangeRate gbpToEur = new ExchangeRate(eur, 1.12344m);
            gbp.ExchangeRates.Add(gbpToUsd);
            gbp.ExchangeRates.Add(gbpToAud);
            gbp.ExchangeRates.Add(gbpToEur);

            List<Currency> currencies = new List<Currency>() { gbp, usd, aud, eur };

            return currencies;
        }

        public IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<Currency> currencies)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();

            foreach (Currency currency in currencies)
            {
                selectList.Add(new SelectListItem
                {
                    Value = currency.Id.ToString(),
                    Text = currency.Name
                });
            }

            return selectList;
        }
    }
}
