using CQRS.Core;
using CQRS.Core.Entities;
using CQRS.Core.Interfaces;
using CQRS.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Domain.Commands.OrderCommand
{
    public class OrderCommandHandler : IRequestHandler<OrderCreateCommand, CommandResult>,
                                       IRequestHandler<OrderUpdateCommand, CommandResult>,
                                       IRequestHandler<OrderDeleteCommand, CommandResult>
    {
        private readonly IOrderRepository _orderRepository;
        public OrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<CommandResult> Handle(OrderCreateCommand command, CancellationToken cancellationToken)
        {
            Order order = new Order(command.UserId, command.CourseId, command.Price);

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();

            //CommandResultun Id propertysine set edilen constructor ın içerisine girecektir.
            return new CommandResult(order.Id);
        }

        public async Task<CommandResult> Handle(OrderUpdateCommand command, CancellationToken cancellationToken)
        {
            var dbOrder = await _orderRepository.GetByIdAsync(command.Id);

            dbOrder.UpdateUser(command.UserId);
            dbOrder.UpdateCourse(command.CourseId);
            dbOrder.UpdatePrice(command.Price);


            _orderRepository.Update(dbOrder);
            await _orderRepository.SaveChangesAsync();

            return new CommandResult(dbOrder.Id);
        }

        public async Task<CommandResult> Handle(OrderDeleteCommand command, CancellationToken cancellationToken)
        {
            var dbOrder = await _orderRepository.GetByIdAsync(command.Id);

            _orderRepository.Deactivate(dbOrder);

            return new CommandResult(dbOrder.Id);
        }
    }
}
