using CQRS.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Commands.CampaignCommands
{
    public class CampaignDeleteCommand : IRequest<CommandResult>
    {
        public CampaignDeleteCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}

