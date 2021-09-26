using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CQRS.Domain.Commands.CourseCommand
{
    public class CourseCreateCommand : IRequest<Guid>
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
