using CQRS.Core.Interfaces;
using CQRS.Domain.Commands.CourseCommands;
using CQRS.Domain.Commands.OrderCommand;
using CQRS.Domain.Commands.UserCommand;
using CQRS.Domain.Dtos.CourseDtos;
using CQRS.Domain.Dtos.OrderDtos;
using CQRS.Domain.Dtos.UserDtos;
using CQRS.Domain.Queries.CourseQueries;
using CQRS.Domain.Queries.OrderQueries;
using CQRS.Domain.Queries.UserQueries;
using CQRS.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application
{
    public class Injector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region Repositories
            //Repository Dependency Injection
            services.AddScoped(typeof(ICustomRepository<>), typeof(CustomRepository<>));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            #endregion

            #region Commands
            //Command-CommandHandler Dependecy Injection
            services.AddScoped<IRequestHandler<CourseCreateCommand, Guid>, CourseCommandHandler>();
            services.AddScoped<IRequestHandler<CourseUpdateCommand, Guid>, CourseCommandHandler>();

            services.AddScoped<IRequestHandler<OrderCreateCommand, Guid>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<OrderUpdateCommand, Guid>, OrderCommandHandler>();

            services.AddScoped<IRequestHandler<UserCreateCommand, Guid>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<UserUpdateCommand, Guid>, UserCommandHandler>();
            #endregion

            #region Queries
            //Query-QueryHandler Dependecy Injection
            services.AddScoped<IRequestHandler<GetCoursesQuery, List<CourseDto>>, CourseQueryHandler>();
            services.AddScoped<IRequestHandler<GetCourseDetailQuery, CourseDto>, CourseQueryHandler>();
            services.AddScoped<IRequestHandler<GetOrdersQuery, List<OrderDto>>, OrderQueryHandler>();
            services.AddScoped<IRequestHandler<GetOrderDetailQuery, OrderDto>, OrderQueryHandler>();
            services.AddScoped<IRequestHandler<GetUsersQuery, List<UserDto>>, UserQueryHandler>();
            services.AddScoped<IRequestHandler<GetUserDetailQuery, UserDto>, UserQueryHandler>();
            #endregion
        }
    }
}
