using AutoMapper;
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
	
	public class ProductController : ApiBaseController 
	{
	
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public ProductController(IUnitOfWork unitOfWork,IMapper mapper)
        {
			
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		[HttpGet]
		[ProducesResponseType (typeof(Product), 200)]
		[ProducesResponseType (typeof(ApiResponse),StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IReadOnlyList<Order>>> GetProducts()
		{
			
			var Products = await _unitOfWork .Repositry <Product>().GetAllAsync();
			return Ok(Products);
		}
		[HttpGet("{productId}")]
		[ProducesResponseType(typeof(Product), 200)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<Order>> GetProduct(int productId)
		{
			
			var Product = await _unitOfWork.Repositry<Product>().GetByIdAsync(productId);
			return Ok(Product);
		}
		[ProducesResponseType(typeof(Product), 200)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		[HttpPost]

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> CreateProduct([FromBody] Product product)
		{
			await _unitOfWork.Repositry<Product>().AddAsync(product);
			return CreatedAtAction(nameof(GetProduct), new { productId = product.Id }, product);
		}
		[ProducesResponseType(typeof(Product), 200)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		[HttpPut("productId")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> UpdateProduct(int productId, [FromBody] Product product)
		{
			product.Id = productId;
			await _unitOfWork.Repositry<Product>().UpdateAsync(product);
			return NoContent();
		}

	}
}
