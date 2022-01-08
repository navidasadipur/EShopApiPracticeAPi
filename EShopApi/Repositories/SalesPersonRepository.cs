using EShopApi.Contracts;
using EShopApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShopApi.Repositories
{
    public class SalesPersonRepository : ISalesPersonRepository
    {
        EshopApi_DBContext _dbContext;

        public SalesPersonRepository(EshopApi_DBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<SalesPersons> Add(SalesPersons customer)
        {
            await _dbContext.SalesPersons.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<int> CountSalesPersons()
        {
            return await _dbContext.SalesPersons.CountAsync();
        }

        public async Task<SalesPersons> Find(int id)
        {
            return await _dbContext.SalesPersons.Include(c => c.Orders).SingleOrDefaultAsync(c => c.SalesPersonId == id);
        }

        public async Task<IEnumerable<SalesPersons>> GetAll()
        {
            return await _dbContext.SalesPersons.ToListAsync();
        }

        public async Task<bool> IsExist(int id)
        {
            return await _dbContext.SalesPersons.AnyAsync(c => c.SalesPersonId == id);
        }

        public Task<bool> IsExists(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<SalesPersons> Remove(int id)
        {
            var customer = await _dbContext.SalesPersons.SingleAsync(a => a.SalesPersonId == id);
            _dbContext.SalesPersons.Remove(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<SalesPersons> Update(SalesPersons customer)
        {
            _dbContext.Update(customer);
            await _dbContext.SaveChangesAsync();

            return customer;
        }
    }
}
