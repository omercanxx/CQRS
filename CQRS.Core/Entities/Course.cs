using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Entities
{
    public class Course : BaseEntity
    {
        public Course(string title, decimal price)
        {
            Title = title;
            Price = price;
        }
        public string Title { get; protected set; }
        public decimal Price { get; protected set; }
        public virtual ICollection<Order> Orders { get; protected set; }

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

