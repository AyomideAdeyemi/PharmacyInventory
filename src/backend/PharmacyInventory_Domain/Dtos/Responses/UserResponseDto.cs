using PharmacyInventory_Domain.Enum;

namespace PharmacyInventory_Domain.Dtos.Responses
{
    public class UserResponseDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
       // public string Password { get; set; }
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public Position Position { get; set; }

    }
}
