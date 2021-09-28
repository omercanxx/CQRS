using CQRS.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Domain.Commands.UserCommand
{
    public class UserCommandHandler : IRequestHandler<UserCreateCommand, Guid>,
                                        IRequestHandler<UserUpdateCommand, Guid>
    {
        private readonly AppDbContext _context;
        public UserCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
