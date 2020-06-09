using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CurrencyConverter.Models;
using CurrencyConverter.Models.CurrencyConversion;
using CurrencyConverter.ViewModels.CurrencyConversion;
using System.Linq;
using System.Collections.Generic;
using System;
using CurrencyConverter.Models.ConversionAudit;

namespace CurrencyConverter.Controllers
{
    public class CurrencyConversionController : Controller
    {
        private readonly ConversionAuditContext _context;

        public CurrencyConversionController(ConversionAuditContext context)
        {
            _context = context;
        }

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
                ModelState.AddModelError("InvalidInput", "Invalid value to convert");
            }
            else if (resultModel.SelectedFromCurrencyId == resultModel.SelectedToCurrencyId)
            {
                ModelState.AddModelError("SameCurrencies", "Currencies to convert cannot be the same");
            }
            else
            {
                int selectedFromCurrencyId = resultModel.SelectedFromCurrencyId;
                int selectedToCurrencyId = resultModel.SelectedToCurrencyId;
                Currency fromCurrency = GetCurrencyFromId(resultModel.Currencies, selectedFromCurrencyId);
                Currency toCurrency = GetCurrencyFromId(resultModel.Currencies, selectedToCurrencyId);

                decimal conversionResult = Convert(fromCurrency, toCurrency, resultModel.AmountToConvert);

                resultModel.ConvertedResult = conversionResult;

                AddConversionToDatabase(fromCurrency, toCurrency, resultModel, conversionResult);
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

        public decimal Convert(Currency currencyFrom, Currency currencyTo, decimal amount)
        {
            if (amount < 0 || amount >= decimal.MaxValue)
            {
                throw new ArgumentOutOfRangeException("Invalid value to convert");
            }
            else
            {
                decimal exactConversion = currencyFrom.FindExchangeRate(currencyTo).Rate * amount;
                return Math.Round(exactConversion, 2);
            }
        }

        private void AddConversionToDatabase(Currency fromCurrency, Currency toCurrency, CurrencyConversionViewModel resultModel, decimal conversionResult)
        {
            Conversion conversion = new Conversion
            {
                FromCurrencyName = fromCurrency.Name,
                ToCurrencyName = toCurrency.Name,
                AmountConverted = resultModel.AmountToConvert,
                ConversionResult = conversionResult,
                DateSubmitted = DateTime.UtcNow.Date
            };

            _context.Add(conversion);
            _context.SaveChanges();
        }
    }
}
