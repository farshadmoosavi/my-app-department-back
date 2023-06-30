using System;
using System.ComponentModel.DataAnnotations;

namespace accounting.Models
{
	public class Users //Users of an organization
	{
        [Key]
        public int UserId { get; set; }
        [Required]
        public int OrganizationId { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Role { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime LastUpdate { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}

