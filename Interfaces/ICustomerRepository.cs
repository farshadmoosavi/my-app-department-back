using System;
using accounting.Models;

namespace accounting.Repository
{
	public interface ICustomerRepository
	{
            Task<Customer> GetById(int id);
            Task<Customer> Create(Customer customer);
            Task Update(Customer customer);
            Task Delete(int id);
            Task<IEnumerable<Customer>> GetAll();
    }
}

