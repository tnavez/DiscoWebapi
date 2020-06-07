using Disco.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disco.Validator
{
    public class MemberValidator : AbstractValidator<Member>
    {
        public MemberValidator()
        {
            RuleFor(x => x.Email).NotEmpty().When(m => string.IsNullOrEmpty(m.PhoneNumber)).WithMessage("*Either Email or Phone Number is required");
        }
    }
}
