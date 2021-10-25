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
    public class ProductCreateRequest
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public class ProductCreateValidator : AbstractValidator<ProductCreateRequest>
        {
            public ProductCreateValidator()
            {
                RuleFor(c => c.Title).NotEmpty().WithMessage("Lütfen isim giriniz.");
                RuleFor(c => c.Price).NotEmpty().WithMessage("Lütfen fiyat giriniz.");
            }
        }
    }
}
