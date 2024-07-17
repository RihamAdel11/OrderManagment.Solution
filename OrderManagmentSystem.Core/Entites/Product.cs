using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentSystem.Core.Entites
{
	public class Product : BaseEntity
	{
		public decimal Price { get; set; }
		public string Name { get; set; }
		public int Stock { get; set; }
	}
}