/*author: Kimberly Bird
purpose: model schema for database for Product */

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

        // date product is added - automatically generated on POST
        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateAdded { get; set; }

        [Required]
        [StringLength(55)]
        public string ProductName { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        [StringLength(140)]
        public string Description { get; set; }

        [Required]
        public int Quantity { get; set; }

        // foreign key customer Id represents user who created the product
        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } 

        // foreign key for product type
        [Required]
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; } 

    }
}