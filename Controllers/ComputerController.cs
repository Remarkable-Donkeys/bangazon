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
	public class ComputerController : Controller
	{
		private BangazonContext _context;

		public ComputerController(BangazonContext ctx)
		{
			_context = ctx;
		}

		//GET api/customer
		[HttpGet]
		public IActionResult Get()
		{
			var computers = _context.Computer.ToList();
			if (computers == null)
			{
				return NotFound();
			}
			return Ok(computers);
		}


		// GET api/customer/5
		[HttpGet("{id}", Name = "GetSingleComputer")]
		public IActionResult Get(int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				Computer computer = _context.Computer.Single(g => g.ComputerId == id);

				if (computer == null)
				{
					return NotFound();
				}

				return Ok(computer);
			}
			catch (System.InvalidOperationException ex)
			{
				return NotFound();
			}
		}

		[HttpPost("employee")]
		public IActionResult Post([FromBody]EmployeeComputer empcomp)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				Employee employee = _context.Employee.Single(g => g.EmployeeId == empcomp.EmployeeId);
				Computer computer = _context.Computer.Single(g => g.ComputerId == empcomp.ComputerId);

				if (employee == null || computer == null)
				{
					return NotFound();
				}

				_context.EmployeeComputer.Add(empcomp);

				_context.SaveChanges();
				return CreatedAtRoute("GetSingleComputer", new { id = empcomp.ComputerId }, empcomp);
			}
			catch (System.InvalidOperationException ex)
			{
				return NotFound();
			}
		}
		// POST api/customer
		[HttpPost]
		public IActionResult Post([FromBody]Computer computer)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			_context.Computer.Add(computer);

			try
			{
				_context.SaveChanges();
			}
			catch (DbUpdateException)
			{
				if (ComputerExists(computer.ComputerId))
				{
					return new StatusCodeResult(StatusCodes.Status409Conflict);
				}
				else
				{
					throw;
				}
			}
			return CreatedAtRoute("GetSingleComputer", new { id = computer.ComputerId }, computer);
		}

		// PUT api/customer/5
		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody]Computer computer)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != computer.ComputerId)
			{
				return BadRequest();
			}
			_context.Computer.Update(computer);
			try
			{
				_context.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ComputerExists(id))
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
			Computer computer = _context.Computer.Single(p => p.ComputerId == id);

			if (computer == null)
			{
				return NotFound();
			}

			_context.Computer.Remove(computer);
			_context.SaveChanges();
			return Ok(computer);
		}

		// DELETE remove a computer from an employee
		[HttpDelete("employee/{id}")]
		public IActionResult DeleteEmployeeTraining(int id)
		{
			EmployeeComputer empcomp = _context.EmployeeComputer.Single(p => p.EmployeeComputerId == id);

			if (empcomp == null)
			{
				return NotFound();
			}

			_context.EmployeeComputer.Remove(empcomp);
			_context.SaveChanges();
			return Ok(empcomp);

		}

		private bool ComputerExists(int computerId)
		{
			return _context.Computer.Any(g => g.ComputerId == computerId);
		}
	}
}
