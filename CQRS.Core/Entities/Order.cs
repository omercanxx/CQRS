using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Entities
{
    public class Order : BaseEntity
    {
        public Order(Guid userId)
        {
            UserId = userId;
            Order_Products = new HashSet<Order_Product>();
        }
        public Guid UserId { get; protected set; }
        public virtual User User { get; protected set; }
        public virtual ICollection<Order_Product> Order_Products { get; protected set; }
    }
}
