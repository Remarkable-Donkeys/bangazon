
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
    public class EmployeeController
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
                return NotFound();
            }
            return Ok(employees);
        }

        






    }
}