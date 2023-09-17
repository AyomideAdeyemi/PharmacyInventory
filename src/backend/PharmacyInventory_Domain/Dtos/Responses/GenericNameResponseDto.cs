using PharmacyInventory_Domain.Dtos.Requests;

namespace PharmacyInventory_Domain.Dtos.Responses
{
    public class GenericNameResponseDto : AuditableBaseEntityDto
    {
        public string Id { get; set; }
    }
}
