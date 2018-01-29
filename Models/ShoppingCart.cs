using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bangazon.Models
{
	public class ShoppingCart
	{
		[Key]
		public int ShoppingCartId { get; set; }

		[Required]
		public DateTime DateCreated { get; set; }

		public DateTime DateOrdered { get; set; }

		[Required]
		public int CustomerId { get; set; }
		public Customer Customer { get; set; }

		public int PaymentTypeId { get; set; }
		public PaymentType PaymentType { get; set; }

		public ICollection<OrderedProduct> OrderedProducts {get;set;}

	}
}