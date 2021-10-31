using AutoMapper;
using CQRS.Domain.Dtos.ProductDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Queries.ProductQueries
{
    public class GetProductDetailQuery : IRequest<ProductDto>
    {
        public GetProductDetailQuery(Guid id)
        {
            Id = id;
        }
        public GetProductDetailQuery(Guid id, IMapper mapper)
        {
            Id = id;
            Mapper = mapper;
        }
        public Guid Id { get; set; }
        public IMapper Mapper { get; set; }
    }
}
