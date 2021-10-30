using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Requests.UserRequests
{
    public class UserProductItemInsertRequest
    {
        public Guid UserProductId { get; set; }
        public Guid ProductId { get; set; }
        public class UserProductItemInsertValidator : AbstractValidator<UserProductItemInsertRequest>
        {
            public UserProductItemInsertValidator()
            {
                RuleFor(c => c.UserProductId).NotEmpty().WithMessage("Lütfen kullanıcı liste id giriniz.");
                RuleFor(c => c.ProductId).NotEmpty().WithMessage("Lütfen ürün id giriniz.");
            }
        }
    }
}