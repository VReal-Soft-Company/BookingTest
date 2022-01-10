using System.ComponentModel.DataAnnotations;

namespace BookingTest.DTO.User.Basic
{
    public abstract class BasicAuthDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
