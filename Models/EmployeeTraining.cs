/*author: Kristen Norris
purpose: model schema for database for Employee/Training joiner table */
using System.ComponentModel.DataAnnotations;

namespace Bangazon.Models
{
    public class EmployeeTraining
    {
        [Key]
        public int EmployeeTrainingId {get; set;}

        [Required]        
        public int TrainingProgramId { get; set; }
        public TrainingProgram TrainingProgram { get; set; }

        [Required]        
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}