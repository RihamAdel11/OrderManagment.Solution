using OrderManagmentSystem.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderManagmentSystem.Core.Specifications
{
	public class InvoiceWithOrderSpecifications:BaseSpecification <Invoice>
	{
        public InvoiceWithOrderSpecifications():base()
        {
            Includes.Add(o => o.Order);
        }
    }
}
