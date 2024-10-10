using LinkDev.Talabat.Core.Domain.Entities.Products;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data
{
	public class StoreContext : DbContext
	{
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductBrand> Brands { get; set; }
		public DbSet<ProductCategory> Categories { get; set; }
		public StoreContext(DbContextOptions<StoreContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly);
		}

		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
		{
			
				return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}
	}
}
