using Store.Data.Entities.OrderEntities;

namespace Store.Repository.Specification.OrderSpecs
{
    public class OrderWithPaymenyIntentSpecification : BaseSpecification<Order>
    {
        public OrderWithPaymenyIntentSpecification(string? paymentIntentId) : base(order => order.PaymentIntentId == paymentIntentId)
        {
        }
    }
}
