using CQRS.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Commands.OrderCommand
{
    public class OrderCreateCommand : IRequest<CommandResult>
    {
        public OrderCreateCommand(Guid userId, Guid courseId, decimal price)
        {
            UserId = userId;
            CourseId = courseId;
            Price = price;
        }

        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public decimal Price { get; set; }
    }
}
