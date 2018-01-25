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
    public class TrainingProgramController : Controller
    {
        private BangazonContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        
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

        // POST api/values
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

        //checks to see if the TrainingProgram exists
        private bool TrainingProgramExists(int trainingProgramId)
        {
            return _context.TrainingProgram.Any(p => p.TrainingProgramId == trainingProgramId);
        }
    }
}