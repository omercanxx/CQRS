using CQRS.Core.Entities;
using CQRS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Repositories
{
    public class CampaignRepository : CustomRepository<Campaign>, ICampaignRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }
        public CampaignRepository(AppDbContext context) : base(context)
        {
        }
    }
}