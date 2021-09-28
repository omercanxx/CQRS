using CQRS.Domain.Dtos.CourseDtos;
using CQRS.Domain.Dtos.OrderDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Queries.OrderQueries
{
    public class GetOrderDetailQuery : IRequest<OrderDto>
    {
        public GetOrderDetailQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
