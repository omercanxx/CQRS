using AutoMapper;
using CQRS.Application.Requests.UserRequests;
using CQRS.Domain.Commands.UserCommand;
using CQRS.Domain.Queries.UserQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return Ok(await Mediator.Send(_mapper.Map<UserCreateCommand>(request)));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateRequest request)
        {
            return Ok(await Mediator.Send(_mapper.Map<UserUpdateCommand>(request)));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserDetail(Guid id)
        {
            return Ok(await Mediator.Send(new GetUserDetailQuery(id)));
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await Mediator.Send(new GetUsersQuery()));

        }
    }
}
