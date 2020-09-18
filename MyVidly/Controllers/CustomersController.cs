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
            // Commeting below code as it's directly fetched from Web API using AJAX calls
            //var customers = _vidlyDBContext.Customer.Include(c => c.MembershipType).ToList();
            //return View(customers);

            return View();
        }
        public ActionResult Edit(int id)
        {
            var customer = _vidlyDBContext.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            //var viewModel = new NewCustomerViewModel
            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _vidlyDBContext.MembershipType.ToList()
            };

            return View("CustomerForm", viewModel);
        }

        public ActionResult New()
        {
            var membershipTypes = _vidlyDBContext.MembershipType.ToList();

            //var viewModel = new NewCustomerViewModel
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _vidlyDBContext.MembershipType.ToList()
                };

                return View("CustomerForm", viewModel);
            }
            if(customer.Id == 0)
            {
                _vidlyDBContext.Customers.Add(customer);

            }
            else
            {
                var customerInDB = _vidlyDBContext.Customers.Single(c => c.Id == customer.Id);

                customerInDB.Name = customer.Name;
                customerInDB.Birthdate = customer.Birthdate;
                customerInDB.MembershipTypeId = customer.MembershipTypeId;
                customerInDB.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

                //TryUpdateModel(customerInDB);
            }

            _vidlyDBContext.SaveChanges();

            return RedirectToAction("Index", "Customers");
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