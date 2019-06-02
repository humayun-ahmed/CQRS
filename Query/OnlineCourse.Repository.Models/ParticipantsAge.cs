using System;

namespace OnlineCourse.Repository.Models
{
	public class ParticipantsAge
	{
		public Guid CourseGuid { get; set; }
		public string CourseName { get; set; }
		public int MaxAge { get; set; }

		public int MinAge { get; set; }

		public int AvgAge { get; set; }

		public Guid ParticipantsAgeGuid { get; set; }

	}
}
