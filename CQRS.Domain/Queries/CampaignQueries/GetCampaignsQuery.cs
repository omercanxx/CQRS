using CQRS.Domain.Dtos.CampaignDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Queries.CampaignQueries
{
    public class GetCampaignsQuery : IRequest<List<CampaignDto>>
    {
    }
}
