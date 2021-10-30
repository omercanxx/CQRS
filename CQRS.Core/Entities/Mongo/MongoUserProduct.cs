using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Entities.Mongo
{
    public class MongoUserProduct : MongoBaseEntity
    {
        public MongoUserProduct(string userId, string productId, int quantity)
        {
            UserId = userId;
            ProductId = productId;
            Quantity = quantity;
        }
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }

    }
}