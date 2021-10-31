using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Requests.UserRequests
{
    public class UserProductCreateRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public class UserProductCreateValidator : AbstractValidator<UserProductCreateRequest>
        {
            public UserProductCreateValidator()
            {
                RuleFor(c => c.Name).NotEmpty().WithMessage("Lütfen ad giriniz.");
                RuleFor(c => c.Description).NotEmpty().WithMessage("Lütfen tanım giriniz.");
            }
        }
    }
}