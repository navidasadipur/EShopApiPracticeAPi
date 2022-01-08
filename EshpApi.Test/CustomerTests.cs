using EShopApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace EshpApi.Test
{
    [TestClass]
    public class CustomerTests
    {
        private HttpClient _client;

        public CustomerTests()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [TestMethod]
        public void CustomerGetAllTest()
        {
            var request = new HttpRequestMessage(new HttpMethod("Get"), "/api/customers");

            var resoponse = _client.SendAsync(request).Result;

            Assert.AreEqual(HttpStatusCode.OK, resoponse.StatusCode);
        }

        [TestMethod]
        [DataRow(1)]
        public void CustomerGetOneTest(int id)
        {
            var request = new HttpRequestMessage(new HttpMethod("Get"), $"/api/Customers/{id}");

            var respons = _client.SendAsync(request).Result;

            Assert.AreEqual(HttpStatusCode.OK, respons.StatusCode);
        }

        [TestMethod]
        public void CustomerPostTest()
        {
            var request = new HttpRequestMessage(new HttpMethod("Post"), $"/api/customers");

            var response = _client.SendAsync(request).Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
