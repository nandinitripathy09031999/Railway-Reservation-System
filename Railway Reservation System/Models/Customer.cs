using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Railway_Reservation_System.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        //[Required]
        //[RegularExpression("[aA-zZ]*", ErrorMessage = "Name name must be only alphabets")]
        public string Name { get; set; } = null!;

        [Required]
        public int Age { get; set; }
        [Required]
        public string Gender { get; set; } = null!;

        //[Required]
        //[DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;


    }
}
