using Disco.Entities;
using Disco.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disco.Validator
{
    public class IdentityCardValidator : AbstractValidator<IdentityCard>
    {
        public IdentityCardValidator()
        {      
            RuleFor(x => x.NatNumber)
            .NotEmpty()
            .WithMessage("Niss is mandatory.")
            .Must((o, list, DiscoContext) =>
            {
                if (null != o.NatNumber)
                {
                    DiscoContext.MessageFormatter.AppendArgument("NISS", o.NatNumber);
                    return Tools.IsValidNN(o.NatNumber);
                }
                return true;
            })
        .WithMessage("National number ({NatNumber})) not valid");


            RuleFor(x => x.BirthDate)
            .NotEmpty()
            .WithMessage("BirthDate not empty.")
            .Must((o, list, DiscoContext) =>
            {
                if (null != o.BirthDate)
                {
                    DiscoContext.MessageFormatter.AppendArgument("Birthdate", o.BirthDate);
                    return Tools.CheckMinAge(o.BirthDate);
                }
                return true;
            })
        .WithMessage("Age cannot be lower than 18");
        }

    }
}
