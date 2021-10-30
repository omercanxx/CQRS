using CQRS.Core;
using CQRS.Core.Entities;
using CQRS.Core.Entities.Mongo;
using CQRS.Core.Enums;
using CQRS.Core.Interfaces.CommandInterfaces;
using CQRS.Core.Interfaces.CommandInterfaces.Mongo;
using CQRS.Core.Interfaces.QueryInterfaces;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Domain.Commands.OrderCommands
{
    public class OrderCommandHandler : IRequestHandler<OrderCreateCommand, CommandResult>,
                                       IRequestHandler<OrderDeleteCommand, CommandResult>
    {
        private readonly ICommandOrderRepository _orderRepository;
        private readonly IQueryCampaignRepository _campaignRepository;
        private readonly IQueryProductRepository _productRepository;
        private readonly ICommandMongoOrderRepository _mongoOrderRepository;
        public OrderCommandHandler(ICommandOrderRepository orderRepository, IQueryProductRepository productRepository, IQueryCampaignRepository campaignRepository, ICommandMongoOrderRepository mongoOrderRepository)
        {
            _orderRepository = orderRepository;
            _campaignRepository = campaignRepository;
            _productRepository = productRepository;
            _mongoOrderRepository = mongoOrderRepository;
        }
        public async Task<CommandResult> Handle(OrderCreateCommand command, CancellationToken cancellationToken)
        {
            Order order = new Order(command.UserId);

            List<MongoProduct> mongoProducts = new List<MongoProduct>();
            List<MongoProductResult> productResults = new List<MongoProductResult>();
            decimal totalPrice = 0;
            
            foreach(var item in command.Products)
            {
                var dbProduct = await _productRepository.GetByIdAsync(item.Id);
                Order_Product orderProduct = new Order_Product(order.Id, dbProduct.Id, command.UserId, item.Quantity, dbProduct.Title, dbProduct.Price);
                order.Order_Products.Add(orderProduct);

                //Mongo
                MongoProduct mongoProduct = new MongoProduct(dbProduct.Id.ToString(), dbProduct.Title, dbProduct.Price, item.Quantity);
                mongoProducts.Add(mongoProduct);
                totalPrice += (dbProduct.Price * item.Quantity);

                //Top 10 Products
                MongoProductResult productResult = new MongoProductResult(dbProduct.Id.ToString(), item.Quantity);
                productResults.Add(productResult);
            }

            if(command.CampaignId != null)
            {
                var dbCampaign =await  _campaignRepository.GetByIdAsync((Guid)command.CampaignId);
                Order_Campaign orderCampaign = new Order_Campaign(order.Id, (Guid)command.CampaignId, dbCampaign.Title, dbCampaign.Amount, dbCampaign.DiscountType);
                order.Order_Campaigns.Add(orderCampaign);

                if (dbCampaign.DiscountType == DiscountTypes.Quantitative)
                    totalPrice -= dbCampaign.Amount;

                else
                {
                    var discount = (totalPrice * dbCampaign.Amount) / 100;
                    totalPrice -= discount;
                }
            }

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();


            MongoOrder mongoOrder = new MongoOrder(order.Id.ToString(), command.UserId.ToString(), totalPrice, mongoProducts);
            await _mongoOrderRepository.InsertOneAsync(mongoOrder);

            return new CommandResult(order.Id, productResults);
        }

        
        public async Task<CommandResult> Handle(OrderDeleteCommand command, CancellationToken cancellationToken)
        {
            var dbOrder = await _orderRepository.GetByIdAsync(command.Id);

            await _orderRepository.RemoveAsync(dbOrder);
            await _orderRepository.SaveChangesAsync();

            return new CommandResult(dbOrder.Id);
        }
    }
}
