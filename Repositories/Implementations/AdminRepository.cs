using System;
using Line.Data;
using Line.Models;
using Line.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Line.Repositories.Implementations
{
	public class AdminRepository: GenericRepository<Admin>, IAdminRepository
    {
        private readonly LineDbContext _dbContext;

        public AdminRepository(LineDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Admin> SignIn(string userName, string password)
        {
            try
            {
                // Find the user by the provided username
                var admin = await _dbContext.Admins
                    .FirstOrDefaultAsync(u => u.Username == userName);

                if (admin != null)
                {
                    string hashedPassword = HashPassword(password);
                    // Validate the password (implement proper password hashing)
                    if (admin.Password == hashedPassword)
                    {
                        return admin; // User signed in successfully
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

        public async Task<Admin> SignUp(string fullname, string userName, string password)
        {
            try
            {
                // check if any user with given username and password already exists
                //var existingUser = await _dbContext.Users.FindAsync(userName);
                var existingAdmin = await _dbContext.Admins
                    .FirstOrDefaultAsync(u => u.FullName == fullname && u.Username == userName);
    
                if (existingAdmin != null)
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
                var admin = new Admin
                {
                    Username = userName,
                    Password = hashedPassword,
                    FullName = fullname,
                };

                // Add the new user to the database
                _dbContext.Admins.Add(admin);
                await _dbContext.SaveChangesAsync();

                return admin; // User signed up successfully

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

