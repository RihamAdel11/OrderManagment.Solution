namespace OrderManagmentSystem.Error
{
	public class APiExceptionResponse:ApiResponse 
	{
		public string? Details { get; set; }
		public APiExceptionResponse(int statusCode, string? message = null, string? details = null) :
			base(statusCode, message)
		{
			Details = details;
		}
	}
}
