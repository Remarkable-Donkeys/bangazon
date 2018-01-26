using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bangazon.Models
{
    public class OrderedProduct
    {

        [Key]
        public int OrderedProductId { get; set; }

        [Required]
        public int ShoppingCartId {get; set;}
        public ShoppingCart ShoppingCart {get;set;}
    
        [Required]
        public int ProductId {get; set;}
        public Product Product {get;set;}

    }
}