using CQRS.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Commands.ProductCommands
{
    public class ProductDeleteCommand : IRequest<CommandResult>
    {
        public ProductDeleteCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
