using System.Collections.Generic;
using System.Threading.Tasks;
using Vending.Contracts.Model;

namespace Vending.Contracts.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(int id);
        Task UpdateProduct(Product product);
    }
}