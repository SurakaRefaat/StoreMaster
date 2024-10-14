namespace Store.Data.Entities.OrderEntities
{
    public class Order :BaseEntitiy<Guid>
    {
        public string? BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public ShippingAddress SippingAddress { get; set; }

        public DeliveryMethod DeliveryMethod { get; set; }

        public int? DeliveryMethodId { get; set; }
        public OrdeStatus OrdeStatus { get; set; } = OrdeStatus.Placed;

        public OrderPaymentStatus OrderPaymentStatus { get; set; } = OrderPaymentStatus.Pending;

        public IReadOnlyList<OrderItem> OrderItems { get; set; }

        public decimal SubTotal { get; set; }

        public decimal GetTotal() 
            => SubTotal + DeliveryMethod.Price;
        public string? BasketId { get; set; }
        public string? PaymentIntentId { get; set; }
    }
}
