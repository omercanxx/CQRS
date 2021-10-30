using CQRS.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Commands.UserCommands
{
    public class UserProductCreateCommand : IRequest<CommandResult>
    {
        public UserProductCreateCommand(Guid userId, string name, string description)
        {
            UserId = userId;
            Name = name;
            Description = description;
        }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
