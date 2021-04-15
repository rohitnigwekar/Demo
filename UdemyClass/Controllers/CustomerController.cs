using UdemyClass.Models;
using UdemyClass.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace UdemyClass.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext _Context;

        public CustomerController()
        {
            _Context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _Context.Dispose();
        }
        // GET: Customer
        public ActionResult Index()
        {
            var customers = _Context.Customers.Include(c => c.MembershipType).ToList();
            var ViewModel = new ListOfCustomer
            {
                Customers = customers
            };
            return View(ViewModel);
        }
        public ActionResult Details(int Id)
        {
            var customers = _Context.Customers.SingleOrDefault(c => c.Id == Id);
            return View(customers);
        }
        public ActionResult CustomerForm(CustomerFormViewModel viewModel)
        {
            if (viewModel.Customer == null)
            {
                var membershipTypes = _Context.MembershipTypes.ToList();
                viewModel = new CustomerFormViewModel
                {
                    MembershipTypes = membershipTypes
                };
            }

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("CustomerForm", "Customer", customer);
            }

            if (customer.Id == 0)
            {


                _Context.Customers.Add(customer);
                
            }
            else
            {
                var customersinDb = _Context.Customers.Single(c => c.Id == customer.Id);
                customersinDb.Name = customer.Name;
                customersinDb.BirthDate = customer.BirthDate;
                customersinDb.MembershipTypeId = customer.MembershipTypeId;
                customersinDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

            }
            _Context.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }
        
        public ActionResult Edit(int id)
        {
            var customers = _Context.Customers.SingleOrDefault(c => c.Id == id);
            if(customers == null)
            {
                return HttpNotFound();
            }

            var viewModel = new CustomerFormViewModel
            {
                Customer = customers,
                MembershipTypes = _Context.MembershipTypes.ToList()
            
            };

            return View("CustomerForm", viewModel);
        }
    }
}