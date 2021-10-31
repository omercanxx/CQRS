using CQRS.Core.Enums;
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
        //gRPC ve API'den gelen isteklerin ayrılması ihtiyacı doğmuştur. Çünkü aynı network altında olmadığı için gRPC Client tarafından istek atıldığında Mongoya bağlanılmamaktadır.
        Task<List<TEntity>> GetAll(RequestTypes requestType);
        IEnumerable<TEntity> FilterBy(Expression<Func<TEntity, bool>> filterExpression);
        Task<TEntity> FindByIdAsync(string id);

    }
}
