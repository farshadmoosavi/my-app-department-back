using System;
using Line.Models;

namespace Line.Repositories.Interfaces
{
    public interface IAdminRepository : IGenericRepository<Admin>
    {
        Task<Admin> SignUp(string fullname, string userName, string password);
        Task<Admin> SignIn(string userName, string password);
    }
}

