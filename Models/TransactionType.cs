using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Line.Models
{
	public class TransactionType  // type is sell or buy
	{
        [Key]
        public int TransactionTypeId { get; set; }
        [Required]
        public string? TransactionTypeName { get; set; }

        public string? Description { get; set; }

        public virtual ICollection<Transaction>? Transactions { get; set; } // every Transactiontype has many Transactions
    }
}

