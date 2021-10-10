using CQRS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Dtos.CampaignDtos
{
    public class CampaignDto
    {
        public Guid Id { get; set; }
        public Guid Title { get; set; }
        public Guid Amount { get; set; }
        public DiscountTypes DiscountType { get; set; }
    }
}
