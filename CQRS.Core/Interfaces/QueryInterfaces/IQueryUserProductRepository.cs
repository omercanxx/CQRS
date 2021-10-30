using CQRS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Interfaces.QueryInterfaces
{
    public interface IQueryUserProductRepository : IQueryRepository<User_Product>
    {
        Task<User_Product> FindAsyncWithUserProductItems(Guid userProductId);
        Task<List<User_Product>> FindAsyncWithUserProductsWithItemsByUserId(Guid userId);
    }
}
