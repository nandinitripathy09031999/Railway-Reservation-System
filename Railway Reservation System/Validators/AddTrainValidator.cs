using FluentValidation;

namespace Railway_Reservation_System.Validators
{
    public class AddTrainValidator:AbstractValidator<Models.Train>
    {
        public AddTrainValidator()
        {
            RuleFor(x => x.SourceStation).NotEmpty();
            RuleFor(x => x.DestinationStation).NotEmpty();
            RuleFor(x => x.TotalSeats).GreaterThan(0);
            RuleFor(x => x.AvailableSeats).GreaterThan(0);
            RuleFor(x => x.AvailableSeats).LessThan(x => x.TotalSeats);
        }
    }
}
