using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CurrencyConverter.Models;
using CurrencyConverter.Models.CurrencyConversion;
using Microsoft.AspNetCore.Mvc.Rendering;
using CurrencyConverter.ViewModels.CurrencyConversion;
using System.Linq;
using System.Collections.Generic;
using System;

namespace CurrencyConverter.Controllers
{
    public class CurrencyConversionController : Controller
    {
        public IActionResult Index()
        {
            CurrencyConversionViewModel viewModel = new CurrencyConversionViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(CurrencyConversionViewModel resultModel)
        {
            if (resultModel.AmountToConvert < 0 || resultModel.AmountToConvert >= decimal.MaxValue)
            {
                throw new ArgumentOutOfRangeException("Invalid value to convert");
            }
            else if (resultModel.SelectedFromCurrencyId == resultModel.SelectedToCurrencyId)
            {
                throw new Exception("Currencies to convert cannot be the same");
            }
            else
            {
                int selectedFromCurrencyId = resultModel.SelectedFromCurrencyId;
                int selectedToCurrencyId = resultModel.SelectedToCurrencyId;
                resultModel.FromCurrencies = CreateSelectList(resultModel.FromCurrencies, selectedFromCurrencyId);
                resultModel.ToCurrencies = CreateSelectList(resultModel.ToCurrencies, selectedToCurrencyId);

                Currency fromCurrency = GetCurrencyFromId(resultModel.Currencies, selectedFromCurrencyId);
                Currency toCurrency = GetCurrencyFromId(resultModel.Currencies, selectedToCurrencyId);

                decimal conversionResult = fromCurrency.Convert(toCurrency, resultModel.AmountToConvert);

                resultModel.ConvertedResult = conversionResult;
            }

            return View(resultModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private Currency GetCurrencyFromId(List<Currency> currencies, int currencyId)
        {
            return currencies.Where(currency => currency.Id == currencyId).SingleOrDefault();
        }

        private SelectList CreateSelectList(IEnumerable<SelectListItem> currencyListItems, int selectedCurrencyId)
        {
            return new SelectList(currencyListItems, "Value", "Text", selectedCurrencyId);
        }
    }
}
