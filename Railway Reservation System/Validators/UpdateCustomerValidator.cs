using FluentValidation;

namespace Railway_Reservation_System.Validators
{
    public class UpdateCustomerValidator:AbstractValidator<Models.Customer>
    {
        public UpdateCustomerValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Gender).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Age).GreaterThanOrEqualTo(16);
        }
    }
}
