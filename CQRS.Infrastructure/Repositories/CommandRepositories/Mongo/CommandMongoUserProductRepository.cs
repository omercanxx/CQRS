using CQRS.Core.Entities.Mongo;
using CQRS.Core.Interfaces.CommandInterfaces.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Repositories.CommandRepositories.Mongo
{
    public class CommandMongoUserProductRepository : CommandMongoRepository<MongoUserProduct>, ICommandMongoUserProductRepository
    {
        public CommandMongoUserProductRepository(IMongoDatabaseSettings settings) : base(settings)
        {
        }

    }
}
