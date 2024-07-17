using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagmentSystem.Error;
using OrderMangmentSystem.Repositry.Data;

namespace OrderManagmentSystem.Controllers
{
	
	public class BuggyController : ApiBaseController 
	{
		private readonly StoreContext _dbcontext;

		public BuggyController(StoreContext dbcontext)
		{
			_dbcontext = dbcontext;
		}
		[HttpGet("NotFound")]
		public IActionResult GetNotFoundRequest()
		{
			var order = _dbcontext.Orders.Find(100);
			if (order == null)
			{
				return NotFound(new ApiResponse(404));
			}
			return Ok(order);
		}
		[HttpGet("ServerError")]
		public IActionResult GetServerErrorRequest()
		{
			  var order = _dbcontext.Orders.Find(100);

			var OrderReturn = order.ToString();

			return Ok(OrderReturn);
		}
		[HttpGet("BadRequest")]
		public IActionResult GetbadRequestRequest()
		{


			return BadRequest(new ApiResponse(400));
		}
		[HttpGet("BadRequest/{id}")]//Validation Error
		public IActionResult GetbadRequestRequest(int id)
		{


			return Ok();
		}
		[HttpGet("UnAuthorixedError")]
		public IActionResult GetUnAuthorixedErrorRequest()
		{


			return Unauthorized(new ApiResponse(401));
		}
	}
}
