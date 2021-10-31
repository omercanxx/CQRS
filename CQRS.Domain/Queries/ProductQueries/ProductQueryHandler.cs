using AutoMapper;
using CQRS.Core.Entities.Mongo;
using CQRS.Core.Enums;
using CQRS.Core.Interfaces.QueryInterfaces;
using CQRS.Core.Interfaces.QueryInterfaces.Mongo;
using CQRS.Domain.Dtos.ProductDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Domain.Queries.ProductQueries
{
    public class ProductQueryHandler : IRequestHandler<GetProductsQuery, List<ProductDto>>,
                                      IRequestHandler<GetProductDetailQuery, ProductDto>,
                                      IRequestHandler<GetTopTenProductsQuery, List<MongoProductResultDto>>,
                                      IRequestHandler<GetFavoritesProductsQuery, List<MongoProductResultDto>>
    {
        private readonly IQueryProductRepository _productRepository;
        private readonly IQueryMongoProductSaleRepository _productResultRepository;
        private readonly IQueryMongoUserProductRepository _userProductResultRepository;
        private readonly IMapper _mapper;
        public ProductQueryHandler(IQueryProductRepository productRepository, IQueryMongoProductSaleRepository productResultRepository, IQueryMongoUserProductRepository userProductResultRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _productResultRepository = productResultRepository;
            _userProductResultRepository = userProductResultRepository;
            _mapper = mapper;
        }
        public async Task<ProductDto> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
        {
            var dbCourse = await _productRepository.GetByIdAsync(request.Id);

            if(request.Mapper != null)
                return request.Mapper.Map<ProductDto>(dbCourse);

            else
                return _mapper.Map<ProductDto>(dbCourse);
        }

        public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<ProductDto>>(await _productRepository.GetAllAsync());
        }

        public async Task<List<MongoProductResultDto>> Handle(GetTopTenProductsQuery request, CancellationToken cancellationToken)
        {
            var dbTopTenProducts = await _productResultRepository.GetAll(RequestTypes.gRPC);
            return _mapper.Map<List<MongoProductResultDto>>(dbTopTenProducts.OrderByDescending(x => x.Quantity).ToList());
        }

        public async Task<List<MongoProductResultDto>> Handle(GetFavoritesProductsQuery request, CancellationToken cancellationToken)
        {
            var dbTopTenProducts = await _userProductResultRepository.GetAll(RequestTypes.gRPC);
            return _mapper.Map<List<MongoProductResultDto>>(dbTopTenProducts.OrderByDescending(x => x.Quantity).ToList());
        }
    }
}
