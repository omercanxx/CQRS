using CQRS.Core;
using CQRS.Core.Entities;
using CQRS.Core.Interfaces.CommandInterfaces;
using CQRS.Core.Interfaces.CommandInterfaces.Mongo;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Domain.Commands.OrderCommands
{
    public class OrderCommandHandler : IRequestHandler<OrderCreateCommand, CommandResult>,
                                       IRequestHandler<OrderDeleteCommand, CommandResult>
    {
        private readonly ICommandOrderRepository _orderRepository;
        private readonly ICommandMongoOrderRepository _mongoOrderRepository;
        public OrderCommandHandler(ICommandOrderRepository orderRepository, ICommandMongoOrderRepository mongoOrderRepository)
        {
            _orderRepository = orderRepository;
            _mongoOrderRepository = mongoOrderRepository;
        }
        public async Task<CommandResult> Handle(OrderCreateCommand command, CancellationToken cancellationToken)
        {
            Order order = new Order(command.UserId);

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();

            return new CommandResult(order.Id);
        }

        
        public async Task<CommandResult> Handle(OrderDeleteCommand command, CancellationToken cancellationToken)
        {
            var dbOrder = await _orderRepository.GetByIdAsync(command.Id);

            await _orderRepository.RemoveAsync(dbOrder);
            await _orderRepository.SaveChangesAsync();

            return new CommandResult(dbOrder.Id);
        }
    }
}
