using CQRS.Application.Requests.CampaignRequests;
using CQRS.Application.Requests.OrderRequests;
using CQRS.Application.Requests.ProductRequests;
using CQRS.Application.Requests.UserRequests;
using CQRS.Core;
using CQRS.Core.Entities.Mongo;
using CQRS.Domain.Dtos.CampaignDtos;
using CQRS.Domain.Dtos.OrderDtos;
using CQRS.Domain.Dtos.ProductDtos;
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
        Task<MongoOrder> GetOrderDetail(string id);
        Task<List<MongoOrder>> GetOrders();
        Task<CommandResult> CreateOrder(OrderCreateRequest request);
        Task<CommandResult> DeleteOrder(Guid id);
        #endregion

        #region Campaign
        Task<CampaignDto> GetCampaignDetail(Guid id);
        Task<List<CampaignDto>> GetCampaigns();
        Task<CommandResult> CreateCampaign(CampaignCreateRequest request);
        Task<CommandResult> UpdateCampaign(CampaignUpdateRequest request);
        Task<CommandResult> DeleteCampaign(Guid id);
        #endregion

        #region Product
        Task<ProductDto> GetProductDetail(Guid id);
        Task<List<ProductDto>> GetProducts();
        Task<List<MongoProductResultDto>> GetTopTenProducts();
        Task<CommandResult> CreateProduct(ProductCreateRequest request);
        Task<CommandResult> UpdateProduct(ProductUpdateRequest request);
        Task<CommandResult> DeleteProduct(Guid id);
        #endregion

        #region User
        Task<UserDto> GetUserDetail(Guid id);
        Task<List<UserDto>> GetUsers();
        Task<CommandResult> CreateUser(UserCreateRequest request);
        Task<CommandResult> UpdateUser(UserUpdateRequest request);
        Task<CommandResult> DeleteUser(Guid id);
        Task<string> Authenticate(LoginRequest request);
        #endregion

        #region User Product List
        Task<CommandResult> CreateUserProduct(UserProductCreateRequest request);
        Task<CommandResult> InsertUserProductItem(UserProductItemInsertRequest request);
        #endregion
    }
}
