using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagmentSystem.Error;

namespace OrderManagmentSystem.Controllers
{
	[Route("errors/{code}")]
	[ApiController]
	[ApiExplorerSettings(IgnoreApi = true)]
	public class ErrorController : ControllerBase
	{
		public ActionResult Error(int code)
		{
			if (code == 400)
				return BadRequest(new ApiResponse (400));
			else if (code == 404)
				return NotFound(new ApiResponse(code));
			else if (code == 401)
				return Unauthorized(new ApiResponse(401));
			else return BadRequest(new ApiResponse(code));

		}
	}
}
