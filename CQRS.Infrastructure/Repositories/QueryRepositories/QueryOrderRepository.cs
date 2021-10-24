using CQRS.Core.Entities;
using CQRS.Core.Interfaces.QueryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Repositories.QueryRepositories
{
    public class QueryOrderRepository : QueryRepository<Order>, IQueryOrderRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }
        public QueryOrderRepository(AppDbContext context) : base(context)
        {
        }
    }
}