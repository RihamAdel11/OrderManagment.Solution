using OrderManagmentSystem.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentSystem.Core.Specifications
{
	public class OrderWithCustomerSpecification:BaseSpecification <Order>
	{
        public OrderWithCustomerSpecification():base()
        {
            Includes .Add(o=>o.Customer);
        }
        public OrderWithCustomerSpecification(int id):base(o=>o.Id==id)
		{
			Includes.Add(o => o.Customer);

		}
    }
}
