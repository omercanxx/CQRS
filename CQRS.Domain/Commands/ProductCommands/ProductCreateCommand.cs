using CQRS.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CQRS.Domain.Commands.ProductCommands
{
    public class ProductCreateCommand : IRequest<CommandResult>
    {
        public ProductCreateCommand(string title, decimal price)
        {
            Title = title;
            Price = price;
        }
        public string Title { get; set; }
        public decimal Price { get; set; }
    }
}
