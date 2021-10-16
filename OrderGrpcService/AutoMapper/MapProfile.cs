using AutoMapper;
using CQRS.Domain.Dtos.OrderDtos;
using CQRS.Domain.MongoDtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderGrpcService
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
