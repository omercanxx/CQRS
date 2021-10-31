using CQRS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Interfaces.CommandInterfaces
{
    public interface ICommandRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetByIdAsync(Guid id);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        Task SaveChangesAsync();
    }
}
