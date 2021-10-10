using AutoMapper;
using CQRS.Application.Requests.CampaignRequests;
using CQRS.Domain.Commands.CampaignCommands;
using CQRS.Domain.Queries.CampaignQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CQRS.Application.Requests.CampaignRequests.CampaignCreateRequest;
using static CQRS.Application.Requests.CampaignRequests.CampaignUpdateRequest;

namespace CQRS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= (IMediator)HttpContext.RequestServices.GetService(typeof(IMediator));
        private readonly IMapper _mapper;
        public CampaignController(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCampaign([FromBody] CampaignCreateRequest request)
        {
            string errorMessage = null;
            var validator = new CampaignCreateValidator();
            var result = validator.Validate(request);
            if (result.IsValid)
            {
                var commandResult = await Mediator.Send(_mapper.Map<CampaignCreateCommand>(request));
                Log.Information($"{commandResult.Id} isimli kampanya eklenmiştir.");
                return Ok();
            }
            
            return BadRequest(errorMessage);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCampaign([FromBody] CampaignUpdateRequest request)
        {
            string errorMessage = null;
            var validator = new CampaignUpdateValidator();
            var result = validator.Validate(request);
            if (result.IsValid)
            {
                var commandResult = await Mediator.Send(_mapper.Map<CampaignUpdateCommand>(request));
                Log.Information($"{commandResult.Id} id'li kampanya güncellenmiştir.");
                return Ok();
            }

            return BadRequest(errorMessage);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCampaignDetail(Guid id)
        {
            Log.Information("Kampanya detay servisi çağrılmıştır.");
            return Ok(await Mediator.Send(new GetCampaignDetailQuery(id)));
        }

        [HttpGet]
        public async Task<IActionResult> GetCampaigns()
        {
            Log.Information("Kampanya liste servisi çağrılmıştır.");
            return Ok(await Mediator.Send(new GetCampaignsQuery()));

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCampaign(Guid id)
        {
            Log.Information("Kampanya silme servisi çağrılmıştır.");
            return Ok(await Mediator.Send(new CampaignDeleteCommand(id)));
        }
    }
}