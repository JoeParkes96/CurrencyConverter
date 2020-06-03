using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CurrencyConverter.Models;
using CurrencyConverter.Models.CurrencyConversion;
using Microsoft.AspNetCore.Mvc.Rendering;
using CurrencyConverter.ViewModels.CurrencyConversion;
using System.Linq;

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
            int selectedFromCurrencyId = resultModel.SelectedFromCurrencyId;
            int selectedToCurrencyId = resultModel.SelectedToCurrencyId;
            resultModel.FromCurrencies = new SelectList(resultModel.FromCurrencies, "Value", "Text", selectedFromCurrencyId);
            resultModel.ToCurrencies = new SelectList(resultModel.ToCurrencies, "Value", "Text", selectedToCurrencyId);

            Currency fromCurrency = resultModel.Currencies.Where(currency => currency.Id == selectedFromCurrencyId).SingleOrDefault();
            Currency toCurrency = resultModel.Currencies.Where(currency => currency.Id == selectedToCurrencyId).SingleOrDefault();

            decimal conversionResult = fromCurrency.Convert(toCurrency, resultModel.AmountToConvert);

            resultModel.ConvertedResult = conversionResult;

            return View(resultModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
