using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PharmacyInventory_Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace PharmacyInventory_Domain.Dtos.Requests
{
    public class DrugRequestDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public double Quantity { get; set; }

        [Required(ErrorMessage = "Price per unit is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price per unit must be greater than 0")]
        public decimal PricePerUnit { get; set; }

        [Required(ErrorMessage = "Expiration date is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Expiration Date")]
        public DateTime ExpireDate { get; set; }
       
        public string UnitId { get; set; }
        
        public string GenericNameId { get; set; }
        public string BrandId { get; set; }
        public string SupplierId { get; set; }
        public string? ImageUrl { get; set; }


    }
}

