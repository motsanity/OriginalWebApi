using FluentValidation;
using webapi.CQRS.Command.CommandUser;

namespace webapi.CQRS.Validation.CommandValidation.UserCommandValidator.CommandValidator
{
    public class UserCommandValidator
    {
        public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
        {
            public AddUserCommandValidator()
            {
                RuleFor(x => x.UserName)
                    .NotEmpty().WithMessage("UserName is required.")
                    .Length(5, 10).WithMessage("UserName must be between 5 and 10 characters");
            }
        }

        public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
        {
            public UpdateUserCommandValidator()
            {
                RuleFor(x => x.UserId)
                    .NotEqual(Guid.Empty).WithMessage("UserId is required.");

                RuleFor(x => x.UserName)
                    .NotEmpty().WithMessage("UserName is required.")
                    .Length(5, 10).WithMessage("UserName must be between 5-10");
            }
        }
    }
}