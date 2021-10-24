using CQRS.Domain.Dtos.ProductDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Queries.CourseQueries
{
    public class GetProductDetailQuery : IRequest<ProductDto>
    {
        public GetProductDetailQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
