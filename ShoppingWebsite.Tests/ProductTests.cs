using Microsoft.EntityFrameworkCore;
using shopping_website.Controllers;
using shopping_website.Models;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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

    public class ProductsControllerTests
    {
        [Fact]
        public void Index_ReturnsViewWithProducts()
        {
            // Arrange: Use in-memory database
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb").Options;
            using var context = new AppDbContext(options);
            context.Products.AddRange(
                new Product { Id = 1, Name = "A", Price = 1, Stock = 1 },
                new Product { Id = 2, Name = "B", Price = 2, Stock = 2 }
            );
            context.SaveChanges();
            var controller = new ProductsController(context);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsAssignableFrom<List<Product>>(result.Model);
            Assert.Equal(2, model.Count);
        }
    }
}
