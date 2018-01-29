using System;
using System.Collections.Generic;


namespace Bangazon.Models
{
    public class ShoppingCartDisplay
    {
		public int ShoppingCartId { get; set; }

		public DateTime DateCreated { get; set; }

		public DateTime DateOrdered { get; set; }

		public int CustomerId { get; set; }

		public int PaymentTypeId { get; set; }

		public ICollection<Product> Products {get;set;}

	}
}