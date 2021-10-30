using AutoMapper;
using CQRS.Application.RabbitMq;
using CQRS.Application.RabbitMq.Orders;
using CQRS.Application.Requests.CampaignRequests;
using CQRS.Application.Requests.OrderRequests;
using CQRS.Application.Requests.ProductRequests;
using CQRS.Application.Requests.UserRequests;
using CQRS.Core;
using CQRS.Core.Entities.Mongo;
using CQRS.Domain.Commands.CampaignCommands;
using CQRS.Domain.Commands.OrderCommands;
using CQRS.Domain.Commands.ProductCommands;
using CQRS.Domain.Commands.UserCommands;
using CQRS.Domain.Dtos.CampaignDtos;
using CQRS.Domain.Dtos.OrderDtos;
using CQRS.Domain.Dtos.ProductDtos;
using CQRS.Domain.Dtos.UserDtos;
using CQRS.Domain.Queries.CampaignQueries;
using CQRS.Domain.Queries.ProductQueries;
using CQRS.Domain.Queries.OrderQueries;
using CQRS.Domain.Queries.UserQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CQRS.Token;
using CQRS.Application.RabbitMq.Users;

namespace CQRS.Application
{
    public class SystemAppService : ISystemAppService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IMediator _mediator;
        protected IMediator Mediator;
        private readonly IMapper _mapper;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IOptions<RabbitMqConfiguration> _rabbitMqOptions;
        private readonly IProducerOrderProductMessage _producerOrderProductMessage;
        private readonly IProducerUserProductMessage _producerUserProductMessage;
        private readonly IProducerProductMessage _producerProductMessage;

        public SystemAppService(IHttpContextAccessor httpContextAccessor, IMapper mapper, ITokenGenerator tokenGenerator, IOptions<RabbitMqConfiguration> rabbitMqOptions, IProducerOrderProductMessage producerOrderProductMessage, IProducerUserProductMessage producerUserProductMessage, IProducerProductMessage producerProductMessage)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            Mediator = _mediator ??= (IMediator)_httpContextAccessor.HttpContext.RequestServices.GetService(typeof(IMediator));
            _tokenGenerator = tokenGenerator;
            _rabbitMqOptions = rabbitMqOptions;
            _producerOrderProductMessage = producerOrderProductMessage;
            _producerUserProductMessage = producerUserProductMessage;
            _producerProductMessage = producerProductMessage;
        }
        #region Order
        public async Task<List<MongoOrder>> GetOrders()
        {
            return await _mediator.Send(new GetOrdersQuery());
        }
        public async Task<MongoOrder> GetOrderDetail(string id)
        {
            return await _mediator.Send(new GetOrderDetailQuery(id));
        }
        public async Task<CommandResult> CreateOrder(OrderCreateRequest request)
        {
            CommandResult commandResult = await Mediator.Send(_mapper.Map<OrderCreateCommand>(request));
            _producerOrderProductMessage.SendOrderMessage(commandResult.ProductResults);
            
            return commandResult;
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

        #region Product

        public async Task<List<ProductDto>> GetProducts()
        {
            return await _mediator.Send(new GetProductsQuery());
        }
        public async Task<List<MongoProductResultDto>> GetTopTenProducts()
        {
            return await _mediator.Send(new GetTopTenProductsQuery());
        }
        public async Task<ProductDto> GetProductDetail(Guid id)
        {
            return await _mediator.Send(new GetProductDetailQuery(id));
        }
        public async Task<CommandResult> CreateProduct(ProductCreateRequest request)
        {
            return await Mediator.Send(_mapper.Map<ProductCreateCommand>(request));
        }
        public async Task<CommandResult> UpdateProduct(ProductUpdateRequest request)
        {
            var commandResult = await Mediator.Send(_mapper.Map<ProductUpdateCommand>(request));
            if (commandResult.IsDiscounted)
                _producerProductMessage.SendProductMessage(commandResult.ProductId);

            return commandResult;
        }
        public async Task<CommandResult> DeleteProduct(Guid id)
        {
            return await _mediator.Send(new ProductDeleteCommand(id));
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
        public async Task<string> Authenticate(LoginRequest request)
        {
            string token = null;
            var dbUser =  await _mediator.Send(_mapper.Map<AuthenticateQuery>(request));
            if (dbUser != null)
                token = _tokenGenerator.GenerateJSONWebToken(dbUser);

            return token;
        }
        #endregion

        #region User Product List
        public async Task<CommandResult> CreateUserProduct(UserProductCreateRequest request)
        {
            return await Mediator.Send(_mapper.Map<UserProductCreateCommand>(request));
        }
        public async Task<CommandResult> InsertUserProductItem(UserProductItemInsertRequest request)
        {
            var commandResult =  await Mediator.Send(_mapper.Map<UserProductItemInsertCommand>(request));
            MongoUserProduct userProduct = new MongoUserProduct(commandResult.UserId, commandResult.ProductId, 1);
            
            _producerUserProductMessage.SendUserProductMessage(userProduct);

            return commandResult;
        }
        #endregion
    }
}
