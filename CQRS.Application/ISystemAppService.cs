using CQRS.Application.Requests.CampaignRequests;
using CQRS.Application.Requests.CourseRequests;
using CQRS.Application.Requests.OrderRequests;
using CQRS.Application.Requests.UserRequests;
using CQRS.Core;
using CQRS.Domain.Dtos.CampaignDtos;
using CQRS.Domain.Dtos.CourseDtos;
using CQRS.Domain.Dtos.OrderDtos;
using CQRS.Domain.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application
{
    public interface ISystemAppService
    {
        #region Order
        Task<OrderDto> GetOrderDetail(Guid id);
        Task<List<OrderDto>> GetOrders();
        Task<CommandResult> CreateOrder(OrderCreateRequest request);
        Task<CommandResult> UpdateOrder(OrderUpdateRequest request);
        Task<CommandResult> DeleteOrder(Guid id);
        #endregion

        #region Campaign
        Task<CampaignDto> GetCampaignDetail(Guid id);
        Task<List<CampaignDto>> GetCampaigns();
        Task<CommandResult> CreateCampaign(CampaignCreateRequest request);
        Task<CommandResult> UpdateCampaign(CampaignUpdateRequest request);
        Task<CommandResult> DeleteCampaign(Guid id);
        #endregion

        #region Course
        Task<CourseDto> GetCourseDetail(Guid id);
        Task<List<CourseDto>> GetCourses();
        Task<CommandResult> CreateCourse(CourseCreateRequest request);
        Task<CommandResult> UpdateCourse(CourseUpdateRequest request);
        Task<CommandResult> DeleteCourse(Guid id);
        #endregion

        #region User
        Task<UserDto> GetUserDetail(Guid id);
        Task<List<UserDto>> GetUsers();
        Task<CommandResult> CreateUser(UserCreateRequest request);
        Task<CommandResult> UpdateUser(UserUpdateRequest request);
        Task<CommandResult> DeleteUser(Guid id);
        #endregion
    }
}
