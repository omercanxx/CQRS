using CQRS.API;
using CQRS.API.Configurations;
using CQRS.Core.Entities;
using CQRS.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            
            //Kirli datayı engellemek amaçlı inmemory kullanılıyor.
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("CQRSTestDb"));

            Log.Logger = new LoggerConfiguration().WriteTo.MSSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Database=Bootcamp; Integrated Security=True", "Logs").CreateLogger();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddDependencyInjectionSetup();
            services.AddAutoMapperSetup();
        }
        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var scope = app.ApplicationServices.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            Seed(context);

            base.Configure(app, env);
        }

        public void Seed(AppDbContext context)
        {
            for (int i = 1; i < 10; i++)
            {
                User user = new User($"{i}. isim", $"{i}. soyisim", $"{i}. email", $"{i}{i}{i}{i}{i}{i}", DateTime.Now.AddYears(-20));

                context.Users.Add(user);

                Course course = new Course($"{i}. ürün", i * 2 + 10);
                
                context.Courses.Add(course);

                context.SaveChanges();
            }
        }
    }
}
