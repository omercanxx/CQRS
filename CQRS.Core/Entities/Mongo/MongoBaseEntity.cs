using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Entities.Mongo
{
    public abstract class MongoBaseEntity
    {
        public MongoBaseEntity()
        {
            Id = ObjectId.GenerateNewId();
        }

        public ObjectId Id { get; protected set; }
    }
}

