using AutoMapper;
using Vending.Contracts.Model;
using Vending.Models;

namespace Vending.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductViewModel, Product>().ReverseMap();
        }
    }
}