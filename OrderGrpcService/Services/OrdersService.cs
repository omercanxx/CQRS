using AutoMapper;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderGrpcService.Services
{
    public class OrdersService : OrderService.OrderServiceBase
    {
        private readonly IMapper _mapper;
        public OrdersService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public override Task<GetOrdersResponse> GetOrders(GetOrdersResponse request, ServerCallContext context)
        {
            return base.GetOrders(request, context);
        }
    }
}