using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagmentSystem.Core.Entites.Payments;

namespace OrderManagmentSystem.Core.Entites
{

    public class Order:BaseEntity
	{
		
		

	

		public int CustomerId { get; set; }
        public DateTimeOffset OrderDate { get; set; }= DateTimeOffset.Now;
        
        public decimal TotalAmount { get; set; }
        public string Name { get; set; }
        public PaymentMethod  PaymentMethod { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Customer Customer { get; set; }
        //nav
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();


    }
}
