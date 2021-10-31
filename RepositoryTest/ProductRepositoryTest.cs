using CQRS.Core.Entities;
using CQRS.Core.Interfaces;
using CQRS.Core.Interfaces.CommandInterfaces;
using CQRS.Core.Interfaces.QueryInterfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace CQRS.RepositoryTest
{
    public class ProductRepositoryTest
    {
        List<Product> products = new List<Product>();
        Guid testId = Guid.Parse("a4fd1a70-05c1-40ae-be16-15ee28ebca61");
        string testTitle = "1.Kurs";
        decimal testPrice = 12;
        Guid newId = Guid.Parse("a4fd1a70-05c1-40ae-be16-15ee28ebca51");
        public ProductRepositoryTest()
        {
            for (int i = 1; i < 10; i++)
            {
                products.Add(new Product(Guid.Parse($"a4fd1a70-05c1-40ae-be16-15ee28ebca6{i}"), $"{i}.Kurs", (i * 2) + 10));
            }
        }
        [Fact]
        public async Task GetAllAsync()
        {
            var mockProductRepository = new Mock<IQueryProductRepository>();

            // return all products
            mockProductRepository.Setup(mr => mr.GetAllAsync()).Returns(async () => products);

            var productRepository = mockProductRepository.Object;
            var dbProducts = await productRepository.GetAllAsync();

            Assert.Equal(products.Count, dbProducts.Count());
        }
        [Fact]
        public async Task GetByIdAsync()
        {
            var mockProductRepository = new Mock<IQueryProductRepository>();

            // return a product by Id
            mockProductRepository.Setup(mr => mr.GetByIdAsync(
                It.IsAny<Guid>())).Returns(async (Guid id) => products.SingleOrDefault(x => x.Id == id));

            var productRepository = mockProductRepository.Object;
            var dbProduct = await productRepository.GetByIdAsync(testId);

            Assert.NotNull(dbProduct);

            //Different DateTime
            //Assert.Equal(new Course(Guid.Parse($"a4fd1a70-05c1-40ae-be16-15ee28ebca61"), "1.Kurs", 12), dbCourse);

            Assert.Equal(testTitle, dbProduct.Title);
            Assert.Equal(testPrice, dbProduct.Price);

        }
        [Fact]
        public async Task AddAsync()
        {
            var mockProductRepository = new Mock<ICommandProductRepository>();

            var product = new Product(newId, "test", 100);
            mockProductRepository.Setup(mr => mr.AddAsync(
                It.IsAny<Product>())).Returns(async (Product product) =>
                {
                    products.Add(product);
                });

            var productRepository = mockProductRepository.Object;
            await productRepository.AddAsync(product);


            Assert.True(products.Any(x => x.Id == newId));

        }
        [Fact]
        public void Delete()
        {
            var mockProductRepository = new Mock<ICommandProductRepository>();


            mockProductRepository.Setup(mr => mr.Remove(
                It.IsAny<Product>())).Callback((Product product) =>
                {
                    var dbProduct = products.Where(x => x.Id == product.Id).SingleOrDefault();
                    dbProduct.Delete();
                });


            var productRepository = mockProductRepository.Object;
            var dbProduct = products.Where(x => x.Id == testId).SingleOrDefault();
            Assert.True(dbProduct.IsActive);
            productRepository.Remove(dbProduct);
            
            
            Assert.True(!dbProduct.IsActive);

        }

        [Fact]
        public async Task Update()
        {
            var mockProductRepository = new Mock<ICommandProductRepository>();
            var product = new Product(testId, "Test", 100);

            mockProductRepository.Setup(mr => mr.Update(
                It.IsAny<Product>())).Callback((Product product) =>
                {
                    var dbProduct = products.Where(x => x.Id == product.Id).SingleOrDefault();
                    dbProduct.UpdatePrice(product.Price);
                    dbProduct.UpdateTitle(product.Title);
                }
                );
            
            var productRepository = mockProductRepository.Object;
            productRepository.Update(product);

            var dbProduct = products.Where(x => x.Id == testId).SingleOrDefault();


            Assert.Equal(product.Title, dbProduct.Title);
            Assert.Equal(product.Price, dbProduct.Price);

        }
    }
}
