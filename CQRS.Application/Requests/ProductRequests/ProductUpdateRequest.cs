using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Requests.ProductRequests
{
    public class ProductUpdateRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public class ProductUpdateValidator : AbstractValidator<ProductUpdateRequest>
        {
            public ProductUpdateValidator()
            {
                RuleFor(c => c.Id).NotEmpty().WithMessage("Lütfen kurs id giriniz.");
                RuleFor(c => c.Title).NotEmpty().WithMessage("Lütfen isim giriniz.");
                RuleFor(c => c.Price).NotEmpty().NotNull().WithMessage("Lütfen fiyat giriniz.");
            }
        }
    }
}
