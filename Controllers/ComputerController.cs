/*Autor: Sean Williams
purpose: add/update/delete for Computer
methods: 
    GET list of all Computers
    GET single Computer
    POST a new Computer or assign a computer to an employee
    PUT change information on a Computer
    DELETE a Computer
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
	public class ComputerController : Controller
	{
		private BangazonContext _context;

		// Constructor method to create an instance of context to communicate with our database.
		public ComputerController(BangazonContext ctx)
		{
			_context = ctx;
		}

		//GET list of all Computers
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


		/* GET single Computer: 
        api/computer/[ComputerId] */
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

		/*POST Adds a computer to an employee
			api/comupter/employee
			Arguments: EmployeeComputer {
				"EmployeeId": required Foreign Key,
				"ComputerId": required Foreign Key,
				"ReturnDate": not required
				}
		*/
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

		/*POST Computer to database
			Arguments: Computer {
				"DatePurchase": required field of type DateTime,
				"DateDecomissioned": not required field of type DateTime,
				"Functioning": required field of type bool
			}
		*/
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

		/* PUT update Computer: 
        	api/computer/[ComputerId]
        	Arguments: Computer {
				"ComputerId": required int, 
				"DatePurchase": required field of type DateTime,
				"DateDecomissioned": not required field of type DateTime,
				"Functioning": required field of type bool
			}
		 */
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

		/* DELETE single Computer: 
        api/computer/[ComputerId] */
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

		/* DELETE remove a computer from an employee
		api/computer/employee/[EmployeeComputerId]
		*/
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
