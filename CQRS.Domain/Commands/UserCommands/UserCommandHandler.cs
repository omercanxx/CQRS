using CQRS.Core;
using CQRS.Core.Entities;
using CQRS.Core.Interfaces;
using CQRS.Core.Interfaces.CommandInterfaces;
using CQRS.Core.Interfaces.QueryInterfaces;
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
                                      IRequestHandler<UserDeleteCommand, CommandResult>,
                                      IRequestHandler<UserProductCreateCommand, CommandResult>,
                                      IRequestHandler<UserProductUpdateCommand, CommandResult>,
                                      IRequestHandler<UserProductDeleteCommand, CommandResult>,
                                      IRequestHandler<UserProductItemInsertCommand, CommandResult>,
                                      IRequestHandler<UserProductItemRemoveCommand, CommandResult>
    {
        private readonly ICommandUserRepository _userRepository;
        private readonly IQueryProductRepository _productRepository;
        private readonly ICommandUserProductRepository _commandUserProductRepository;
        private readonly ICommandUserProductItemRepository _commandUserProductItemRepository;
        private readonly IQueryUserProductRepository _queryUserProductRepository;
        public UserCommandHandler(ICommandUserRepository userRepository, IQueryProductRepository productRepository,ICommandUserProductRepository commandUserProductRepository, ICommandUserProductItemRepository commandUserProductItemRepository, IQueryUserProductRepository queryUserProductRepository)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _commandUserProductRepository = commandUserProductRepository;
            _commandUserProductItemRepository = commandUserProductItemRepository;
            _queryUserProductRepository = queryUserProductRepository;
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
        public async Task<CommandResult> Handle(UserProductCreateCommand command, CancellationToken cancellationToken)
        {
            User_Product userProduct = new User_Product(command.UserId, command.Name, command.Description);

            await _commandUserProductRepository.AddAsync(userProduct);
            await _commandUserProductRepository.SaveChangesAsync();

            return new CommandResult(userProduct.Id);
        }
        public async Task<CommandResult> Handle(UserProductUpdateCommand command, CancellationToken cancellationToken)
        {

            var dbUserProduct = await _commandUserProductRepository.GetByIdAsync(command.Id);

            dbUserProduct.UpdateName(command.Name);
            dbUserProduct.UpdateDescription(command.Description);
            

            _commandUserProductRepository.Update(dbUserProduct);
            await _commandUserProductRepository.SaveChangesAsync();

            return new CommandResult(dbUserProduct.Id);
        }
        public async Task<CommandResult> Handle(UserProductDeleteCommand command, CancellationToken cancellationToken)
        {
            var dbUserProduct = await _commandUserProductRepository.GetByIdAsync(command.Id);

            await _commandUserProductRepository.RemoveAsync(dbUserProduct);
            await _commandUserProductRepository.SaveChangesAsync();

            return new CommandResult(dbUserProduct.Id);
        }
        public async Task<CommandResult> Handle(UserProductItemInsertCommand command, CancellationToken cancellationToken)
        {
            var dbUserProduct = await _queryUserProductRepository.FindAsyncWithUserProductItems(command.UserProductId);

            var dbProduct = await _productRepository.GetByIdAsync(command.ProductId);

            User_ProductItem userProductItem = new User_ProductItem(dbUserProduct.Id , command.ProductId, dbProduct.Title, dbProduct.Price);

            dbUserProduct.User_ProductItems.Add(userProductItem);

            _commandUserProductRepository.Update(dbUserProduct);
            await _commandUserProductRepository.SaveChangesAsync();

            return new CommandResult(dbUserProduct.UserId.ToString(), dbProduct.Id.ToString());
        }

        public async Task<CommandResult> Handle(UserProductItemRemoveCommand command, CancellationToken cancellationToken)
        {
            var dbUserProductItem = await _commandUserProductItemRepository.GetByIdAsync(command.Id);

            await _commandUserProductItemRepository.RemoveAsync(dbUserProductItem);
            await _commandUserProductItemRepository.SaveChangesAsync();

            return new CommandResult(dbUserProductItem.Id);
        }

    }
}
