using CQRS.Core.Entities;
using CQRS.Core.Interfaces;
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
        private readonly IUserRepository _userRepository;
        public UserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Guid> Handle(UserCreateCommand command, CancellationToken cancellationToken)
        {
            User user = new User(command.Name, command.Surname, command.Email, command.Password, command.Birthdate);

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return user.Id;
        }

        public async Task<Guid> Handle(UserUpdateCommand command, CancellationToken cancellationToken)
        {
            var dbUser = await _userRepository.GetByIdAsync(command.Id);

            dbUser.UpdateEmail(command.Email);
            dbUser.UpdateName(command.Name);
            dbUser.UpdateSurname(command.Surname);
            dbUser.UpdatePassword(command.Password);


            _userRepository.Update(dbUser);
            await _userRepository.SaveChangesAsync();

            return dbUser.Id;
        }
    }
}
