using CQRS.Core.Entities;
using CQRS.Core.Interfaces.CommandInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Interfaces.CommandInterfaces
{
    public interface ICommandCampaignRepository : ICommandRepository<Campaign>
    {
    }
}
