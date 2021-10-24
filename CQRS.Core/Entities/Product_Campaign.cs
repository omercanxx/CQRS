using CQRS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Entities
{
    public class Product_Campaign : BaseEntity
    {
        public Product_Campaign(Guid productId, Guid campaignId, string title, decimal amount, DiscountTypes discountType)
        {
            ProductId = productId;
            CampaignId = campaignId;
            Title = title;
            Amount = amount;
            DiscountType = discountType;
        }
        public Guid ProductId { get; protected set; }
        public Guid CampaignId { get; protected set; }
        public string Title { get; protected set; }
        public decimal Amount { get; protected set; }
        public DiscountTypes DiscountType { get; protected set; }
        public virtual Product Product { get; protected set; }
        public virtual Campaign Campaign { get; protected set; }
    }
}
