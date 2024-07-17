using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagmentSystem.Core;
using OrderManagmentSystem.Core.Entites;
using OrderManagmentSystem.Core.Repositries;
using OrderManagmentSystem.Core.Specifications;
using OrderManagmentSystem.DTOs;
using OrderManagmentSystem.Error;

namespace OrderManagmentSystem.Controllers
{

	public class CustomerController : ApiBaseController
	{
		
		private readonly IUnitOfWork _unitOfWork;

		public CustomerController(IUnitOfWork  unitOfWork)
		{
			
			_unitOfWork = unitOfWork;
		}
		[HttpGet("{customerId}/orders")]
		[ProducesResponseType(typeof(Product), 200)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		
		public async Task<ActionResult<IReadOnlyList<Customer>>> GetOrdersForCustomer(int customerId)
		{
			
			var orders = await _unitOfWork .Repositry <Customer >().GetByIdAsync(customerId);
		
			return Ok(orders);
		}
		[HttpPost]
		public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
		{
			await _unitOfWork.Repositry<Customer>().AddAsync(customer);
			return CreatedAtAction(nameof(GetOrdersForCustomer), new { customerId = customer.Id }, customer);
		}

	}
}
