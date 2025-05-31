using Microsoft.EntityFrameworkCore;
using shopping_website.Controllers;
using shopping_website.Models;
using Xunit;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using shopping_website;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Moq;

namespace ShoppingWebsite.Tests
{
    public class ProductTests
    {
        [Fact]
        public void Product_CanBeCreated_WithValidValues()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Test Product",
                Description = "A sample product.",
                Price = 9.99m,
                Stock = 10
            };

            Assert.Equal(1, product.Id);
            Assert.Equal("Test Product", product.Name);
            Assert.Equal("A sample product.", product.Description);
            Assert.Equal(9.99m, product.Price);
            Assert.Equal(10, product.Stock);
        }
    }

    public class ProductsApiControllerTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
            var context = new AppDbContext(options);
            context.Products.AddRange(
                new Product { Id = 1, Name = "A", Price = 1, Stock = 1 },
                new Product { Id = 2, Name = "B", Price = 2, Stock = 2 }
            );
            context.SaveChanges();
            return context;
        }

        [Fact]
        public void GetProducts_ReturnsAllProducts()
        {
            using var context = GetInMemoryDbContext();
            var controller = new ProductsApiController(context);
            var result = controller.GetProducts().Result as OkObjectResult;
            Assert.NotNull(result);
            var products = Assert.IsAssignableFrom<IEnumerable<Product>>(result.Value);
            Assert.Equal(2, products.Count());
        }

        [Fact]
        public void GetProductById_ReturnsProduct_WhenExists()
        {
            using var context = GetInMemoryDbContext();
            var controller = new ProductsApiController(context);
            var result = controller.GetProductById(1).Result as OkObjectResult;
            Assert.NotNull(result);
            var product = Assert.IsType<Product>(result.Value);
            Assert.Equal(1, product.Id);
        }

        [Fact]
        public void GetProductById_ReturnsNotFound_WhenNotExists()
        {
            using var context = GetInMemoryDbContext();
            var controller = new ProductsApiController(context);
            var result = controller.GetProductById(999).Result;
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void CreateProduct_AddsProduct()
        {
            using var context = GetInMemoryDbContext();
            var controller = new ProductsApiController(context);
            var newProduct = new Product { Name = "C", Price = 3, Stock = 3 };
            var result = controller.CreateProduct(newProduct).Result as CreatedAtActionResult;
            Assert.NotNull(result);
            var product = Assert.IsType<Product>(result.Value);
            Assert.Equal("C", product.Name);
            Assert.Equal(3, context.Products.Count());
        }

        [Fact]
        public void UpdateProduct_UpdatesExistingProduct()
        {
            using var context = GetInMemoryDbContext();
            var controller = new ProductsApiController(context);
            var update = new Product { Name = "Updated", Description = "Desc", Price = 10, Stock = 5 };
            var result = controller.UpdateProduct(1, update);
            Assert.IsType<NoContentResult>(result);
            var product = context.Products.Find(1);
            Assert.Equal("Updated", product.Name);
            Assert.Equal(10, product.Price);
        }

        [Fact]
        public void UpdateProduct_ReturnsNotFound_WhenNotExists()
        {
            using var context = GetInMemoryDbContext();
            var controller = new ProductsApiController(context);
            var update = new Product { Name = "Updated", Description = "Desc", Price = 10, Stock = 5 };
            var result = controller.UpdateProduct(999, update);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteProduct_RemovesProduct()
        {
            using var context = GetInMemoryDbContext();
            var controller = new ProductsApiController(context);
            var result = controller.DeleteProduct(1);
            Assert.IsType<NoContentResult>(result);
            Assert.Single(context.Products);
        }

        [Fact]
        public void DeleteProduct_ReturnsNotFound_WhenNotExists()
        {
            using var context = GetInMemoryDbContext();
            var controller = new ProductsApiController(context);
            var result = controller.DeleteProduct(999);
            Assert.IsType<NotFoundResult>(result);
        }
    }

    public class StartupTests
    {
        [Fact]
        public void ConfigureServices_AddsDbContextAndControllers()
        {
            var services = new ServiceCollection();
            var configMock = new Mock<IConfiguration>();
            configMock.Setup(c => c.GetConnectionString(It.IsAny<string>())).Returns("DataSource=:memory:");
            var startup = new Startup(configMock.Object);
            startup.ConfigureServices(services);
            var provider = services.BuildServiceProvider();
            Assert.NotNull(provider.GetService<AppDbContext>());
            Assert.NotNull(provider.GetService<Microsoft.AspNetCore.Mvc.Controllers.IControllerActivator>());
        }
    }
}
