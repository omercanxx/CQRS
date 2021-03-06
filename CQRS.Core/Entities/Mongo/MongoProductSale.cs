using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Entities.Mongo
{
    public class MongoProductSale : MongoBaseEntity
    {
        public MongoProductSale(string productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
        public string ProductId { get; set; }
        public int Quantity { get; set; }

    }
}
