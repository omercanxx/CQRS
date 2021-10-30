using CQRS.Core.Entities;
using CQRS.Core.Interfaces.CommandInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Repositories.CommandRepositories
{
    public class CommandUserProductItemRepository : CommandRepository<User_ProductItem>, ICommandUserProductItemRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }
        public CommandUserProductItemRepository(AppDbContext context) : base(context)
        {
        }
    }
}
