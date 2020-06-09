using System;
using System.ComponentModel.DataAnnotations;

namespace CurrencyConverter.Models.ConversionAudit
{
    public class Conversion
    {
        public int Id { get; set; }

        [Display(Name = "From Currency Name")]
        public string FromCurrencyName { get; set; }

        [Display(Name = "To Currency Name")]
        public string ToCurrencyName { get; set; }

        [Display(Name = "Amount Converted")]
        public decimal AmountConverted { get; set; }

        [Display(Name = "Conversion Result")]
        public decimal ConversionResult { get; set; }

        [Display(Name = "Date Submitted")]
        [DataType(DataType.Date)]
        public DateTime DateSubmitted { get; set; }
    }
}
