using CQRS.Domain.Dtos.OrderDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Requests.OrderRequests
{
    public class OrderCreateRequest
    {
        public Guid UserId { get; set; }
        public Guid? CampaignId { get; set; }
        public List<Order_ProductDto> Products { get; set; }

        public class OrderCreateValidator : AbstractValidator<OrderCreateRequest>
        {
            public OrderCreateValidator()
            {
                RuleFor(c => c.UserId).NotEmpty().WithMessage("Lütfen kullanıcı id giriniz.");
            }
        }
    }
}
