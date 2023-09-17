using System.ComponentModel.DataAnnotations;

namespace PharmacyInventory_Domain.Dtos.Requests
{
    public class UserLoginDto
    {
        [EmailAddress(ErrorMessage = "Email is invalid ")]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
