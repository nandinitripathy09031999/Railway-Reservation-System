using FluentValidation;

namespace Railway_Reservation_System.Validators
{
    public class AddPaymentValidator:AbstractValidator<Models.Payment>
    {
        public AddPaymentValidator()
        {
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.CustomerId).GreaterThan(0);
            RuleFor(x => x.ReservationId).GreaterThan(0);
        }
    }
}
