using CQRS.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Commands.UserCommands
{
    public class UserProductItemInsertCommand : IRequest<CommandResult>
    {
        public UserProductItemInsertCommand(Guid userProductId, Guid productId)
        {
            UserProductId = userProductId;
            ProductId = productId;
        }
        public Guid UserProductId { get; set; }
        public Guid ProductId { get; set; }
    }
}
