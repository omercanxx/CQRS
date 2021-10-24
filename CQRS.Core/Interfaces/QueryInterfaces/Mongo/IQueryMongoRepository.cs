using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Interfaces.QueryInterfaces.Mongo
{
    public interface IQueryMongoRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> AsQueryable();
        IEnumerable<TEntity> FilterBy(Expression<Func<TEntity, bool>> filterExpression);
        Task<TEntity> FindByIdAsync(string id);

    }
}
