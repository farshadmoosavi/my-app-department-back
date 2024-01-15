using System;
using System.ComponentModel.DataAnnotations;

namespace Line.Models
{
	public class User //User of the web site
	{
        [Key]
        public int UserId { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public bool Deleted { get; set; }

        public string? FullName { get; set; }
     
        public string? Email { get; set; }
  
        public DateTime? CreatedAt { get; set; }
        
        public DateTime? LastUpdate { get; set; }
   
        public bool? IsActive { get; set; }

        public string? PasswordHash { get; set; }

        public string? Nationality { get; set; }

        public string? FatherName { get; set; }

        public string? issuePlace { get; set; }

        public string? IdentityCode { get; set; }

        public DateTime? Birthday { get; set; }

        public string? Country { get; set; }

        public string? City { get; set; }

        public string? Address { get; set; }

        public string? PostalCode { get; set; }

        public string? Description { get; set; }

        public string? AccountNumber { get; set; }

        public string? MobilePhone { get; set; }

        public string? LandingPhone { get; set; }

        public virtual ICollection<Document>? Documents { get; set; } // every user has many Documents
    }
}

