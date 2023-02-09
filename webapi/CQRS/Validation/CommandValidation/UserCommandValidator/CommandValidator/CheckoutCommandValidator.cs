using FluentValidation;
using webapi.CQRS.Command.CommandCartItem;
using webapi.CQRS.Command.CommandCheckout;

namespace webapi.CQRS.Validation.CommandValidation.UserCommandValidator.CommandValidator
{
    public class CheckoutCommandValidator
    {
        public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
        {
            public CheckoutOrderCommandValidator()
            {
                RuleFor(x => x.Status)
                    .NotEqual((short)0).WithMessage("Status is required 0 = pending; 1 = processed; 2 = Cancelled");

                RuleFor(x => x.CustomerId)
                    .NotEqual(Guid.Empty).WithMessage("Customer Id is required.");
            }
        }
    }
}
