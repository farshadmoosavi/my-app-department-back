using System;
using System.ComponentModel.DataAnnotations;

namespace accounting.Models
{
	public class Currency
	{
        [Key]
        public int CurrencyId { get; set; }
        [Required]
        public int CurrencyName { get; set; }
        [Required]
        public int CurrencyAbbreviation { get; set; }
    }
}

