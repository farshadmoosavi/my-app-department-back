using System;
using System.ComponentModel.DataAnnotations;

namespace accounting.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        public string? CustomerType { get; set; } // legal, real persons
        [Required]
        public int OrganizationId { get; set; }
        [Required]
        public string? FullName { get; set; }

        public string? Nationality { get; set; }
        public string? FatherName { get; set; }
        public string? IdentityCardType { get; set; }
        public string? issuePlace { get; set; }
        public int IdentityCode { get; set; }
        public DateTime CardExpiration { get; set; }
        public DateTime Birthday { get; set; }
        public int TaxValueNumber { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public int PostalCode { get; set; }
        public bool Limting { get; set; }
        public int AllowedAmountCurrency { get; set; }
        public string? grouping { get; set; }
        public string? Description { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Phone]
        public string? MobilePhone { get; set; }
        [Phone]
        public string? LandingPhone { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime LastUpdate { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int AccountNumber { get; set; }
        
    }
}


