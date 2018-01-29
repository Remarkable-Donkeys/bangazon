/*
    author: Tyler Bowman
    purpose: add/update/get employees
    methods: 
        GET list of all employees
        GET{id} get a single employee
        POST new a new employee
        PUT change information on a customer
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
    public class EmployeeController : Controller
    {
        private BangazonContext _context;
        public EmployeeController(BangazonContext ctx)
        {
            _context = ctx;
        }

        //GET api/employee
        [HttpGet]
        public IActionResult Get()
        {
            var employees = _context.Employee.ToList();
            if (employees == null)
            {
                return Ok();
            }
            return Ok(employees);
        }

        //GET api/employee/5 (5=id of employee)
        [HttpGet("{id}", Name = "GetSingleEmployee")]
        public IActionResult GetAction(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Employee employee = _context.Employee.Single(g => g.EmployeeId == id);

                if (employee == null)
                {
                    return NotFound();
                }

                return Ok(employee);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }
        }

        /*
        POST: api/employee
        POST employee to database
        Arguments: Employee {
            "FirstName": required string (max 55 characters, ex. "Jimmy"),
            "LastName": required int (max 55 characters, ex. "Buttz"),
            "StartDate": required DateTime (format: "YYYY-MM-DD"),
            "EndDate": DateTime (format: "YYYY-MM-DD"),
            "DepartmentId": required int,
            "Supervisor": required int (0 = false, 1 = true),
            "Status": required string (ex. "Employed", "Retired", "Terminated", etc.)

        }*/
        [HttpPost]
        public IActionResult Post([FromBody]Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Employee.Add(employee);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EmployeeExists(employee.EmployeeId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("GetSingleEmployee", new { id = employee.EmployeeId }, employee);
        }

        /*
            PUT: api/employee/5 (5 = employee id)
            PUT employee to database
            Arguments: Employee {
                "EmployeeId": required int,
                "FirstName": required string (max 55 characters, ex. "Jimmy"),
                "LastName": required int (max 55 characters, ex. "Buttz"),
                "StartDate": required DateTime (format: "YYYY-MM-DD"),
                "EndDate": DateTime (format: "YYYY-MM-DD"),
                "DepartmentId": required int,
                "Supervisor": required int (0 = false, 1 = true),
                "Status": required string (ex. "Employed", "Retired", "Terminated", etc.)

        }*/
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }

            _context.Employee.Update(employee);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        private bool EmployeeExists(int employeeId)
        {
            return _context.Employee.Any(g => g.EmployeeId == employeeId);
        }



    }
}