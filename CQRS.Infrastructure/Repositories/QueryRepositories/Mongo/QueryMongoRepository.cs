using CQRS.Core.Interfaces.QueryInterfaces.Mongo;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Repositories.QueryRepositories.Mongo
{
    public class QueryMongoRepository<TEntity> : IQueryMongoRepository<TEntity> where TEntity : class
    {
        private readonly IMongoCollection<TEntity> _collection;

        public QueryMongoRepository(IMongoDatabaseSettings settings)
        {
            var database = GetMongoDatabase(settings);
            _collection = database.GetCollection<TEntity>(typeof(TEntity).Name);
        }
        private protected IMongoDatabase GetMongoDatabase(IMongoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            return database;
        }
        public IQueryable<TEntity> AsQueryable()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> FilterBy(Expression<Func<TEntity, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> FindByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}

