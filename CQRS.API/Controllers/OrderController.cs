using AutoMapper;
using CQRS.Application;
using CQRS.Application.Requests.OrderRequests;
using CQRS.Domain.Commands.OrderCommands;
using CQRS.Domain.Queries.OrderQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CQRS.Application.Requests.OrderRequests.OrderCreateRequest;
using static CQRS.Application.Requests.OrderRequests.OrderUpdateRequest;

namespace CQRS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= (IMediator)HttpContext.RequestServices.GetService(typeof(IMediator));
        private readonly IMapper _mapper;
        private readonly ISystemAppService _systemAppService;
        public OrderController(IMapper mapper, ISystemAppService systemAppService)
        {
            _mapper = mapper;
            _systemAppService = systemAppService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateRequest request)
        {
            string errorMessage = null;
            var validator = new OrderCreateValidator();
            var result = validator.Validate(request);
            if (result.IsValid)
            {
                var commandResult = await _systemAppService.CreateOrder(request);
                Log.Information($"{commandResult.Id} id'li sipariş eklenmiştir.");
                return Ok();
            }

            return BadRequest(errorMessage);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderUpdateRequest request)
        {
            string errorMessage = null;
            var validator = new OrderUpdateValidator();
            var result = validator.Validate(request);
            if (result.IsValid)
            {
                var commandResult = await _systemAppService.UpdateOrder(request);
                Log.Information($"{commandResult.Id} id'li sipariş güncellenmiştir.");
                return Ok();
            }
            
            return BadRequest(errorMessage);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetail(Guid id)
        {
            Log.Information("Sipariş detay servisi çağrılmıştır.");
            return Ok(await _systemAppService.GetOrderDetail(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            Log.Information("Sipariş liste servisi çağrılmıştır.");
            return Ok(await _systemAppService.GetOrders());

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var commandResult = await _systemAppService.DeleteOrder(id);
            Log.Information($"{commandResult.Id} id'li sipariş silme servisi çağrılmıştır.");
            return Ok();
        }
    }
}
