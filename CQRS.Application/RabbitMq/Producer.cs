using CQRS.Core.Entities.Mongo;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CQRS.Application.RabbitMq
{
    public class Producer
    {
        private readonly List<MongoProductResult> _products;
        public Producer(List<MongoProductResult> products)
        {
            _products = products;
        }
        public async Task Produce()
        {
            string rabbitMqUri = "192.168.1.27";
            string queue = "product-queue";
            string userName = "admin";
            string password = "123456";

            var bus = Bus.Factory.CreateUsingRabbitMq(factory =>
            {
                factory.Host(rabbitMqUri, configurator =>
                {
                    configurator.Username(userName);
                    configurator.Password(password);
                });
            });

            var sendToUri = new Uri($"queue:{queue}");
            var endPoint = await bus.GetSendEndpoint(sendToUri);

            Message message = new Message(JsonSerializer.Serialize(_products));

            await Task.Run(async () =>
            {
                await endPoint.Send<IMessage>(message: message);
            });
        }
    }
}
