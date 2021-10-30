using CQRS.Core.Entities;
using CQRS.Core.Interfaces.CommandInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Repositories.CommandRepositories
{
    public class CommandUserProductRepository : CommandRepository<User_Product>, ICommandUserProductRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }
        public CommandUserProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
