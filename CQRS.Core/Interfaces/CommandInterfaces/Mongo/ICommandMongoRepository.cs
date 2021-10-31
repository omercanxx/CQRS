using CQRS.Core.Entities.Mongo;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Interfaces.CommandInterfaces.Mongo
{
    public interface ICommandMongoRepository<TEntity> where TEntity : MongoBaseEntity
    {
        Task InsertOneAsync(TEntity entity);
        Task ReplaceOneByProductIdAsync(string productId, int quantity, TEntity entity);
        Task DeleteByIdAsync(TEntity identity);
        Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filterExpression);
    }
}
