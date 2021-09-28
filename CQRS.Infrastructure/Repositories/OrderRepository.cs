using CQRS.Core.Entities;
using CQRS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Repositories
{
    public class OrderRepository : CustomRepository<Order>, IOrderRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }
        public OrderRepository(AppDbContext context) : base(context)
        {
        }
    }
}