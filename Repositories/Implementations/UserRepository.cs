using System;
using System.Security.Cryptography;
using System.Text;
using Line.Data;
using Line.Models;
using Line.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Line.Repositories.Implementations
{
	public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly LineDbContext _dbContext;

        public UserRepository(LineDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> SignIn(string userName, string password)
        {
            try
            {
                // Find the user by the provided username
                var user = await _dbContext.Users
                    .FirstOrDefaultAsync(u => u.Username == userName);

                if (user != null)
                {
                    string hashedPassword = HashPassword(password);
                    // Validate the password (implement proper password hashing)
                    if (user.Password == hashedPassword && user.Deleted == false)
                    {
                        return user; // User signed in successfully
                    }
                }

                return null; // Sign-in failed
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the sign-in process
                // Log the error and return null or throw an exception depending on your requirements
                Console.WriteLine($"Error occurred during sign-in: {ex.Message}");
                return null;
            }
        }

        public async Task<User> SignUp(string fullname, string userName, string password, string mobilePhone, string email)
        {
            try
            {
                // check if any user with given username and password already exists
                //var existingUser = await _dbContext.Users.FindAsync(userName);
                var existingUser = await _dbContext.Users
                    .FirstOrDefaultAsync(u => u.FullName == fullname && u.Username == userName);

                if (existingUser != null)
                {
                    return null; // user already exists                        
                }

                if (string.IsNullOrWhiteSpace(password))
                {
                    return null; // Empty or whitespace password is not allowed
                }

                // Hash the password using SHA256 algorithm
                string hashedPassword = HashPassword(password);

                // Create a new user with the provided username and hashed password
                var user = new User
                {
                    Username = userName,
                    Password = hashedPassword,
                    FullName = fullname,
                    CreatedAt = DateTime.UtcNow,
                    MobilePhone = mobilePhone,
                    Email = email,
                    IsActive = true,
                    Deleted = false
                };

                // Add the new user to the database
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();

                return user; // User signed up successfully

            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the sign-in process
                // Log the error and return null or throw an exception depending on your requirements
                Console.WriteLine($"Error occurred during sign-up: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return null;
            }

        }

        public async Task DeleteByUpdate(int userId)
        {
            // Retrieve the user from the database
            var user = await _dbContext.Users.FindAsync(userId);

            if (user != null)
            {
                // Set the Deleted property to true instead of physically deleting
                user.Deleted = true;

                // Mark the entity as modified
                _dbContext.Entry(user).State = EntityState.Modified;

                // Save changes
                await SaveChangesAsync();
            }
            // Handle the case where the user with the given ID is not found (optional)
            else
            {
                // You can throw an exception, return a specific response, or handle it based on your requirements.
                // For simplicity, this example returns a NotFound response.
                Console.WriteLine($"User with ID {userId} not found.");
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
    }
}

