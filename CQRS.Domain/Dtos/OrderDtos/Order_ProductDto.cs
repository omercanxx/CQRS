using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Dtos.OrderDtos
{
    public class Order_ProductDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
