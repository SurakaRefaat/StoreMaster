namespace Store.Data.Entities.OrderEntities
{
    public class OrderItem : BaseEntitiy<Guid>
    {
        public decimal Price { get; set; }

        public int Quantity { get; set; }
        public ProductItem ItemOrdered { get; set; }
        public Guid OrderId { get; set; }
    }
}
