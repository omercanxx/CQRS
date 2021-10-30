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
    public class QueryUserRepository : QueryRepository<User>, IQueryUserRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }
        public QueryUserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<string>> GetEmailsByIds(List<Guid> ids)
        {
            var dbUsers = await _context.Users.Where(x => ids.Contains(x.Id)).ToListAsync();
            return dbUsers.Select(x => x.Email).ToList();
        }
    }
}