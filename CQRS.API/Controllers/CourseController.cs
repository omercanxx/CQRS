using AutoMapper;
using CQRS.Application.Requests.CourseRequests;
using CQRS.Domain.Commands.CourseCommands;
using CQRS.Domain.Queries.CourseQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CQRS.Application.Requests.CourseRequests.CourseCreateRequest;

namespace CQRS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= (IMediator)HttpContext.RequestServices.GetService(typeof(IMediator));
        private readonly IMapper _mapper;
        public CourseController(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CourseCreateRequest request)
        {
            var validator = new CourseCreateValidator();
            var result = validator.Validate(request);
            if(result.IsValid)
            {
                var id = await Mediator.Send(_mapper.Map<CourseCreateCommand>(request));
                Log.Information($"{id} li kurs eklendi");
                return Ok();
            }

            return BadRequest(result.Errors);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCourse([FromBody] CourseUpdateRequest request)
        {
            return Ok(await Mediator.Send(_mapper.Map<CourseUpdateCommand>(request)));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseDetail(Guid id)
        {
            return Ok(await Mediator.Send(new GetCourseDetailQuery(id)));
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            return Ok(await Mediator.Send(new GetCoursesQuery()));

        }
    }
}
