using CQRS.Core.Entities.Mongo;
using CQRS.Core.Enums;
using CQRS.Core.Interfaces.QueryInterfaces.Mongo;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Repositories.QueryRepositories.Mongo
{
    public class QueryMongoRepository<TEntity> : IQueryMongoRepository<TEntity> where TEntity : MongoBaseEntity
    {
        private IMongoCollection<TEntity> _collection;

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
        public async Task<List<TEntity>> GetAll(RequestTypes requestType)
        {
            if(requestType == RequestTypes.gRPC)
            {
                var client = new MongoClient("mongodb://192.168.1.21:27017");
                var database = client.GetDatabase("CqrsDB");
                _collection = database.GetCollection<TEntity>(typeof(TEntity).Name);
            }

            return await _collection.Find(_ => true).ToListAsync();
        }

        public IEnumerable<TEntity> FilterBy(Expression<Func<TEntity, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).ToEnumerable();
        }

        public Task<TEntity> FindByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, objectId);
                return _collection.Find(filter).SingleOrDefaultAsync();
            });
        }
    }
}

