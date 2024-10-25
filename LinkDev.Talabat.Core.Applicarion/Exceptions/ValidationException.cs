namespace LinkDev.Talabat.Core.Applicarion.Exceptions
{
	public class ValidationException : BadRequestException
	{
		public required IEnumerable<string> Errors { get; set; }
		public ValidationException(string message = "bad request")
			: base(message)
		{
		}

	}
}
