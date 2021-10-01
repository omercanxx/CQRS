using CQRS.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Commands.OrderCommand
{
    public class OrderUpdateCommand : IRequest<CommandResult>
    {
        public OrderUpdateCommand(Guid id, Guid userId, Guid courseId, decimal price)
        {
            Id = id;
            UserId = userId;
            CourseId = courseId;
            Price = price;
        }
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public decimal Price { get; set; }
    }
}
