using System.ComponentModel.DataAnnotations;

namespace OpticalFibersRetailShop.Models.DTO
{
    public class UserTypeDTO
    {
        public int RoleId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? RoleName { get; set; }

    }
}
