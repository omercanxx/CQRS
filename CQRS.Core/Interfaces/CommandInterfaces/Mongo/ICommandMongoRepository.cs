using CQRS.Core.Entities.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Interfaces.CommandInterfaces.Mongo
{
    public interface ICommandMongoRepository<TEntity> where TEntity : MongoBaseEntity
    {
        Task InsertOneAsync(TEntity entity);
        Task ReplaceOneAsync(TEntity entity);
        Task DeleteByIdAsync(TEntity identity);
    }
}
