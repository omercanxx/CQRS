using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Entities.Mongo
{
    public class MongoOrder : MongoBaseEntity
    {
        public MongoOrder(string orderId, string userId, decimal totalPrice, List<MongoProduct> products)
        {
            OrderId = orderId;
            UserId = userId;
            TotalPrice = totalPrice;
            Products = products;
        }
        public string OrderId { get; protected set; }
        public string UserId { get; protected set; }
        public decimal TotalPrice { get; protected set; }
        public List<MongoProduct> Products { get; protected set; }
    }
}
