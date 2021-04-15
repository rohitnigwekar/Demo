using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UdemyClass.Models;

namespace UdemyClass.Controllers
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _Context;




        public CustomersController()
        {
            _Context = new ApplicationDbContext();
        }
        // GET: api/Customers
        public IEnumerable<Customer> Get()
        {
            var customer = _Context.Customers.ToList();
           
            return customer;
        }

        // GET: api/Customers/5
        public Customer Get(int id)
        {
            var customer = _Context.Customers.SingleOrDefault( m => m.Id == id);

            return customer;
        }

        // POST: api/Customers
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Customers/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Customers/5
        public void Delete(int id)
        {
            var customer = _Context.Customers.SingleOrDefault(m => m.Id == id);
            _Context.Customers.Remove(customer);
            _Context.SaveChanges();
        }
    }
}
