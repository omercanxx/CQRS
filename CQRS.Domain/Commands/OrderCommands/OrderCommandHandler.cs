using CQRS.Core;
using CQRS.Core.Entities;
using CQRS.Core.Entities.Mongo;
using CQRS.Core.Enums;
using CQRS.Core.Interfaces.CommandInterfaces;
using CQRS.Core.Interfaces.CommandInterfaces.Mongo;
using CQRS.Core.Interfaces.QueryInterfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OrderCommandHandler(ICommandOrderRepository orderRepository, IQueryProductRepository productRepository, IQueryCampaignRepository campaignRepository, ICommandMongoOrderRepository mongoOrderRepository, IHttpContextAccessor httpContextAccessor)
        {
            _orderRepository = orderRepository;
            _campaignRepository = campaignRepository;
            _productRepository = productRepository;
            _mongoOrderRepository = mongoOrderRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<CommandResult> Handle(OrderCreateCommand command, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "Id").Value;
            Guid parsedUserId = Guid.Parse(userId);

            Order order = new Order(parsedUserId);

            List<MongoProduct> mongoProducts = new List<MongoProduct>();
            List<MongoProductSale> productResults = new List<MongoProductSale>();
            decimal totalPrice = 0;
            
            foreach(var item in command.Products)
            {
                var dbProduct = await _productRepository.GetByIdAsync(item.Id);
                Order_Product orderProduct = new Order_Product(order.Id, dbProduct.Id, parsedUserId, item.Quantity, dbProduct.Title, dbProduct.Price);
                order.Order_Products.Add(orderProduct);

                //Mongo
                MongoProduct mongoProduct = new MongoProduct(dbProduct.Id.ToString(), dbProduct.Title, dbProduct.Price, item.Quantity);
                mongoProducts.Add(mongoProduct);
                totalPrice += (dbProduct.Price * item.Quantity);

                //Top 10 Products
                MongoProductSale productResult = new MongoProductSale(dbProduct.Id.ToString(), item.Quantity);
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


            MongoOrder mongoOrder = new MongoOrder(order.Id.ToString(), parsedUserId.ToString(), totalPrice, mongoProducts);
            await _mongoOrderRepository.InsertOneAsync(mongoOrder);

            return new CommandResult(order.Id, productResults);
        }

        
        public async Task<CommandResult> Handle(OrderDeleteCommand command, CancellationToken cancellationToken)
        {
            var dbOrder = await _orderRepository.GetByIdAsync(command.Id);

            _orderRepository.Remove(dbOrder);
            await _orderRepository.SaveChangesAsync();

            return new CommandResult(dbOrder.Id);
        }
    }
}
