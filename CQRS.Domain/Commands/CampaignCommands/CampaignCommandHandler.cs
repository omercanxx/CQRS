using CQRS.Core;
using CQRS.Core.Entities;
using CQRS.Core.Interfaces;
using CQRS.Core.Interfaces.CommandInterfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Domain.Commands.CampaignCommands
{
    public class CampaignCommandHandler : IRequestHandler<CampaignCreateCommand, CommandResult>,
                                       IRequestHandler<CampaignUpdateCommand, CommandResult>,
                                       IRequestHandler<CampaignDeleteCommand, CommandResult>
    {
        private readonly ICommandCampaignRepository _campaignRepository;
        public CampaignCommandHandler(ICommandCampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }
        public async Task<CommandResult> Handle(CampaignCreateCommand command, CancellationToken cancellationToken)
        {
            Campaign campaign = new Campaign(command.Title, command.Amount, command.DiscountType);

            await _campaignRepository.AddAsync(campaign);
            await _campaignRepository.SaveChangesAsync();

            return new CommandResult(campaign.Id);
        }

        public async Task<CommandResult> Handle(CampaignUpdateCommand command, CancellationToken cancellationToken)
        {
            var dbCampaign = await _campaignRepository.GetByIdAsync(command.Id);

            dbCampaign.UpdateAmount(command.Amount);
            dbCampaign.UpdateDiscountType(command.DiscountType);
            dbCampaign.UpdateTitle(command.Title);


            _campaignRepository.Update(dbCampaign);
            await _campaignRepository.SaveChangesAsync();

            return new CommandResult(dbCampaign.Id);
        }

        public async Task<CommandResult> Handle(CampaignDeleteCommand command, CancellationToken cancellationToken)
        {
            var dbCampaign = await _campaignRepository.GetByIdAsync(command.Id);

            _campaignRepository.Remove(dbCampaign);
            await _campaignRepository.SaveChangesAsync();

            return new CommandResult(dbCampaign.Id);
        }
    }
}
