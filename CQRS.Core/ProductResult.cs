using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core
{
    public class ProductResult
    {
        public ProductResult(string id, int quantity)
        {
            Id = id;
            Quantity = quantity;
        }
        public string Id { get; set; }
        public int Quantity { get; set; }
    }
}
