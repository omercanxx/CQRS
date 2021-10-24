using CQRS.Core;
using CQRS.Core.Entities;
using CQRS.Core.Interfaces;
using CQRS.Core.Interfaces.CommandInterfaces;
using CQRS.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Domain.Commands.UserCommands
{
    public class UserCommandHandler : IRequestHandler<UserCreateCommand, CommandResult>,
                                      IRequestHandler<UserUpdateCommand, CommandResult>,
                                      IRequestHandler<UserDeleteCommand, CommandResult>
    {
        private readonly ICommandUserRepository _userRepository;
        public UserCommandHandler(ICommandUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<CommandResult> Handle(UserCreateCommand command, CancellationToken cancellationToken)
        {
            // Kullanıcı password veritabanına şifrelenerek kaydedilmiştir. Encryption ve decryption işlemleri Core katmanında yapılmaktadır.
            User user = new User(command.Name, command.Surname, command.Email, EncryptionHelper.EncryptPlainTextToCipherText(command.Password), command.Birthdate);

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return new CommandResult(user.Id);
        }

        public async Task<CommandResult> Handle(UserUpdateCommand command, CancellationToken cancellationToken)
        {
            var dbUser = await _userRepository.GetByIdAsync(command.Id);

            dbUser.UpdateEmail(command.Email);
            dbUser.UpdateName(command.Name);
            dbUser.UpdateSurname(command.Surname);
            dbUser.UpdatePassword(EncryptionHelper.EncryptPlainTextToCipherText(command.Password));


            _userRepository.Update(dbUser);
            await _userRepository.SaveChangesAsync();

            return new CommandResult(dbUser.Id);
        }

        public async Task<CommandResult> Handle(UserDeleteCommand command, CancellationToken cancellationToken)
        {
            var dbUser = await _userRepository.GetByIdAsync(command.Id);

            await _userRepository.RemoveAsync(dbUser);
            await _userRepository.SaveChangesAsync();

            return new CommandResult(dbUser.Id);
        }
    }
}
