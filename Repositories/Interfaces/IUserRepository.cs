using System;
using Line.Models;

namespace Line.Repositories.Interfaces
{
	public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> SignUp(string fullname, string userName, string password, string mobilePhone, string email);
        Task<User> SignIn(string userName, string password);
        Task DeleteByUpdate(int userId);
    }
}

