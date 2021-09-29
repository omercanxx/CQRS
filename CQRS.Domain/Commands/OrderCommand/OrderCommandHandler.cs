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
    public class OrderCommandHandler : IRequestHandler<OrderCreateCommand, Guid>,
                                        IRequestHandler<OrderUpdateCommand, Guid>
    {
        private readonly IOrderRepository _orderRepository;
        public OrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Guid> Handle(OrderCreateCommand command, CancellationToken cancellationToken)
        {
            Order course = new Order(command.UserId, command.CourseId, command.Price);

            await _orderRepository.AddAsync(course);
            await _orderRepository.SaveChangesAsync();

            return course.Id;
        }

        public async Task<Guid> Handle(OrderUpdateCommand command, CancellationToken cancellationToken)
        {
            var dbOrder = await _orderRepository.GetByIdAsync(command.Id);

            dbOrder.UpdateUser(command.UserId);
            dbOrder.UpdateCourse(command.CourseId);
            dbOrder.UpdatePrice(command.Price);


            _orderRepository.Update(dbOrder);
            await _orderRepository.SaveChangesAsync();

            return dbOrder.Id;
        }
    }
}
