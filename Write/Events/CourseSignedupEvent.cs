using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events
{
	using CoolBrains.Bus.Contracts.Event;

	public class CourseSignedupEvent : CoolEvent
	{
		public Guid CourseGuid { get; set; }
		public long CourseId { get; set; }

		public string Name { get; set; }

		public int Age { get; set; }
	}
}
