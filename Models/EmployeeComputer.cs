using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bangazon.Models
{
    public class EmployeeComputer
    {
        [Key]
        public int EmployeeComputerId   {get; set;}
    }
}