using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Entities
{
    public class User_Product : BaseEntity
    {
        public User_Product(Guid productId, Guid userId, string name, string description)
        {
            ProductId = productId;
            UserId = userId;
            Name = name;
            Description = description;
        }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public virtual User User { get; protected set; }
        public virtual Product Product { get; protected set; }
    }
}
