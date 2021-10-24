using AutoMapper;
using CQRS.Core.Entities.Mongo;
using CQRS.Core.Interfaces;
using CQRS.Core.Interfaces.QueryInterfaces.Mongo;
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
    public class OrderQueryHandler : IRequestHandler<GetOrdersQuery, List<MongoOrder>>,
                                      IRequestHandler<GetOrderDetailQuery, MongoOrder>
    {
        private readonly IQueryMongoOrderRepository _orderRepository;
        public OrderQueryHandler(IQueryMongoOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<MongoOrder> Handle(GetOrderDetailQuery request, CancellationToken cancellationToken)
        {
            var dbOrder = _orderRepository.FilterBy(x => x.OrderId == request.Id).FirstOrDefault();
            return dbOrder;
        }

        public async Task<List<MongoOrder>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            return _orderRepository.AsQueryable().ToList();
        }
    }
}
