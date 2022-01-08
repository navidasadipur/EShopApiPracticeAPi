using EShopApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShopApi.Contracts
{
    public interface ISalesPersonRepository
    {
        Task<IEnumerable<SalesPersons>> GetAll();
        Task<SalesPersons> Add(SalesPersons sales);
        Task<SalesPersons> Find(int id);
        Task<SalesPersons> Update(SalesPersons sales);
        Task<SalesPersons> Remove(int id);
        Task<bool> IsExists(int id);
    }
}
