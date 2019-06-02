namespace OnlineCourse.Repository.Domain.EtConfiguration
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	using OnlineCourse.Repository.Entity;

	public class CourseConfiguration : IEntityTypeConfiguration<Course>
	{
		public void Configure(EntityTypeBuilder<Course> builder)
		{
			
		}
	}
}
