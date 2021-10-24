using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Entities
{
    public class Product : BaseEntity
    {
        public Product(string title, decimal price)
        {
            Title = title;
            Price = price;
            Order_Products = new HashSet<Order_Product>();
            User_Products = new HashSet<User_Product>();
            Product_Campaigns = new HashSet<Product_Campaign>();
        }
        // For Unit Test
        public Product(Guid id, string title, decimal price)
        {
            Id = id;
            Title = title;
            Price = price;
        }
        public string Title { get; protected set; }
        public decimal Price { get; protected set; }
        public virtual ICollection<Order_Product> Order_Products { get; protected set; }
        public virtual ICollection<User_Product> User_Products { get; protected set; }
        public virtual ICollection<Product_Campaign> Product_Campaigns { get; protected set; }

        public void UpdateTitle(string title)
        {
            Title = title;
        }
        public void UpdatePrice(decimal price)
        {
            Price = price;
        }
    }
}

