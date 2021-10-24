using AutoMapper;
using CQRS.Core.Entities.Mongo;
using CQRS.Domain.Dtos.OrderDtos;
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
            CreateMap<MongoOrder, Order>();
            CreateMap<OrderDto, Order>();
        }
    }
}
