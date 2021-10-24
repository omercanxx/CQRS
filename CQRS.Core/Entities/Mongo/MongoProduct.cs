using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Entities.Mongo
{
    public class MongoProduct
    {
        public MongoProduct(string productId, string title, decimal price)
        {
            ProductId = productId;
            Title = title;
            Price = price;
        }
        public string ProductId { get; protected set; }
        public string Title { get; protected set; }
        public decimal Price { get; protected set; }
    }
}
