﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Requests.UserRequests
{
    public class UserCreateRequest
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public class UserCreateValidator : AbstractValidator<UserCreateRequest>
        {
            public UserCreateValidator()
            {
                RuleFor(c => c.Email).NotEmpty().WithMessage("Lütfen mail giriniz.");
                RuleFor(c => c.Name).NotEmpty().WithMessage("Lütfen ad giriniz.");
                RuleFor(c => c.Surname).NotEmpty().WithMessage("Lütfen soyad giriniz.");
                RuleFor(c => c.Password).NotEmpty().WithMessage("Lütfen şifre giriniz.");
            }
        }
    }
}
