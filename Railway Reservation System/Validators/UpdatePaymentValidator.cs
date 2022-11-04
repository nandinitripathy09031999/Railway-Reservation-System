using FluentValidation;

namespace Railway_Reservation_System.Validators
{
    public class UpdatePaymentValidator:AbstractValidator<Models.Payment>
    {
        public UpdatePaymentValidator()
        {
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.CustomerId).GreaterThan(0);
            RuleFor(x => x.ReservationId).GreaterThan(0);
        }
    }
}
