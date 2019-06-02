using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.CommandHandlers
{
	using System.Diagnostics;
	using System.Threading.Tasks;

	using CoolBrains.Bus.Contracts.Command;
	using CoolBrains.Bus.ServiceBus.Command;

	using Domain.Commands;

	public class AddCourseCommandHandler : CoolCommandHandlerAsync<AddCourseCommand>
	{
		public override Task<CommandResponse> Handle(AddCourseCommand command)
		{
			Trace.Write("### From AddCourseCommandHandler with ID" + command.CourseGuid);
			return Task.FromResult(new CommandResponse
				                       {
					                       Success = true
				                       });
		}
	}
}
