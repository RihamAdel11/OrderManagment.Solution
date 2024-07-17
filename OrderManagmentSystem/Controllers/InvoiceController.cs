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
	[Authorize (Roles ="Admin")]
	public class InvoiceController : ApiBaseController 
	{
		
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public InvoiceController(IUnitOfWork unitOfWork,IMapper mapper)
        {
			
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		[HttpGet ]
		[Authorize(Roles = "Admin")]
		[ProducesResponseType(typeof(Product), 200)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IReadOnlyList<InvoiceDto>>> GetInvoicess()
		{
			var Spec = new InvoiceWithOrderSpecifications();
			var Invoices = await _unitOfWork .Repositry <Invoice >().GetAllWithSpecAsync(Spec);
			var mappedInvoice = _mapper.Map<IEnumerable<Invoice >, IEnumerable<InvoiceDto>>(Invoices);
			return Ok(mappedInvoice);
		}
		[HttpGet("{invoiceId}")]
		[Authorize(Roles = "Admin")]
		public async Task<ActionResult<Order>> GetInvoice(int invoiceId)
		{

			var Invoice = await _unitOfWork.Repositry<Invoice>().GetByIdAsync(invoiceId);
			return Ok(Invoice);
		}
	}
}
