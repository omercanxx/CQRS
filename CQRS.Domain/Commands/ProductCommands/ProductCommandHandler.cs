using CQRS.Core;
using CQRS.Core.Entities;
using CQRS.Core.Interfaces;
using CQRS.Core.Interfaces.CommandInterfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Domain.Commands.ProductCommands
{
    //Projede commandların handle edildiği CommandHandlerlar kullanılmıştır. Injector classı içerisinde implementasyonu gözükmektedir.
    public class ProductCommandHandler : IRequestHandler<ProductCreateCommand, CommandResult>,
                                        IRequestHandler<ProductUpdateCommand, CommandResult>,
                                        IRequestHandler<ProductDeleteCommand, CommandResult>
    {
        private readonly ICommandProductRepository _productRepository;
        public ProductCommandHandler(ICommandProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<CommandResult> Handle(ProductCreateCommand command, CancellationToken cancellationToken)
        {
            Product product = null;

            if (command.Id == Guid.Empty)
                product = new Product(command.Title, command.Price);

            else
                product = new Product(command.Id, command.Title, command.Price);

            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();

            //CommandResultun Id ve Title propertysine set edilen constructor ın içerisine girecektir.
            return new CommandResult(product.Id);
        }

        public async Task<CommandResult> Handle(ProductUpdateCommand command, CancellationToken cancellationToken)
        {
            var dbProduct = await _productRepository.GetByIdAsync(command.Id);
            bool isDiscounted = false;
            if (dbProduct.Price > command.Price)
                isDiscounted = true;
            dbProduct.UpdatePrice(command.Price);
            dbProduct.UpdateTitle(command.Title);

            _productRepository.Update(dbProduct);


            await _productRepository.SaveChangesAsync();

            return new CommandResult(dbProduct.Id.ToString(), isDiscounted);
        }

        public async Task<CommandResult> Handle(ProductDeleteCommand command, CancellationToken cancellationToken)
        {
            var dbProduct = await _productRepository.GetByIdAsync(command.Id);

            _productRepository.Remove(dbProduct);
            await _productRepository.SaveChangesAsync();

            return new CommandResult(dbProduct.Id);
        }
    }
}
