using CQRS.Core.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Requests.CampaignRequests
{
    public class CampaignCreateRequest
    {
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public DiscountTypes DiscountType { get; set; }

        public class CampaignCreateValidator : AbstractValidator<CampaignCreateRequest>
        {
            public CampaignCreateValidator()
            {
                RuleFor(c => c.Title).NotEmpty().WithMessage("Lütfen isim giriniz.");
                RuleFor(c => c.Amount).NotEmpty().NotNull().WithMessage("Lütfen tutar giriniz.");
                RuleFor(c => c.DiscountType).NotEmpty().NotNull().WithMessage("Lütfen indirim tipi giriniz.");
            }
        }
    }
}