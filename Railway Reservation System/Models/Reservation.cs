using System.ComponentModel.DataAnnotations;

namespace Railway_Reservation_System.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(6)]
        public int trainno { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int NoOfPeople { get; set; }

        [Required]
        [RegularExpression("[aA-zZ]*", ErrorMessage = "Source Station name must be only alphabets")]
        public string SourceStation { get; set; } = null!;

        [Required]
        [RegularExpression("[aA-zZ]*", ErrorMessage = "Destination Station name must be only alphabets")]
        public string DestinationStation { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DatetimeOfCreation { get; set; }

        [Required]
        public bool Status { get; set; }
    }
}
