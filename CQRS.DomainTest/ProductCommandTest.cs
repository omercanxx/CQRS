using AutoFixture;
using AutoMapper;
using CQRS.Core.Entities;
using CQRS.Core.Interfaces.QueryInterfaces;
using CQRS.Core.Interfaces.QueryInterfaces.Mongo;
using CQRS.Domain.Dtos.ProductDtos;
using CQRS.Domain.Queries.ProductQueries;
using CQRS.Infrastructure;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CQRS.DomainTest
{
    public class ProductCommandTest
    {
        List<Product> products = new List<Product>();
        Guid testId = Guid.Parse("a4fd1a70-05c1-40ae-be16-15ee28ebca61");
        Guid newId = Guid.Parse("a4fd1a70-05c1-40ae-be16-15ee28ebca51");
        private readonly IMapper _mapper;
        private readonly CancellationToken _cancellationToken;
        public ProductCommandTest(IMapper mapper, CancellationToken cancellationToken)
        {
            _mapper = mapper;
            _cancellationToken = cancellationToken;
            for (int i = 1; i < 10; i++)
            {
                products.Add(new Product(Guid.Parse($"a4fd1a70-05c1-40ae-be16-15ee28ebca6{i}"), $"{i}.Kurs", (i * 2) + 10));
             }
        }

        [Fact]
        public async Task GetProductsQuery()
        {
            var query = new GetProductsQuery();
            var mockproductRepository = new Mock<IQueryProductRepository>();
            var mockproductSaleRepository = new Mock<IQueryMongoProductSaleRepository>();

            var mockDbContext = new Mock<AppDbContext>();

            mockproductRepository.Setup(mr => mr.GetAllAsync()).Returns(async () => products);

            var productRepository = mockproductRepository.Object;
            var productSaleRepository = mockproductSaleRepository.Object;
            var dbCourses = await productRepository.GetAllAsync();

            Assert.Equal(products.Count, dbCourses.Count());

            var handler = new ProductQueryHandler(productRepository, productSaleRepository, _mapper);
            await handler.Handle(query, _cancellationToken);
            //var fixture = new Fixture();
            //var query = fixture.Create<GetProductsQuery>();
            //var mockHandler = new Mock<IRequestHandler<GetProductsQuery, List<ProductDto>>>();
            //var handler = new ProductQueryHandler(mockUnitOfWork.Object);


            //// return a product by Id
            //mockHandler.Setup(mr => mr.Handle.).Returns(async () => courses);

            //var courseRepository = mockCourseRepository.Object;
            //var dbCourses = await courseRepository.GetAllAsync();

            //Assert.Equal(courses.Count, dbCourses.Count());
        }
    }
}

