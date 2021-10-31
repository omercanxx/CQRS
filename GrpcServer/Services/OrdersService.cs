using AutoMapper;
using CQRS.Application;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer
{
    public class OrdersService : OrderService.OrderServiceBase
    {
        private readonly IMapper _mapper;
        private readonly ISystemAppService _systemAppService;
        public OrdersService(IMapper mapper, ISystemAppService systemAppService)
        {
            _mapper = mapper;
            _systemAppService = systemAppService;
        }
        public override async Task<GetOrdersResponse> GetOrders(GetOrdersRequest request, ServerCallContext context)
        {
            var dbTopTenProducts = await _systemAppService.GetTopTenProducts();

            GetOrdersResponse response = new GetOrdersResponse();
            response.Products.AddRange(_mapper.Map<List<Product>>(dbTopTenProducts));

            return response;
        }
    }
}