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
        private readonly ISystemAppService _systemAppService;
        public UserController(ISystemAppService systemAppService)
        {
            _systemAppService = systemAppService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateRequest request)
        {
            return Ok(await _systemAppService.CreateUser(request));
        }
        [HttpPost("Auth")]
        public async Task<IActionResult> Auth([FromBody] LoginRequest request)
        {
            return Ok(await _systemAppService.Authenticate(request));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateRequest request)
        {
            return Ok(await _systemAppService.UpdateUser(request));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserDetail(Guid id)
        {
            return Ok(await _systemAppService.GetUserDetail(id));
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _systemAppService.GetUsers());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            return Ok(await _systemAppService.DeleteUser(id));
        }
        [HttpPost("/UserProduct")]
        public async Task<IActionResult> CreateUserProduct([FromBody] UserProductCreateRequest request)
        {
            return Ok(await _systemAppService.CreateUserProduct(request));
        }
        [HttpPost("/UserProductItem")]
        public async Task<IActionResult> InsertUserProductItem([FromBody] UserProductItemInsertRequest request)
        {
            return Ok(await _systemAppService.InsertUserProductItem(request));
        }
    }
}
