using ProductApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductAPI.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task DecrementStockAsync(int id, int quantity);
        Task IncrementStockAsync(int id, int quantity);
    }
}