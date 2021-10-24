using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core
{
    //Commandların dönüş değerlerini özelleştirmek için CommandResult kullanılmıştır, farklı amaçlara hizmet etmesi amacıyla da birden fazla constructor kullanılmıştır.
    public class CommandResult
    {
        public CommandResult(Guid id)
        {
            Id = id;
        }
        public CommandResult(Guid id, List<ProductResult> productResults)
        {
            OrderId = id;
            ProductResults = productResults;
        }
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public List<ProductResult> ProductResults { get; set; }
    }
}
