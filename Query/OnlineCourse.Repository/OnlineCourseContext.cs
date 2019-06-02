namespace OnlineCourse.Repository
{
	using Infrastructure.Repository;

	using Microsoft.EntityFrameworkCore;

	using OnlineCourse.Repository.EtConfiguration;
	using OnlineCourse.Repository.Models;

	

	public class OnlineCourseContext : BaseContext
	{
		public OnlineCourseContext(DbContextOptions options)
			: base(options)
		{
		}

		public DbSet<ParticipantsAge> ParticipantsAges { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new ParticipantsAgeConfiguration());
			base.OnModelCreating(modelBuilder);
		}
	}
}