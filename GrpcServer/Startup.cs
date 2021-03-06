using CQRS.API.Configurations;
using CQRS.Application;
using CQRS.Application.RabbitMq;
using CQRS.Application.RabbitMq.Orders;
using CQRS.Core.Entities;
using CQRS.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var rabbitConfig = Configuration.GetSection("rabbit");
            services.Configure<RabbitMqConfiguration>(rabbitConfig);
            
            services.AddGrpc();
            services.AddAutoMapper(typeof(Startup));


            //appsettings içerisindeki database connection string burada kullanılıyor.
            services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("DevConnection")));
            services.AddIdentity<User, Role>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


            services.AddMediatR(typeof(Startup));
            //services.AddMediatR(Assembly.GetExecutingAssembly());

            services.Configure<MongoDatabaseSettings>(Configuration.GetSection(nameof(MongoDatabaseSettings)));
            services.AddSingleton<IMongoDatabaseSettings>(x => x.GetRequiredService<IOptions<MongoDatabaseSettings>>().Value);

            //DependencyInjection ve Automapper gibi yapılar configurations içerisine extension yazılarak proje daha modüler bir yapı haline getiriliyor.
            services.AddDependencyInjectionSetup();
            services.AddAutoMapperSetup();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<OrdersService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
