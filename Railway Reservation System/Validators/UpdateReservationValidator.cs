using FluentValidation;

namespace Railway_Reservation_System.Validators
{
    public class UpdateReservationValidator:AbstractValidator<Models.Reservation>
    {
        public UpdateReservationValidator()
        {
            RuleFor(x => x.SourceStation).NotEmpty();
            RuleFor(x => x.DestinationStation).NotEmpty();
            RuleFor(x => x.trainno).GreaterThan(0);
            RuleFor(x => x.CustomerId).GreaterThan(0);
            RuleFor(x => x.NoOfPeople).ExclusiveBetween(1, 5);
        }
    }
}
