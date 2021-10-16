using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.MongoDtos.Orders
{
    public class CreatedOrderDto
    {
        public ObjectId Id => ObjectId.GenerateNewId();
        [BsonElement("Price")]
        public decimal Price { get; set; }
    }
}
