using CQRS.Domain.Dtos.ProductDtos;
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
        public decimal TotalPrice => Products.Sum(x => x.Price);
        public UserDto User { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
