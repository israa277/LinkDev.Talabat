namespace LinkDev.Talabat.Core.Application.Common.Exceptions
{
	public class NotFoundException : ApplicationException
	{
        public NotFoundException(string v) : base("Not Found")
        {
            
        }

		public NotFoundException(string v, string basketId) : this(v)
		{
			BasketId = basketId;
		}

		public string BasketId { get; }
	}
}
