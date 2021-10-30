using CQRS.Core.Entities.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core
{
    //Commandların dönüş değerlerini özelleştirmek için CommandResult kullanılmıştır, farklı amaçlara hizmet etmesi amacıyla da birden fazla constructor kullanılmıştır.
    public class CommandResult
    {
        public CommandResult(Guid id)
        {
            Id = id;
        }
        //Top 10 Sales
        public CommandResult(Guid id, List<MongoProductSale> productResults)
        {
            OrderId = id;
            ProductResults = productResults;
        }
        //Top 10 Favorite Products
        public CommandResult(string userId, string productId)
        {
            UserId = userId;
            ProductId = productId;
        }
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public List<MongoProductSale> ProductResults { get; set; }
    }
}
