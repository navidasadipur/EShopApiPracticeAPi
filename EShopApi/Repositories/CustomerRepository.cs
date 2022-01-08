using EShopApi.Contracts;
using EShopApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShopApi.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private EshopApi_DBContext _dbContext;
        private IMemoryCache _cashe;

        public CustomerRepository(EshopApi_DBContext dBContext, IMemoryCache cache)
        {
            _dbContext = dBContext;
            _cashe = cache;
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
            var casheCustomer = _cashe.Get<Customer>(id);
            if (casheCustomer != null)
            {
                return casheCustomer;
            }

           var customer = await _dbContext.Customer.Include(c => c.Orders).SingleOrDefaultAsync(c => c.CustomerId == id);

           var casheOption = new MemoryCacheEntryOptions()
               .SetSlidingExpiration(TimeSpan.FromSeconds(60));
           _cashe.Set(customer.CustomerId, customer, casheOption);

            return customer;
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
