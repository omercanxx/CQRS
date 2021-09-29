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

        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
