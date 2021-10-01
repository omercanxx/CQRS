using CQRS.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Commands.UserCommand
{
    public class UserDeleteCommand : IRequest<CommandResult>
    {
        public UserDeleteCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}

