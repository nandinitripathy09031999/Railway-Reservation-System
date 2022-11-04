using System.ComponentModel.DataAnnotations;

namespace Railway_Reservation_System.Models
{
    public class Admin
    {
        public int AdminId { get; set; }


        public string Name { get; set; } = null!;

        [Required]
        public int Age { get; set; }

        [Required]
        public string Gender { get; set; } = null!;


        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
    }
}
