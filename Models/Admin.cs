using System;
using System.ComponentModel.DataAnnotations;

namespace Line.Models
{
    public class Admin //admin of the website
    {
        [Key]
        public int AdminId { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }

        public string? FullName { get; set; }

        public string? PasswordHash { get; set; }
    }
}

