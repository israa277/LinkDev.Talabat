namespace LinkDev.Talabat.Core.Applicarion.Exceptions
{
	public class UnAuthorizedException: ApplicationException
	{
		public UnAuthorizedException(string message) 
			: base(message) { }
	}
}
