using CQRS.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Commands.CourseCommands
{
    public class CourseUpdateCommand : IRequest<CommandResult>
    {
        public CourseUpdateCommand(Guid id, Guid? campaignId, string title, decimal price)
        {
            Id = id;
            CampaignId = campaignId;
            Title = title;
            Price = price;
        }

        public Guid Id { get; set; }
        public Guid? CampaignId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
    }
}
