using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Entities
{
    public class Order : BaseEntity
    {
        public Order(Guid userId, Guid courseId, decimal price)
        {
            UserId = userId;
            CourseId = courseId;
            Price = price;
        }
        public Guid UserId { get; protected set; }
        public Guid CourseId { get; protected set; }
        public decimal Price { get; protected set; }
        public virtual User User { get; protected set; }
        public virtual Course Course { get; protected set; }
        public void UpdateUser(Guid userId)
        {
            UserId = userId;
        }
        public void UpdateCourse(Guid courseId)
        {
            CourseId = courseId;
        }
        public void UpdatePrice(decimal price)
        {
            Price = price;
        }
    }
}
