using AutoMapper;
using CQRS.Domain.Dtos.OrderDtos;
using CQRS.Domain.MongoDtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer.AutoMapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<CreatedOrderDto, Order>();
            CreateMap<OrderDto, Order>();
        }
    }
}
