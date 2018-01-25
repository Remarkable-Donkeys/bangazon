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
    public class TrainingProgramController : Controller
    {
        private BangazonContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public TrainingProgramController(BangazonContext ctx)
        {
            _context = ctx;
        }
        //gets list of training programs
        [HttpGet]
        public IActionResult Get()
        {
            var training_programs = _context.PaymentType.ToList();
            if (training_programs == null)
            {
                return NotFound();
            }
            return Ok(training_programs);
        }

        // GET single training program: api/TrainingProgram/[p]
        [HttpGet("{id}", Name = "GetSingleTrainingProgram")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                TrainingProgram training_program = _context.TrainingProgram.Single(p => p.TrainingProgramId == id);

                if (training_program == null)
                {
                    return NotFound();
                }

                return Ok(training_program);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }
        }

        // GET single employee training relationship
        [HttpGet("{id}", Name = "GetSingleEmployeeTrainging")]
        public IActionResult GetEmployeeTraining(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                EmployeeTraining employee_training = _context.EmployeeTraining.Single(p => p.EmployeeTrainingId == id);

                if (employee_training == null)
                {
                    return NotFound();
                }

                return Ok(employee_training);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }
        }

        // POST training program to database
        [HttpPost]
        public IActionResult Post([FromBody]TrainingProgram training_program)
        {
            if (!ModelState.IsValid)
            {
                //if not valid data according to conditions then return the error
                return BadRequest(ModelState);
            }

            _context.TrainingProgram.Add(training_program);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TrainingProgramExists(training_program.TrainingProgramId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetSingleTrainingProgram", new { id = training_program.TrainingProgramId }, training_program);
        }

        //POST adding employee to training program
        [HttpPost]
        public IActionResult Post([FromBody]EmployeeTraining employee_training)
        {
            if (!ModelState.IsValid)
            {
                //if not valid data according to conditions then return the error
                return BadRequest(ModelState);
            }

            //adds the employee training relationship to the joiner table
            _context.EmployeeTraining.Add(employee_training);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EmployeeTrainingExists(employee_training.EmployeeTrainingId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            //returns the employee/training program relationship that was just added
            return CreatedAtRoute("GetSingleEmployeeTrainging", new { id = employee_training.EmployeeTrainingId }, employee_training);

        }
        

        // PUT api/values/[p]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]TrainingProgram training_program)
        {
            //checks to see if input is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != training_program.TrainingProgramId)
            {
                return BadRequest();
            }

            //try to update the specfic payment type
            _context.TrainingProgram.Update(training_program);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if id does not exist return BadRequest
                if (!TrainingProgramExists(id))
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
            TrainingProgram training_program = _context.TrainingProgram.Single(p => p.TrainingProgramId == id);

            DateTime today = DateTime.Today;

            if (training_program == null)
            {
                return NotFound();
            }
            
            if(training_program.StartDate > today){
                _context.TrainingProgram.Remove(training_program);
                _context.SaveChanges();
                return Ok(training_program);
            } 

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }

        // DELETE remove an employee from a training program
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployeeTraining(int id)
        {
            EmployeeTraining employee_training = _context.EmployeeTraining.Single(p => p.EmployeeTrainingId == id);

            DateTime today = DateTime.Today;

            if (employee_training == null)
            {
                return NotFound();
            }
            
            _context.EmployeeTraining.Remove(employee_training);
            _context.SaveChanges();
            return Ok(employee_training);

        }

        //checks to see if the TrainingProgram exists
        private bool TrainingProgramExists(int trainingProgramId)
        {
            return _context.TrainingProgram.Any(p => p.TrainingProgramId == trainingProgramId);
        }

        //checks to see if the employee is already attending the training
        private bool EmployeeTrainingExists(int employeeTrainingId)
        {
            return _context.EmployeeTraining.Any(p => p.EmployeeTrainingId == employeeTrainingId);
        }
    }
}