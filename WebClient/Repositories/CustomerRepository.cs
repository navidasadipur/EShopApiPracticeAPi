using EShopApi.Contracts;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebClient.Models;

namespace WebClient.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private string apiUrl = "http://localhost:5247/api/customers";
        private HttpClient _client;
        private IHttpContextAccessor _contextAccessor;

        public CustomerRepository(IHttpContextAccessor contextAccessor)
        {
            _client = new HttpClient();
            _contextAccessor = contextAccessor;
        }

        public List<Customer> GetAllCustomer()
        {
            AddTokenTo_client();

            var result = _client.GetStringAsync(apiUrl).Result;

            List<Customer> list = JsonConvert.DeserializeObject<List<Customer>>(result);

            return list;
        }

        public Customer GetCutomerById(int customerId)
        {
            AddTokenTo_client();

            var result = _client.GetStringAsync(apiUrl + "/" + customerId).Result;

            Customer customer = JsonConvert.DeserializeObject<Customer>(result);

            return customer;
        }

        public void AddCustomer(Customer customer)
        {
            AddTokenTo_client();

            string jsonCustomer = JsonConvert.SerializeObject(customer);

            StringContent content = new StringContent(jsonCustomer, Encoding.UTF8, "application/json");

            var res = _client.PostAsync(apiUrl, content).Result;
        }

        public void UpdateCustomer(Customer customer)
        {
            AddTokenTo_client();

            string jsonCustomer = JsonConvert.SerializeObject(customer);

            StringContent content = new StringContent(jsonCustomer, Encoding.UTF8, "application/json");

            var res = _client.PutAsync(apiUrl + "/" + customer.CustomerId, content).Result;
        }

        public void DeleteCustomer(int customerId)
        {
            AddTokenTo_client();

            var res = _client.DeleteAsync(apiUrl + "/" + customerId).Result;
        }

        private void AddTokenTo_client()
        {
            var token = _contextAccessor.HttpContext.User.FindFirst("AccessToken").Value;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }


}
