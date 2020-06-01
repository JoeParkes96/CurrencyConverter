using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
