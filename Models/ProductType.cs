using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bangazon.Models
{
    public class ProductType
    {
        [Key]
        public int ProductTypeId { get; set; }

        // product category (clothing, kitchen, etc.)
        [Required]
        [StringLength(55)]
        public string Type { get; set; }
        
    }
}