using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Dtos.ProductDtos
{
    public class MongoProductResultDto
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
