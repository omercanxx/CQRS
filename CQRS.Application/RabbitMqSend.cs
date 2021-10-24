using CQRS.Core;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CQRS.Application
{
    public class RabbitMqSend
    {
        public void Connect(CommandResult result)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "admin",
                Password = "123456"
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "product-sales",
                                    durable: false,
                                    autoDelete: false,
                                    arguments: null);

                string json = JsonSerializer.Serialize(result);
                channel.BasicPublish(body: Encoding.UTF8.GetBytes(json),
                                    exchange: "", 
                                    routingKey: "product-sales",
                                    basicProperties: null);
            }
        }
    }
}
