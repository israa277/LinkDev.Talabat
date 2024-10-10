namespace LinkDev.Talabat.Core.Domain.Entities.Employees
{
	public class Department : BaseAuditableEntity<int>
	{
		public required string Name { get; set; }
		public DateOnly CreationDate { get; set; }
	}
}
