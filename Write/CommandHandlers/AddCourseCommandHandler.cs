using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.CommandHandlers
{
	using System.Diagnostics;
	using System.Threading.Tasks;

	using CoolBrains.Bus.Contracts;
	using CoolBrains.Bus.Contracts.Command;
	using CoolBrains.Bus.ServiceBus.Command;

	using Domain.Commands;

	using Infrastructure.Repository.Contracts;

	using MassTransit.MessageData;

	using OnlineCourse.Repository.Entity;

	public class AddCourseCommandHandler : CoolCommandHandlerAsync<AddCourseCommand>
	{
		private readonly IRepository repository;

		private readonly ICoolBus bus;

		public AddCourseCommandHandler(IRepository repository, ICoolBus bus)
		{
			this.repository = repository;
			this.bus = bus;
		}
		public override async Task<CommandResponse> Handle(AddCourseCommand command)
		{
			var course = new Course();
			course.CourseGuid = command.CourseGuid;
			course.Name = command.Name;
			course.LastUpdated = DateTime.UtcNow;
			course.Teacher = command.Teacher;
			course.MaxParticipants = command.MaxParticipants;
			await course.Create(this.bus);
			this.repository.Add(course);
			await this.repository.SaveChanges();
			
			Trace.Write("### From AddCourseCommandHandler with ID" + command.CourseGuid);
			
			return new CommandResponse
				       {
					       Success = true
				       };
		}
	}
}
