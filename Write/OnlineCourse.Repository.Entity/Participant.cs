namespace OnlineCourse.Repository.Entity
{
	public class Participant: IAggregate
	{
		public string Name { get; set; }

		public int Age { get; set; }

		public long CourseId { get; set; }

		public Course Course { get; set; }

		public long ParticipantId { get; set; }

	}
}
