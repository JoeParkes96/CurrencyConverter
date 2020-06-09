using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter.Models.ConversionAudit
{
    public class ConversionAuditContext : DbContext
    {
        public ConversionAuditContext(DbContextOptions<ConversionAuditContext> options)
             : base(options)
        {
        }

        public DbSet<Conversion> Conversion { get; set; }
    }
}
