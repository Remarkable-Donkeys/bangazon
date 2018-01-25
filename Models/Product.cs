using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bangazon.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]

        // date product is added 
        public DateTime DateAdded { get; set; }

        [Required]
        [StringLength(55)]
        public string ProductName { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        [StringLength(140)]
        public string Description { get; set; }

        [Required]
        public int Quantity { get; set; }

        // customer Id represents user who created the product
        [Required]
        public string CustomerId { get; set; }
        public Customer Customer { get; set; } 

    }
}