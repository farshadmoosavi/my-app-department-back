using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Line.Models;
using Line.Repositories;
using Line.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Line.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                var user = await _userRepository.GetById(id);
                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the user: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAll();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            try
            {
                var existingUser = await _userRepository.GetById(id);
                if (existingUser == null)
                    return NotFound();

                existingUser.FullName = user.FullName;
                existingUser.Username = user.Username;
                existingUser.Email = user.Email;
                existingUser.LastUpdate = DateTime.UtcNow;
                existingUser.IsActive = user.IsActive;
                if (user.Password != null || user.Password != "string")
                {
                    string hashedPassword = HashPassword(user.Password);
                    existingUser.Password = hashedPassword;
                }

                await _userRepository.Update(existingUser);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the user: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserByUpdate(int id)
        {
            try
            {
                await _userRepository.DeleteByUpdate(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while soft deleting the user: {ex.Message}");
            }
        }


        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequestModel model)
        {
            try
            {
                if (!IsUsernameValid(model.UserName))
                {
                    return BadRequest("Invalid username. Usernames can only contain letters, numbers, and underscores.");
                }
                var user = await _userRepository.SignUp(model.Fullname, model.UserName, model.Password, model.MobilePhone, model.Email);
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

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequestModel model)
        {
            try
            {
                var user = await _userRepository.SignIn(model.UserName, model.Password);
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

        private string HashPassword(string password)
        {
            // Create a hash value from the password
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        private bool IsUsernameValid(string username)
        {
            // Define a regular expression pattern for allowed characters
            string pattern = @"^[a-zA-Z0-9_]+$"; // Allow letters, numbers, and underscores

            // Use Regex.IsMatch to check if the username matches the pattern
            return Regex.IsMatch(username, pattern);
        }
    }
}

