﻿namespace OnlineCourse.Repository.Entity
{
	using System;

	public class Participant
	{
		public long ParticipantId { get; set; }
		public string Name { get; set; }

		public int Age { get; set; }

		public long CourseId { get; set; }

	}
}
