using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyVidly.Models;
using MyVidly.ViewModels;

namespace MyVidly.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Index()
        {
            var customers = new List<Customer>
            {
                new Customer { Name = "John Smith" , Id = 1},
                new Customer { Name = "Mary Williams", Id = 2 }
            };

            var viewModel = new CustomerFormViewModel
            {
                Customers = customers
            };

            return View(viewModel);
        }
    }
}