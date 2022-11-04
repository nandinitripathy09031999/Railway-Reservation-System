using System.ComponentModel.DataAnnotations;

namespace Railway_Reservation_System.Models
{
    public class Train
    {
        
        public int Id { get; set; }

        [Required]
        [RegularExpression("[aA-zZ]*", ErrorMessage = "Name name must be only alphabets")]
        public string Name { get; set; } = null!;


        [Required]
        [RegularExpression("[aA-zZ]*", ErrorMessage = "Source Station name must be only alphabets")]
        public string SourceStation { get; set; } = null!;

        [Required]
        [RegularExpression("[aA-zZ]*", ErrorMessage = "Destination Station name must be only alphabets")]

        public string DestinationStation { get; set; } = null!;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ArrivalDatetime { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DepartureDatetime { get; set; }

        [Required]
        public int TotalSeats { get; set; }

        [Required]
        public int AvailableSeats { get; set; }

        [Required]
        public string Class { get; set; } = null!;

        [Required]
        public int Fare { get; set; }
    }
}

