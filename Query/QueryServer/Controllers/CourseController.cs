using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QueryServer.Controllers
{
	using Infrastructure.Logger.Contracts;
	using Infrastructure.Repository.Contracts;

	using OnlineCourse.Repository.Models;

	[Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
	    public ILog Log { get; set; }
	    public IReadOnlyRepository Repository { get; set; }

	    public CourseController(ILog log, IRepository repository)
	    {
		    this.Log = log;
		    this.Repository = repository;
	    }
		// GET api/values
		[HttpGet]
	    public IQueryable<ParticipantsAge> Get()
	    {
		    return  this.Repository.GetItems<ParticipantsAge>();
	    }
	}
}