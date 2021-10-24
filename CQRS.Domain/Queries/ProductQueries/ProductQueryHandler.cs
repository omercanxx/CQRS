using AutoMapper;
using CQRS.Core.Interfaces.QueryInterfaces;
using CQRS.Domain.Dtos.ProductDtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Domain.Queries.CourseQueries
{
    public class ProductQueryHandler : IRequestHandler<GetProductsQuery, List<ProductDto>>,
                                      IRequestHandler<GetProductDetailQuery, ProductDto>
    {
        private readonly IQueryProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductQueryHandler(IQueryProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ProductDto> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
        {
            var dbCourse = await _productRepository.GetByIdAsync(request.Id);
            return _mapper.Map<ProductDto>(dbCourse);
        }

        public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<ProductDto>>(await _productRepository.GetAllAsync());
        }
    }
}
