using AutoMapper;
using CQRS.Core.Interfaces;
using CQRS.Domain.Dtos.CampaignDtos;
using CQRS.Domain.Dtos.CourseDtos;
using CQRS.Domain.Dtos.OrderDtos;
using CQRS.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Domain.Queries.CampaignQueries
{
    public class CampaignQueryHandler : IRequestHandler<GetCampaignsQuery, List<CampaignDto>>,
                                      IRequestHandler<GetCampaignDetailQuery, CampaignDto>
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IMapper _mapper;
        public CampaignQueryHandler(ICampaignRepository campaignRepository, IMapper mapper)
        {
            _campaignRepository = campaignRepository;
            _mapper = mapper;
        }
        public async Task<CampaignDto> Handle(GetCampaignDetailQuery request, CancellationToken cancellationToken)
        {
            var dbCampaign = await _campaignRepository.GetByIdAsync(request.Id);
            return _mapper.Map<CampaignDto>(dbCampaign);
        }
        public async Task<List<CampaignDto>> Handle(GetCampaignsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<CampaignDto>>(await _campaignRepository.GetAllAsync());
        }
    }
}
