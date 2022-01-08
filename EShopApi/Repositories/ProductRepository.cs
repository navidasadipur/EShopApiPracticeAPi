using EShopApi.Contracts;
using EShopApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShopApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        EshopApi_DBContext _dbContext;

        public ProductRepository(EshopApi_DBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<Products> Add(Products customer)
        {
            await _dbContext.Products.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<int> CountProducts()
        {
            return await _dbContext.Products.CountAsync();
        }

        public async Task<Products> Find(int id)
        {
            return await _dbContext.Products.SingleOrDefaultAsync(c => c.ProductId == id);
        }

        public async Task<IEnumerable<Products>> GetAll()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<bool> IsExist(int id)
        {
            return await _dbContext.Products.AnyAsync(c => c.ProductId == id);
        }

        public Task<Products> products(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Products> Remove(int id)
        {
            var customer = await _dbContext.Products.SingleAsync(a => a.ProductId == id);
            _dbContext.Products.Remove(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<Products> Update(Products customer)
        {
            _dbContext.Update(customer);
            await _dbContext.SaveChangesAsync();

            return customer;
        }
    }
}
