using CQRS.Domain.Dtos.CourseDtos;
using CQRS.Domain.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Dtos.OrderDtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public decimal Price { get; set; }
        public virtual UserDto User { get; set; }
        public virtual CourseDto Course { get; set; }
    }
}
