using AutoMapper;
using CQRS.Application.Requests.OrderRequests;
using CQRS.Domain.Commands.OrderCommand;
using CQRS.Domain.Queries.OrderQueries;
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
            return Ok(await Mediator.Send(_mapper.Map<OrderCreateCommand>(request)));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderUpdateRequest request)
        {
            return Ok(await Mediator.Send(_mapper.Map<OrderUpdateCommand>(request)));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetail(Guid id)
        {
            return Ok(await Mediator.Send(new GetOrderDetailQuery(id)));
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            return Ok(await Mediator.Send(new GetOrdersQuery()));

        }
    }
}
