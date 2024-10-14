namespace LinkDev.Talabat.APIs.Controllers.Exceptions
{
	public class NotFoundException : ApplicationException
	{
        public NotFoundException() : base("Not Found")
        {
            
        }
    }
}
