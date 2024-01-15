using System;
namespace Line.Models
{
	public class SignUpRequestModel
	{
        public string? Fullname { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? MobilePhone { get; set; }
        public string? Email { get; set; }
    }
}

