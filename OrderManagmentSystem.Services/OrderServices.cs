using Microsoft.EntityFrameworkCore;
using OrderManagmentSystem.Core;
using OrderManagmentSystem.Core.Entites;
using OrderManagmentSystem.Core.Repositries;
using OrderManagmentSystem.Core.Services;
using OrderManagmentSystem.Core.Services.EmailService;
using OrderManagmentSystem.Core.Specifications.OrderSpec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentSystem.Services
{
    public class OrderServices : IOrderServices
	{

		private readonly IUnitOfWork _unitOfWork;
		private readonly IEmailServices _emailservices;

		public OrderServices(IUnitOfWork unitOfWork,IEmailServices emailservices)
		{

			_unitOfWork = unitOfWork;
			_emailservices = emailservices;
			;
		}
		public async Task CreateOrderAsync(Order order)
		{

			// Validate order items

			ValidateOrder(order);

			// Apply discounts
			ApplyDiscounts(order);
			// Update product stock
			UpdateProductStock(order);
			// Create order
			var createdOrder =  _unitOfWork .Repositry <Order>().AddAsync(order);

			
		// Generate invoice
        GenerateInvoice(order);
			// Send email notification
			



		}
		private async Task ValidateOrder(Order order)
		{
			foreach (var item in order.OrderItems)
			{
				var product = await _unitOfWork.Repositry<Product>().GetByIdAsync(item.ProductId);
				if (product.Stock < item.Quantity)
				{
					throw new Exception($"Insufficient stock for product: {product.Name}");
				}
			}
		}
				private async void ApplyDiscounts(Order order)
		{
			if (order.TotalAmount > 200)
			{
				order.TotalAmount *= 0.9m; // 10% discount
			}
			else if (order.TotalAmount > 100)
			{
				order.TotalAmount *= 0.95m; // 5% discount
			}
		}
			private async Task UpdateProductStock(Order order)
			{
				foreach (var orderItem in order.OrderItems)
				{
				var product = await _unitOfWork.Repositry<Product>().GetByIdAsync(orderItem.ProductId);
				product.Stock -= orderItem.Quantity;
				await _unitOfWork.Repositry<Product>().UpdateAsync(product);
			}


			}
		private async Task GenerateInvoice(Order order)
		{
			var invoice = new Invoice
			{
				OrderId = order.Id,
				InvoiceDate = DateTime.Now,
				TotalAmount = order.TotalAmount
			};

			await _unitOfWork.Repositry<Invoice>().AddAsync(invoice);
			await _unitOfWork .CompleteAsync ();
		}



		public Task<Order> GetDetailsOfSpecificOrder(int orderId)
		{
			var orderRepo = _unitOfWork .Repositry<Order>();
			var orderSpec = new OrderSpecifications (orderId);
			var order = orderRepo.GetByIdWithSpecAsync(orderSpec);
			return order;
		}


		public async Task<Order> UpdateOrderStatusAsync(int orderId, OrderStatus newStatus)
		{
			var order = await _unitOfWork .Repositry <Order>().GetByIdAsync(orderId);
			if (order == null)
			{
				return null;
			}
			order.Status = newStatus;
			await _unitOfWork.CompleteAsync();
			return order; ;
		}

		public async Task<Customer> GetCustomerAsync(int customerId)
		{
			return await _unitOfWork .Repositry <Customer >().GetByIdAsync(customerId);
		}
	}
}

