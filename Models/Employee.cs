using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bangazon.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(55)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(55)]
        public string LastName { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [Required]
        public int Supervisor { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        [DataType(DataType.Date)]
        
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        
        public Nullable<DateTime> EndDate { get; set; }

    
    }
}