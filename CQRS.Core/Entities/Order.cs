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
            Order_Campaigns = new HashSet<Order_Campaign>();
        }
        public Guid UserId { get; protected set; }
        public virtual User User { get; protected set; }
        public virtual ICollection<Order_Product> Order_Products { get; protected set; }
        public virtual ICollection<Order_Campaign> Order_Campaigns { get; protected set; }
    }
}
