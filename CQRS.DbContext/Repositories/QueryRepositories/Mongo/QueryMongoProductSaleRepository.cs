using CQRS.Core.Entities.Mongo;
using CQRS.Core.Interfaces.QueryInterfaces.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Repositories.QueryRepositories.Mongo
{
    public class QueryMongoProductSaleRepository : QueryMongoRepository<MongoProductSale>, IQueryMongoProductSaleRepository
    {
        public QueryMongoProductSaleRepository(IMongoDatabaseSettings settings) : base(settings)
        {
        }

    }
}
