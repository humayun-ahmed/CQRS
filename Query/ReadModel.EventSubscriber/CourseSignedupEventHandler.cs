using System;
using System.Collections.Generic;
using System.Text;

namespace ReadModel.EventSubscriber
{
	using System.Threading.Tasks;

	using CoolBrains.Bus.ServiceBus.Event;

	using Domain.Events;

	using Infrastructure.Repository.Contracts;

	using OnlineCourse.Repository.Models;

	public class CourseSignedupEventHandler : CoolEventHandlerAsync<CourseSignedupEvent>
	{
		private readonly IRepository repository;

		public CourseSignedupEventHandler(IRepository repository)
		{
			this.repository = repository;
		}
		public override async Task Handle(CourseSignedupEvent @event)
		{
			var participantsAge = await this.repository.GetItem<ParticipantsAge>(x => x.CourseGuid == @event.CourseGuid);
			if (participantsAge == null)
			{
				participantsAge=new ParticipantsAge();
				participantsAge.CourseGuid = @event.CourseGuid;
				participantsAge.MaxAge = @event.Age;
				participantsAge.MinAge= @event.Age;
				participantsAge.AvgAge = @event.Age;
				participantsAge.TotalParticipants = 1;
				this.repository.Add(participantsAge);
			}
			else
			{
				if (participantsAge.MaxAge < @event.Age)
				{
					participantsAge.MaxAge = @event.Age;
				}
				if (participantsAge.MinAge > @event.Age)
				{
					participantsAge.MinAge = @event.Age;
				}
				int updatedTotalParticipant=participantsAge.TotalParticipants+1;
				participantsAge.AvgAge = ((participantsAge.AvgAge * participantsAge.TotalParticipants)+ @event.Age )/ updatedTotalParticipant;
				participantsAge.TotalParticipants = updatedTotalParticipant;
				this.repository.Update(participantsAge);
			}

			await this.repository.SaveChanges();
			
			Console.WriteLine($"CourseCreatedEvent: User has created with Id {@event.CourseId}");
		}
	}
}
