using FluentValidation;
using MyBank.Domain.Entities;
using System;

namespace MyBank.Service.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(c => c)
                    .NotNull()
                    .OnAnyFailure(x =>
                    {
                        throw new ArgumentNullException("Can't found the object.");
                    });

            RuleFor(c => c.Agency)
                .NotEmpty().WithMessage("Is necessary to inform the Agency.")
                .NotNull().WithMessage("Is necessary to inform the Agency.");

            RuleFor(c => c.Account)
                .NotEmpty().WithMessage("Is necessary to inform the birth Account.")
                .NotNull().WithMessage("Is necessary to inform the birth Account.");

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Is necessary to inform the Password.")
                .NotNull().WithMessage("Is necessary to inform the birth Password.");

            RuleFor(c => c.IdData)
                .NotEmpty().WithMessage("Is necessary to inform the Data.")
                .NotNull().WithMessage("Is necessary to inform the birth Data.");
        }
    }
}
