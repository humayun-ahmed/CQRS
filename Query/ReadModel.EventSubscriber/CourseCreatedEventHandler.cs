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

	public class CourseCreatedEventHandler:CoolEventHandlerAsync<CourseCreatedEvent>
	{
		private readonly IRepository repository;

		public CourseCreatedEventHandler(IRepository repository)
		{
			this.repository = repository;
		}
		public override async Task Handle(CourseCreatedEvent @event)
		{
			Course course=new Course();
			course.CourseGuid = @event.CourseGuid;
			course.LastUpdated = @event.LastUpdated;
			course.MaxParticipants = @event.MaxParticipants;
			course.Name = @event.Name;
			course.Teacher = @event.Teacher;
			this.repository.Add(course);
			await this.repository.SaveChanges();
			Console.WriteLine($"CourseCreatedEvent: User has created with Id {@event.CourseGuid}");
		}
	}
}
