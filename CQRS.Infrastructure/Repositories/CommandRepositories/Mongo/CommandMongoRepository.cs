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
    public class CommandMongoRepository<TEntity> : ICommandMongoRepository<TEntity> where TEntity : class
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
        public Task DeleteByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task InsertOneAsync(TEntity document)
        {
            throw new NotImplementedException();
        }

        public Task ReplaceOneAsync(TEntity document)
        {
            throw new NotImplementedException();
        }
    }
}
