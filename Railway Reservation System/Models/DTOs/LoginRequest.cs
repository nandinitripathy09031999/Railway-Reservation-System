using System.ComponentModel.DataAnnotations;

namespace Railway_Reservation_System.Models.DTOs
{
    public class LoginRequest
    {
        [Required]
        //[RegularExpression("[aA-zZ]*", ErrorMessage = "Name name must be only alphabets")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }    

    }
}
