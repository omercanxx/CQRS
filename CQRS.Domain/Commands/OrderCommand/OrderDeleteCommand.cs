using CQRS.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Commands.OrderCommand
{
    public class OrderDeleteCommand : IRequest<CommandResult>
    {
        public OrderDeleteCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}

