using CQRS.Core;
using CQRS.Core.Entities;
using CQRS.Core.Interfaces;
using CQRS.Core.Interfaces.CommandInterfaces;
using CQRS.Core.Interfaces.QueryInterfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Domain.Commands.UserCommands
{
    public class UserCommandHandler : IRequestHandler<UserCreateCommand, CommandResult>,
                                      IRequestHandler<UserDeleteCommand, CommandResult>,
                                      IRequestHandler<UserProductCreateCommand, CommandResult>,
                                      IRequestHandler<UserProductUpdateCommand, CommandResult>,
                                      IRequestHandler<UserProductDeleteCommand, CommandResult>,
                                      IRequestHandler<UserProductItemInsertCommand, CommandResult>,
                                      IRequestHandler<UserProductItemRemoveCommand, CommandResult>
    {
        private readonly IQueryProductRepository _productRepository;
        private readonly ICommandUserProductRepository _commandUserProductRepository;
        private readonly ICommandUserProductItemRepository _commandUserProductItemRepository;
        private readonly IQueryUserProductRepository _queryUserProductRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserCommandHandler(IQueryProductRepository productRepository,
                                  ICommandUserProductRepository commandUserProductRepository,
                                  ICommandUserProductItemRepository commandUserProductItemRepository,
                                  IQueryUserProductRepository queryUserProductRepository,
                                  UserManager<User> userManager,
                                  SignInManager<User> signInManager,
                                  RoleManager<Role> roleManager,
                                  IHttpContextAccessor httpContextAccessor)
        {
            _productRepository = productRepository;
            _commandUserProductRepository = commandUserProductRepository;
            _commandUserProductItemRepository = commandUserProductItemRepository;
            _queryUserProductRepository = queryUserProductRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<CommandResult> Handle(UserCreateCommand command, CancellationToken cancellationToken)
        {
            // Kullanıcı password veritabanına şifrelenerek kaydedilmiştir. Encryption ve decryption işlemleri Core katmanında yapılmaktadır.
            User user = new User(command.Name, command.Surname, command.Email, command.Email);

            IdentityResult result = await _userManager.CreateAsync(user, command.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "user");
            }


            return new CommandResult(user.Id);
        }
        
        public async Task<CommandResult> Handle(UserDeleteCommand command, CancellationToken cancellationToken)
        {
            var dbUser = await _userManager.FindByIdAsync(command.Id.ToString());

            await _userManager.DeleteAsync(dbUser);

            return new CommandResult(dbUser.Id);
        }
        public async Task<CommandResult> Handle(UserProductCreateCommand command, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "Id").Value;
            Guid parsedUserId = Guid.Parse(userId);

            User_Product userProduct = new User_Product(parsedUserId, command.Name, command.Description);

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

            _commandUserProductRepository.Remove(dbUserProduct);
            await _commandUserProductRepository.SaveChangesAsync();

            return new CommandResult(dbUserProduct.Id);
        }
        public async Task<CommandResult> Handle(UserProductItemInsertCommand command, CancellationToken cancellationToken)
        {
            var dbUserProduct = await _queryUserProductRepository.FindAsyncWithUserProductItems(command.UserProductId);

            var dbProduct = await _productRepository.GetByIdAsync(command.ProductId);

            User_ProductItem userProductItem = new User_ProductItem(dbUserProduct.Id, command.ProductId, dbProduct.Title, dbProduct.Price);

            dbUserProduct.User_ProductItems.Add(userProductItem);

            _commandUserProductRepository.Update(dbUserProduct);
            await _commandUserProductRepository.SaveChangesAsync();

            return new CommandResult(dbUserProduct.UserId.ToString(), dbProduct.Id.ToString());
        }

        public async Task<CommandResult> Handle(UserProductItemRemoveCommand command, CancellationToken cancellationToken)
        {
            var dbUserProductItem = await _commandUserProductItemRepository.GetByIdAsync(command.Id);

            _commandUserProductItemRepository.Remove(dbUserProductItem);
            await _commandUserProductItemRepository.SaveChangesAsync();

            return new CommandResult(dbUserProductItem.Id);
        }

    }
}
