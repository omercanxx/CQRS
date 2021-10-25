using CQRS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Entities
{
    public class Order_Campaign : BaseEntity
    {
        public Order_Campaign(Guid orderId, Guid campaignId, string title, decimal amount, DiscountTypes discountType)
        {
            OrderId = orderId;
            CampaignId = campaignId;
            Title = title;
            Amount = amount;
            DiscountType = discountType;
        }
        public Guid OrderId { get; protected set; }
        public Guid CampaignId { get; protected set; }
        public string Title { get; protected set; }
        public decimal Amount { get; protected set; }
        public DiscountTypes DiscountType { get; protected set; }
        public virtual Order Order { get; protected set; }
        public virtual Campaign Campaign { get; protected set; }
    }
}
