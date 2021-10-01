using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Interfaces
{
    //Kod tekrarını azaltmak amacıyla custom repository kullanılmıştır.
    public interface ICustomRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        void Deactivate(TEntity entity);
        void Activate(TEntity entity);
        void Update(TEntity entity);
        Task SaveChangesAsync();
    }
}
