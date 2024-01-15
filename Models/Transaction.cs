using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Line.Models
{
	public class Transaction
	{
        [Key]
        public int TransactionId { get; set; }
        [Required]
        public string? CurrencyName { get; set; }
        [Required]
        public string? CurrencyAbbreviation { get; set; }
        [Required]
        public bool Deleted { get; set; }
        [Required]
        public int TransactionAmount { get; set; }

        public string? Description { get; set; }

        public int TransactionTypeId { get; set; } // every transaction has only one transactiontype

        public virtual TransactionType? TransactionTypes { get; set; }

        public int TransactionStateId { get; set; } // every transaction has only one transactionstate

        public virtual TransactionState? TransactionStates { get; set; }
    }
}

