using AutoMapper;
using Store.Repository.Baket.Modles;


namespace Store.Service.Services.BasketService.Dtos
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket , CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItem , BasketItemDto>().ReverseMap();
        }
    }
}
