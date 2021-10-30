using AutoMapper;
using CQRS.Application;
using CQRS.Application.Requests.UserRequests;
using CQRS.Domain.Commands.UserCommands;
using CQRS.Domain.Queries.UserQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CQRS.Application.Requests.UserRequests.LoginRequest;
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
        private readonly ISystemAppService _systemAppService;
        private readonly IMapper _mapper;
        public UserController(ISystemAppService systemAppService, IMapper mapper)
        {
            _systemAppService = systemAppService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateRequest request)
        {
            var validator = new UserCreateValidator();
            var result = validator.Validate(request);
            if (result.IsValid)
            {
                var commandResult = await Mediator.Send(_mapper.Map<UserCreateCommand>(request));
                return Ok();
            }

            return BadRequest();
        }
        [HttpPost("Auth")]
        public async Task<IActionResult> Auth([FromBody] LoginRequest request)
        {
            return Ok(await _systemAppService.Authenticate(request));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateRequest request)
        {
            var validator = new UserUpdateValidator();
            var result = validator.Validate(request);
            if (result.IsValid)
            {
                var commandResult = await Mediator.Send(_mapper.Map<UserUpdateCommand>(request));
                return Ok();
            }

            return BadRequest();
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
