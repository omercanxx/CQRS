using CQRS.Core.Entities.Mongo;
using CQRS.Core.Interfaces.CommandInterfaces.Mongo;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CQRS.Application.RabbitMq
{
    public class Consumer
    {
        public class Message : IMessage
        {
            private readonly ICommandMongoProductResultRepository _commandProductRepository;
            public Message(ICommandMongoProductResultRepository commandProductRepository)
            {
                _commandProductRepository = commandProductRepository;
            }
            public string Text { get; set; }
        }
        public class MessageConsumer : IConsumer<Message>  
        {
            
            public async Task Consume(ConsumeContext<IMessage> context)
            {
                var x = JsonSerializer.Deserialize<List<MongoProductResult>>(context.Message.Text);
                
            }

            public Task Consume(ConsumeContext<Message> context)
            {
                throw new NotImplementedException();
            }
        }
        public class Program
        {
            static async Task Main(string[] args)
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

                    factory.ReceiveEndpoint(queue, endpoint =>
                    {
                        endpoint.Consumer<MessageConsumer>();
                    });
                });
                await bus.StartAsync();

            }
    }
}

}
