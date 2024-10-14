using Store.Service.Services.BasketService.Dtos;
using Store.Service.Services.OrderService.Dtos;

namespace Store.Service.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(CustomerBasketDto input);
        Task<OrderDetailsDto> UpdateOrderPaymentSucceeded(string paymentIntendId);
        Task<OrderDetailsDto> UpdateOrderPaymentSFaild(string paymentIntendId);

    }
}
