using CQRS.API.Configurations;
using CQRS.Application;
using CQRS.Application.AutoMapper;
using CQRS.Application.RabbitMq;
using CQRS.Application.RabbitMq.Orders;
using CQRS.Application.RabbitMq.Products;
using CQRS.Core.Entities.Mongo;
using CQRS.Infrastructure;
using CQRS.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
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
        [Obsolete]
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddHostedService<ConsumerOrderProductMessage>();
            //services.AddSingleton<ConsumerOrderProductMessage>();
            var rabbitConfig = Configuration.GetSection("rabbit");
            services.Configure<RabbitMqConfiguration>(rabbitConfig);
            
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CQRS.API", Version = "v1" });
            });

            //appsettings içerisindeki database connection string burada kullanılıyor.
            services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("DevConnection")));

            services.AddMediatR(typeof(Startup));

            services.Configure<MongoDatabaseSettings>(Configuration.GetSection(nameof(MongoDatabaseSettings)));
            services.AddSingleton<IMongoDatabaseSettings>(x => x.GetRequiredService<IOptions<MongoDatabaseSettings>>().Value);

            //DependencyInjection ve Automapper gibi yapılar configurations içerisine extension yazılarak proje daha modüler bir yapı haline getiriliyor.
            services.AddDependencyInjectionSetup();
            services.AddAutoMapperSetup();

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CQRS.API v1"));
            }
            app.UseHttpsRedirection();

            //var svc = app.ApplicationServices.GetService<ConsumerOrderProductMessage>();
            //var svc2 = app.ApplicationServices.GetService<ConsumerFavoriteProductMessage>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
