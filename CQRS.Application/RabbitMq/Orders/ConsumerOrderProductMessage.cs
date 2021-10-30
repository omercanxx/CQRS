using CQRS.Core.Entities.Mongo;
using CQRS.Core.Interfaces.CommandInterfaces.Mongo;
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
    public class ConsumerOrderProductMessage : BackgroundService
    {
        private readonly string _hostname;
        private readonly string _password;
        private readonly string _username;
        private IConnection _connection;
        private IModel _channel;

        private readonly ICommandMongoProductResultRepository _productRepository;
        public ConsumerOrderProductMessage(IOptions<RabbitMqConfiguration> rabbitMqOptions, IServiceProvider serviceProvider)
        {
            _hostname = rabbitMqOptions.Value.Hostname;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
            _productRepository = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ICommandMongoProductResultRepository>();
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
            _channel.QueueDeclare(queue: "order-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);

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
                    var mongoProducts = JsonSerializer.Deserialize<List<MongoProductResult>>(message);
                    foreach (var item in mongoProducts)
                    {
                        await _productRepository.InsertOneAsync(item);
                    }
                }

            };
            _channel.BasicConsume("order-queue", false, consumer);

        }
    }
}