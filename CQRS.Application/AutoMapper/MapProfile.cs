using AutoMapper;
using CQRS.Application.Requests.CampaignRequests;
using CQRS.Application.Requests.OrderRequests;
using CQRS.Application.Requests.ProductRequests;
using CQRS.Application.Requests.UserRequests;
using CQRS.Core;
using CQRS.Core.Entities;
using CQRS.Core.Entities.Mongo;
using CQRS.Domain.Commands.CampaignCommands;
using CQRS.Domain.Commands.OrderCommands;
using CQRS.Domain.Commands.ProductCommands;
using CQRS.Domain.Commands.UserCommands;
using CQRS.Domain.Dtos.CampaignDtos;
using CQRS.Domain.Dtos.ProductDtos;
using CQRS.Domain.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.AutoMapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            #region Dtos
            CreateMap<Campaign, CampaignDto>();
            CreateMap<Product, ProductDto>();
            CreateMap<User, UserDto>();

            CreateMap<ProductResult, MongoProductResult>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id));
            #endregion

            #region RequestToDomain

            CreateMap<CampaignCreateRequest, CampaignCreateCommand>();
            CreateMap<CampaignUpdateRequest, CampaignUpdateCommand>();

            CreateMap<ProductCreateRequest, ProductCreateCommand>();
            CreateMap<ProductUpdateRequest, ProductUpdateCommand>();

            CreateMap<OrderCreateRequest, OrderCreateCommand>();

            CreateMap<UserCreateRequest, UserCreateCommand>();
            CreateMap<UserUpdateRequest, UserUpdateCommand>();

            #endregion
        }
    }
}
