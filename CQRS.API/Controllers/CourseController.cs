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
using static CQRS.Application.Requests.CourseRequests.CourseUpdateRequest;

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
            string errorMessage = null;
            var validator = new CourseCreateValidator();
            var result = validator.Validate(request);
            if(result.IsValid)
            {
                //Birden fazla constructora sahip olan commandresult Core katmanında tanımlanmıştır.
                var commandResult = await Mediator.Send(_mapper.Map<CourseCreateCommand>(request));
                Log.Information($"{commandResult.Title} isimli kurs eklendi");
                return Ok();
            }
            //Birden fazla hata mesajı geldiğinde arka arkaya loglamak için foreach kullanılıyor.
            foreach(var item in result.Errors)
            {
                errorMessage += $" {item.ErrorMessage}";
            }
            Log.Information(errorMessage);
            return BadRequest(errorMessage);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCourse([FromBody] CourseUpdateRequest request)
        {
            string errorMessage = null;
            var validator = new CourseUpdateValidator();
            var result = validator.Validate(request);
            if (result.IsValid)
            {
                var commandResult = await Mediator.Send(_mapper.Map<CourseUpdateCommand>(request));
                Log.Information($"{commandResult.Title} isimli kurs güncellenmiştir.");
                return Ok();
            }
            foreach (var item in result.Errors)
            {
                errorMessage += $" {item.ErrorMessage}";
            }
            Log.Information(errorMessage);
            return BadRequest(errorMessage);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseDetail(Guid id)
        {
            Log.Information("Kurs detay servisi çağrılmıştır.");
            return Ok(await Mediator.Send(new GetCourseDetailQuery(id)));
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            Log.Information("Kurs liste servisi çağrılmıştır.");
            return Ok(await Mediator.Send(new GetCoursesQuery()));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            Log.Information("Kurs silme servisi çağrılmıştır.");
            return Ok(await Mediator.Send(new CourseDeleteCommand(id)));
        }

    }
}
