using EShopApi.Contracts;
using EShopApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShopApi.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        EshopApi_DBContext _dbContext;

        public CustomerRepository(EshopApi_DBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<Customer> Add(Customer customer)
        {
            await _dbContext.Customer.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<int> CountCustomer()
        {
            return await _dbContext.Customer.CountAsync();
        }

        public async Task<Customer> Find(int id)
        {
            return await _dbContext.Customer.Include(c => c.Orders).SingleOrDefaultAsync(c => c.CustomerId == id);
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _dbContext.Customer.ToListAsync();
        }

        public async Task<bool> IsExist(int id)
        {
            return await _dbContext.Customer.AnyAsync(c => c.CustomerId == id);
        }

        public async Task<Customer> Remove(int id)
        {
            var customer = await _dbContext.Customer.SingleAsync(a => a.CustomerId == id);
            _dbContext.Customer.Remove(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> Update(Customer customer)
        {
            _dbContext.Update(customer);
            await _dbContext.SaveChangesAsync();

            return customer;
        }
    }
}
