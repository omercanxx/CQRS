using AutoMapper;
using CQRS.Application.Requests.OrderRequests;
using CQRS.Domain.Commands.OrderCommand;
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
        public OrderController(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateRequest request)
        {
            string errorMessage = null;
            var validator = new OrderCreateValidator();
            var result = validator.Validate(request);
            if (result.IsValid)
            {
                var commandResult = await Mediator.Send(_mapper.Map<OrderCreateCommand>(request));
                Log.Information($"{commandResult.Id} id'li kurs eklenmiştir.");
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
        public async Task<IActionResult> UpdateOrder([FromBody] OrderUpdateRequest request)
        {
            string errorMessage = null;
            var validator = new OrderUpdateValidator();
            var result = validator.Validate(request);
            if (result.IsValid)
            {
                var commandResult = await Mediator.Send(_mapper.Map<OrderUpdateCommand>(request));
                Log.Information($"{commandResult.Id} id'li kurs güncellenmiştir.");
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
        public async Task<IActionResult> GetOrderDetail(Guid id)
        {
            Log.Information("Sipariş detay servisi çağrılmıştır.");
            return Ok(await Mediator.Send(new GetOrderDetailQuery(id)));
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            Log.Information("Sipariş liste servisi çağrılmıştır.");
            return Ok(await Mediator.Send(new GetOrdersQuery()));

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            Log.Information("Sipariş silme servisi çağrılmıştır.");
            return Ok(await Mediator.Send(new OrderDeleteCommand(id)));
        }
    }
}
