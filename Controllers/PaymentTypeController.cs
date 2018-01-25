using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bangazon.Data;
using Bangazon.Models;

namespace Bangazon.Controllers
{
    [Route("api/[controller]")]
    public class PaymentTypeController : Controller
    {
        private BangazonContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        
        //gets list of payment types
        [HttpGet]
        public IActionResult Get()
        {
            var payment_types = _context.PaymentType.ToList();
            if (payment_types == null)
            {
                return NotFound();
            }
            return Ok(payment_types);
        }

        // GET single payment type: api/PaymentType/[p]
        [HttpGet("{id}", Name = "GetSinglePaymentType")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                PaymentType payment_type = _context.PaymentType.Single(p => p.PaymentTypeId == id);

                if (payment_type == null)
                {
                    return NotFound();
                }

                return Ok(payment_type);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]PaymentType payment_type)
        {
            if (!ModelState.IsValid)
            {
                //if not valid data according to conditions then return the error
                return BadRequest(ModelState);
            }

            _context.PaymentType.Add(payment_type);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PaymentTypeExists(payment_type.PaymentTypeId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("GetSinglePaymentType", new { id = payment_type.PaymentTypeId }, payment_type);
        }

        // PUT api/values/[p]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]PaymentType payment_type)
        {
            //checks to see if input is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != payment_type.PaymentTypeId)
            {
                return BadRequest();
            }

            //try to update the specfic payment type
            _context.PaymentType.Update(payment_type);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if id does not exist return BadRequest
                if (!PaymentTypeExists(id))
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

        // DELETE api/values/[p]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            PaymentType payment_type = _context.PaymentType.Single(p => p.PaymentTypeId == id);

            if (payment_type == null)
            {
                return NotFound();
            }
            _context.PaymentType.Remove(payment_type);
            _context.SaveChanges();
            return Ok(payment_type);
        }

        //checks to see if the PaymentType exists
        private bool PaymentTypeExists(int paymentTypeId)
        {
            return _context.PaymentType.Any(p => p.PaymentTypeId == paymentTypeId);
        }
    }
}
