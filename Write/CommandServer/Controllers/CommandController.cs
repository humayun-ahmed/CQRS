using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommandServer.Controllers
{
	using System.Net;
	using System.Net.Http;

	using CoolBrains.Bus.Contracts;
	using CoolBrains.Bus.Contracts.Command;

	using Domain.Commands;

	using Infrastructure.Logger.Contracts;
	using Infrastructure.Validator.Contract;

	using Microsoft.AspNetCore.Server.Kestrel.Core;
	using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;

	//[Route("api/[controller]")]
    [ApiController]
	public class CommandController : Controller
	{
		private readonly ICoolBus bus;

		private readonly IValidator<AddCourseCommand> addCourseCommandValidator;

		public ILog Log { get; set; }

		public CommandController(ICoolBus bus, ILog log, IValidator<AddCourseCommand> addCourseCommandValidator)
		{
			this.bus = bus;
			this.Log = log;
			this.addCourseCommandValidator = addCourseCommandValidator;
		}

		[HttpPost]
		[Route("api/Course/Create")]
		public IActionResult Create([FromBody] AddCourseCommand command)
		{
			var validationResult= this.addCourseCommandValidator.PerformValidation(command);
			if (validationResult.IsValid)
			{
				this.bus.SendUsingMedia(command);
				return this.Ok();
			}
			else
			{
				return this.BadRequest(validationResult);
			}
		}

		[HttpPost]
		[Route("api/Course/Signup")]
		public Task Signup([FromBody] SignupCourseCommand command)
		{
			return this.bus.SendUsingMedia(command);
		}

		[HttpGet]
		[Route("api/course")]
		public string Get()
		{
			return "test";
		}
	}
}