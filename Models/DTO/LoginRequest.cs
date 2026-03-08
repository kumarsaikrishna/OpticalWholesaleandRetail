using System.ComponentModel.DataAnnotations;

namespace OpticalFibersRetailShop.Models.DTO
{
    public class LoginRequest
    {
        [Required]
        public string Uname { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[Display(Name = "Remember Me")]
        //public bool RememberMe { get; set; }
    }
}
