using Microsoft.AspNetCore.Identity;
using PharmacyInventory_Domain.Enum;

namespace PharmacyInventory_Domain.Entities
{
    public class User : IdentityUser
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string SecoundName { get; set; }
        public Position Position { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Drug> Drugs { get; set; }

    }
}
