using CQRS.Core.Entities.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Interfaces.CommandInterfaces.Mongo
{
    public interface ICommandMongoOrderRepository : ICommandMongoRepository<MongoOrder>
    {
    }
}
