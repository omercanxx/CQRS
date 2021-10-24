using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core
{
    public class ProductResult
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }

    }
}
