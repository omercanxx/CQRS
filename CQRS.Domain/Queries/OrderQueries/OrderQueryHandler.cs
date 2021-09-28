using AutoMapper;
using CQRS.Core.Interfaces;
using CQRS.Domain.Dtos.CourseDtos;
using CQRS.Domain.Dtos.OrderDtos;
using CQRS.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Domain.Queries.OrderQueries
{
    public class OrderQueryHandler : IRequestHandler<GetOrdersQuery, List<OrderDto>>,
                                      IRequestHandler<GetOrderDetailQuery, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public OrderQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public async Task<OrderDto> Handle(GetOrderDetailQuery request, CancellationToken cancellationToken)
        {
            var dbCourse = await _orderRepository.GetByIdAsync(request.Id);
            return _mapper.Map<OrderDto>(dbCourse);
        }

        public async Task<List<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<OrderDto>>(await _orderRepository.GetAllAsync());
        }
    }
}
