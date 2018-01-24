using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bangazon.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

		public ShoppingCart ShoppingCart {get; set;}

		public Product Product {get; set;}
    }
}