using OrderManagmentSystem.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentSystem.Core.Specifications
{
	public class OrderItemWithOrderSpecification:BaseSpecification <OrderItem>
	{
        public OrderItemWithOrderSpecification():base()
        {
            Includes .Add(o=>o.Order);
        }
    }
}
