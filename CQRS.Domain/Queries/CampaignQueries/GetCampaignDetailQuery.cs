using CQRS.Domain.Dtos.CampaignDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Queries.CampaignQueries
{
    public class GetCampaignDetailQuery : IRequest<CampaignDto>
    {
        public GetCampaignDetailQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
