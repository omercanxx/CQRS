using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Entities
{
    public class Order_Product : BaseEntity
    {
        public Order_Product(Guid orderId, Guid productId, Guid userId, string title, decimal price)
        {
            OrderId = orderId;
            ProductId = productId;
            UserId = userId;
            Title = title;
            Price = price;
        }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; protected set; }
        public decimal Price { get; protected set; }
        public virtual Order Order { get; protected set; }
        public virtual Product Product { get; protected set; }
        public virtual User User { get; protected set; }
    }
}
