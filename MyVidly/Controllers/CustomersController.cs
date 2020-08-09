using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyVidly.Models;
using MyVidly.ViewModels;

namespace MyVidly.Controllers
{
    public class CustomersController : Controller
    {
        private VidlyContext _vidlyDBContext;

        public CustomersController()
        {
            _vidlyDBContext = new VidlyContext();
        }

        protected override void Dispose(bool disposing)
        {
            _vidlyDBContext.Dispose();
        }

        // GET: Customers
        public ActionResult Index()
        {
            var customers = _vidlyDBContext.Customer.Include(c => c.MembershipType).ToList();

            return View(customers);
        }
        public ActionResult Details(int id)
        {
            var customer = _vidlyDBContext.Customer.SingleOrDefault(c => c.Id == id);
            
            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }
        
        //private IEnumerable<Customer> GetCustomers()
        //{
        //    return new List<Customer>
        //    {
        //        new Customer { Name = "John Smith" , Id = 1},
        //        new Customer { Name = "Mary Williams", Id = 2 }
        //    };

        //}
    }
}