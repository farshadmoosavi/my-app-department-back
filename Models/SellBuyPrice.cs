using System;
using System.ComponentModel.DataAnnotations;


namespace Line.Models
{
	public class SellBuyPrice
	{
            [Key]
            public int SellBuyPriceId { get; set; }

            [Required]
            public bool Deleted { get; set; }

            public DateTime? LastUpdate { get; set; }

            public int SellPrice { get; set; }

            public int BuyPrice { get; set; }

            public int currencyId { get; set; }

            public virtual Currency? currencies { get; set; }
    }
}

