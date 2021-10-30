using CQRS.Core.Entities.Mongo;
using CQRS.Core.Interfaces.CommandInterfaces.Mongo;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Application.RabbitMq.Users
{
    public class ConsumerUserProductMessage : BackgroundService
    {
        private readonly string _hostname;
        private readonly string _password;
        private readonly string _username;
        private IConnection _connection;
        private IModel _channel;

        private readonly ICommandMongoUserProductRepository _userProductRepository;
        public ConsumerUserProductMessage(IOptions<RabbitMqConfiguration> rabbitMqOptions, IServiceProvider serviceProvider)
        {
            _hostname = rabbitMqOptions.Value.Hostname;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
            _userProductRepository = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ICommandMongoUserProductRepository>();
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
            _channel.QueueDeclare(queue: "user-product-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);

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
                    var mongoUserProduct = JsonSerializer.Deserialize<MongoUserProduct>(message);
                    await _userProductRepository.InsertOneAsync(mongoUserProduct);
                }

            };
            _channel.BasicConsume("user-product-queue", false, consumer);

        }
    }
}