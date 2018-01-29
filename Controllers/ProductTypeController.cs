/*author: Kimberly Bird
purpose: add/update/delete a product type for customers
methods: 
    GET list of all product types
    GET single product type
    POST new a new product type
    PUT change information on a product type
    DELETE a product type
 */

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

        // GET list of product types. URL: api/productType
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

        // GET single product type. URL: api/productType/[ProductTypeId]
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

        /* POST api/values
        POST product type to database
        Arguments: ProductType 
            {
            "Type": required string (max 55 characters, ex. "clothing")
            } */
        [HttpPost]
        public IActionResult Post([FromBody]ProductType productType)
        {
            if (!ModelState.IsValid)
            {
                //if not valid data according to conditions then return the error
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

        /* PUT update product type: 
        api/ProductType/[ProductTypeId]
        Arguments: ProductType 
            {
            "Type": required string (max 55 characters, ex. "clothing"
            } */
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
            try
            {
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

        /* DELETE single product type: 
        api/ProductType/[ProductTypeId] */
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

        //checks to see if the ProductType exists
        private bool ProductTypeExists(int productTypeId)
        {
            return _context.ProductType.Any(p => p.ProductTypeId == productTypeId);
        }
    }
}
