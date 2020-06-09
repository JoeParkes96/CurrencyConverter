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

        //Hardcoded method create currencies, TODO alter data structure to make more extendable with automation of adding exchange rates
        public static List<Currency> CreateCurrencies()
        {
            Currency gbp = new Currency(1, "GBP", "\u00A3");
            Currency usd = new Currency(2, "USD", "\u0024");
            Currency aud = new Currency(3, "AUD", "A\u0024");
            Currency eur = new Currency(4, "EUR", "\u20AC");

            ExchangeRate gbpToUsd = new ExchangeRate(usd, 1.25822m);
            ExchangeRate gbpToAud = new ExchangeRate(aud, 1.81688m);
            ExchangeRate gbpToEur = new ExchangeRate(eur, 1.11918m);
            gbp.ExchangeRates.Add(gbpToUsd);
            gbp.ExchangeRates.Add(gbpToAud);
            gbp.ExchangeRates.Add(gbpToEur);

            ExchangeRate usdToGbp = new ExchangeRate(gbp, 0.794722m);
            ExchangeRate usdToAud = new ExchangeRate(aud, 1.44371m);
            ExchangeRate usdToEur = new ExchangeRate(eur, 0.889094m);
            usd.ExchangeRates.Add(usdToGbp);
            usd.ExchangeRates.Add(usdToAud);
            usd.ExchangeRates.Add(usdToEur);

            ExchangeRate audToGbp = new ExchangeRate(gbp, 0.550393m);
            ExchangeRate audToUsd = new ExchangeRate(usd, 0.692937m);
            ExchangeRate audToEur = new ExchangeRate(eur, 0.616006m);
            aud.ExchangeRates.Add(audToGbp);
            aud.ExchangeRates.Add(audToUsd);
            aud.ExchangeRates.Add(audToEur);

            ExchangeRate eurToGbp = new ExchangeRate(gbp, 0.893509m);
            ExchangeRate eurToUsd = new ExchangeRate(usd, 1.12475m);
            ExchangeRate eurToAud = new ExchangeRate(aud, 1.62353m);
            eur.ExchangeRates.Add(eurToGbp);
            eur.ExchangeRates.Add(eurToUsd);
            eur.ExchangeRates.Add(eurToAud);

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
