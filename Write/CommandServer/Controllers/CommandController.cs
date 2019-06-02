using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommandServer.Controllers
{
	using CoolBrains.Bus.Contracts;
	using CoolBrains.Bus.Contracts.Command;

	using Domain.Commands;

	using Infrastructure.Logger.Contracts;

	//[Route("api/[controller]")]
    [ApiController]
	public class CommandController : Controller
	{
		private readonly ICoolBus bus;

		public ILog Log { get; set; }

		public CommandController(ICoolBus bus, ILog log)
		{
			this.bus = bus;

			this.Log = log;
		}

		[HttpPost]
		[Route("api/Course/Create")]
		public Task Create([FromBody] AddCourseCommand command)
		{
			return this.bus.SendUsingMedia(command);
		}

		[HttpGet]
		[Route("api/course3")]
		public string Get()
		{
			return "test";
		}
	}
}