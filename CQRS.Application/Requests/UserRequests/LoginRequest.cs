using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Requests.UserRequests
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public class LoginValidator : AbstractValidator<LoginRequest>
        {
            public LoginValidator()
            {
                RuleFor(c => c.Email).NotEmpty().WithMessage("Lütfen mail giriniz.");
                RuleFor(c => c.Password).NotEmpty().WithMessage("Lütfen şifre giriniz.");
            }
        }
    }
}
