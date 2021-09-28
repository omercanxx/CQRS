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
    public class OrderUpdateRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public decimal Price { get; set; }

        public class OrderUpdateValidator : AbstractValidator<OrderUpdateRequest>
        {
            public OrderUpdateValidator()
            {
                RuleFor(c => c.Id).NotEmpty().WithMessage("Lütfen order id giriniz.");
                RuleFor(c => c.UserId).NotEmpty().WithMessage("Lütfen kullanıcı id giriniz.");
                RuleFor(c => c.CourseId).NotEmpty().WithMessage("Lütfen kurs id giriniz.");
                RuleFor(c => c.Price).NotEmpty().NotNull().WithMessage("Lütfen fiyat giriniz.");
            }
        }
    }
}
