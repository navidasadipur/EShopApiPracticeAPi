using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Models;

namespace WebClient.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private CustomerRepository _customerRepository;

        public HomeController(ILogger<HomeController> logger, CustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        public IActionResult Index()
        {
            return View(_customerRepository.GetAllCustomer());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            _customerRepository.AddCustomer(customer);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var customer = _customerRepository.GetCutomerById(id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            _customerRepository.UpdateCustomer(customer);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _customerRepository.DeleteCustomer(id);
            return RedirectToAction("Index");
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
