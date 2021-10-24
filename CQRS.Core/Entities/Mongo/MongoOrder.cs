using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Entities.Mongo
{
    public class MongoOrder : MongoBaseEntity
    {
        public MongoOrder(string orderId, string userId, List<MongoProduct> products)
        {
            OrderId = orderId;
            UserId = userId;
            Products = products;
        }
        public string OrderId { get; protected set; }
        public string UserId { get; protected set; }
        public decimal TotalPrice => Products.Sum(x => x.Price);
        public List<MongoProduct> Products { get; protected set; }
    }
}
