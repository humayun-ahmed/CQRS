using System;

namespace OnlineCourse.Repository.Models
{
	using System.Collections.Generic;

	public class Course
    {
        public DateTime LastUpdated { get; set; }

        public string Name { get; set; }

        public string Teacher { get; set; }

        public int MaxParticipants { get; set; }
		

		public Guid CourseGuid { get; set; }

		
       
    }
}