using CQRS.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Commands.UserCommands
{
    public class UserProductUpdateCommand : IRequest<CommandResult>
    {
        public UserProductUpdateCommand(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

