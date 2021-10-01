using CQRS.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Commands.CourseCommands
{
    public class CourseDeleteCommand : IRequest<CommandResult>
    {
        public CourseDeleteCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
