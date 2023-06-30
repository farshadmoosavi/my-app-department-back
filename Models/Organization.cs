using System;
using System.ComponentModel.DataAnnotations;

namespace accounting.Models
{
	public class Organization // Organization which you sell the software
	{
        [Key]
        public int OrganizationId { get; set; }
        [Required]
        public string? Name { get; set; }
        
        public string? ContactInfo { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? IndustryField { get; set; }
        [Required]
        public DateTime RegistrationDate { get; set; }
        [Required]
        public DateTime LastUpdate { get; set; }
        [Required]
        public bool IsActive { get; set; }

    }
}

