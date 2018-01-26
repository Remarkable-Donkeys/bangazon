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
    public class ProductTypeController : Controller
    {
        private BangazonContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public ProductTypeController(BangazonContext ctx)
        {
            _context = ctx;
        }

        // GET list of product types
        [HttpGet]
        public IActionResult Get()
        {
            var productTypes = _context.ProductType.ToList();
            if (productTypes == null)
            {
                return NotFound();
            }
            return Ok(productTypes);
        }

        // GET single product type
        [HttpGet("{id}", Name = "GetSingleProductType")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProductType productType = _context.ProductType.Single(p => p.ProductTypeId == id);

                if (productType == null)
                {
                    return NotFound();
                }
                return Ok(productType);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]ProductType productType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.ProductType.Add(productType);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductTypeExists(productType.ProductTypeId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;    
                }
            }
            return CreatedAtRoute("GetSingleProductType", new { id = productType.ProductTypeId }, productType);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ProductType productType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != productType.ProductTypeId)
            {
                return BadRequest();
            }
            _context.ProductType.Update(productType);
            try{
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductTypeExists(id))
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
            ProductType productType = _context.ProductType.Single(p => p.ProductTypeId == id);

            if (productType == null)
            {
                return NotFound();
            }
            _context.ProductType.Remove(productType);
            _context.SaveChanges();
            return Ok(productType);
        }

        private bool ProductTypeExists(int productTypeId)
        {
            return _context.ProductType.Any(p => p.ProductTypeId == productTypeId);
        }
    }
}
