using CQRS.Core.Entities;
using CQRS.Core.Interfaces.QueryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Repositories.QueryRepositories
{
    public class QueryProductRepository : QueryRepository<Product>, IQueryProductRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }
        public QueryProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
