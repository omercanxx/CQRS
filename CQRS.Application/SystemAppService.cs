using AutoMapper;
using CQRS.Application.Requests.CampaignRequests;
using CQRS.Application.Requests.CourseRequests;
using CQRS.Application.Requests.OrderRequests;
using CQRS.Application.Requests.UserRequests;
using CQRS.Core;
using CQRS.Domain.Commands.CampaignCommands;
using CQRS.Domain.Commands.CourseCommands;
using CQRS.Domain.Commands.OrderCommands;
using CQRS.Domain.Commands.UserCommand;
using CQRS.Domain.Dtos.CampaignDtos;
using CQRS.Domain.Dtos.CourseDtos;
using CQRS.Domain.Dtos.OrderDtos;
using CQRS.Domain.Dtos.UserDtos;
using CQRS.Domain.Queries.CampaignQueries;
using CQRS.Domain.Queries.CourseQueries;
using CQRS.Domain.Queries.OrderQueries;
using CQRS.Domain.Queries.UserQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application
{
    public class SystemAppService : ISystemAppService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IMediator _mediator;
        protected IMediator Mediator;
        private readonly IMapper _mapper;
        public SystemAppService(IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            Mediator = _mediator ??= (IMediator)_httpContextAccessor.HttpContext.RequestServices.GetService(typeof(IMediator));
        }
        #region Order
        public async Task<List<OrderDto>> GetOrders()
        {
            return await _mediator.Send(new GetOrdersQuery());
        }
        public async Task<OrderDto> GetOrderDetail(Guid id)
        {
            return await _mediator.Send(new GetOrderDetailQuery(id));
        }
        public async Task<CommandResult> CreateOrder(OrderCreateRequest request)
        {
            return await Mediator.Send(_mapper.Map<OrderCreateCommand>(request));
        }
        public async Task<CommandResult> UpdateOrder(OrderUpdateRequest request)
        {
            return await Mediator.Send(_mapper.Map<OrderUpdateCommand>(request));
        }
        public async Task<CommandResult> DeleteOrder(Guid id)
        {
            return await _mediator.Send(new OrderDeleteCommand(id));
        }
        #endregion

        #region Campaign

        public async Task<List<CampaignDto>> GetCampaigns()
        {
            return await _mediator.Send(new GetCampaignsQuery());
        }
        public async Task<CampaignDto> GetCampaignDetail(Guid id)
        {
            return await _mediator.Send(new GetCampaignDetailQuery(id));
        }
        public async Task<CommandResult> CreateCampaign(CampaignCreateRequest request)
        {
            return await Mediator.Send(_mapper.Map<CampaignCreateCommand>(request));
        }
        public async Task<CommandResult> UpdateCampaign(CampaignUpdateRequest request)
        {
            return await Mediator.Send(_mapper.Map<CampaignUpdateCommand>(request));
        }
        public async Task<CommandResult> DeleteCampaign(Guid id)
        {
            return await _mediator.Send(new CampaignDeleteCommand(id));
        }
        #endregion

        #region Course

        public async Task<List<CourseDto>> GetCourses()
        {
            return await _mediator.Send(new GetCoursesQuery());
        }
        public async Task<CourseDto> GetCourseDetail(Guid id)
        {
            return await _mediator.Send(new GetCourseDetailQuery(id));
        }
        public async Task<CommandResult> CreateCourse(CourseCreateRequest request)
        {
            return await Mediator.Send(_mapper.Map<CourseCreateCommand>(request));
        }
        public async Task<CommandResult> UpdateCourse(CourseUpdateRequest request)
        {
            return await Mediator.Send(_mapper.Map<CourseUpdateCommand>(request));
        }
        public async Task<CommandResult> DeleteCourse(Guid id)
        {
            return await _mediator.Send(new CourseDeleteCommand(id));
        }
        #endregion

        #region User

        public async Task<List<UserDto>> GetUsers()
        {
            return await _mediator.Send(new GetUsersQuery());
        }
        public async Task<UserDto> GetUserDetail(Guid id)
        {
            return await _mediator.Send(new GetUserDetailQuery(id));
        }
        public async Task<CommandResult> CreateUser(UserCreateRequest request)
        {
            return await Mediator.Send(_mapper.Map<UserCreateCommand>(request));
        }
        public async Task<CommandResult> UpdateUser(UserUpdateRequest request)
        {
            return await Mediator.Send(_mapper.Map<UserUpdateCommand>(request));
        }
        public async Task<CommandResult> DeleteUser(Guid id)
        {
            return await _mediator.Send(new UserDeleteCommand(id));
        }
        #endregion
    }
}
