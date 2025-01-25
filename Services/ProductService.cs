using ProductApi.Models;
using ProductAPI.Repositories.Interfaces;
using ProductAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync() => await _repository.GetAllProductsAsync();

        public async Task<Product> GetProductByIdAsync(int id) => await _repository.GetProductByIdAsync(id);

        public async Task AddProductAsync(Product product)
        {
            await _repository.AddProductAsync(product);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _repository.UpdateProductAsync(product);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            await _repository.DeleteProductAsync(id);
            await _repository.SaveChangesAsync();
        }

        public async Task DecrementStockAsync(int id, int quantity)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product != null && product.StockAvailable >= quantity)
            {
                product.StockAvailable -= quantity;
                await _repository.SaveChangesAsync();
            }
        }

        public async Task IncrementStockAsync(int id, int quantity)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product != null)
            {
                product.StockAvailable += quantity;
                await _repository.SaveChangesAsync();
            }
        }
    }
}