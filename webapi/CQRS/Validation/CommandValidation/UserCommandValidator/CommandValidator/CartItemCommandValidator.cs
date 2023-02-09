using FluentValidation;
using webapi.CQRS.Command.CommandCartItem;
using webapi.CQRS.Command.CommandUser;

namespace webapi.CQRS.Validation.CommandValidation.UserCommandValidator.CommandValidator
{
    public class CartItemCommandValidator
    {
        public class AddCartItemCommandValidator : AbstractValidator<AddCartItemCommand>
        {
            public AddCartItemCommandValidator()
            {
                RuleFor(x => x.CartItemName)
                    .NotEmpty().WithMessage("Cart Item is required.")
                    .Length(3, 15).WithMessage("UserName must be between 3 and 15 characters");

                RuleFor(x => x.CustomerId)
                    .NotEqual(Guid.Empty).WithMessage("Customer Id is required.");
            }
        }

        public class UpdateCartItemCommandValidator: AbstractValidator<UpdateCartItemCommand>
        {
            public UpdateCartItemCommandValidator()
            {
                RuleFor(x => x.CartItemName)
                    .NotEmpty().WithMessage("Cart Item is required.")
                    .Length(3, 15).WithMessage("UserName must be between 3 and 15 characters");

                RuleFor(x => x.CartItemId)
                    .NotEqual(Guid.Empty).WithMessage("Customer Id is required.");
            }

        }

        public class DeleteCartItemCommandValidator : AbstractValidator<DeleteCartItemCommand>
        {
            public DeleteCartItemCommandValidator()
            {
                
                RuleFor(x => x.CartItemId)
                    .NotEqual(Guid.Empty).WithMessage("Cart Item Id is required.");
            }

        }


    }
}
