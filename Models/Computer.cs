using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bangazon.Models
{
    public class Computer
    {
        [Key]
        public int ComputerId {get;set;}

        [Required]
        public DateTime DatePurchased {get;set;}

        [Required]
        public DateTime DateDecommisioned {get;set;}

        [Required]
        public bool Functioning {get;set;}
    }
}