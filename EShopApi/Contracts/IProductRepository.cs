using EShopApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShopApi.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Products>> GetAll();
        Task<Products> Add(Products product);
        Task<Products> Find(int id);
        Task<Products> Update(Products product);
        Task<Products> products(int id);
        Task<bool> IsExist(int id);
        Task<Products> Remove(int id);
    }
}
