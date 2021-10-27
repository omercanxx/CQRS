using CQRS.Core.Entities.Mongo;
using CQRS.Core.Interfaces.CommandInterfaces.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Repositories.CommandRepositories.Mongo
{
    public class CommandMongoProductResultRepository : CommandMongoRepository<MongoProductResult>, ICommandMongoProductResultRepository
    {
        public CommandMongoProductResultRepository(IMongoDatabaseSettings settings) : base(settings)
        {
        }

    }
}

