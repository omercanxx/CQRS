using CQRS.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Commands.CourseCommands
{
    public class CourseUpdateCommand : IRequest<CommandResult>
    {
        public CourseUpdateCommand(Guid id, string title, decimal price)
        {
            Id = id;
            Title = title;
            Price = price;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
    }
}
