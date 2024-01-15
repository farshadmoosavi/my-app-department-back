using System;
using System.ComponentModel.DataAnnotations;

namespace Line.Models
{
	public class Currency
	{
        [Key]
        public int CurrencyId { get; set; }
        [Required]
        public string? CurrencyName { get; set; }
        public string? CurrencyAbbreviation { get; set; }
        public virtual ICollection<SellBuyPrice>? SellBuyPrices { get; set; } // every currency relates woth many sellbuyprices
    }
}

