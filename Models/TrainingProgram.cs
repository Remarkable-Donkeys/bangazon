using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bangazon.Models
{
    public class TrainingProgram
    {
        [Key]
        public int TrainingProgramId {get; set;}

        [Required]
        [StringLength(55)]
        public string Name {get; set;}

        [StringLength(150)]
        public string Description {get; set;}

        [Required]
        public DateTime StartDate {get; set;}
        
        [Required]
        public DateTime EndDate {get; set;}

        [Required]
        public int MaxAttendees {get; set;}
    }
}