using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commands
{
	using CoolBrains.Bus.Contracts.Command;

	public class AddCourseCommand: CoolCommand
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// gets or sets MaxParticipants
		/// </summary>
		public int MaxParticipants { get; set; }

		/// <summary>
		/// gets or sets Teacher
		/// </summary>
		public string Teacher { get; set; }

		/// <summary>
		/// Gets or sets the Course unique identifier.
		/// </summary>
		/// <value>
		/// The Course unique identifier.
		/// </value>
		public Guid CourseGuid { get; set; }
	}
}
