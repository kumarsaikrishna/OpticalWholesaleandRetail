using System.ComponentModel.DataAnnotations;

namespace OpticalFibersRetailShop.Models.Entity
{
    public class UserTypeEntites
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? RoleName { get; set; }

        public bool? IsDeleted { get; set; }

    }
}
