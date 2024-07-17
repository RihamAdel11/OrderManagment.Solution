using OrderManagmentSystem.Core.Entites;

namespace OrderManagmentSystem.DTOs
{
	public class OrderItemDto
	{
		public int OrderId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal Discount { get; set; }
		public string Order { get; set; }
	}
}
