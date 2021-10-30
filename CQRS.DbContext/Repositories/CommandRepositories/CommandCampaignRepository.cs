using CQRS.Core.Entities;
using CQRS.Core.Interfaces.CommandInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Repositories.CommandRepositories
{
    public class CommandCampaignRepository : CommandRepository<Campaign>, ICommandCampaignRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }
        public CommandCampaignRepository(AppDbContext context) : base(context)
        {
        }
    }
}
