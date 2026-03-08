using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace OpticalFibersRetailShop.Models.Entity
{
    public class UserEntity
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? FullName { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string? PasswordHash { get; set; }
        [Required]
        [MaxLength(10)]
        public string? Phone { get; set; }
        public int RoleId { get; set; }
 
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
