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
        public ProductCreateCommand(Guid id,string title, decimal price)
        {
            Id = id;
            Title = title;
            Price = price;
        }
        public ProductCreateCommand(string title, decimal price)
        {
            Title = title;
            Price = price;
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
    }
}
