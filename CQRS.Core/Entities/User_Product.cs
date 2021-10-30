using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Entities
{
    public class User_Product : BaseEntity
    {
        public User_Product(Guid userId, string name, string description)
        {
            UserId = userId;
            Name = name;
            Description = description;
            User_ProductItems = new HashSet<User_ProductItem>();
        }
        public Guid UserId { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public void UpdateName(string name)
        {
            Name = name;
        }
        public void UpdateDescription(string description)
        {
            Description = description;
        }
        public virtual User User { get; protected set; }
        public virtual ICollection<User_ProductItem> User_ProductItems { get; protected set; }
    }
}
