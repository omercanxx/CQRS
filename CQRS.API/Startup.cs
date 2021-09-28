using CQRS.Application.AutoMapper;
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
using CQRS.Infrastructure;
using CQRS.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CQRS.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            
            

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CQRS.API", Version = "v1" });
            });

            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(typeof(MapProfile));

            services.AddScoped(typeof(ICustomRepository<>), typeof(CustomRepository<>));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            #region Commands
            services.AddScoped<IRequestHandler<CourseCreateCommand, Guid>, CourseCommandHandler>();
            services.AddScoped<IRequestHandler<CourseUpdateCommand, Guid>, CourseCommandHandler>();

            services.AddScoped<IRequestHandler<OrderCreateCommand, Guid>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<OrderUpdateCommand, Guid>, OrderCommandHandler>();

            services.AddScoped<IRequestHandler<UserCreateCommand, Guid>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<UserUpdateCommand, Guid>, UserCommandHandler>();
            #endregion

            #region Queries
            services.AddScoped<IRequestHandler<GetCoursesQuery, List<CourseDto>>, CourseQueryHandler>();
            services.AddScoped<IRequestHandler<GetCourseDetailQuery, CourseDto>, CourseQueryHandler>();
            services.AddScoped<IRequestHandler<GetOrdersQuery, List<OrderDto>>, OrderQueryHandler>();
            services.AddScoped<IRequestHandler<GetOrderDetailQuery, OrderDto>, OrderQueryHandler>();
            services.AddScoped<IRequestHandler<GetUsersQuery, List<UserDto>>, UserQueryHandler>();
            services.AddScoped<IRequestHandler<GetUserDetailQuery, UserDto>, UserQueryHandler>();
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CQRS.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
