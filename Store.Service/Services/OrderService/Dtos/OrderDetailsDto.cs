﻿using Store.Data.Entities.OrderEntities;

namespace Store.Service.Services.OrderService.Dtos
{
    public class OrderDetailsDto
    {
        public Guid Id { get; set; }

        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; }

        public AddressDto SippingAddress { get; set; }

        public string DeliveryMethodName { get; set; }

        public OrderPaymentStatus OrderPaymentStatus { get; set; }

        public OrdeStatus OrdeStatus { get; set; }

        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }

        public decimal SubTotal { get; set; }

        public decimal ShippingPrice { get; set; }

        public decimal Total { get; set; }
        public string? BasketId { get; set; }
        public string? PaymentIntentId { get; set; }



    }
}
