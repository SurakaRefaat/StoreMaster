using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Entities.OrderEntities
{
    public class OrderItem : BaseEntitiy<Guid>
    {
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public ProductItem public ProductItem UnitPrice { get; set; }
 { get; set; }

        public Guid OrderId { get; set; }
    }
}
