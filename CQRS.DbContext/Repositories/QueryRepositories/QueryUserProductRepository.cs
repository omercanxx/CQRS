using CQRS.Core.Entities;
using CQRS.Core.Interfaces.QueryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Repositories.QueryRepositories
{
    public class QueryUserProductRepository : QueryRepository<User_Product>, IQueryUserProductRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }
        public QueryUserProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<User_Product> FindAsyncWithUserProductItems(Guid userProductId)
        {
            return await _context.User_Products
                .Include(x => x.User_ProductItems)
                .Where(x => x.Id == userProductId).SingleOrDefaultAsync();
        }

        public async Task<List<User_Product>> FindAsyncWithUserProductsWithItemsByUserId(Guid userId)
        {
            return await _context.User_Products
                .Include(x => x.User_ProductItems)
                .Where(x => x.UserId == userId).ToListAsync();
        }
    }
}