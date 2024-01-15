using System;
using System.Text;
using Line.ModelView;
using Line.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Line.Models;
using Line.Repositories.Implementations;
using System.Text.RegularExpressions;

namespace Line.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;

        public AdminController(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequestModel model)
        {
            try
            {
                var admin = await _adminRepository.SignIn(model.UserName, model.Password);
                if (admin != null)
                {
                    //return Ok(user);
                    return Ok(new { success = true, admin }); // Return a response with success and user object
                }
                else
                {
                    return BadRequest("backend Invalid username or password");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred during sign-in: {ex.Message}");
                return StatusCode(500, "An error occurred during sign-in");
            }
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequestModelAdmin model)
        {
            try
            {
                if (!IsUsernameValid(model.UserName))
                {
                    return BadRequest("Invalid username. Usernames can only contain letters, numbers, and underscores.");
                }
                var user = await _adminRepository.SignUp(model.Fullname, model.UserName, model.Password);
                if (user != null)
                {
                    //return Ok(user);
                    return Ok(new { success = true, user }); // Return a response with success and user object
                }
                else
                {
                    return BadRequest("backend Invalid username or password");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred during sign-in: {ex.Message}");
                return StatusCode(500, "An error occurred during sign-in");
            }
        }

        private bool IsUsernameValid(string username)
        {
            // Define a regular expression pattern for allowed characters
            string pattern = @"^[a-zA-Z0-9_]+$"; // Allow letters, numbers, and underscores

            // Use Regex.IsMatch to check if the username matches the pattern
            return Regex.IsMatch(username, pattern);
        }


        private string HashPassword(string password)
        {
            // Create a hash value from the password
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
