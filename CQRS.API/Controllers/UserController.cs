using AutoMapper;
using CQRS.Application.Requests.UserRequests;
using CQRS.Domain.Commands.UserCommand;
using CQRS.Domain.Queries.UserQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CQRS.Application.Requests.UserRequests.UserCreateRequest;
using static CQRS.Application.Requests.UserRequests.UserUpdateRequest;

namespace CQRS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= (IMediator)HttpContext.RequestServices.GetService(typeof(IMediator));
        private readonly IMapper _mapper;
        public UserController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateRequest request)
        {
            string errorMessage = null;
            var validator = new UserCreateValidator();
            var result = validator.Validate(request);
            if (result.IsValid)
            {
                var commandResult = await Mediator.Send(_mapper.Map<UserCreateCommand>(request));
                Log.Information($"{commandResult.Name} isimli kullanıcı eklenmiştir.");
                return Ok();
            }
            foreach (var item in result.Errors)
            {
                errorMessage += $" {item.ErrorMessage}";
            }
            Log.Information(errorMessage);
            return BadRequest(errorMessage);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateRequest request)
        {
            string errorMessage = null;
            var validator = new UserUpdateValidator();
            var result = validator.Validate(request);
            if (result.IsValid)
            {
                var commandResult = await Mediator.Send(_mapper.Map<UserUpdateCommand>(request));
                Log.Information($"{commandResult.Name} isili kullanıcı güncellenmiştir.");
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
        public async Task<IActionResult> GetUserDetail(Guid id)
        {
            Log.Information("Kullanıcı detay servisi çağrılmıştır.");
            return Ok(await Mediator.Send(new GetUserDetailQuery(id)));
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            Log.Information("Kullanıcı liste servisi çağrılmıştır.");
            return Ok(await Mediator.Send(new GetUsersQuery()));

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            Log.Information("Kullanıcı silme servisi çağrılmıştır.");
            return Ok(await Mediator.Send(new UserDeleteCommand(id)));
        }
    }
}
