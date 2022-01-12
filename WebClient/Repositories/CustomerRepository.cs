using EShopApi.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebClient.Models;

namespace WebClient.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private string apiUrl = "http://localhost:5247/api/customers";
        HttpClient _client;

        public CustomerRepository()
        {
            _client = new HttpClient();
        }

        public List<Customer> GetAllCustomer()
        {
            var result = _client.GetStringAsync(apiUrl).Result;

            List<Customer> list = JsonConvert.DeserializeObject<List<Customer>>(result);

            return list;
        }

        public Customer GetCutomerById(int customerId)
        {
            var result = _client.GetStringAsync(apiUrl + "/" + customerId).Result;

            Customer customer = JsonConvert.DeserializeObject<Customer>(result);

            return customer;
        }

        public void AddCustomer(Customer customer)
        {
            string jsonCustomer = JsonConvert.SerializeObject(customer);

            StringContent content = new StringContent(jsonCustomer, Encoding.UTF8, "application/json");

            var res = _client.PostAsync(apiUrl, content).Result;
        }

        public void UpdateCustomer(Customer customer)
        {
            string jsonCustomer = JsonConvert.SerializeObject(customer);

            StringContent content = new StringContent(jsonCustomer, Encoding.UTF8, "application/json");

            var res = _client.PutAsync(apiUrl + "/" + customer.CustomerId, content).Result;
        }

        public void DeleteCustomer(int customerId)
        {
            var res = _client.DeleteAsync(apiUrl + "/" + customerId).Result;
        }
    }


}
