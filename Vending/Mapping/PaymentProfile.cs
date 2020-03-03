using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Vending.Contracts.Model;
using Vending.Models;

namespace Vending.Mapping
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<IEnumerable<CoinStack>, PaymentViewModel>()
                .ConstructUsing(currencies =>
                    new PaymentViewModel
                    {
                        Currencies = currencies.Select(x => new CurrencyViewModel
                        {
                            Count = x.Count,
                            Value = x.Value
                        }).ToList()
                    }
                )
                .ReverseMap()
                .ConstructUsing(paymentModel =>
                    paymentModel.Currencies.Select(
                        x => new CoinStack(x.Value, x.Count)
                    ));
        }
    }

    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductViewModel, Product>().ReverseMap();
        }
    }
}
