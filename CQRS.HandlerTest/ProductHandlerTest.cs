using AutoMapper;
using CQRS.Core.Entities;
using CQRS.Core.Interfaces.CommandInterfaces;
using CQRS.Core.Interfaces.QueryInterfaces;
using CQRS.Core.Interfaces.QueryInterfaces.Mongo;
using CQRS.Domain.Commands.ProductCommands;
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

namespace CQRS.HandlerTest
{
    public class ProductHandlerTest : IClassFixture<CommonTestFixture>
    {
        List<Product> products = new List<Product>();
        Guid testId = Guid.Parse("a4fd1a70-05c1-40ae-be16-15ee28ebca61");
        Guid newId = Guid.Parse("a4fd1a70-05c1-40ae-be16-15ee28ebca51");
        string newTitle = "test";
        decimal newPrice = 100;
        private readonly IMapper _mapper;
        public ProductHandlerTest(CommonTestFixture testFixture)
        {
            _mapper = testFixture.Mapper;
            for (int i = 1; i < 10; i++)
            {
                products.Add(new Product(Guid.Parse($"a4fd1a70-05c1-40ae-be16-15ee28ebca6{i}"), $"{i}.Kurs", (i * 2) + 10));
            }
        }

        [Fact]
        public async Task AddCommand()
        {
            //Arange
            var mockproductCommandRepository = new Mock<ICommandProductRepository>();
            mockproductCommandRepository.Setup(mr => mr.AddAsync(
                It.IsAny<Product>())).Returns(async (Product product) =>
                {
                    products.Add(product);
                });

            //Act
            ProductCreateCommand command = new ProductCreateCommand(newId, newTitle, newPrice);
            var commandHandler = new ProductCommandHandler(mockproductCommandRepository.Object);
            var commandResult = await commandHandler.Handle(command, new System.Threading.CancellationToken());

            //Arrange

            var dbProduct = products.Where(x => x.Id == newId).SingleOrDefault();
            Assert.Equal<Guid>(newId, dbProduct.Id);
            Assert.Equal(newTitle, dbProduct.Title);
            Assert.Equal(newPrice, dbProduct.Price);
        }
        [Fact]
        public async Task UpdateCommand()
        {
            //Arange
            var mockproductCommandRepository = new Mock<ICommandProductRepository>();
            mockproductCommandRepository.Setup(mr => mr.Update(
                It.IsAny<Product>())).Callback((Product product) =>
                {
                    var dbProduct = products.Where(x => x.Id == product.Id).SingleOrDefault();
                    dbProduct.UpdatePrice(product.Price);
                    dbProduct.UpdateTitle(product.Title);
                }
                );

            mockproductCommandRepository.Setup(mr => mr.GetByIdAsync(
                It.IsAny<Guid>())).Returns(async (Guid id) => products.SingleOrDefault(x => x.Id == id));



            var mockproductQueryRepository = new Mock<IQueryProductRepository>();
            var mockproductSaleRepository = new Mock<IQueryMongoProductSaleRepository>();



            //Act
            ProductUpdateCommand command = new ProductUpdateCommand(testId, newTitle, newPrice);
            var commandHandler = new ProductCommandHandler(mockproductCommandRepository.Object);
            var commandResult = await commandHandler.Handle(command, new System.Threading.CancellationToken());




            //Arrange
            var dbProduct = products.Where(x => x.Id == testId).SingleOrDefault();
            Assert.Equal(testId, dbProduct.Id);
            Assert.Equal(newTitle, dbProduct.Title);
            Assert.Equal(newPrice, dbProduct.Price);
        }
        [Fact]
        public async Task DeleteCommand()
        {
            var mockproductCommandRepository = new Mock<ICommandProductRepository>();
            var mockproductQueryRepository = new Mock<IQueryProductRepository>();
            var mockproductSaleRepository = new Mock<IQueryMongoProductSaleRepository>();

            mockproductCommandRepository.Setup(mr => mr.GetByIdAsync(
                It.IsAny<Guid>())).Returns(async (Guid id) => products.SingleOrDefault(x => x.Id == id));

            mockproductCommandRepository.Setup(mr => mr.Remove(
                It.IsAny<Product>())).Callback((Product product) =>
                {
                    var dbProduct = products.Where(x => x.Id == product.Id).SingleOrDefault();
                    dbProduct.Delete();
                });


            //Act
            ProductDeleteCommand command = new ProductDeleteCommand(testId);
            var commandHandler = new ProductCommandHandler(mockproductCommandRepository.Object);
            var commandResult = await commandHandler.Handle(command, new System.Threading.CancellationToken());

            //Arrange
            var dbProduct = products.Where(x => x.Id == testId).SingleOrDefault();
            Assert.Equal(dbProduct.IsActive, false);

        }
        [Fact]
        public async Task GetProductDetailQuery()
        {
            //Arange

            var mockproductQueryRepository = new Mock<IQueryProductRepository>();
            var mockproductSaleRepository = new Mock<IQueryMongoProductSaleRepository>();

            // return a product by Id
            mockproductQueryRepository.Setup(mr => mr.GetByIdAsync(
                It.IsAny<Guid>())).Returns(async (Guid id) => products.SingleOrDefault(x => x.Id == id));


            //Act
            var query = new GetProductDetailQuery(testId);
            var queryHandler = new ProductQueryHandler(mockproductQueryRepository.Object, mockproductSaleRepository.Object, _mapper);
            var queryResult = await queryHandler.Handle(query, new System.Threading.CancellationToken());

            //Arrange
            var dbProduct = products.Where(x => x.Id == testId).SingleOrDefault();
            Assert.Equal(queryResult.Id, dbProduct.Id);
            Assert.Equal(queryResult.Title, dbProduct.Title);
            Assert.Equal(queryResult.Price, dbProduct.Price);
        }
        [Fact]
        public async Task GetProductsQuery()
        {
            //Arange

            var mockproductQueryRepository = new Mock<IQueryProductRepository>();
            var mockproductSaleRepository = new Mock<IQueryMongoProductSaleRepository>();
            // return products
            mockproductQueryRepository.Setup(mr => mr.GetAllAsync()).Returns(async () => products);


            //Act
            var query = new GetProductsQuery();
            var queryHandler = new ProductQueryHandler(mockproductQueryRepository.Object, mockproductSaleRepository.Object, _mapper);
            var queryResult = await queryHandler.Handle(query, new System.Threading.CancellationToken());

            //Arrange
            Assert.Equal(queryResult.Count(), products.Count());
        }

    }
}

