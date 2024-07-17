using OrderManagmentSystem.Error;
using System.Net;
using System.Text.Json;

namespace OrderManagmentSystem.MiddleWare
{
	public class ExceptionMiddleWare
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddleWare> _loggerFactory;
		private readonly IHostEnvironment _env;


		public ExceptionMiddleWare(RequestDelegate next, HttpContext httpContextcontext, ILogger<ExceptionMiddleWare> loggerFactory, IHostEnvironment env)
		{
			_next = next;
			_loggerFactory = loggerFactory;
			_env = env;
		}



		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next.Invoke(httpContext);

			}
			catch (Exception ex)
			{
				_loggerFactory.LogError(ex.Message);
				httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				httpContext.Response.ContentType = "application/json";
				var response = _env.IsDevelopment() ?
					new APiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString()) :
					new APiExceptionResponse((int)HttpStatusCode.InternalServerError);
				var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
				var json = JsonSerializer.Serialize(response, options);
				await httpContext.Response.WriteAsync(json);
			}
		}
	}
}

