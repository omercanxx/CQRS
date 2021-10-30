using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Entities
{
    public class User_ProductItem : BaseEntity
    {
        public User_ProductItem(Guid userProductId, Guid productId, string title, decimal price)
        {
            UserProductId = userProductId;
            ProductId = productId;
            Title = title;
            Price = price;
        }
        public Guid UserProductId { get; protected set; }
        public Guid ProductId { get; protected set; }
        public string Title { get; protected set; }
        public decimal Price { get; protected set; }
        public virtual User_Product User_Product { get; protected set; }
        public virtual Product Product { get; protected set; }
    }
}