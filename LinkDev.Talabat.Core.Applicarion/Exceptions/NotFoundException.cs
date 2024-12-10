namespace LinkDev.Talabat.Core.Application.Exceptions
{
	public class NotFoundException : ApplicationException
	{
		

		public NotFoundException(string name,object key) 
            : base($"{name} with ({key}) Not Found")
        {
            
        }
    }
}
