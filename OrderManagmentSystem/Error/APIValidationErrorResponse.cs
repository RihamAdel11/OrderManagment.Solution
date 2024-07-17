namespace OrderManagmentSystem.Error
{
	public class APIValidationErrorResponse:ApiResponse 
	{
		public IEnumerable<string> Errors { get; set; }
		public APIValidationErrorResponse() : base(400)
		{
			Errors = new List<string>();

		}
	}
}
