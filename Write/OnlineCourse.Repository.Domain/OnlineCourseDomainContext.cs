namespace OnlineCourse.Repository.Domain
{
	using Infrastructure.Repository;

	using Microsoft.EntityFrameworkCore;

	using OnlineCourse.Repository.Domain.EtConfiguration;
	using OnlineCourse.Repository.Entity;

	public class OnlineCourseDomainContext : BaseContext
	{
		public OnlineCourseDomainContext(DbContextOptions options)
			: base(options)
		{
		}

		public DbSet<Course> Courses { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new CourseConfiguration());
			base.OnModelCreating(modelBuilder);
		}
	}
}