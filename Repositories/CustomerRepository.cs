using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using accounting.Data;
using accounting.Models;
using accounting.Repository;
using Microsoft.EntityFrameworkCore;

namespace accounting.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AccountingDbContext _dbContext;

        public CustomerRepository(AccountingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer> GetById(int id)
        {
            var customer = await _dbContext.Customers.FindAsync(id);
            if (customer == null)
            {
                throw new Exception("There is no customer with that id.");
            }

            return customer;
        }

        public async Task<Customer> Create(Customer customer)
        {
            if (await IsDuplicated(customer.FullName))
            {
                throw new Exception("Duplicate data is not allowed!");
            }

            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task Update(Customer customer)
        {
            _dbContext.Entry(customer).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var customerToDelete = await _dbContext.Customers.FindAsync(id);
            if (customerToDelete != null)
            {
                _dbContext.Customers.Remove(customerToDelete);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("There is no customer with that id.");
            }
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _dbContext.Customers.ToListAsync();
        }

        public async Task<bool> IsDuplicated(string fullName)
        {
            return await _dbContext.Customers.AnyAsync(c => c.FullName == fullName);
        }
    }
}









//using System;
//using accounting.Data;
//using accounting.Models;
//using accounting.Repository;
//using Microsoft.EntityFrameworkCore;

//namespace accounting.Repositories
//{
//    public class CustomerRepository : ICustomerRepository
//    {
//        private readonly AccountingDbContext _dbContext;

//        public CustomerRepository(AccountingDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public async Task<Customer> GetById(int id)
//        {
//            var CustomerToGet = await _dbContext.Customers.FirstOrDefaultAsync(x => x.CustomerId == id);
//            if (CustomerToGet == null)
//            {
//                throw new Exception("There is not any customer with that id");
//            }
//            else
//                return CustomerToGet;
//            //return await _context.Cusomers.FindAsync(id);
//        }

//        public async Task<Customer> Create(Customer customer)
//        {
//            if (await IsDuplicated(customer))
//            {
//                throw new Exception("duplicate data is not allowed!");
//            }
//            else
//            {
//                _dbContext.Customers.Add(customer);
//                await _dbContext.SaveChangesAsync();
//                return customer;
//            }
//        }

//        public async Task Update(Customer customer)
//        {
//            _dbContext.Entry(customer).State = EntityState.Modified;
//            await _dbContext.SaveChangesAsync();
//        }

//        public async Task Delete(int id)
//        {
//            var ContactToDelete = await _dbContext.Customers.FindAsync(id);
//            if (ContactToDelete != null)
//            {
//                _dbContext.Customers.Remove(ContactToDelete);
//                await _dbContext.SaveChangesAsync();
//            }
//            else
//                throw new Exception("There is not any contect with that id");
//        }

//        public async Task<IEnumerable<Customer>> GetAll()
//        {
//            if (_dbContext.Customers.Any())
//            {
//                return await _dbContext.Customers.ToListAsync();
//            }
//            else
//            {
//                throw new Exception("unfortunately database is empty!");
//            }

//        }

//        public async Task<bool> IsDuplicated(Customer customer)
//        {
//            var query1 = await _dbContext.Customers.Where(p => p.FullName == customer.FullName).ToListAsync();
//            if (query1.Count() == 0) 
//                return false;
//            else
//                return true;
//        }
//    }

//}

