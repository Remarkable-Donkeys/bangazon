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
    public class ProductController : Controller
    {
        private BangazonContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public ProductController(BangazonContext ctx)
        {
            _context = ctx;
        }

        // GET list of products
        [HttpGet]
        public IActionResult Get()
        {
            var products = _context.Product.ToList();
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        // GET single product
        [HttpGet("{id}", Name = "GetSingleProduct")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Product product = _context.Product.Single(p => p.ProductId == id);

                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Product.Add(product);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(product.ProductId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;    
                }
            }
            return CreatedAtRoute("GetSingleProduct", new { id = product.ProductId }, product);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != product.ProductId)
            {
                return BadRequest();
            }
            _context.Product.Update(product);
            try{
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product product = _context.Product.Single(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }
            _context.Product.Remove(product);
            _context.SaveChanges();
            return Ok(product);
        }

        private bool ProductExists(int productId)
        {
            return _context.Product.Any(p => p.ProductId == productId);
        }
    }
}
