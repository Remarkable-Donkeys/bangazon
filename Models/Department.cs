/*Autor: Sean Williams
Purpose:  Department model schema for Bangazon database */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace Bangazon.Models
{
    public class Department
    {

        [Key]
        public int DepartmentId {get;set;}

        [Required]
        [StringLength(25)]
        public string Name {get;set;}

        [Required]
        public int Budget {get;set;}

    }
}