using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.Controllers
{
    public class ConversionAuditController : Controller
    {
        // GET: /<controller>/
        public IActionResult ConversionAudit()
        {
            return View();
        }
    }
}
