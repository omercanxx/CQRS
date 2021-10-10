using CQRS.Core;
using CQRS.Core.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Commands.CampaignCommands
{
    public class CampaignUpdateCommand : IRequest<CommandResult>
    {
        public CampaignUpdateCommand(Guid id, string title, decimal amount, DiscountTypes discountType)
        {
            Id = id;
            Title = title;
            Amount = amount;
            DiscountType = discountType;
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public DiscountTypes DiscountType { get; set; }
    }
}