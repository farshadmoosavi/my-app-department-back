using System;
using System.ComponentModel.DataAnnotations;

namespace Line.ModelView
{
    public class CreateDocumentDto
    {
        
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public string? DocumentType { get; set; }

        [Required]
        public DateTime DocumentDateTime { get; set; }

        [Required]
        public string? UserName { get; set; }

        public string? DocumentDescription { get; set; }

        public string? ConfidentialDocumentDescription { get; set; }

        [Required]
        public string? Currency { get; set; }

        public int DebtorValue { get; set; }

        public int CreditorValue { get; set; }

        public int BalanceValue { get; set; }
    }
}

