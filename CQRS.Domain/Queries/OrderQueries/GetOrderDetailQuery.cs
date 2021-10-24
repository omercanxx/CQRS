using CQRS.Core.Entities.Mongo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Queries.OrderQueries
{
    public class GetOrderDetailQuery : IRequest<MongoOrder>
    {
        public GetOrderDetailQuery(string id)
        {
            Id = id;
        }
        public string Id { get; set; }
    }
}
