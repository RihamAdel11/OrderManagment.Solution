using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OrderManagmentSystem.Core;
using OrderManagmentSystem.Core.Entites;
using OrderManagmentSystem.Core.Entites.Payments;
using OrderManagmentSystem.Core.Repositries;
using OrderManagmentSystem.Core.Services.EmailService;
using OrderManagmentSystem.Core.Specifications;
using OrderManagmentSystem.DTOs;
using OrderManagmentSystem.Error;
using OrderManagmentSystem.Services;
using System.Net;
using System.Net.Mail;

namespace OrderManagmentSystem.Controllers
{

    public class OrderController : ApiBaseController 
	{
	
		private readonly IUnitOfWork _unitofwork;
		private readonly IMapper _mapper;
		private readonly OrderServices _orderServices;
		private readonly IEmailServices _emailService;
		private readonly JwtSettings _jwtSettings;

		public OrderController(IUnitOfWork unitofwork,IMapper mapper,OrderServices orderServices,IEmailServices EmailService, IOptions<JwtSettings> jwtOptions)
        {
			_unitofwork = unitofwork;
			_mapper = mapper;
			_orderServices = orderServices;
			_emailService = EmailService;
			_jwtSettings = jwtOptions.Value;
		}

		[HttpPost]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Customer")]
		public async Task<ActionResult<Order>> CreateOrder([FromBody] Order order)
		{

			var Order = _orderServices.CreateOrderAsync(order);
			if (Order is null) return BadRequest(new ApiResponse(400));
			return Ok(Order);
		}
		[HttpGet("{orderId}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Customer,Admin")]
		public async Task<ActionResult<Order>> GetOrderForUser(int id)
		{
			var Spec = new OrderWithCustomerSpecification(id);
			var order = await _unitofwork.Repositry<Order>().GetByIdWithSpecAsync(Spec);
			if (order is null) return NotFound(new ApiResponse(404));

			return Ok(order);
		}
		[HttpGet]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
		public async Task<ActionResult<IReadOnlyList<Order>>> GetAllOrders()
		{
			var Spec = new OrderWithCustomerSpecification();
			var orders = await _unitofwork .Repositry <Order>().GetAllWithSpecAsync(Spec);
			var mappedOrder =  _mapper.Map < IEnumerable<Order>,IEnumerable< OrderDto>>(orders);
			return Ok(mappedOrder);
		}
		
		
		
		[HttpGet("PaymentMethods")]
		public async Task<ActionResult<IReadOnlyList<PaymentMethod>>> GetPaymentMethods()
		{
			var PaymentMethod = await _unitofwork .Repositry <Order>().GetAllAsync();
			return Ok(PaymentMethod);
		}

		[HttpPut("{orderId}/status")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
		public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] OrderStatus newStatus)
		{
			var order = await _orderServices .UpdateOrderStatusAsync(orderId, newStatus);
			if (order == null)
			{
				return NotFound();
			}
			await SendOrderStatusUpdateNotification(order, newStatus);
			return NoContent();
		}
		public async Task SendOrderStatusUpdateNotification(Order order, OrderStatus newStatus)
		{
			var customer = await _orderServices .GetCustomerAsync(order.CustomerId);
			var emailMessage = new MailMessage
			{
				From = new MailAddress(_jwtSettings .From),
				To = { new MailAddress(customer.Email) },
				Subject = $"Order Status Update: {order.Id}",
				Body = $"Dear {customer.Name},\n\nYour order ({order.Id}) has been updated to the status: {newStatus}.\n\nThank you for your business.\n\nRegards,\nOrder Management System"
			};
			await _emailService.SendEmailAsync(emailMessage);
		}
	}

}

