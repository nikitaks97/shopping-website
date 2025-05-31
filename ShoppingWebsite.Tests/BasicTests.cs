using Microsoft.EntityFrameworkCore;
using shopping_website.Controllers;
using shopping_website.Models;
using shopping_website.Data;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingWebsite.Tests
{
    public class BasicTests
    {
        [Fact]
        public void AlwaysPasses()
        {
            Assert.True(true);
        }
    }

    public class HomeControllerTests
    {
        [Fact]
        public void Index_ReturnsViewWithProducts()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
            using var context = new AppDbContext(options);
            context.Products.Add(new Product { Id = 1, Name = "Test", Price = 1, Stock = 1 });
            context.SaveChanges();
            var controller = new HomeController(context);
            var result = controller.Index() as ViewResult;
            Assert.NotNull(result);
            var model = Assert.IsAssignableFrom<List<Product>>(result.Model);
            Assert.Single(model);
        }
    }

    public class DbInitializerTests
    {
        [Fact]
        public void Initialize_SeedsDatabase_WhenEmpty()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
            using var context = new AppDbContext(options);
            DbInitializer.Initialize(context);
            Assert.True(context.Products.Any());
        }

        [Fact]
        public void Initialize_DoesNotSeed_WhenAlreadySeeded()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
            using var context = new AppDbContext(options);
            context.Products.Add(new Product { Name = "Already", Price = 1, Stock = 1 });
            context.SaveChanges();
            DbInitializer.Initialize(context);
            Assert.Equal(1, context.Products.Count());
        }
    }

    public class ProductsControllerTests
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
        public void Index_ReturnsViewWithProducts()
        {
            using var context = GetInMemoryDbContext();
            var controller = new ProductsController(context);
            var result = controller.Index() as ViewResult;
            Assert.NotNull(result);
            var model = Assert.IsAssignableFrom<List<Product>>(result.Model);
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public void Details_ReturnsViewWithProduct_WhenExists()
        {
            using var context = GetInMemoryDbContext();
            var controller = new ProductsController(context);
            var result = controller.Details(1) as ViewResult;
            Assert.NotNull(result);
            var product = Assert.IsType<Product>(result.Model);
            Assert.Equal(1, product.Id);
        }

        [Fact]
        public void Details_ReturnsNotFound_WhenNotExists()
        {
            using var context = GetInMemoryDbContext();
            var controller = new ProductsController(context);
            var result = controller.Details(999);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
