/*Autor: Sean Williams
purpose: add/update/delete for Shopping Carts
methods: 
    GET list of all Shopping Carts
    GET single Shopping Cart
    POST a new Shopping Cart or add a product to a Shopping Cart
    PUT change information on a Shopping Cart
    DELETE a Shopping Cart
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
	public class ShoppingCartController : Controller
	{
		private BangazonContext _context;

		// Constructor method to create an instance of context to communicate with our database.
		public ShoppingCartController(BangazonContext ctx)
		{
			_context = ctx;
		}

		//GET list of all Shopping Carts
		[HttpGet]
		public IActionResult Get()
		{
			var shoppingcarts = _context.ShoppingCart.ToList();
			List<ShoppingCartDisplay> shoppingCartDisplays = new List<ShoppingCartDisplay>();
			if (shoppingcarts == null)
			{
				return NotFound();
			}

			// foreach is taking data from shopping cart and putting in shopping cart display for proper formatting in JSON
			foreach (var shoppingCart in shoppingcarts)
			{
				ShoppingCart shoppingcart = _context.ShoppingCart
											.Include(s => s.OrderedProducts)
											.ThenInclude(p => p.Product)
											.Single(g => g.ShoppingCartId == shoppingCart.ShoppingCartId);

				ShoppingCartDisplay shoppingcartdisplay = new ShoppingCartDisplay();
				shoppingcartdisplay.CustomerId = shoppingcart.CustomerId;
				shoppingcartdisplay.DateCreated = shoppingcart.DateCreated;
				shoppingcartdisplay.DateOrdered = shoppingcart.DateOrdered;
				shoppingcartdisplay.PaymentTypeId = shoppingcart.PaymentTypeId;
				shoppingcartdisplay.ShoppingCartId = shoppingcart.ShoppingCartId;
				shoppingcartdisplay.Products = new List<Product>();

				foreach (OrderedProduct op in shoppingcart.OrderedProducts)
				{
					shoppingcartdisplay.Products.Add(_context.Product.Single(p => p.ProductId == op.ProductId));
				}

				shoppingCartDisplays.Add(shoppingcartdisplay);
			}
			return Ok(shoppingCartDisplays);
		}


		/* GET single Shopping Cart: 
        api/shoppingcart/[ShoppingCartId] */
		[HttpGet("{id}", Name = "GetSingleShoppingCart")]
		public IActionResult Get(int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				ShoppingCart shoppingcart = _context.ShoppingCart
											.Include(s => s.OrderedProducts)
											.ThenInclude(p => p.Product)
											.Single(g => g.ShoppingCartId == id);

				ShoppingCartDisplay shoppingcartdisplay = new ShoppingCartDisplay();
				shoppingcartdisplay.CustomerId = shoppingcart.CustomerId;
				shoppingcartdisplay.DateCreated = shoppingcart.DateCreated;
				shoppingcartdisplay.DateOrdered = shoppingcart.DateOrdered;
				shoppingcartdisplay.PaymentTypeId = shoppingcart.PaymentTypeId;
				shoppingcartdisplay.ShoppingCartId = shoppingcart.ShoppingCartId;
				shoppingcartdisplay.Products = new List<Product>();

				foreach (OrderedProduct op in shoppingcart.OrderedProducts)
				{
					shoppingcartdisplay.Products.Add(_context.Product.Single(p => p.ProductId == op.ProductId));
				}

				if (shoppingcart == null)
				{
					return NotFound();
				}

				return Ok(shoppingcartdisplay);
			}
			catch (System.InvalidOperationException ex)
			{
				return NotFound();
			}
		}

		/*POST Adds a product to a Shopping Cart
			api/shoppingcart/product
			Arguments: OrderProduct {
				"ShoppingCartId": required Foreign Key,
				"ProductId": required Foreign Key 
				}
		*/
		[HttpPost("product")]
		public IActionResult Post([FromBody]OrderedProduct op)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				ShoppingCart shoppingcart = _context.ShoppingCart.Single(g => g.ShoppingCartId == op.ShoppingCartId);
				Product product = _context.Product.Single(g => g.ProductId == op.ProductId);

				if (shoppingcart == null || product == null)
				{
					return NotFound();
				}

				_context.OrderedProduct.Add(op);

				try
				{
					_context.SaveChanges();
				}
				catch (DbUpdateException)
				{
					if (ShoppingCartExists(shoppingcart.ShoppingCartId))
					{
						return new StatusCodeResult(StatusCodes.Status409Conflict);
					}
					else
					{
						throw;
					}
				}
				return CreatedAtRoute("GetSingleShoppingCart", new { id = op.ShoppingCartId }, shoppingcart);
			}
			catch (System.InvalidOperationException ex)
			{
				return NotFound();
			}
		}

		/*POST Shopping Cart to database
			Arguments: ShoppingCart {
				"CustomerId": required Foreign Key, 
				"PaymentTypeId": required Foreign Key, 
				"DateOrdered": not required
			}
		*/
		[HttpPost]
		public IActionResult Post([FromBody]ShoppingCart shoppingcart)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			_context.ShoppingCart.Add(shoppingcart);

			try
			{
				_context.SaveChanges();
			}
			catch (DbUpdateException)
			{
				if (ShoppingCartExists(shoppingcart.ShoppingCartId))
				{
					return new StatusCodeResult(StatusCodes.Status409Conflict);
				}
				else
				{
					throw;
				}
			}
			return CreatedAtRoute("GetSingleShoppingCart", new { id = shoppingcart.ShoppingCartId }, shoppingcart);
		}

		/* PUT update Shopping Cart: 
        	api/shoppingcart/[ShoppingCartId]
        	Arguments: ShoppingCart {
				"ShoppingCartId" required int,
				"CustomerId": required Foreign Key, 
				"PaymentTypeId": required Foreign Key,
				"DateOrdered": not required
			}
		 */
		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody]ShoppingCart shoppingcart)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != shoppingcart.ShoppingCartId)
			{
				return BadRequest();
			}
			_context.ShoppingCart.Update(shoppingcart);
			try
			{
				_context.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ShoppingCartExists(id))
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

		/* DELETE single shopping Cart: 
        api/shoppingcart/[ShoppingCartId] */
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			ShoppingCart shoppingCart = _context.ShoppingCart.Single(p => p.ShoppingCartId == id);

			if (shoppingCart == null)
			{
				return NotFound();
			}

			_context.ShoppingCart.Remove(shoppingCart);
			_context.SaveChanges();
			return Ok(shoppingCart);
		}

		/* DELETE remove a product from a shoppingcart
		api/shoppingcart/product/[OrderedProductId]
		*/
		[HttpDelete("product/{id}")]
		public IActionResult DeleteEmployeeTraining(int id)
		{
			OrderedProduct op = _context.OrderedProduct.Single(p => p.OrderedProductId == id);

			if (op == null)
			{
				return NotFound();
			}

			_context.OrderedProduct.Remove(op);
			_context.SaveChanges();
			return Ok(op);

		}

		private bool ShoppingCartExists(int shoppingcartId)
		{
			return _context.ShoppingCart.Any(g => g.ShoppingCartId == shoppingcartId);
		}
	}
}
