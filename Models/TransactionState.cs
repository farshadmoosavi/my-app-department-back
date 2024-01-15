using System;
using System.ComponentModel.DataAnnotations;

namespace Line.Models
{
	public class TransactionState  // state is: Pending - Rejected - Confirmed - NeedToAlter
	{
        [Key]
        public int TransactionStateId { get; set; }
        [Required]
        public string? TransactionStateName { get; set; }

        public string? Description { get; set; }

        public virtual ICollection<Transaction>? Transactions { get; set; } // every Transactionstate has many Transactions

    }
}

