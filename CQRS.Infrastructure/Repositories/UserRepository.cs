using CQRS.Core.Entities;
using CQRS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Repositories
{
    public class UserRepository : CustomRepository<User>, IUserRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}