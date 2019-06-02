namespace OnlineCourse.Repository.Entity
{
	using System;
	using System.Collections.Generic;

	public class Course: IAggregateRoot
	{
		public DateTime LastUpdated { get; set; }

		public string Name { get; set; }

		public string Teacher { get; set; }

		public int MaxParticipants { get; set; }

		public ICollection<Participant> Participants { get; set; }

		public Guid CourseGuid { get; set; }

		public long CourseId { get; set; }

		public bool Add()
		{
			return true;
		}

		public bool Edit()
		{
			throw new System.NotImplementedException();
		}

		public bool Remove()
		{
			throw new System.NotImplementedException();
		}
	}
}
