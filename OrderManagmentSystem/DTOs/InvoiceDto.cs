using OrderManagmentSystem.Core.Entites;

namespace OrderManagmentSystem.DTOs
{
	public class InvoiceDto
	{
		public int OrderId { get; set; }
		public DateOnly InvoiceDate { get; set; }

		public decimal TotalAmount { get; set; }
		public string Order { get; set; }
	}
}
