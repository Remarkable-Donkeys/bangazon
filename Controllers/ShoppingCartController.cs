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
	public class ShoppingCartConrtoller : Controller
	{
		private BangazonContext _context;

		public ShoppingCartConrtoller(BangazonContext ctx)
		{
			_context = ctx;
		}

		//GET api/customer
		[HttpGet]
		public IActionResult Get()
		{
			var shoppingcarts = _context.ShoppingCart.ToList();
			if (shoppingcarts == null)
			{
				return NotFound();
			}
			return Ok(shoppingcarts);
		}


		// GET api/customer/5
		[HttpGet("{id}", Name = "GetSingleShoppingCart")]
		public IActionResult Get(int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				ShoppingCart shoppingcart = _context.ShoppingCart.Single(g => g.ShoppingCartId == id);

				if (shoppingcart == null)
				{
					return NotFound();
				}

				return Ok(shoppingcart);
			}
			catch (System.InvalidOperationException ex)
			{
				return NotFound();
			}
		}

		// POST api/customer
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

		// PUT api/customer/5
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

		private bool ShoppingCartExists(int shoppingcartId)
		{
			return _context.ShoppingCart.Any(g => g.ShoppingCartId == shoppingcartId);
		}
	}
}
