using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Vending.Contracts.Interfaces;
using Vending.Contracts.Model;
using Vending.Repositories.Context;

namespace Vending.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly VendingDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductRepository(VendingDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Product>> GetAll()
        {
            var entities = await _dbContext.Products.ToListAsync();
            return entities.Select(_mapper.Map<Product>);
        }

        public async Task<Product> GetById(int id)
        {
            var entities = await _dbContext.Products.SingleAsync(x => x.Id == id);

            return _mapper.Map<Product>(entities);
        }

        public async Task UpdateProduct(Product product)
        {
            var entity = await _dbContext.Products.SingleAsync(x => x.Id == product.Id);

            entity.Portions = product.Portions;
            entity.Name = product.Name;
            entity.Price = product.Price;

            await _dbContext.SaveChangesAsync();
        }
    }
}
