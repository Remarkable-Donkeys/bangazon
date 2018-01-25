using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bangazon.Models
{
    public class EmployeeComputer
    {
        [Key]
        public int EmployeeComputerId { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [Required]
        public int ComputerId { get; set; }
        public Computer Computer { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime IssueDate { get; set; }

        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ReturnDate { get; set; }


    }
}