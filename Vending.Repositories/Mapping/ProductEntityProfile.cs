using AutoMapper;
using Vending.Contracts.Model;
using Vending.Repositories.Entities;

namespace Vending.Repositories.Mapping
{
    public class ProductEntityProfile : Profile
    {
        public ProductEntityProfile()
        {
            CreateMap<ProductEntity, Product>().ReverseMap();
        }
    }
}
