namespace OnlineCourse.Repository.Entity
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using CoolBrains.Bus.Contracts;
	using CoolBrains.Bus.Contracts.Event;

	using Domain.Events;

	public class Course:BaseAggregateRoot, IAggregateRoot,ICourse
	{
		public Course(): base()
		{
			
		}
	
		public DateTime LastUpdated { get; set; }

		public string Name { get; set; }

		public string Teacher { get; set; }

		public int MaxParticipants { get; set; }

		public ICollection<Participant> Participants { get; set; }

		public Guid CourseGuid { get; set; }

		public long CourseId { get; set; }

		public Task Create(ICoolBus bus)
		{
			var @event=new CourseCreatedEvent
				           {
							   CourseGuid = this.CourseGuid,
							   Name = this.Name,
							   LastUpdated = this.LastUpdated,
							   Teacher = this.Teacher,
							   MaxParticipants = this.MaxParticipants,
			               };
			

			return bus.PublishUsingMedia(@event);
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

	public interface ICourse
	{
	}

	public class BaseAggregateRoot
	{
		public BaseAggregateRoot()
		{
			
		}

	}
}
