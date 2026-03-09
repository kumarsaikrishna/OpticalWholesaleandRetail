using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OpticalsWholesaleandRetail.Models.Entity
{
    public class Suppliers
    {
        [Key]
        public int SupplierId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [StringLength(150)]
        public string ContactPerson { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(100)]
        public string Country { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(300)]
        public string Address { get; set; }

        public bool IsActive { get; set; }

        public bool? IsDeleted { get; set; }


    }
}
