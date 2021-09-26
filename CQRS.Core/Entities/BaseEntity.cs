using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Entities
{
    public abstract class BaseEntity
    {

        public BaseEntity()
        {
            Id = Guid.NewGuid();
            IsActive = true;
            CreatedOn = DateTime.Now;
        }

        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
