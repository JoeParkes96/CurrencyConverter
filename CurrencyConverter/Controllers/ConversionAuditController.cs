using System.Linq;
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

        // GET: ConversionAudit/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversion = await _context.Conversion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conversion == null)
            {
                return NotFound();
            }

            return View(conversion);
        }

        // GET: ConversionAudit/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ConversionAudit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FromCurrencyName,ToCurrencyName,AmountConverted,ConversionResult,DateSubmitted")] Conversion conversion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conversion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ConversionAudit));
            }
            return View(conversion);
        }
    }
}
