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
        public Guid CourseId { get; set; }
        public decimal Price { get; set; }

        public class OrderCreateValidator : AbstractValidator<OrderCreateRequest>
        {
            public OrderCreateValidator()
            {
                RuleFor(c => c.UserId).NotEmpty().WithMessage("Lütfen kullanıcı id giriniz.");
                RuleFor(c => c.CourseId).NotEmpty().WithMessage("Lütfen kurs id giriniz.");
                RuleFor(c => c.Price).NotEmpty().NotNull().WithMessage("Lütfen fiyat giriniz.");
            }
        }
    }
}
