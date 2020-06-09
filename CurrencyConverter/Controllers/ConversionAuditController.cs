using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CurrencyConverter.Models.ConversionAudit;

namespace CurrencyConverter.Controllers
{
    public class ConversionAuditController : Controller
    {
        private readonly ConversionAuditContext _context;

        public ConversionAuditController(ConversionAuditContext context)
        {
            _context = context;
        }

        // GET: ConversionAudit
        public async Task<IActionResult> ConversionAudit()
        {
            return View(await _context.Conversion.ToListAsync());
        }
    }
}
