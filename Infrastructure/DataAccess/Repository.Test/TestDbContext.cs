namespace Infrastructure.Repository.Contracts.Test
{
	using Infrastructure.Repository.Contracts.Test.Models;

	using Microsoft.EntityFrameworkCore;

	public class TestDbContext : BaseContext
    {

        public TestDbContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<ProductCategory> ProductCategories { get; set; }
    }
}
