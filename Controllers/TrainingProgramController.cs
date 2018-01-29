/*author: Kristen Norris
purpose: add/update/delete a training program and add/delete employees to a training program
methods: 
    GET list of training programs 
    GET single training program 
    POST new training program to database 
    PUT change information on a training program
    DELETE a training program

    GET single employee/training program relationship
    POST employee to a training program
    DELETE remove an employee from a training program
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

    public class TrainingProgramController : Controller
    {
        private BangazonContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public TrainingProgramController(BangazonContext ctx)
        {
            _context = ctx;
        }
        
        //GET list of training programs
        [HttpGet]
        public IActionResult Get()
        {
            var training_programs = _context.TrainingProgram.ToList();
            if (training_programs == null)
            {
                return NotFound();
            }
            return Ok(training_programs);
        }

        /* GET single training program
        api/TrainingProgram/[TrainingProgramId] */
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

        /* GET single employee/training relationship. 
        This method is primarily used to reture the employee/training relationship after it is added to the database.
        api/TrainingProgram/employee/[EmployeeTrainingId] */
        [HttpGet("employee/{id}", Name = "GetSingleEmployeeTraining")]
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
            //return the training program that was just added
            return CreatedAtRoute("GetSingleTrainingProgram", new { id = training_program.TrainingProgramId }, training_program);
        }

        //POST adding employee to training program api/trainingprogram/employee
        [HttpPost("employee")]
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
            return CreatedAtRoute("GetSingleEmployeeTraining", new { id = employee_training.EmployeeTrainingId }, employee_training);

        }
        

        /* PUT update the information on a training program that already exists
        api/trainingprogram/[TrainingProgramId] */
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

            //return the training program that was just updated
            return CreatedAtRoute("GetSingleTrainingProgram", id, training_program);
        }

        /* DELETE a training program but only if the training program is happening in the future. This will also delete all employee/training relationships associated with the deleted training program
        api/trainingprogram/[TrainingProgramId] */
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

        /* DELETE remove an employee from a training program
        api/trainingprogram/employee[EmployeeTrainingId]*/
        [HttpDelete("employee/{id}")]
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
