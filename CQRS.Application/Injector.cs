using CQRS.Core;
using CQRS.Core.Entities.Mongo;
using CQRS.Domain.Commands.CampaignCommands;
using CQRS.Domain.Commands.ProductCommands;
using CQRS.Domain.Commands.OrderCommands;
using CQRS.Domain.Commands.UserCommands;
using CQRS.Domain.Dtos.CampaignDtos;
using CQRS.Domain.Dtos.ProductDtos;
using CQRS.Domain.Dtos.UserDtos;
using CQRS.Domain.Queries.CampaignQueries;
using CQRS.Domain.Queries.ProductQueries;
using CQRS.Domain.Queries.OrderQueries;
using CQRS.Domain.Queries.UserQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using CQRS.Core.Interfaces.QueryInterfaces;
using CQRS.Infrastructure.Repositories.QueryRepositories;
using CQRS.Core.Interfaces.QueryInterfaces.Mongo;
using CQRS.Infrastructure.Repositories.CommandRepositories;
using CQRS.Core.Interfaces.CommandInterfaces;
using CQRS.Infrastructure.Repositories.CommandRepositories.Mongo;
using CQRS.Core.Interfaces.CommandInterfaces.Mongo;
using CQRS.Infrastructure.Repositories.QueryRepositories.Mongo;
using CQRS.Application.RabbitMq.Orders;
using CQRS.Infrastructure;
using CQRS.Token;

namespace CQRS.Application
{
    public class Injector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ISystemAppService, SystemAppService>();
            services.AddHttpContextAccessor();
            services.AddScoped<IProducerOrderProductMessage, ProducerOrderProductMessage>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();

            #region Repositories
            //Repository Dependency Injection
            services.AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>));
            services.AddScoped(typeof(IQueryMongoRepository<>), typeof(QueryMongoRepository<>));
            services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));
            services.AddScoped(typeof(ICommandMongoRepository<>), typeof(CommandMongoRepository<>));

            services.AddScoped<IQueryCampaignRepository, QueryCampaignRepository>();
            services.AddScoped<IQueryProductRepository, QueryProductRepository>();
            services.AddScoped<IQueryOrderRepository, QueryOrderRepository>();
            services.AddScoped<IQueryUserProductRepository, QueryUserProductRepository>();
            services.AddScoped<IQueryUserRepository, QueryUserRepository>();

            services.AddScoped<ICommandCampaignRepository, CommandCampaignRepository>();
            services.AddScoped<ICommandProductRepository, CommandProductRepository>();
            services.AddScoped<ICommandOrderRepository, CommandOrderRepository>();
            services.AddScoped<ICommandUserProductRepository, CommandUserProductRepository>();
            services.AddScoped<ICommandUserProductItemRepository, CommandUserProductItemRepository>();
            services.AddScoped<ICommandUserRepository, CommandUserRepository>();

            #region Mongo
            services.AddScoped<IQueryMongoOrderRepository, QueryMongoOrderRepository>();
            services.AddScoped<IQueryMongoProductSaleRepository, QueryMongoProductSaleRepository>();

            services.AddScoped<ICommandMongoOrderRepository, CommandMongoOrderRepository>();
            services.AddScoped<ICommandMongoProductSaleRepository, CommandMongoProductSaleRepository>();
            services.AddScoped<ICommandMongoUserProductRepository, CommandMongoUserProductRepository>();
            #endregion

            #endregion

            #region Commands
            //Command-CommandHandler Dependecy Injection
            services.AddScoped<IRequestHandler<CampaignCreateCommand, CommandResult>, CampaignCommandHandler>();
            services.AddScoped<IRequestHandler<CampaignUpdateCommand, CommandResult>, CampaignCommandHandler>();
            services.AddScoped<IRequestHandler<CampaignDeleteCommand, CommandResult>, CampaignCommandHandler>();

            services.AddScoped<IRequestHandler<ProductCreateCommand, CommandResult>, ProductCommandHandler>();
            services.AddScoped<IRequestHandler<ProductUpdateCommand, CommandResult>, ProductCommandHandler>();
            services.AddScoped<IRequestHandler<ProductDeleteCommand, CommandResult>, ProductCommandHandler>();

            services.AddScoped<IRequestHandler<OrderCreateCommand, CommandResult>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<OrderDeleteCommand, CommandResult>, OrderCommandHandler>();

            services.AddScoped<IRequestHandler<UserCreateCommand, CommandResult>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<UserDeleteCommand, CommandResult>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<UserProductCreateCommand, CommandResult>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<UserProductDeleteCommand, CommandResult>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<UserProductItemInsertCommand, CommandResult>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<UserProductItemRemoveCommand, CommandResult>, UserCommandHandler>();
            #endregion

            #region Queries
            //Query-QueryHandler Dependecy Injection

            services.AddScoped<IRequestHandler<GetCampaignsQuery, List<CampaignDto>>, CampaignQueryHandler>();
            services.AddScoped<IRequestHandler<GetCampaignDetailQuery, CampaignDto>, CampaignQueryHandler>();

            services.AddScoped<IRequestHandler<GetProductsQuery, List<ProductDto>>, ProductQueryHandler>();
            services.AddScoped<IRequestHandler<GetTopTenProductsQuery, List<MongoProductResultDto>>, ProductQueryHandler>();
            services.AddScoped<IRequestHandler<GetProductDetailQuery, ProductDto>, ProductQueryHandler>();

            services.AddScoped<IRequestHandler<GetOrdersQuery, List<MongoOrder>>, OrderQueryHandler>();
            services.AddScoped<IRequestHandler<GetOrderDetailQuery, MongoOrder>, OrderQueryHandler>();
            
            services.AddScoped<IRequestHandler<GetUsersQuery, List<UserDto>>, UserQueryHandler>();
            services.AddScoped<IRequestHandler<GetUserDetailQuery, UserDto>, UserQueryHandler>();
            services.AddScoped<IRequestHandler<AuthenticateQuery, UserDto>, UserQueryHandler>();
            #endregion

        }
    }
}
