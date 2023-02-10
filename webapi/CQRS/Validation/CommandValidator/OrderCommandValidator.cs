using FluentValidation;
using webapi.CQRS.Command.CommandOrder;

namespace webapi.CQRS.Validation.CommandValidator
{
    public class OrderCommandValidator
    {
        public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
        {
            public DeleteOrderCommandValidator()
            {
                
                RuleFor(x => x.OrderId)
                    .NotEqual(Guid.Empty).WithMessage("Order Id is required.");
            }
        }
    }
}
