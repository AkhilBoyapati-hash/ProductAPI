using Moq;
using ProductApi.Controllers;
using ProductApi.Models;
using ProductAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductApi.Tests.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _mockProductService = new Mock<IProductService>();
            _controller = new ProductsController(_mockProductService.Object);
        }
        [Fact]
        public async Task GetAllProducts_ReturnsOkResult()
        {
            // Arrange
            var products = new List<Product>
            {
                new() { ProductId = 1, Name = "Product 1", Price = 100,Description="Description" },
                new() { ProductId = 2, Name = "Product 2", Price = 150,Description="Description" }
            };
            _mockProductService.Setup(service => service.GetAllProductsAsync()).ReturnsAsync(products);

            // Act
            var result = await _controller.GetAllProducts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProducts = Assert.IsType<List<Product>>(okResult.Value);
            Assert.Equal(2, returnedProducts.Count);
        }

        [Fact]
        public async Task GetProductById_ReturnsOkResult()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "Product 1", Price = 100, Description = "Description" };
            _mockProductService.Setup(service => service.GetProductByIdAsync(1)).ReturnsAsync(product);

            // Act
            var result = await _controller.GetProductById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProduct = Assert.IsType<Product>(okResult.Value);
            Assert.Equal(1, returnedProduct.ProductId);
            Assert.Equal("Product 1", returnedProduct.Name);
        }

        [Fact]
        public async Task CreateProduct_AddsProductSuccessfully()
        {
            // Arrange
            var newProduct = new Product
            {
                ProductId = 3,
                Name = "Product 3",
                Price = 200,
                StockAvailable = 50,
                Description = "Description",
            };
            _mockProductService.Setup(service => service.AddProductAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateProduct(newProduct);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(201, createdAtActionResult.StatusCode);
        }

        [Fact]
        public async Task UpdateProduct_UpdatesProductSuccessfully()
        {
            // Arrange
            var updatedProduct = new Product
            {
                ProductId = 1,
                Name = "Updated Product 1",
                Price = 120,
                StockAvailable = 60,
                Description = "Description"
            };
            _mockProductService.Setup(service => service.UpdateProductAsync(updatedProduct)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateProduct(updatedProduct.ProductId, updatedProduct);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);  
            Assert.Equal(204, noContentResult.StatusCode); 
        }

        [Fact]
        public async Task DeleteProduct_RemovesProductSuccessfully()
        {
            // Arrange
            int productIdToDelete = 1;
            var product = new Product { ProductId = productIdToDelete, Name = "Test Product", Price = 100, StockAvailable = 50, Description = "Description" };
           
            _mockProductService.Setup(service => service.GetProductByIdAsync(productIdToDelete))
                .ReturnsAsync(product);
            _mockProductService.Setup(service => service.DeleteProductAsync(productIdToDelete))
                .Returns(Task.CompletedTask);  
            // Act
            var result = await _controller.DeleteProduct(productIdToDelete);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        [Fact]
        public async Task DecrementStock_ReducesStockSuccessfully()
        {
            // Arrange
            int productId = 1;
            int decrementQuantity = 10;
            var product = new Product { ProductId = productId, Name = "Test Product", StockAvailable = 20, Description = "Description" };

            _mockProductService.Setup(service => service.GetProductByIdAsync(productId))
                .ReturnsAsync(product);

            _mockProductService.Setup(service => service.UpdateProductAsync(It.IsAny<Product>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DecrementStock(productId, decrementQuantity);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);  
            Assert.Equal(200, okResult.StatusCode); 
        }

        [Fact]
        public async Task IncrementStock_IncreasesStockSuccessfully()
        {
            // Arrange
            int productId = 1;
            int incrementQuantity = 10;
            var product = new Product { ProductId = productId, Name = "Test Product", StockAvailable = 20, Description = "Description" };

            _mockProductService.Setup(service => service.GetProductByIdAsync(productId))
                .ReturnsAsync(product);

            _mockProductService.Setup(service => service.UpdateProductAsync(It.IsAny<Product>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.IncrementStock(productId, incrementQuantity);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);  
            Assert.Equal(200, okResult.StatusCode);  
        }
    }
}
