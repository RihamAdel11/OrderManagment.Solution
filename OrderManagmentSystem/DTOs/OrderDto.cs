using OrderManagmentSystem.Core.Entites;

namespace OrderManagmentSystem.DTOs
{
	public class OrderDto
	{
		public int CustomerId { get; set; }
		public DateOnly OrderDate { get; set; }
		public decimal TotalAmount { get; set; }
        public OrderItem  OrderItems { get; set; }
        public string PaymentMethod { get; set; }
		public string Status { get; set; }
		public string Customer { get; set; }
	}
}
