using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineCourse.Repository.Models;

namespace OnlineCourse.Repository.EtConfiguration
{
	public class ParticipantsAgeConfiguration : IEntityTypeConfiguration<ParticipantsAge>
	{
		public void Configure(EntityTypeBuilder<ParticipantsAge> builder)
		{
			builder.HasKey(p => p.CourseGuid);
		}
	}
}
