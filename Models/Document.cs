using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Line.Models
{
	public class Document
	{
        [Key]
        public int DocumentId { get; set; }
        [Required]
        public string? DocumentType { get; set; }
        [Required]
        public DateTime? DocumentDateTime { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Currency { get; set; }
        [Required]
        public bool Deleted { get; set; }

        public string? DocumentDescription { get; set; } 

        public int DebtorValue { get; set; } 

        public int CreditorValue { get; set; } 

        public int BalanceValue { get; set; }

        public int? UserId { get; set; }

        public virtual User? Users { get; set; }

    }
}

