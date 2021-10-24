using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Interfaces.CommandInterfaces.Mongo
{
    public interface ICommandMongoRepository<TEntity> where TEntity : class
    {
        Task InsertOneAsync(TEntity document);
        Task ReplaceOneAsync(TEntity document);
        Task DeleteByIdAsync(string id);
    }
}
