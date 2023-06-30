using System;
using accounting.Models;

namespace accounting.Repository
{
	public interface ICustomerRepository
	{
            Customer GetById(int id);
            void Add(Customer customer);
            void Update(Customer customer);
            void Delete(Customer customer);
        
    }
}

