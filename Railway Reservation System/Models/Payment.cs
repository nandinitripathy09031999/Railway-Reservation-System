using System.ComponentModel.DataAnnotations;

namespace Railway_Reservation_System.Models
{
    public class Payment
    {
        public int Id { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int ReservationId { get; set; }

    }
}
