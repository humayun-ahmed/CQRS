namespace Domain.Commands
{
	using System;

	using CoolBrains.Bus.Contracts.Command;

	/// <summary>
	/// </summary>
	public class SignupCourseCommand : CoolCommand
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// gets or sets Age
		/// </summary>
		public int Age { get; set; }

		/// <summary>
		/// Gets or sets the Course unique identifier.
		/// </summary>
		/// <value>
		/// The Course unique identifier.
		/// </value>
		public Guid CourseGuid { get; set; }
	}
}