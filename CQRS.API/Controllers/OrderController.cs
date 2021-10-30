using AutoMapper;
using CQRS.Application;
using CQRS.Application.Requests.OrderRequests;
using CQRS.Domain.Commands.OrderCommands;
using CQRS.Domain.Queries.OrderQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CQRS.Application.Requests.OrderRequests.OrderCreateRequest;

namespace CQRS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ISystemAppService _systemAppService;
        public OrderController(ISystemAppService systemAppService)
        {
            _systemAppService = systemAppService;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateRequest request)
        {
            var validator = new OrderCreateValidator();
            var result = validator.Validate(request);
            if (result.IsValid)
            {
                var commandResult = await _systemAppService.CreateOrder(request);
                return Ok();
            }

            return BadRequest();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetail(string id)
        {
            return Ok(await _systemAppService.GetOrderDetail(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            return Ok(await _systemAppService.GetOrders());

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var commandResult = await _systemAppService.DeleteOrder(id);
            return Ok();
        }
    }
}
