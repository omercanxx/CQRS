﻿using CQRS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Entities
{
    public class Campaign : BaseEntity
    {
        public Campaign(string title, decimal amount, DiscountTypes discountType)
        {
            Title = title;
            Amount = amount;
            DiscountType = discountType;
            Product_Campaigns = new HashSet<Product_Campaign>();
        }
        public string Title { get; protected set; }
        public decimal Amount { get; protected set; }
        public DiscountTypes DiscountType { get; protected set; }
        public void UpdateTitle(string title)
        {
            Title = title;
        }
        public void UpdateAmount(decimal amount)
        {
            Amount = amount;
        }
        public void UpdateDiscountType(DiscountTypes discountType)
        {
            DiscountType = discountType;
        }
        public virtual ICollection<Product_Campaign> Product_Campaigns { get; protected set; }
    }
}
