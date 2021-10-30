using CQRS.Core;
using CQRS.Core.Entities.Mongo;
using CQRS.Core.Interfaces.QueryInterfaces;
using CQRS.Core.Interfaces.QueryInterfaces.Mongo;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Application.RabbitMq.Orders
{
    public class ConsumerProductMessage : BackgroundService
    {
        private readonly string _hostname;
        private readonly string _password;
        private readonly string _username;
        private IConnection _connection;
        private IModel _channel;
        private readonly IQueryMongoUserProductRepository _mongoProductRepository;
        private readonly IQueryUserRepository _userRepository;
        private readonly IQueryProductRepository _productRepository;
        private readonly ISendMail _sendMail;
        public ConsumerProductMessage(IOptions<RabbitMqConfiguration> rabbitMqOptions, IServiceProvider serviceProvider, ISendMail sendMail)
        {
            _hostname = rabbitMqOptions.Value.Hostname;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
            _mongoProductRepository = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IQueryMongoUserProductRepository>();
            _userRepository = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IQueryUserRepository>();
            _productRepository = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IQueryProductRepository>();
            _sendMail = sendMail;
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "product-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            // Received event'i sürekli listen modunda olacaktır.
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());

                if (message != "null" && message != "" && message != null)
                {
                    var productId = JsonSerializer.Deserialize<string>(message);
                    var dbProduct = await _productRepository.GetByIdAsync(Guid.Parse(productId));

                    var dbUserIds = _mongoProductRepository.FilterBy(x => x.ProductId == productId).Select(x => x.UserId).Distinct().ToList();
                    List<Guid> dbUserGuids = dbUserIds.Cast<Guid>().ToList();
                    var dbUserEmails = await _userRepository.GetEmailsByIds(dbUserGuids);

                    foreach(var userEmail in dbUserEmails)
                    {
                        _sendMail.Send(userEmail, "İndirim!", $"Listenize eklediğiniz {dbProduct.Title} isimli ürün indirime girmiştir!" );
                    }
                    
                }

            };
            _channel.BasicConsume("product-queue", false, consumer);

        }
    }
}