using CQRS.Core;
using CQRS.Domain.Dtos.OrderDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Commands.OrderCommands
{
    public class OrderCreateCommand : IRequest<CommandResult>
    {
        public OrderCreateCommand(Guid userId, Guid? campaignId, List<Order_ProductDto> products)
        {
            UserId = userId;
            CampaignId = campaignId;
            Products = products;
        }

        public Guid UserId { get; set; }
        public Guid? CampaignId { get; set; }
        public List<Order_ProductDto> Products { get; set; }
    }
}
