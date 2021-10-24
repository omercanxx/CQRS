using CQRS.Core;
using CQRS.Core.Entities;
using CQRS.Core.Interfaces;
using CQRS.Core.Interfaces.CommandInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Application
{
    public class OrderService : BackgroundService
    {
        private IServiceProvider _sp;
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;

        public OrderService(IServiceProvider sp)
        {
            _sp = sp;

            _factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "admin",
                Password = "123456"
            };

            _connection = _factory.CreateConnection();

            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
                queue: "product-sales",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (stoppingToken.IsCancellationRequested)
            {
                _channel.Dispose();
                _connection.Dispose();
                return Task.CompletedTask;
            }

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();

                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine(" [x] Received {0}", message);


                Task.Run(() =>
                {
                    var order = new User("rabbit mq", "test", "test@gmail.com", "asd123", DateTime.Now);

                    using (var scope = _sp.CreateScope())
                    {
                        var orderRepository = scope.ServiceProvider.GetRequiredService<ICommandUserRepository>();
                        orderRepository.AddAsync(order);
                    }
                });
            };

            _channel.BasicConsume(queue: "product-sales", autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }
    }
}