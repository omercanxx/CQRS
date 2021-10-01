using CQRS.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CQRS.Domain.Commands.CourseCommands
{
    public class CourseCreateCommand : IRequest<CommandResult>
    {
        public CourseCreateCommand(string title, decimal price)
        {
            Title = title;
            Price = price;
        }

        public string Title { get; set; }
        public decimal Price { get; set; }
    }
}
