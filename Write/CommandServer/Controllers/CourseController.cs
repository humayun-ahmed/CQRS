namespace CommandServer.Controllers
{
	using System.Linq;

	using Infrastructure.Logger.Contracts;
	using Infrastructure.Repository.Contracts;

	using Microsoft.AspNetCore.Mvc;

	using OnlineCourse.Repository.Entity;

	//[Route("api/[controller]")]
	[ApiController]
	public class CourseController : ControllerBase
	{
		public CourseController(ILog log, IRepository repository)
		{
			this.Log = log;
			this.Repository = repository;
		}

		public ILog Log { get; set; }

		public IReadOnlyRepository Repository { get; set; }

		// GET api/values
		[HttpGet]
		[Route("api/course")]
		public IQueryable<Participant> Get()
		{
			return this.Repository.GetItems<Participant>();
		}
	}
}