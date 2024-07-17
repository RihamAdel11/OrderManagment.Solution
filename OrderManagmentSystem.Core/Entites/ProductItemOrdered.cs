using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentSystem.Core.Entites
{
	public class ProductItemOrdered
	{
		

		private ProductItemOrdered()
		{

		}
		public ProductItemOrdered(int productId, string name,int Stock)
		{
			productId = ProductId;
			name = ProductName;
			Stock = stock;
		}
		public int ProductId { get; set; }
		public string ProductName { get; set; } = null!;
		public int stock { get; set; } 
	}
}
