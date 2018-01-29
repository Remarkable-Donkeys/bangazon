
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bangazon.Data;
using Bangazon.Models;
using System.Collections.Specialized;

namespace Bangazon.Controllers
{
    [Route("api/[controller]")]

    public class CustomerController : Controller
    {

        private BangazonContext _context;

        public CustomerController(BangazonContext ctx)
        {
            _context = ctx;
        }

        //GET api/customer
        [HttpGet]
        public IActionResult Get(bool? active)
        {
            //returns all customers
            if (active == null)
            {
                var customers = _context.Customer.ToList();
                if (customers == null)
                {
                    return NotFound();
                }
                return Ok(customers);
            }
            //if the query string of active=false is found the API returns only innactive customers
            else
            {
                //query for all customers that have a shopping cart with a paymentId
                var activeCustomer =
                from s in _context.ShoppingCart
                join c in _context.Customer on s.CustomerId equals c.CustomerId
                select c;

                //a list of customers that have never placed an order
                var innactiveCustomers = _context.Customer.Except(activeCustomer);

                return Ok(innactiveCustomers);

            }


        }

        // GET api/customer/5
        [HttpGet("{id}", Name = "GetSingleCustomer")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Customer customer = _context.Customer.Single(g => g.CustomerId == id);

                if (customer == null)
                {
                    return NotFound();
                }

                return Ok(customer);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }
        }


        // POST api/customer
        [HttpPost]
        public IActionResult Post([FromBody]Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Customer.Add(customer);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.CustomerId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("GetSingleCustomer", new { id = customer.CustomerId }, customer);
        }

        // PUT api/customer/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            _context.Customer.Update(customer);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }

        private bool CustomerExists(int customerId)
        {
            return _context.Customer.Any(g => g.CustomerId == customerId);
        }
    }
}