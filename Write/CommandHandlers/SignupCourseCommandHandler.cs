using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.CommandHandlers
{
	using System.Diagnostics;
	using System.Threading.Tasks;

	using CoolBrains.Bus.Contracts;
	using CoolBrains.Bus.Contracts.Command;
	using CoolBrains.Bus.ServiceBus.Command;

	using Domain.Commands;

	using Infrastructure.Repository.Contracts;

	using OnlineCourse.Repository.Entity;

	public class SignupCourseCommandHandler : CoolCommandHandlerAsync<SignupCourseCommand>
	{
		private readonly IRepository repository;

		private readonly ICoolBus bus;

		public SignupCourseCommandHandler(IRepository repository, ICoolBus bus)
		{
			this.repository = repository;
			this.bus = bus;
		}
		public override async Task<CommandResponse> Handle(SignupCourseCommand command)
		{

			var course = await this.repository.GetItem<Course>(x => x.CourseGuid == command.CourseGuid);
			
			var participant = new Participant();
			participant.CourseId = course.CourseId;
			participant.Name = command.Name;
			participant.Age = command.Age;
			
			await course.Signup(this.bus, participant);
			this.repository.Add(participant);
			await this.repository.SaveChanges();

			Trace.Write("### From SignupCourseCommand with ID" + command.CourseGuid);

			return new CommandResponse
				       {
					       Success = true
				       };
		}
	}
}
