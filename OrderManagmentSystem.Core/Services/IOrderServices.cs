using OrderManagmentSystem.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentSystem.Core.Services
{
	public interface  IOrderServices
	{
		
		Task CreateOrderAsync(Order order);
		Task<Order> GetDetailsOfSpecificOrder(int orderId);
		Task<Order> UpdateOrderStatusAsync(int orderId, OrderStatus newStatus);
		Task<Customer> GetCustomerAsync(int customerId);


	}
}
