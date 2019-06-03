using System;

namespace OnlineCourse.Repository.Models
{
	public class ParticipantsAge
	{
		public Guid CourseGuid { get; set; }
		
		public int MaxAge { get; set; }

		public int MinAge { get; set; }

		public double AvgAge { get; set; }

		public int TotalParticipants { get; set; }

	}
}
