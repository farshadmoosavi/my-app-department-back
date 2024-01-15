using System;
using System.ComponentModel.DataAnnotations;

namespace Line.Models
{
	public class Invoice
	{
        [Key]
        public int InvoiceId { get; set; }
        [Required]
        public string? AccountName { get; set; }
        [Required]
        public int? OrganizationId { get; set; }

        public string? CurrencyAbbreviation { get; set; }
    }
}

