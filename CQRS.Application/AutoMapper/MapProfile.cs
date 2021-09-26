using AutoMapper;
using CQRS.Application.Requests.CourseRequests;
using CQRS.Core.Entities;
using CQRS.Domain.Commands.CourseCommand;
using CQRS.Domain.Dtos.CourseDtos;
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
            CreateMap<Course, CourseDto>();
            #endregion

            #region RequestToDomain
            CreateMap<CourseCreateRequest, CourseCreateCommand>();
            CreateMap<CourseUpdateRequest, CourseUpdateCommand>();
            #endregion
        }
    }
}
