﻿using AutoMapper;
using CQRS.Application.Requests.CampaignRequests;
using CQRS.Application.Requests.CourseRequests;
using CQRS.Application.Requests.OrderRequests;
using CQRS.Application.Requests.UserRequests;
using CQRS.Core.Entities;
using CQRS.Domain.Commands.CampaignCommands;
using CQRS.Domain.Commands.CourseCommands;
using CQRS.Domain.Commands.OrderCommands;
using CQRS.Domain.Commands.UserCommand;
using CQRS.Domain.Dtos.CampaignDtos;
using CQRS.Domain.Dtos.CourseDtos;
using CQRS.Domain.Dtos.OrderDtos;
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
            CreateMap<Course, CourseDto>();
            CreateMap<User, UserDto>();
            CreateMap<Order, OrderDto>()
                .ForMember(x => x.Course, opt => opt.MapFrom(dest => dest.Course))
                .ForMember(x => x.User, opt => opt.MapFrom(dest => dest.User));
            #endregion

            #region RequestToDomain

            CreateMap<CampaignCreateRequest, CampaignCreateCommand>();
            CreateMap<CampaignUpdateRequest, CampaignUpdateCommand>();

            CreateMap<CourseCreateRequest, CourseCreateCommand>();
            CreateMap<CourseUpdateRequest, CourseUpdateCommand>();

            CreateMap<OrderCreateRequest, OrderCreateCommand>();
            CreateMap<OrderUpdateRequest, OrderUpdateCommand>();

            CreateMap<UserCreateRequest, UserCreateCommand>();
            CreateMap<UserUpdateRequest, UserUpdateCommand>();

            #endregion
        }
    }
}
