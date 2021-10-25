using CQRS.Core.Entities.Mongo;
using CQRS.Core.Interfaces.CommandInterfaces.Mongo;
using CQRS.Core.Interfaces.QueryInterfaces.Mongo;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Repositories.CommandRepositories.Mongo
{
    public class CommandMongoRepository<TEntity> : ICommandMongoRepository<TEntity> where TEntity : MongoBaseEntity
    {
        private readonly IMongoCollection<TEntity> _collection;

        public CommandMongoRepository(IMongoDatabaseSettings settings)
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
        public async Task DeleteByIdAsync(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, entity.Id);
            _collection.FindOneAndDelete(filter);
        }

        public Task InsertOneAsync(TEntity entity)
        {
            return Task.Run(() => _collection.InsertOneAsync(entity));
        }

        public async Task ReplaceOneAsync(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, entity.Id);
            await _collection.FindOneAndReplaceAsync(filter, entity);
        }
    }
}
