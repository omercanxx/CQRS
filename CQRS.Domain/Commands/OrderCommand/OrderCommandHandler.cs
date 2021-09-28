using CQRS.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Domain.Commands.OrderCommand
{
    public class OrderCommandHandler : IRequestHandler<OrderCreateCommand, Guid>,
                                        IRequestHandler<OrderUpdateCommand, Guid>
    {
        private readonly AppDbContext _context;
        public OrderCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> Handle(OrderUpdateCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
