namespace LinkDev.Talabat.APIs.Controllers.Exceptions
{
	public class NotFoundException : ApplicationException
	{
		public NotFoundException()
		{
		}

		public NotFoundException(string name,object key) 
            : base($"{name} with ({key}) Not Found")
        {
            
        }
    }
}
