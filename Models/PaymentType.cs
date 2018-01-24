using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bangazon.Models
{
    public class PaymentType
    {
        [Key]
        public int PaymentTypeId {get; set;}

        [Required]
        public int AccountNumber {get; set;}

        [Required]
        [StringLength(20)]
        public string Name {get; set;}
        
        [Required]        
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        
    }
}