using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Entities
{
    public abstract class BaseEntity
    {
        //Sınıf hiyerarşisinde base class tanımlamak için BaseEntity adında abstract class oluşturulmuştur. Id, IsActive ve CreatedOn alanlarına sahiptir. Delete işlemi ile veri silme yerine isActive alanı false set edilecektir. CreatedOn ise verinin oluşturulduğu tarihi tutmak amaçlı kullanılmıştır.
        public BaseEntity()
        {
            Id = Guid.NewGuid();
            IsActive = true;
            CreatedOn = DateTime.Now;
        }

        public Guid Id { get; protected set; }
        public bool IsActive { get; protected set; }
        public DateTime CreatedOn { get; protected set; }
        public DateTime? ModifiedOn { get; protected set; }
    }
}
