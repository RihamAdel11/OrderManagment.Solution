using OrderManagmentSystem.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentSystem.Core.Specifications.OrderSpec
{
	public class OrderSpecifications:BaseSpecification <Order>
	{
		public OrderSpecifications() : base()
		{
			Includes.Add(O => O.PaymentMethod );
			Includes.Add(O => O.OrderItems);
			AddOrderByDesc(O => O.OrderDate);

		}
		public OrderSpecifications(int orderId) : base(
			O => O.Id == orderId )
		{
			Includes.Add(O => O.PaymentMethod);
			Includes.Add(O => O.OrderItems);

		}
	}
}
