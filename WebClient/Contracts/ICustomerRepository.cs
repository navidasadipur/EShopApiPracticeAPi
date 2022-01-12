using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Models;
using WebClient.Repositories;

namespace EShopApi.Contracts
{
    public interface ICustomerRepository
    {
        List<Customer> GetAllCustomer();
        Customer GetCutomerById(int customerId);
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        public void DeleteCustomer(int customerId);
    }
}
