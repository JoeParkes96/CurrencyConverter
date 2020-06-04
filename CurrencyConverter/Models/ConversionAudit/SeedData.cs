using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace CurrencyConverter.Models.ConversionAudit
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ConversionAuditContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ConversionAuditContext>>()))
            {
                // Look for any conversions.
                if (context.Conversion.Any())
                {
                    return;   // DB has been seeded
                }

                // Adding dummy entries to enable useful date querying
                context.Conversion.AddRange(
                    new Conversion
                    {
                        FromCurrencyName = "GBP",
                        ToCurrencyName = "EUR",
                        AmountConverted = 12345m,
                        ConversionResult = 13816.28m,
                        DateSubmitted = new DateTime(2019, 07, 24)
                    },
                    new Conversion
                    {
                        FromCurrencyName = "AUD",
                        ToCurrencyName = "USD",
                        AmountConverted = 250.75m,
                        ConversionResult = 173.75m,
                        DateSubmitted = new DateTime(2020, 01, 12)
                    },
                    new Conversion
                    {
                        FromCurrencyName = "EUR",
                        ToCurrencyName = "GBP",
                        AmountConverted = 95.23m,
                        ConversionResult = 85.09m,
                        DateSubmitted = new DateTime(2020, 06, 04)
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
